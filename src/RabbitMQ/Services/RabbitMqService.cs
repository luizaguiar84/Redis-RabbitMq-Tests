using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Entities;

namespace RabbitMQ.Services;

public interface IRabbitMqService
{
    void Publish(Post post);
    IList<string> Consume();
}

public class RabbitMqService : IRabbitMqService
{
    private readonly string _uri;
    public RabbitMqService(IConfiguration configuration)
    {
        _uri = configuration["RabbitMq:uri"];
    }
    public void Publish(Post post)
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri(_uri)
        };

        var connection = factory.CreateConnection();

        using var channel = connection.CreateModel();

        var x = JsonConvert.SerializeObject(post);
        
        byte[] body = Encoding.UTF8.GetBytes(x);

        channel.BasicPublish(
            exchange: "Exchange",
            routingKey: "",
            body: body
        );
    }

    public IList<string> Consume()
    {
        var response = new List<string>();

        var factory = new ConnectionFactory
        {
            Uri = new Uri(_uri)
        };

        var connection = factory.CreateConnection();

        var channel = connection.CreateModel();

        var consumidor = new EventingBasicConsumer(channel);

        consumidor.Received += (sender, eventArg) =>
        {
            var message = Encoding.UTF8.GetString(eventArg.Body.Span);
            response.Add($"Mensagem recebida: {message}");
            channel.BasicAck(eventArg.DeliveryTag, multiple: false);
        };

        var queue = "queue";
        channel.BasicConsume(queue: queue, autoAck: false, consumer: consumidor);

        return response;
    }
}