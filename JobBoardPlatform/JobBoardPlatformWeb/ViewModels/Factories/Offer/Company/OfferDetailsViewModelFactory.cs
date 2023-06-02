﻿using JobBoardPlatform.DAL.Models.Company;
using JobBoardPlatform.PL.ViewModels.Models.Offer.Company;
using JobBoardPlatform.PL.ViewModels.Utilities.Contracts;

namespace JobBoardPlatform.PL.ViewModels.Factories.Offer.Company
{
    public class OfferDetailsViewModelFactory : IFactory<OfferDetailsViewModel>
    {
        public async Task<OfferDetailsViewModel> Create()
        {
            JobOffer offer = new JobOffer();

            int offerId = 0;
            var viewModel = new OfferDetailsViewModel()
            {
                OfferId = offerId,
                JobTitle = offer.JobTitle,
                City = offer.City,
                Country = offer.Country,
                Address = offer.Address,
                JobDescription = offer.Description,
                ContactAddress = offer.ContactDetails.ContactAddress,
                ContactType = offer.ContactDetails.Id,
                MainTechnologyType = offer.MainTechnologyTypeId,
                WorkLocationType = offer.WorkLocationId,
                SalaryFromRange = offer.JobOfferEmploymentDetails.Select(x => x.SalaryRange.From).ToArray(),
                SalaryToRange = offer.JobOfferEmploymentDetails.Select(x => x.SalaryRange.To).ToArray(),
                SalaryCurrencyType = offer.JobOfferEmploymentDetails.Select(x => x.SalaryRange.SalaryCurrencyId).ToArray(),
                EmploymentTypes = offer.JobOfferEmploymentDetails.Select(x => x.EmploymentTypeId).ToArray(),
                TechKeywords = offer.TechKeywords.Select(x => x.Name).ToArray(),
            };

            return viewModel;
        }
    }
}
