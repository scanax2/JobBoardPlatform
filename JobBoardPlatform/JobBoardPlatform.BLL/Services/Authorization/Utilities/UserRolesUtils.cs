﻿using JobBoardPlatform.DAL.Models.Company;
using System.Security.Claims;

namespace JobBoardPlatform.BLL.Services.Authorization.Utilities
{
    public static class UserRolesUtils
    {
        public static bool IsUserOwner(ClaimsPrincipal user, JobOffer offer)
        {
            bool isUserLoggedIn = user.Identity.IsAuthenticated;
            if (!isUserLoggedIn)
            {
                return false;
            }

            string userRole = UserSessionUtils.GetRole(user);
            if (userRole != UserRoles.Company)
            {
                return false;
            }

            int userId = UserSessionUtils.GetProfileId(user);
            bool isOwner = (offer.CompanyProfileId == userId);

            return isOwner;
        }
    }
}