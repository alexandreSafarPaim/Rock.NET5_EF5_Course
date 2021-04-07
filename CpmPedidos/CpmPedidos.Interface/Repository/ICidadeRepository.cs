using CpmPedidos.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CpmPedidos.Interface
{
    public interface ICidadeRepository
    {
        dynamic Get();
        dynamic Criar(CidadeDTO model);
        dynamic Editar(CidadeDTO model);
        dynamic Deletar(int id);
    }
}
