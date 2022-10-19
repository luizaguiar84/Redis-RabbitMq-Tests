namespace RedisTestApi.Infra;

public class RedisMockRepository : IRedisRepository
{
    private static Dictionary<string, string> cachedObject = new Dictionary<string, string>();
    public async Task<string> GetObjectInCache(string key)
    {
        if (cachedObject.ContainsKey(key))
            return cachedObject[key];

        return null;
    }

    public async Task SetObjectInCache(string key, string data)
    {
        cachedObject.Add(key, data);
    }
}