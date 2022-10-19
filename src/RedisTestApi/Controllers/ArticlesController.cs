using Microsoft.AspNetCore.Mvc;
using RedisTestApi.Repositories;

namespace RedisTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticlesRepository _articlesRepository;


        public ArticlesController(
            IArticlesRepository articlesRepository)
        {
            this._articlesRepository = articlesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var articles = await _articlesRepository.GetArticles();

            return Ok(articles);
        }
    }
}
