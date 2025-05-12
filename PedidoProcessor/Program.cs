using PedidoProcessor.MessageQueue;
using PedidoProcessor.Services;
using PedidoProcessor.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<PedidoService>();
BusConfigurator.Configure(builder.Services);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();
