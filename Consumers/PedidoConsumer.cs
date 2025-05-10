using MassTransit;
using PedidoProcessor.Models;
using PedidoProcessor.Services;

namespace PedidoProcessor.Consumers;

public class PedidoConsumer : IConsumer<Pedido>
{
    private readonly PedidoService _pedidoService;

    public PedidoConsumer(PedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    public async Task Consume(ConsumeContext<Pedido> context)
    {
        await Task.Delay(10000); // simulação do delay
        _pedidoService.AtualizarStatus(context.Message.Id, "processado");
        Console.WriteLine($"Pedido {context.Message.Id} processado.");
    }
}
