using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OT.Assessment.BLL.IServices;
using OT.Assessment.BLL.MessageHandler;
using OT.Assessment.BLL.Services;
using OT.Assessment.Domain.Data;
using OT.Assessment.Infrastructure.IRepository;
using OT.Assessment.Infrastructure.MessageHandler;
using OT.Assessment.Infrastructure.RabbitMQ;
using OT.Assessment.Infrastructure.Repository;
using OT.Assessment.Infrastructure.Settings;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Reflection;
using System.Text;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        

    })
    .ConfigureServices((context, services) =>
    {

        services.AddDbContext<OTDbContext>(options =>
        {
            options.UseSqlServer(context.Configuration.GetConnectionString("DatabaseConnection"));
        });

        services.Configure<RabbitSettings>(context.Configuration.GetSection("RabbitMQ"));

        services.AddAutoMapper(Assembly.Load("OT.Assessment.Infrastructure"));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IMessageHandler, CasionWagerMessageHandler>();
        services.AddScoped<IPlayersService, PlayersService>();

        services.AddHostedService<RabbitMQConsumer>();
        services.AddScoped(sp =>
        {
            return new ConnectionFactory();
        });

    })
    .Build();

/*var factory = new ConnectionFactory();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
factory.ClientProvidedName = "Consumner";

IConnection connection = factory.CreateConnection();

IModel channel = connection.CreateModel();

var exchangeName = "OTExchange";
var routingKey = "OTExchange-routing-key";
var queueName = "OTQueue";

channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
channel.QueueDeclare(queueName, false, false, false);
channel.QueueBind(queueName, exchangeName, routingKey, null);
channel.BasicQos(0, 1, false);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, args) =>
{
    var body = args.Body.ToArray();
    
    string message = Encoding.UTF8.GetString(body);
    Console.WriteLine(message);

    channel.BasicAck(args.DeliveryTag, false);
};

var consumerTag = channel.BasicConsume(queueName, false, consumer);*/

var logger = host.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Application started {time:yyyy-MM-dd HH:mm:ss}", DateTime.Now);

await host.RunAsync();
/*
channel.BasicCancel(consumerTag);
channel.Close();*/

logger.LogInformation("Application ended {time:yyyy-MM-dd HH:mm:ss}", DateTime.Now);