using RestSharp;

namespace RedisTestApi.Services
{
    public class RequestServices
    {
        public static RestResponse Get(string path)
        {
            var client = RequestClientFactory.GetClient(path);
            var request = new RestRequest(path);

            return client.Get(request);
        }
    }
}
