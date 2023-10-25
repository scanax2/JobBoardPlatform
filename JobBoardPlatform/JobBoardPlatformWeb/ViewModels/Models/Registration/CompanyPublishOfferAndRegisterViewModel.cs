﻿using JobBoardPlatform.BLL.Boundaries;
using JobBoardPlatform.PL.ViewModels.Models.Offer.Company;
using JobBoardPlatform.PL.ViewModels.Models.Profile.Company;

namespace JobBoardPlatform.PL.ViewModels.Models.Registration
{
    public class CompanyPublishOfferAndRegisterViewModel : ICompanyProfileAndNewOfferData
    {
        public CompanyProfileData CompanyProfileData { get; set; } = new CompanyProfileViewModel();
        public IOfferData OfferData { get => EditOffer.OfferDetails; set => EditOffer.OfferDetails = (OfferDataViewModel)value; }
        public EditOfferViewModel EditOffer { get; set; } = new EditOfferViewModel();
    }
}
