using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Varldsklass.Domain.Entities.Abstract;
using Varldsklass.Domain.Repositories.Abstract;
using System.Data.Entity;

namespace Varldsklass.Domain.Repositories
{
    public class FakeRepository<T> : IRepository<T> where T : class, IEntity
    {
        protected List<T> context;

        public FakeRepository(params T[] entities)
        {
            context = entities.ToList();
        }

        public DbContext Model { get { return null; } set { return; } }

        public virtual IQueryable<T> FindAll(Func<T,bool> filter = null)
        {
            if (null == filter)
                return context.AsQueryable();
            return context.Where(filter).AsQueryable();
        }

        public virtual T FindByID(int id)
        {
            return context.Find(e => e.ID == id);
        }

        public virtual void Save(T entity)
        {
            var existing = context.Where(e => e.ID == entity.ID).FirstOrDefault();
            if (null != existing)
                context[context.IndexOf(existing)] = entity;
            else
                context.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            context.Remove(entity);
        }

        public virtual void Commit() { }
    }
}