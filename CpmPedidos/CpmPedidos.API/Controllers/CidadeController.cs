using System;
using CpmPedidos.Domain;
using CpmPedidos.Interface;
using Microsoft.AspNetCore.Mvc;


namespace CpmPedidos.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CidadeController : AppBaseController
    {
        public CidadeController(IServiceProvider serviceProvider) : base(serviceProvider) { }

        [HttpGet]
        public dynamic Get()
        {
            //var rep = GetService<ICidadeRepository>();
            return GetService<ICidadeRepository>().Get();
        }

        [HttpPost]
        public dynamic Criar(CidadeDTO model)
        {
            return GetService<ICidadeRepository>().Criar(model);
        }

        [HttpPut]
        public dynamic Editar(CidadeDTO model)
        {
            return GetService<ICidadeRepository>().Editar(model);
        }

        [HttpDelete]
        [Route("{id}")]
        public dynamic Editar(int id)
        {
            return GetService<ICidadeRepository>().Deletar(id);
        }
    }
}
