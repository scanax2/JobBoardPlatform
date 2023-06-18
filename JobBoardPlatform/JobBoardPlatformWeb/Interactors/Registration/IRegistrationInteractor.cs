﻿using JobBoardPlatform.BLL.Models.Contracts;

namespace JobBoardPlatform.PL.Interactors.Registration
{
    public interface IRegistrationInteractor<T> where T : class, IUserLoginData
    {
        Task<RedirectData> ProcessRegistrationAndRedirect(T model);
        Task FinishRegistration(string tokenId, HttpContext httpContext);
    }
}
