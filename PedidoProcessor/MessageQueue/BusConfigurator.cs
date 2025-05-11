using MassTransit;
using PedidoProcessor.Consumers;

namespace PedidoProcessor.MessageQueue;

public static class BusConfigurator
{
    public static void Configure(IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<PedidoConsumer>();

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint("fila-pedidos", e =>
                {
                    e.ConfigureConsumer<PedidoConsumer>(ctx);
                });
            });
        });
    }
}
