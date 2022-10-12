using Newtonsoft.Json;
using RedisTestApi.Entities;
using RedisTestApi.Services;

namespace RedisTestApi.Repositories
{
    public class BankRepository : IBankRepository
    {
        private readonly IRedisRepository _redisRepository;
        private const string CountriesKey = "Banks";


        public BankRepository(IRedisRepository redisRepository)
        {
            this._redisRepository = redisRepository;
        }

        public async Task<List<Banks>> GetBanks()
        {
            var responseData = await _redisRepository.GetObjectInCache(CountriesKey);

            if (responseData == null)
            {
                const string restCountriesUrl = "https://brasilapi.com.br/api/banks/v1";

                var response = RequestServices.Get(restCountriesUrl);

                if (response.IsSuccessful)
                    responseData = response.Content;
            }

            if (responseData == null)
                throw new Exception("Erro ao buscar Bancos");

            var countryList = JsonConvert.DeserializeObject<List<Banks>>(responseData);

            await _redisRepository.SetObjectInCache(CountriesKey, responseData);

            return countryList;
        }
    }
}
