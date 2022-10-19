using Newtonsoft.Json;
using RedisTestApi.Entities;
using RedisTestApi.Infra;
using RedisTestApi.Services;

namespace RedisTestApi.Repositories
{
    public class ArticlesRepository : IArticlesRepository
    {
        private readonly IRedisRepository _redisRepository;
        private const string RestArticlesUrl = "https://brasilapi.com.br/api/banks/v1";
        private const string ArticlesKey = "Articles";


        public ArticlesRepository(IRedisRepository redisRepository)
        {
            this._redisRepository = redisRepository;
        }

        public async Task<List<Articles>> GetArticles()
        {
            var responseData = await _redisRepository.GetObjectInCache(ArticlesKey);

            if (responseData == null)
            {

                var response = RequestServices.Get(RestArticlesUrl);

                if (response.IsSuccessful)
                    responseData = response.Content;
                
                await _redisRepository.SetObjectInCache(ArticlesKey, responseData);
            }

            if (responseData == null)
                throw new Exception("Erro ao buscar Artigos");

            var articleList = JsonConvert.DeserializeObject<List<Articles>>(responseData);
            
            return articleList;
        }
    }
}
