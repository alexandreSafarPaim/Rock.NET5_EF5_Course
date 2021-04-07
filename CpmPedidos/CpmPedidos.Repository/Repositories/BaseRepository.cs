using CpmPedidos.Domain;
using CpmPedidos.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CpmPedidos.Repository
{
    public class BaseRepository
    {
        protected const int TamanhoPagina = 5;

        protected readonly ApplicationDbContext DbContext;
        public BaseRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

    }
}
