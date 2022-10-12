
namespace RedisTestApi.Infra
{
    public interface IRedisRepository
    {
        Task<string> GetObjectInCache(string key);
        Task SetObjectInCache(string key, string data);
    }
}