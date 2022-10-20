using Newtonsoft.Json;
using RabbitMQ.Entities;

namespace RabbitMQ.Services;

public class RabbitMqServiceMock : IRabbitMqService
{
    private static IList<string> listOfObjects = new List<string>();
    public void Publish(Post post)
    {
        var serializedObj = JsonConvert.SerializeObject(post);
        listOfObjects.Add(serializedObj);
    }

    public IList<string> Consume()
    {
        List<string> responseList = new List<string>(listOfObjects);
        listOfObjects.Clear();
        
        return responseList;
    }
}