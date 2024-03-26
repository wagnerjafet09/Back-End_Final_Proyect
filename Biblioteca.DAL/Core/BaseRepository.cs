using Biblioteca.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.DAL.Core
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly BibliotecaContext context;
        private readonly DbSet<TEntity> myDbSet;

        public BaseRepository(BibliotecaContext context)
        {
            this.context = context;
            this.myDbSet = this.context.Set<TEntity>();
        }

        public async virtual Task<List<TEntity>> GetAll()
        {
            List<TEntity> entities = new List<TEntity>();
            entities = context.Set<TEntity>().ToList();
            return entities;
        }

        public async virtual Task<TEntity> GetByID(int id)
        {
            var entity = myDbSet.Find(id);
            return entity;
        }

        public async virtual Task SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}
