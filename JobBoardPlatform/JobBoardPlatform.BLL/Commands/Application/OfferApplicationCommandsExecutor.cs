﻿using JobBoardPlatform.BLL.Services.Actions.Offers.Factory;
using JobBoardPlatform.DAL.Models.Company;
using JobBoardPlatform.DAL.Repositories.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using JobBoardPlatform.DAL.Repositories.Blob.AttachedResume;
using JobBoardPlatform.BLL.Services.Authentification.Authorization;
using static System.Net.Mime.MediaTypeNames;
using JobBoardPlatform.BLL.Boundaries;

namespace JobBoardPlatform.BLL.Commands.Application
{
    public class OfferApplicationCommandsExecutor
    {
        private readonly IRepository<JobOfferApplication> applicationsRepository;
        private readonly IRepository<JobOffer> offersRepository;
        private readonly IProfileResumeBlobStorage profileResumeStorage;
        private readonly IApplicationsResumeBlobStorage resumeStorage;
        private readonly IOfferActionHandlerFactory actionHandlerFactory;


        public OfferApplicationCommandsExecutor(
            IRepository<JobOfferApplication> applicationsRepository,
            IRepository<JobOffer> offersRepository,
            IProfileResumeBlobStorage profileResumeStorage,
            IApplicationsResumeBlobStorage resumeStorage,
            IOfferActionHandlerFactory actionHandlerFactory)
        {
            this.applicationsRepository = applicationsRepository;
            this.offersRepository = offersRepository;
            this.profileResumeStorage = profileResumeStorage;
            this.resumeStorage = resumeStorage;
            this.actionHandlerFactory = actionHandlerFactory;
        }

        public async Task TryPostApplicationFormAsync(
            int offerId, int? userProfileId, HttpRequest request, HttpResponse response, IApplicationForm form)
        {
            var actionsHandler = actionHandlerFactory.GetApplyActionHandler(offerId);
            if (!actionsHandler.IsActionDoneRecently(request))
            {
                var postFormCommand = new PostApplicationFormCommand(
                    applicationsRepository, 
                    offersRepository, 
                    profileResumeStorage, 
                    resumeStorage, 
                    form, 
                    offerId, 
                    userProfileId);
                await postFormCommand.Execute();

                actionsHandler.RegisterAction(request, response);
            }
        }

        /// <returns>result priority value</returns>
        public async Task<int> UpdateApplicationPriorityCommandAsync(int applicationId, int newPriorityIndex)
        {
            var updateApplicationPriorityCommand = new UpdateApplicationPriorityCommand(
                applicationsRepository, applicationId, newPriorityIndex);
            await updateApplicationPriorityCommand.Execute();

            return updateApplicationPriorityCommand.ResultPriorityIndex;
        }
    }
}