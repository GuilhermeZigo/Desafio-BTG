namespace PedidoProcessor.Models;

public class Pedido
{
    public Guid Id { get; set; }
    public string ClienteId { get; set; } = string.Empty;
    public List<ItemPedido> Itens { get; set; } = new();
    public string Status { get; set; } = "pendente";
}
