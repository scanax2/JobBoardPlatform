﻿
namespace JobBoardPlatform.DAL.Repositories.Cache.Tokens
{
    public class DataToken<T>
    {
        public string Id { get; set; }
        public T Value { get; set; }
    }
}
