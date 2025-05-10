using MassTransit;
using Microsoft.AspNetCore.Mvc;
using PedidoProcessor.Models;
using PedidoProcessor.Services;

namespace PedidoProcessor.Controllers;

[ApiController]
[Route("pedidos")]
public class PedidoController : ControllerBase
{
    private readonly PedidoService _pedidoService;
    private readonly IPublishEndpoint _publishEndpoint;

    public PedidoController(PedidoService pedidoService, IPublishEndpoint publishEndpoint)
    {
        _pedidoService = pedidoService;
        _publishEndpoint = publishEndpoint;
    }

  [HttpPost]
public async Task<IActionResult> CriarPedido([FromBody] Pedido pedido)
{
    if (string.IsNullOrWhiteSpace(pedido.ClienteId))
        return BadRequest("ClienteId é obrigatório.");

    if (pedido.Itens == null || !pedido.Itens.Any())
        return BadRequest("A lista de itens não pode estar vazia.");

    var novoPedido = _pedidoService.CriarPedido(pedido);
    await _publishEndpoint.Publish(novoPedido);
    return CreatedAtAction(nameof(ObterPedido), new { id = novoPedido.Id }, novoPedido);
}


    [HttpGet("{id}")]
    public IActionResult ObterPedido(Guid id)
    {
        var pedido = _pedidoService.ObterPedido(id);
        if (pedido == null) return NotFound();
        return Ok(new { pedido.Id, pedido.Status });
    }
}
