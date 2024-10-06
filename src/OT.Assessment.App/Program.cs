using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using OT.Assessment.BLL.IServices;
using OT.Assessment.BLL.Services;
using OT.Assessment.Domain.Data;
using OT.Assessment.Infrastructure.IRepository;
using OT.Assessment.Infrastructure.RabbitMQ;
using OT.Assessment.Infrastructure.Repository;
using OT.Assessment.Infrastructure.Settings;
using RabbitMQ.Client;
using System.Reflection;
using System.Threading.Channels;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddDbContext<OTDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"));
});

builder.Services.Configure<RabbitSettings>(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRabbitMQProducer, RabbitMQProducer>();
builder.Services.AddScoped<IPlayersService, PlayersService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckl
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opts =>
    {
        opts.EnableTryItOutByDefault();
        opts.DocumentTitle = "OT Assessment App";
        opts.DisplayRequestDuration();
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
