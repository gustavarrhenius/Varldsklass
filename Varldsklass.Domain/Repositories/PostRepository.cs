﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Varldsklass.Domain.Entities;
using Varldsklass.Domain.Repositories.Abstract;

namespace Varldsklass.Domain.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        IQueryable<Post> FindPostsByCategoryID(int id);
        IQueryable<Post> FindPostsByCategoryName(string name);
    }

    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository() : base() {}

        public IQueryable<Post> FindPostsByCategoryID(int id)
        {
            return _dbSet.Where(p => p.Category.ID == id).Include(p => p.Category);
        }

        public IQueryable<Post> FindPostsByCategoryName(string name)
        {
            return _dbSet.Where(p => p.Category.Name == name).Include(p => p.Category);
        }

        // Filter Methods for ProductRepository

        public static Func<Post, bool> FilterProductsWithEmptyDescription
        {
            get
            {
                return p => string.IsNullOrEmpty(p.Body);
            }
        }
    }
}