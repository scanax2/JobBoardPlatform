﻿using JobBoardPlatform.BLL.Boundaries;
using Microsoft.AspNetCore.Http;

namespace JobBoardPlatform.IntegrationTests.Common.Mocks.DataStructures
{
    public class ProfileImageMock : IProfileImage
    {
        public IFormFile? File { get; set; }
        public string? ImageUrl { get; set; }
    }
}