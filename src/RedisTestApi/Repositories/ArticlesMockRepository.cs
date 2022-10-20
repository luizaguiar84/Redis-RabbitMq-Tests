using Newtonsoft.Json;
using RedisTestApi.Entities;
using RedisTestApi.Infra;

namespace RedisTestApi.Repositories;

public class ArticlesMockRepository : IArticlesRepository
{
    private readonly IRedisRepository _redisRepository;
    private const string ArticlesKey = "Articles";

    public ArticlesMockRepository(IRedisRepository redisRepository)
    {
        _redisRepository = redisRepository;
    }
    public async Task<List<Articles>> GetArticles()
    {
        
        var responseData = await _redisRepository.GetObjectInCache(ArticlesKey);

        if (responseData == null)
        {
            responseData  = JsonConvert.SerializeObject(new List<Articles>()
            {
                new Articles()
                {
                    Code = 111,
                    FullName = "FullNameTest",
                    Ispb = "ISPB test",
                    Name = "Name"
                }
            });
            
            //simulate db access / get in external Api
            Thread.Sleep(TimeSpan.FromSeconds(5));
            
            await _redisRepository.SetObjectInCache(ArticlesKey, responseData);
        }

        if (responseData == null)
            throw new Exception("Erro ao buscar Artigos");
        
        var articlesList = JsonConvert.DeserializeObject<List<Articles>>(responseData);
            
        return articlesList;
        
        
    }
}