using RedisTestApi.Entities;

namespace RedisTestApi.Repositories
{
    public interface IBankRepository
    {
        public Task<List<Banks>> GetBanks();
    }
}