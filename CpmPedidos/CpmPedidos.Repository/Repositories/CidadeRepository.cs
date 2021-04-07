using CpmPedidos.Domain;
using CpmPedidos.Interface;
using System;
using System.Linq;

namespace CpmPedidos.Repository
{
    public class CidadeRepository : BaseRepository, ICidadeRepository
    {
        public CidadeRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public dynamic Get()
        {
            return DbContext.Cidades
                .Where(x => x.Ativo)
                .Select(x => new
                {
                    x.Id,
                    x.Nome,
                    x.Uf,
                    x.Ativo
                })
                .ToList();
        }

        public dynamic Criar(CidadeDTO model)
        {
            if (model.Id > 0) return null;

            var nomeDuplicado = DbContext.Cidades.Any(x => x.Ativo && x.Nome.ToUpper() == model.Nome.ToUpper());

            if (nomeDuplicado)
            {
                return 0;
            }

            var entity = new Cidade()
            {
                Nome = model.Nome,
                Uf = model.Uf,
                Ativo = model.Ativo
            };
            try
            {
                DbContext.Cidades.Add(entity);
                DbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return entity.Id;

        }

        public dynamic Editar(CidadeDTO model)
        {
            var entity = DbContext.Cidades.Find(model.Id);

            if (entity == null) return 0;

            var nomeDuplicado = DbContext.Cidades.Any(x => x.Ativo && x.Nome.ToUpper() == model.Nome.ToUpper() && x.Id != model.Id);

            if (nomeDuplicado)
            {
                return 0;
            }

            entity.Nome = model.Nome;
            entity.Uf = model.Uf;
            entity.Ativo = model.Ativo;

            try
            {
                DbContext.Cidades.Update(entity);
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return model;
        }

        public dynamic Deletar(int id)
        {
            if (id <= 0) return false;

            var entity = DbContext.Cidades.Find(id);

            if (entity == null) return false;

            try
            {
                DbContext.Cidades.Remove(entity);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
