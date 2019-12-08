using BasicEmailQueueManager.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace BasicEmailQueueManager.SQLImplementation
{
    public class CachedSQLConfiguration : IConfiguration
    {
        private readonly SQLConfiguration _sqlConfiguration;
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _cacheDuration;

        public CachedSQLConfiguration(
            SQLConfiguration sqlConfiguration,
            TimeSpan cacheInterval)
        {
            _sqlConfiguration = sqlConfiguration ?? throw new ArgumentNullException(nameof(sqlConfiguration));
            _cacheDuration = cacheInterval;
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        public bool EnableSsl => _cache
            .GetOrCreate(
                "EnableSsl",
                entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = _cacheDuration;
                    return _sqlConfiguration.EnableSsl;
                });

        public bool UseDefaultCredentials => _cache
            .GetOrCreate(
                "UseDefaultCredentials",
                entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = _cacheDuration;
                    return _sqlConfiguration.UseDefaultCredentials;
                });

        public string Host => _cache
            .GetOrCreate(
                "Host",
                entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = _cacheDuration;
                    return _sqlConfiguration.Host;
                });

        public int Port => _cache
            .GetOrCreate(
                "Port",
                entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = _cacheDuration;
                    return _sqlConfiguration.Port;
                });

        public string UserName => _cache
            .GetOrCreate(
                "UserName",
                entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = _cacheDuration;
                    return _sqlConfiguration.UserName;
                });

        public string Password => _cache
            .GetOrCreate(
                "Password",
                entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = _cacheDuration;
                    return _sqlConfiguration.Password;
                });
    }
}
