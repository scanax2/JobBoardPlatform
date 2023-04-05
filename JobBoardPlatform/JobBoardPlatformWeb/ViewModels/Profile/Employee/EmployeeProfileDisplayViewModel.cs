﻿namespace JobBoardPlatform.PL.ViewModels.Profile.Employee
{
    public class EmployeeProfileDisplayViewModel
    {
        public string? Name { get; set; } = string.Empty;
        public string? Surname { get; set; } = string.Empty;
        public string? Country { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? ProfileImageUrl { get; set; } = string.Empty;
        public string? AttachedResumeUrl { get; set; } = string.Empty;
        public string? AttachedResumeFileSize { get; set; } = string.Empty;
        public string? AttachedResumeFileName { get; set; } = string.Empty;
    }
}
