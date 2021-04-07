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
    public class ProdutoRepository : BaseRepository, IProdutoRepository
    {
        public ProdutoRepository(ApplicationDbContext dbContext) : base(dbContext) {}

        public dynamic Get(string ordem)
        {
            var queryProduto = DbContext.Produtos
                .Include(x => x.CategoriaProduto)
                .Where(x => x.Ativo);

            OrdenarPorNome(ref queryProduto, ordem);

            var queryRetorno = queryProduto
                .Select(x => new {
                    x.Nome,
                    x.Preco,
                    Categoria = x.CategoriaProduto.Nome,
                    Imagens = x.Imagens.Select(i => new {
                        i.Id,
                        i.Nome,
                        i.NomeArquivo
                    })
                });


                return queryRetorno.ToList();
        }

        public dynamic Search(string text, int page, string ordem)
        {
            var quantProdutos = DbContext.Produtos
                .Where(x => x.Ativo && (x.Nome.ToUpper().Contains(text.ToUpper()) || x.Descricao.ToUpper().Contains(text.ToUpper())))
                .Count();

            var quantPaginas = (quantProdutos / TamanhoPagina);
            if (quantPaginas < 1)
            {
                quantPaginas = 1;
            }
            page = (page > quantPaginas) ? quantPaginas : page;
            
            var Paginacao = new { quantPaginas, paginaAtual = page };

            var queryProduto = DbContext.Produtos
                .Include(x => x.CategoriaProduto)
                .Where(x => x.Ativo &&
                      (x.Nome.ToUpper().Contains(text.ToUpper()) ||
                       x.Descricao.ToUpper().Contains(text.ToUpper())))
                .Skip(TamanhoPagina * (page - 1))
                .Take(TamanhoPagina);

            OrdenarPorNome(ref queryProduto, ordem);

            var queryRetorno = queryProduto
                .Select(x => new {
                    x.Nome,
                    x.Preco,
                    Categoria = x.CategoriaProduto.Nome,
                    Imagens = x.Imagens.Select(i => new {
                        i.Id,
                        i.Nome,
                        i.NomeArquivo
                    })
                });


            var produtos = queryRetorno.ToList();

            
            return new { Paginacao , produtos  };
        }

        public dynamic Detail(int? id)
        {
            return DbContext.Produtos
                .Include(x => x.Imagens)
                .Include(x => x.CategoriaProduto)
                .Where(x => x.Ativo && x.Id == id)
                .Select(x => new { 
                    x.Id,
                    x.Nome,
                    x.Codigo,
                    x.Descricao,
                    x.Preco,
                    Categoria = new
                    {
                        x.CategoriaProduto.Id,
                        x.CategoriaProduto.Nome
                    },
                    Imagens = x.Imagens.Select(i => new { 
                        i.Id,
                        i.Nome,
                        i.NomeArquivo
                    })
                })
                .FirstOrDefault();
        }

        public dynamic Imagens(int? id)
        {
            return DbContext.Produtos
                .Include(x => x.Imagens)
                .Where(x => x.Ativo && x.Id == id)
                .SelectMany(x => x.Imagens, (produto, imagem) => new 
                {
                    IdProduto = produto.Id,
                    imagem.Id,
                    imagem.Nome,
                    imagem.NomeArquivo
                })
                .FirstOrDefault();
        }
    }
}
