using CustomerCredit.API.Consumers;
using MassTransit;
using Messaging.Const;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(opt =>
{
    //endpoint ismi veriliyor
    opt.AddConsumer<CreditScoreConsumer>().Endpoint(x => x.Name = EndpointTypes.GetCreditScore);

    opt.UsingRabbitMq((context, config) =>
    {
        config.Host(builder.Configuration.GetConnectionString("RabbitConn"));
        config.ConfigureEndpoints(context);
        //not:event güdümlü geliþtirme config de consumer objesi kullanýlýrken endpoint kullanýlýyor
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
