﻿namespace JobBoardPlatform.BLL.Services.Authentification.Contracts
{
    internal interface IPasswordHasher
    {
        string HashPassword(string password);
        AuthentificationResult VerifyHashedPassword(string providedPassword, string hashedPassword);
    }
}
