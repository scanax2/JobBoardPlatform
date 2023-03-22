﻿using JobBoardPlatform.DAL.Models.Contracts;
using JobBoardPlatform.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using JobBoardPlatform.BLL.Services.Authorization;
using JobBoardPlatform.PL.ViewModels.Authentification;
using NuGet.Protocol.Core.Types;
using JobBoardPlatform.DAL.Models;

namespace JobBoardPlatform.PL.Controllers.Login
{
    /*
    ClaimsPrincipal claimUser = httpContext.User;

    if (claimUser.Identity.IsAuthenticated)
    {
        return AuthorizationResult.Success;
    }
    */
    public abstract class BaseLoginController<T, V> : Controller 
        where T: class, ICredentialEntity
        where V: class, IDisplayDataEntity
    {
        protected IRepository<T> credentialsRepository;
        protected IRepository<V> profileRepository;


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginViewModel userLogin)
        {
            if (ModelState.IsValid)
            {
                var credentials = GetCredentials(userLogin);
                string role = GetRole();

                return await TryLogin(userLogin, credentials, role);
            }
            return View(userLogin);
        }

        protected abstract string GetRole();

        protected abstract T GetCredentials(UserLoginViewModel userLogin);

        private async Task<IActionResult> TryLogin(UserLoginViewModel userLogin, T credentials, string role)
        {
            var session = new SessionService<T, V>(HttpContext, credentialsRepository, profileRepository, role);

            var autorization = await session.TryLoginAsync(credentials);
            if (autorization.IsError)
            {
                ModelState.AddModelError("Autorization error", autorization.Error);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View(userLogin);
        }
    }
}
