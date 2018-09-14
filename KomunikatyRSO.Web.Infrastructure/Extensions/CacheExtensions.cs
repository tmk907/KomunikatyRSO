﻿using KomunikatyRSO.Web.Infrastructure.DTO;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace KomunikatyRSO.Web.Infrastructure.Extensions
{
    public static class CacheExtensions
    {
        public static void SetJwt(this IMemoryCache cache, Guid tokenId, JwtDto jwt)
           => cache.Set(GetJwtKey(tokenId), jwt, TimeSpan.FromSeconds(5));

        public static JwtDto GetJwt(this IMemoryCache cache, Guid tokenId)
            => cache.Get<JwtDto>(GetJwtKey(tokenId));

        private static string GetJwtKey(Guid tokenId)
            => $"{tokenId}-jwt";
    }
}
