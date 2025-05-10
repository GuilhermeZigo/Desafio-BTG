using PedidoProcessor.MessageQueue;
using PedidoProcessor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<PedidoService>();
BusConfigurator.Configure(builder.Services);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();
