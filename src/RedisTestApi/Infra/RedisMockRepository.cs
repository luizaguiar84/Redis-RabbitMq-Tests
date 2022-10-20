namespace RedisTestApi.Infra;

public class RedisMockRepository : IRedisRepository
{
    private static Dictionary<string, string> cachedObject = new Dictionary<string, string>();
    private static DateTime timeToExpire = DateTime.Now;


    public async Task<string> GetObjectInCache(string key)
    {
        //simulate expire of cache
        if (timeToExpire.AddSeconds(20) < DateTime.Now)
            cachedObject.Clear();      

        if (cachedObject.ContainsKey(key))
            return cachedObject[key];

        return null;
    }

    public async Task SetObjectInCache(string key, string data)
    {
        timeToExpire = DateTime.Now;

        if (!cachedObject.ContainsKey(key))
            cachedObject.Add(key, data);
    }
}