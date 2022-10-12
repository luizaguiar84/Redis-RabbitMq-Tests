using Microsoft.AspNetCore.Mvc;
using RedisTestApi.Repositories;

namespace RedisTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController : ControllerBase
    {
        private readonly IBankRepository _bankRepository;


        public BanksController(
            IBankRepository bankRepository)
        {
            this._bankRepository = bankRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var banks = await _bankRepository.GetBanks();

            return Ok(banks);
        }
    }
}
