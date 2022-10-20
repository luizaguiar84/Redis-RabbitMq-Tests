using RabbitMQ.Entities;

namespace RabbitMQ.Services;

public interface IRabbitMqService
{
    void Publish(Post post);
    IList<string> Consume();
}