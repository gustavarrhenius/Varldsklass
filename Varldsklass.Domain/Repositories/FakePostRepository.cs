using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Varldsklass.Domain.Entities;

namespace Varldsklass.Domain.Repositories
{
    public class FakePostRepository : FakeRepository<Post>, IPostRepository
    {
        public FakePostRepository(params Post[] posts)
        {
            context = posts.ToList();
        }

        public IQueryable<Post> FindProductsByCategoryID(int id)
        {
            return new List<Post>().AsQueryable();
        }
    }
}