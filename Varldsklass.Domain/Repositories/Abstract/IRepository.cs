using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Varldsklass.Domain.Entities.Abstract;
using System.Data.Entity;

namespace Varldsklass.Domain.Repositories.Abstract
{
    public interface IRepository<T> where T : class, IEntity
    {
        DbContext Model { get; set; }

        IQueryable<T> FindAll(Func<T, bool> filter = null);
        T FindByID(int id);
        void Save(T entity);
        void Delete(T entity);

        void Commit();
    }
}