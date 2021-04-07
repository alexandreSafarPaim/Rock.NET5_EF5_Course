using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CpmPedidos.API.Controllers
{
    public class AppBaseController : ControllerBase
    {
        protected readonly IServiceProvider ServiceProvider;
        
        protected T GetService<T>()
        {
            return ServiceProvider.GetService<T>();
        }
        public AppBaseController(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
