using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CpmPedidos.Domain
{
    public class PedidoDTO
    {
        public int IdCliente { get; set; }
        public List<ProdutoPedidoDTO> ProdutosPedidos { get; set; }
    }
}
