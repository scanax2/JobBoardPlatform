﻿using JobBoardPlatform.BLL.Services.Authorization.Utilities;
using JobBoardPlatform.BLL.Services.Common.ProfileAdapter.Contracts;
using JobBoardPlatform.DAL.Models.Employee;

namespace JobBoardPlatform.BLL.Services.Common.ProfileAdapter
{
    internal class EmployeeProfileAdapter : IUserProfileAdapter
    {
        private readonly EmployeeProfile profile;


        public string DisplayName => profile.Name;

        public string DisplayProfileImageUrl
        {
            get
            {
                if (profile.ProfileImageUrl == null)
                {
                    return string.Empty;
                }
                else
                {
                    return profile.ProfileImageUrl;
                }
            }
        }

        public string UserRole => UserRoles.Employee;


        public EmployeeProfileAdapter(EmployeeProfile profile)
        {
            this.profile = profile;
        }
    }
}