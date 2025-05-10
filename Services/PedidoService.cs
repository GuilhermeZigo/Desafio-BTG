using PedidoProcessor.Models;
using System.Collections.Concurrent;

namespace PedidoProcessor.Services;

public class PedidoService
{
    private readonly ConcurrentDictionary<Guid, Pedido> _pedidos = new();

    public Pedido CriarPedido(Pedido pedido)
    {
        pedido.Id = Guid.NewGuid();
        _pedidos[pedido.Id] = pedido;
        return pedido;
    }

    public Pedido? ObterPedido(Guid id)
    {
        _pedidos.TryGetValue(id, out var pedido);
        return pedido;
    }

    public void AtualizarStatus(Guid id, string status)
    {
        if (_pedidos.TryGetValue(id, out var pedido))
        {
            pedido.Status = status;
        }
    }
}
