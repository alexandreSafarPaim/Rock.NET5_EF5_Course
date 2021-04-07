using System;
using CpmPedidos.Domain;
using CpmPedidos.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CpmPedidos.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidoController : AppBaseController
    {
        public PedidoController(IServiceProvider serviceProvider) : base(serviceProvider) { }

        [HttpGet]
        [Route("ticket")]
        public decimal TicketMaximo()
        {
           var rep = (IPedidoRepository)ServiceProvider.GetService(typeof(IPedidoRepository));
            return rep.TiketMaximo();
        }

        [HttpGet]
        [Route("por-cliente")]
        public dynamic PedidosClientes()
        {
            var rep = (IPedidoRepository)ServiceProvider.GetService(typeof(IPedidoRepository));
            return rep.PedidosClientes();
        }
        [HttpPost]
        [Route("")]
        public string SalvarPedido(PedidoDTO pedido)
        {
            return GetService<IPedidoRepository>().SalvarPedido(pedido);
        }
    }
}
