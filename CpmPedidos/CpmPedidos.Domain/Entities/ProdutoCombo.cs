using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CpmPedidos.Domain
{
    public class ProdutoCombo
    {
        public int IdProduto { get; set; }
        public virtual Produto Produto { get; set; }

        public int IdCombo { get; set; }
        public virtual Combo Combo { get; set; }
    }
}
