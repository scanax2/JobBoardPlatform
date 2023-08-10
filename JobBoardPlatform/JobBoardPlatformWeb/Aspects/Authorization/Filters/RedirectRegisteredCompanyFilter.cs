﻿using JobBoardPlatform.BLL.Services.Authentification.Authorization;
using JobBoardPlatform.PL.Controllers.Offer;
using JobBoardPlatform.PL.Controllers.Register;
using JobBoardPlatform.PL.Controllers.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JobBoardPlatform.PL.Filters
{
    public class RedirectRegisteredCompanyFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (UserSessionUtils.IsLoggedIn(context.HttpContext.User) && 
                UserRolesUtils.IsUserCompany(context.HttpContext.User))
            {
                RedirectCompany(context);
            }
        }

        private void RedirectCompany(AuthorizationFilterContext context)
        {
            string controller = ControllerUtils.GetControllerName(typeof(CompanyOfferEditorController));
            context.Result = new RedirectToActionResult("Editor", controller, null);
        }
    }
}
