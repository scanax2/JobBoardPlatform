﻿using JobBoardPlatform.PL.ViewModels.Models.Offer.Company;

namespace JobBoardPlatform.PL.ViewModels.Models.Email
{
    public class ApplicationEmailViewModel
    {
        public string CompanyName { get; set; } = string.Empty;
        public string JobTitle { get;set; } = string.Empty;
        public ApplicationCardViewModel Application { get; set; } = new ApplicationCardViewModel();
    }
}
