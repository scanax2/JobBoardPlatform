﻿
namespace JobBoardPlatform.DAL.Repositories.Cache.Tokens
{
    public class RestorePasswordToken : IToken
    {
        public string Id { get; set; }
        public string RelatedLogin { get; set; }
    }
}
