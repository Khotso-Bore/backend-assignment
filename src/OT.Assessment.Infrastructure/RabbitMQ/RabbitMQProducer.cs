using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OT.Assessment.Infrastructure.MessageHandler;
using OT.Assessment.Infrastructure.Settings;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace OT.Assessment.Infrastructure.RabbitMQ
{
    public class RabbitMQProducer : IRabbitMQProducer, IHostedService , IDisposable
    {
        private readonly RabbitSettings _rabbitSettings;
        private readonly ConnectionFactory _connectionFactory;
        private readonly IModel _channel;
        private readonly IConnection _connection;


        public RabbitMQProducer(ConnectionFactory connectionFactory, IOptions<RabbitSettings> rabbitSettings)
        {
            _rabbitSettings = rabbitSettings.Value;

            _connectionFactory = connectionFactory;
            _connectionFactory.UserName = _rabbitSettings.User;
            _connectionFactory.Password = _rabbitSettings.Password;
            _connectionFactory.HostName = _rabbitSettings.HostName;
            _connectionFactory.Port = _rabbitSettings.Port;

            _connectionFactory.ClientProvidedName = "Producer";
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(_rabbitSettings.ExchangeName, ExchangeType.Direct);
            _channel.QueueDeclare(_rabbitSettings.QueueName, false, false, false);
            _channel.QueueBind(_rabbitSettings.QueueName, _rabbitSettings.ExchangeName, _rabbitSettings.RoutingKey, null);

        }

        public void Publish<T>(T obj)
        {

            string message = JsonSerializer.Serialize(obj);
            var body = Encoding.UTF8.GetBytes(message);
            
            _channel.BasicPublish(exchange: _rabbitSettings.ExchangeName,
                                 routingKey: _rabbitSettings.RoutingKey,
                                 basicProperties: null,
                                 body: body);
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();  
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Dispose();
            return Task.CompletedTask;
        }
    }
}
