﻿using JobBoardPlatform.DAL.Models;
using JobBoardPlatform.PL.ViewModels.Profile.Employee;
using JobBoardPlatform.PL.ViewModels.Utilities.Contracts;

namespace JobBoardPlatform.PL.ViewModels.Utilities
{
    internal class EmployeeViewModelToProfileMapper : IMapper<EmployeeProfileViewModel, EmployeeProfile>
    {
        public void Map(EmployeeProfileViewModel from, EmployeeProfile to)
        {
            var fromUpdate = from.Update;

            if (!string.IsNullOrEmpty(fromUpdate.Name))
            {
                to.Name = fromUpdate.Name;
            }

            to.Surname = fromUpdate.Surname;
            to.City = fromUpdate.City;
            to.Country = fromUpdate.Country;
            to.Description = fromUpdate.Description;

            if (!string.IsNullOrEmpty(fromUpdate.AttachedResumeUrl))
            {
                to.ResumeUrl = fromUpdate.AttachedResumeUrl;
            }
            if (!string.IsNullOrEmpty(fromUpdate.ProfileImageUrl))
            {
                to.ProfileImageUrl = fromUpdate.ProfileImageUrl;
            }
        }
    }
}
