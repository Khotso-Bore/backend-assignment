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
using System.Threading.Channels;
using System.Threading.Tasks;

namespace OT.Assessment.Infrastructure.RabbitMQ
{
    public class RabbitMQConsumer : IHostedService , IDisposable
    {
        private readonly RabbitSettings _rabbitSettings;
        private readonly ConnectionFactory _connectionFactory;
        private readonly IModel _channel;
        private readonly IMessageHandler _messageHandler;
        private readonly IConnection _connection;
        private readonly string _consumerTag;


        public RabbitMQConsumer(ConnectionFactory connectionFactory, IMessageHandler messageHandler, IOptions<RabbitSettings> rabbitSettings)
        {
            _rabbitSettings = rabbitSettings.Value;

            _connectionFactory = connectionFactory;
            _connectionFactory.UserName = _rabbitSettings.User;
            _connectionFactory.Password = _rabbitSettings.Password;
            _connectionFactory.HostName = _rabbitSettings.HostName;
            _connectionFactory.Port = _rabbitSettings.Port;

            _connectionFactory.ClientProvidedName = "Consumer";
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(_rabbitSettings.ExchangeName, ExchangeType.Direct);
            _channel.QueueDeclare(_rabbitSettings.QueueName, false, false, false);
            _channel.QueueBind(_rabbitSettings.QueueName, _rabbitSettings.ExchangeName, _rabbitSettings.RoutingKey, null);
            _channel.BasicQos(0, 1, false);
            
            _messageHandler = messageHandler;

            //_messageHandler.HandleMessage([]);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, args) =>
            {
                var message = args.Body.ToArray();
                _messageHandler.HandleMessage(message);
                Console.WriteLine(message);

                _channel.BasicAck(args.DeliveryTag, false);
            };

            var _consumerTag = _channel.BasicConsume(_rabbitSettings.QueueName, false, consumer);

            //Consume();
        }

        public void Consume()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, args) =>
            {
                var message = args.Body.ToArray();
                _messageHandler.HandleMessage(message);
                _channel.BasicAck(args.DeliveryTag, false);
            };
        }

        public void Dispose()
        {
            _channel.BasicCancel(_consumerTag);
            _channel.Close();
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
