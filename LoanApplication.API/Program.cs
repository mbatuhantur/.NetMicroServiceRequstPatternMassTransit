using MassTransit;
using Messaging.Const;
using Messaging.CustomerCredit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(opt =>
{

    //request için exchange gerekli
    opt.AddRequestClient<GetCreditScoreRequest>(new Uri($"exchange:{EndpointTypes.GetCreditScore}"),5000);

    opt.UsingRabbitMq((contex, config) =>
    {
        config.Host(builder.Configuration.GetConnectionString("RabbitConn"));
        config.ConfigureEndpoints(contex);
    });

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
