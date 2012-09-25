using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Varldsklass.Domain.Entities;
using System.Data.Entity;

namespace Varldsklass.Domain.Contexts
{
    public class EFDbContext : DbContext
    {
        public DbSet<Post> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}