using RestSharp;

namespace RedisTestApi.Services
{
    public class RequestClientFactory
    {
        public static RestClient GetClient(string path)
        {
            var client = new RestClient(path);
            client.AddDefaultHeader("Accept", "Application/json");
            return client;
        }
    }
}
