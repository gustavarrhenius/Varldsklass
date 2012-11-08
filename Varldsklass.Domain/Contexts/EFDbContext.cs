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
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Attendant> Attendants { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<PopularCourse> PopularCourses { get; set; }
    }
}
