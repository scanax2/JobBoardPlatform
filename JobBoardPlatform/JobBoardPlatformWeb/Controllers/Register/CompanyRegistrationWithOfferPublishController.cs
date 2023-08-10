﻿using FluentValidation;
using JobBoardPlatform.BLL.Boundaries;
using JobBoardPlatform.BLL.Services.AccountManagement.Registration.Tokens;
using JobBoardPlatform.BLL.Services.Authentification.Authorization;
using JobBoardPlatform.DAL.Models.Company;
using JobBoardPlatform.PL.Aspects.DataValidators;
using JobBoardPlatform.PL.Filters;
using JobBoardPlatform.PL.Interactors.Notifications;
using JobBoardPlatform.PL.Interactors.Registration;
using JobBoardPlatform.PL.ViewModels.Contracts;
using JobBoardPlatform.PL.ViewModels.Factories.Offer;
using JobBoardPlatform.PL.ViewModels.Factories.Offer.Payment;
using JobBoardPlatform.PL.ViewModels.Models.Authentification;
using JobBoardPlatform.PL.ViewModels.Models.Offer.Payment;
using JobBoardPlatform.PL.ViewModels.Models.Registration;
using Microsoft.AspNetCore.Mvc;

namespace JobBoardPlatform.PL.Controllers.Register
{
    [Route("register-employer-offer")]
    public class CompanyRegistrationWithOfferPublishController : Controller
    {
        public const string AdsPricingAction = "RegisterPromotion";
        public const string StartPostOfferAndRegisterAction = "StartPostOfferAndRegister";

        private readonly IRegistrationInteractor<CompanyRegisterViewModel> registrationInteractor;
        private readonly EmailCompanyPublishOfferAndRegistrationInteractor registrationWithPublishOfferInteractor;
        private readonly IValidator<CompanyPublishOfferAndRegisterViewModel> validator;
        private readonly IValidator<CompanyRegisterViewModel> companyRegisterValidator;


        public CompanyRegistrationWithOfferPublishController(
            IRegistrationInteractor<CompanyRegisterViewModel> registrationInteractor,
            EmailCompanyPublishOfferAndRegistrationInteractor registrationWithPublishOfferInteractor,
            IValidator<CompanyPublishOfferAndRegisterViewModel> validator,
            IValidator<CompanyRegisterViewModel> companyRegisterValidator)
        {
            this.registrationInteractor = registrationInteractor;
            this.registrationWithPublishOfferInteractor = registrationWithPublishOfferInteractor;
            this.validator = validator;
            this.companyRegisterValidator = companyRegisterValidator;
        }

        [Route("pricing")]
        public async Task<IActionResult> RegisterPromotion()
        {
            var factory = new OfferPricingTableViewModelFactory();
            var viewModel = await factory.CreateAsync();
            return View(viewModel);
        }

        [Route("post-ad")]
        [TypeFilter(typeof(RedirectRegisteredCompanyFilter))]
        public IActionResult StartPostOfferAndRegister()
        {
            var viewModel = new CompanyPublishOfferAndRegisterViewModel();
            return View(viewModel);
        }

        [Route("post-ad/{formDataTokenId}")]
        public async Task<IActionResult> StartPostOfferAndRegister(string formDataTokenId)
        {
            var viewModel = await registrationWithPublishOfferInteractor.GetPostFormViewModelAsync(formDataTokenId);
            return View(viewModel);
        }

