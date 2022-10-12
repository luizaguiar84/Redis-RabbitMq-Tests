using Microsoft.AspNetCore.Mvc;
using RedisTestApi.Repositories;

namespace RedisTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController : ControllerBase
    {
        private readonly IBankRepository bankRepository;


        public BanksController(
            IBankRepository bankRepository)
        {
            this.bankRepository = bankRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var banks = await bankRepository.GetBanks();

            return Ok(banks);
        }
    }
}
