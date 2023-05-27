﻿using JobBoardPlatform.DAL.Models.Company;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace JobBoardPlatform.DAL.Repositories.Cache
{
    public class MainPageOffersCountCacheRepository : CacheRepositoryCore<int>
    {
        private const string CountKey = "MainPageOffersCount";
        private const int CacheExpirationTimeInMinutes = 5;


        public MainPageOffersCountCacheRepository(IDistributedCache cache) : base(cache)
        {

        }

        protected override string GetEntryKey()
        {
            return CountKey;
        }

        protected override DistributedCacheEntryOptions GetOptions()
        {
            return new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(CacheExpirationTimeInMinutes));
        }
    }
}
