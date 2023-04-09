﻿using JobBoardPlatform.DAL.Models;
using JobBoardPlatform.DAL.Repositories.Models;
using JobBoardPlatform.PL.ViewModels.Authentification;
using JobBoardPlatform.PL.ViewModels.JobOffer;
using Microsoft.AspNetCore.Mvc;

namespace JobBoardPlatform.PL.Controllers.Register
{
    public class EmployeeRegisterController : BaseRegisterController<EmployeeIdentity, EmployeeProfile, UserRegisterViewModel>
    {
        public EmployeeRegisterController(IRepository<EmployeeIdentity> credentialsRepository,
            IRepository<EmployeeProfile> profileRepository)
        {
            this.credentialsRepository = credentialsRepository;
            this.profileRepository = profileRepository;
        }

        protected override EmployeeIdentity GetIdentity(UserRegisterViewModel userRegister)
        {
            var credentials = new EmployeeIdentity()
            {
                Email = userRegister.Email,
                HashPassword = userRegister.Password,
                Profile = new EmployeeProfile()
            };

            return credentials;
        }
    }
}
