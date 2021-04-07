using CpmPedidos.Domain;
using CpmPedidos.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CpmPedidos.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : AppBaseController
    {
        public ProdutoController(IServiceProvider serviceProvider) : base(serviceProvider) { }

        [HttpGet]
        public dynamic Get([FromQuery] string ordem = "")
        {
            var rep = (IProdutoRepository)ServiceProvider.GetService(typeof(IProdutoRepository));
            return rep.Get(ordem);
        }

        [HttpGet]
        [Route("search/{text}/{page?}")]
        public dynamic GetSearch(string text, int page = 1, [FromQuery] string ordem = "")
        {
            var rep = (IProdutoRepository)ServiceProvider.GetService(typeof(IProdutoRepository));
            return rep.Search(text, page, ordem);
        }

        [HttpGet]
        [Route("{id}")] 
        public dynamic Detail(int? id)
        {
            if ((id ?? 0) > 0)
            {
            var rep = (IProdutoRepository)ServiceProvider.GetService(typeof(IProdutoRepository));
            return rep.Detail(id);
            }else
            {
                return null;
            }
        }


        [HttpGet]
        [Route("{id}/imagens")]
        public dynamic Imagens(int? id)
        {
            if ((id ?? 0) > 0)
            {
                var rep = (IProdutoRepository)ServiceProvider.GetService(typeof(IProdutoRepository));
                return rep.Imagens(id);
            }
            else
            {
                return null;
            }
        }
    }
}
