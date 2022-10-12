using Microsoft.Extensions.Caching.Distributed;

namespace RedisTestApi.Repositories
{
    public class RedisRepository : IRedisRepository
    {
        private readonly IDistributedCache _distributedCache;

        public RedisRepository(IDistributedCache distributedCache)
        {
            this._distributedCache = distributedCache;
        }
        public async Task<string> GetObjectInCache(string key)
        {
            var countriesObject = await _distributedCache.GetStringAsync(key);

            return countriesObject;
        }

        public async Task SetObjectInCache(string key, string data)
        {

            var memoryCacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10),
                SlidingExpiration = TimeSpan.FromSeconds(10)
            };

            await _distributedCache.SetStringAsync(key, data, memoryCacheOptions);
        }
    }
}
