using MassTransit;
using PedidoProcessor.Models;
using PedidoProcessor.Services;

namespace PedidoProcessor.Consumers;

public class PedidoConsumer : IConsumer<Pedido>
{
    private const int TempoSimuladoEmMs = 10000;
    private readonly PedidoService _pedidoService;

    public PedidoConsumer(PedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    public async Task Consume(ConsumeContext<Pedido> context)
{
    Console.WriteLine($"[Fila] Pedido recebido: {context.Message.Id}");
    
    await Task.Delay(TempoSimuladoEmMs); // Simula delay

    _pedidoService.AtualizarStatus(context.Message.Id, "processado");

    Console.WriteLine($"[Processador] Pedido {context.Message.Id} marcado como processado.");
}

}
