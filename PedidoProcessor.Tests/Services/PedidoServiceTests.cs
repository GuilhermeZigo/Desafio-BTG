using Xunit;
using PedidoProcessor.Models;
using PedidoProcessor.Services;
using System;
using System.Collections.Generic;

namespace PedidoProcessor.Tests.Services
{
    public class PedidoServiceTests
    {
        [Fact]
        public void CriarPedido_DeveRetornarPedidoComStatusPendente()
        {
            
            var service = new PedidoService();
            var pedido = new Pedido
            {
                ClienteId = "cliente123",
                Itens = new List<ItemPedido>
                {
                    new ItemPedido { Nome = "Teclado", Quantidade = 1 }
                }
            };

           
            var resultado = service.CriarPedido(pedido);

          
            Assert.NotNull(resultado);
            Assert.Equal("pendente", resultado.Status);
            Assert.NotEqual(Guid.Empty, resultado.Id);
            Assert.Equal(pedido.ClienteId, resultado.ClienteId);
            Assert.Single(resultado.Itens);
        }

        [Fact]
        public void AtualizarStatus_DeveAlterarStatusDoPedido()
        {
            
            var service = new PedidoService();
            var pedido = service.CriarPedido(new Pedido
            {
                ClienteId = "cliente456",
                Itens = new List<ItemPedido> { new ItemPedido { Nome = "Mouse", Quantidade = 2 } }
            });

           
            service.AtualizarStatus(pedido.Id, "processado");
            var atualizado = service.ObterPedido(pedido.Id);

    
            Assert.Equal("processado", atualizado?.Status);
        }
    }
}
