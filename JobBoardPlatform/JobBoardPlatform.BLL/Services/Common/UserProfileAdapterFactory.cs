﻿using JobBoardPlatform.BLL.Services.Common.Contracts;
using JobBoardPlatform.DAL.Models;
using JobBoardPlatform.DAL.Models.Contracts;

namespace JobBoardPlatform.BLL.Services.Common
{
    internal class UserProfileAdapterFactory
    {
        public static IUserProfileAdapter CreateProfileAdapter(IUserProfileEntity profile)
        {
            if (profile is EmployeeProfile)
            {
                return new EmployeeProfileAdapter(profile as EmployeeProfile);
            }
            else if (profile is CompanyProfile)
            {
                return new CompanyProfileAdapter(profile as CompanyProfile);
            }
            else
            {
                throw new ArgumentException("Unsupported profile type");
            }
        }
    }
}