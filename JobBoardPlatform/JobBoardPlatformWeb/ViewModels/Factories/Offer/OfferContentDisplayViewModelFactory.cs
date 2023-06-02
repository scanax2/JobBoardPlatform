﻿using JobBoardPlatform.DAL.Data.Loaders;
using JobBoardPlatform.DAL.Models.Company;
using JobBoardPlatform.DAL.Repositories.Models;
using JobBoardPlatform.PL.ViewModels.Models.Offer.Users;
using JobBoardPlatform.PL.ViewModels.Utilities.Contracts;

namespace JobBoardPlatform.PL.ViewModels.Factories.Offer
{
    public class OfferContentDisplayViewModelFactory : IFactory<OfferContentDisplayViewModel>
    {
        public async Task<OfferContentDisplayViewModel> Create()
        {
            throw new Exception("Need to get somehow offer");
            JobOffer offer;

            var viewModel = new OfferContentDisplayViewModel();

            Map(offer, viewModel);

            return viewModel;
        }

        public void Map(JobOffer from, OfferContentDisplayViewModel to)
        {
            var techKeyWords = from.TechKeywords.Select(x => x.Name).ToArray();

            to.TechKeywords = techKeyWords;
            to.JobTitle = from.JobTitle;
            to.JobDescription = from.Description;
            to.CompanyName = from.CompanyProfile.CompanyName;
            to.CompanyImageUrl = from.CompanyProfile.ProfileImageUrl;
            to.MainTechnologyType = from.MainTechnologyType.Type;
            to.WorkLocationType = from.WorkLocation.Type;
            to.ContactForm = from.ContactDetails.ContactType.Type;

            MapSalaryDetails(from, to);
            MapFullAddress(from, to);
        }

        private void MapSalaryDetails(JobOffer from, OfferContentDisplayViewModel to)
        {
            var salaryDisplayText = new List<string>(from.JobOfferEmploymentDetails.Count);
            var employmentTypeDisplayText = new List<string>(from.JobOfferEmploymentDetails.Count);

            foreach (var employmentDetails in from.JobOfferEmploymentDetails)
            {
                string singleSalaryText = "Undisclosed Salary";

                var salaryRange = employmentDetails.SalaryRange;

                if (salaryRange != null)
                {
                    singleSalaryText = $"{salaryRange.From} - {salaryRange.To} {salaryRange.SalaryCurrency.Type}";
                }

                string employmentType = employmentDetails.EmploymentType.Type;

                salaryDisplayText.Add(singleSalaryText);
                employmentTypeDisplayText.Add(employmentType);
            }

            to.SalaryDetails = salaryDisplayText.ToArray();
            to.EmploymentDetails = employmentTypeDisplayText.ToArray();
        }

        private void MapFullAddress(JobOffer from, OfferContentDisplayViewModel to)
        {
            string fullAddress = $"{from.City}, {from.Country}";
            if (!string.IsNullOrEmpty(from.Address))
            {
                fullAddress = $"{from.Address}, {fullAddress}";
            }

            to.FullAddress = fullAddress;
        }
    }
}
