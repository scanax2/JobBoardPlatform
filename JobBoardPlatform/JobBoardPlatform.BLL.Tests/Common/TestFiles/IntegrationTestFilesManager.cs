﻿using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;

namespace JobBoardPlatform.IntegrationTests.Common.TestFiles
{
    public static class IntegrationTestFilesManager
    {
        public static IFormFile GetExampleResumeFile()
        {
            string directoryPath = Directory.GetCurrentDirectory();
            return GetFileAsFormFile(Path.Combine(directoryPath, "../../../Common/TestFiles/Resumes/resume0.pdf"), "application/pdf");
        }

        public static IFormFile GetEmployeeProfileImageFile()
        {
            string directoryPath = Directory.GetCurrentDirectory();
            return GetFileAsFormFile(Path.Combine(directoryPath, "../../../Common/TestFiles/Images/userProfileImage0.png"), "image/bitmap");
        }

        public static IFormFile GetCompanyProfileImageFile()
        {
            string directoryPath = Directory.GetCurrentDirectory();
            return GetFileAsFormFile(Path.Combine(directoryPath, "../../../Common/TestFiles/Images/companyLogo0.jpg"), "image/bitmap");
        }

        private static IFormFile GetFileAsFormFile(string filePath, string contentType)
        {
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                memoryStream.Position = 0;

                var formFile = new FormFile(memoryStream, 0, memoryStream.Length, "file", "file.pdf");
                return formFile;
            }
        }
    }
}
