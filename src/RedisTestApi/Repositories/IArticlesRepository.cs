using RedisTestApi.Entities;

namespace RedisTestApi.Repositories
{
    public interface IArticlesRepository
    {
        public Task<List<Articles>> GetArticles();
    }
}