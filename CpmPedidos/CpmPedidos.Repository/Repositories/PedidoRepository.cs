using CpmPedidos.Domain;
using CpmPedidos.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CpmPedidos.Repository
{
    public class PedidoRepository : BaseRepository, IPedidoRepository
    {
        private string GetProximoNumero()
        {
            var ret = 1.ToString("00000");

            var ultimoNumero = DbContext.Pedidos.Max(x => x.Numero);

            if (!string.IsNullOrEmpty(ultimoNumero))
            {
                ret = (Convert.ToInt32(ultimoNumero) + 1).ToString("00000");
            }

            return ret;
        }
        public PedidoRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public decimal TiketMaximo()
        {
            var hoje = DateTime.Today;

            return DbContext.Pedidos
                .Where(x => x.CriadoEm.Date == hoje)
                .Max(x => (decimal?)x.ValorTotal) ?? 0;
        }

        public dynamic PedidosClientes()
        {
            var hoje = DateTime.Today;
            var inicioMes = new DateTime(hoje.Year, hoje.Month, 1);
            var finalMes = new DateTime(hoje.Year, hoje.Month, DateTime.DaysInMonth(hoje.Year, hoje.Month));

            return DbContext.Pedidos
                .Where(x => x.CriadoEm.Date >= inicioMes && x.CriadoEm.Date <= finalMes)
                .GroupBy(pedido => new { pedido.IdCliente, pedido.Cliente.Nome },
                        (chave, pedidos) => new
                        {
                            Cliente = chave.Nome,
                            Pedidos = pedidos.Count(),
                            Total = pedidos.Sum(pedido => pedido.ValorTotal)
                        }
                    )
                .ToList();
        }

        public string SalvarPedido(PedidoDTO pedido)
        {
            var ret = "";
            try
            {
                using (var transaction = DbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var entity = new Pedido
                        {
                            Numero = GetProximoNumero(),
                            IdCliente = pedido.IdCliente,
                            CriadoEm = DateTime.Now,
                            ProdutosPedidos = new List<ProdutoPedido>()
                        };

                        var valorTotal = 0m;
                        foreach (var prodPed in pedido.ProdutosPedidos)
                        {
                            var precoProduto = DbContext.Produtos
                                .Where(x => x.Id == prodPed.IdProduto)
                                .Select(x => x.Preco)
                                .FirstOrDefault();
                            if (precoProduto > 0)
                            {
                                valorTotal += prodPed.Quantidade * precoProduto;

                                entity.ProdutosPedidos.Add(new ProdutoPedido
                                {
                                    IdProduto = prodPed.IdProduto,
                                    Quantidade = prodPed.Quantidade,
                                    Preco = precoProduto
                                });
                            }
                        }

                        entity.ValorTotal = valorTotal;

                        DbContext.Pedidos.Add(entity);

                        DbContext.SaveChanges();

                        transaction.Commit();

                        ret = entity.Numero;

                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }




            }
            catch (Exception ex)
            {

            }
            return ret;
        }
    }
}