        [Route("post-ad/{formDataTokenId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StartPostOfferAndRegister(
            CompanyPublishOfferAndRegisterViewModel registerData, string formDataTokenId)
        {
            await registrationWithPublishOfferInteractor.DeletePreviousSavedDataAsync(registerData, formDataTokenId);
            return await StartPostOfferAndRegister(registerData);
        }

        [Route("post-ad")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StartPostOfferAndRegister(CompanyPublishOfferAndRegisterViewModel registerData)
        {
            var result = await validator.ValidateAsync(registerData);
            if (result.IsValid)
            {
                string tokenId = await registrationWithPublishOfferInteractor.SavePostFormViewModelAsync(registerData);
                return RedirectToAction("VerifyRegistration", new { formDataTokenId = tokenId });
            }
            else
            {
                // TODO: remove after tests
                string tokenId = await registrationWithPublishOfferInteractor.SavePostFormViewModelAsync(registerData);
                return RedirectToAction("VerifyRegistration", new { formDataTokenId = tokenId });
                //
                result.AddToModelState(this.ModelState);
            }

            return View(registerData);
        }

        [Route("verify/confirm/{confirmationTokenId}")]
        public async Task<IActionResult> TryConfirmRegistrationWithOfferPublish(string confirmationTokenId)
        {
            string formDataTokenId = await registrationWithPublishOfferInteractor.FinishRegistrationAsync(
                confirmationTokenId, HttpContext);
            return RedirectToAction("VerifyRegistration", new { formDataTokenId = formDataTokenId });
        }

        [Route("verify/{formDataTokenId}")]
        public async Task<IActionResult> VerifyRegistration(string formDataTokenId)
        {
            try
            {
                var viewModel = await CreateCompanyVerifyViewModel(formDataTokenId);
                return View(viewModel);
            }
            catch (TokenValidationException e)
            {
                return RedirectToAction("StartPostOfferAndRegister");
            }
        }

        [Route("verify/{formDataTokenId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyRegistration(CompanyVerifyPublishOfferAndRegistrationViewModel viewModel, string formDataTokenId)
        {
            var userRegister = viewModel.UserRegister;

            var formData = await registrationWithPublishOfferInteractor.GetPostFormViewModelAsync(formDataTokenId);
            var companyData = formData.CompanyProfileData;
            userRegister.CompanyName = companyData.CompanyName!;

            var result = await companyRegisterValidator.ValidateAsync(userRegister);
            if (result.IsValid)
            {
                var redirect = await registrationWithPublishOfferInteractor.ProcessRegistrationAndRedirectAsync(
                    userRegister, formDataTokenId);
                NotificationsManager.Instance.SetActionDoneNotification(
                    NotificationsManager.RegisterSection,
                    $"Check your email inbox {userRegister.Email} for a confirmation link to complete your registration.",
                    TempData);
                return RedirectToAction(redirect.ActionName, redirect.Data);
            }
            else
            {
                result.AddToModelState(this.ModelState, nameof(viewModel.UserRegister));
            }

            if (ModelState.ErrorCount == 0)
            {
                viewModel.UserRegister = new CompanyRegisterViewModel();
            }
            return View(viewModel);
        }

        [Route("post-ad/confirm/{tokenId}")]
        public async Task<IActionResult> TryConfirmOfferPaymentAndRegister(string tokenId)
        {
            await registrationInteractor.FinishRegistration(tokenId, HttpContext);
            return View();
        }

        private async Task<CompanyVerifyPublishOfferAndRegistrationViewModel> CreateCompanyVerifyViewModel(string formDataTokenId)
        {
            var viewModel = new CompanyVerifyPublishOfferAndRegistrationViewModel();
            viewModel.FormDataTokenId = formDataTokenId;

            if (UserSessionUtils.IsLoggedIn(User))
            {
                int profileId = UserSessionUtils.GetProfileId(User);
                var offer = await registrationWithPublishOfferInteractor.GetAddedOfferAsync(profileId);
                viewModel.PaymentForm = await GetPaymentFormViewModel(offer);
                viewModel.IsTokenConfirmed = true;
            }

            return viewModel;
        }

        private Task<OfferPaymentFormViewModel> GetPaymentFormViewModel(JobOffer offer)
        {
            var factory = new OfferPaymentFormViewModelFactory(offer);
            return factory.CreateAsync();
        }
    }
}
