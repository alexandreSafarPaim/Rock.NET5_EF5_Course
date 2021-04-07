using CpmPedidos.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CpmPedidos.Interface
{
    public interface IPedidoRepository
    {
        decimal TiketMaximo();
        dynamic PedidosClientes();
        string SalvarPedido(PedidoDTO pedido);
    }
}
