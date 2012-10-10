using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Varldsklass.Domain.Contexts;
using Varldsklass.Domain.Entities;
using System.Security.Cryptography;

namespace Varldsklass.Domain.DBInitializers
{
    public class EFDBInitializer : DropCreateDatabaseAlways<EFDbContext> //IfModelChanges<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            var Standard = new Category { ID = 1, Name = "Standard" };
            var Update = new Category { ID = 2, Name = "Update" };
            var News = new Category { ID = 3, Name = "News" };
            var Kurs = new Category { ID = 4, Name = "Kurs" };

            var posts = new List<Post>
            {
                new Post { ID = 1, Title = @"Kayak", Body = @"A boat for one person", Created = new DateTime(2012, 9, 18), Category = Standard },
                new Post { ID = 2, Title = @"Lifejacket", Body = @"Protective and fashionable", Created = new DateTime(2012, 9, 19), Category = Standard },
                new Post { ID = 3, Title = @"Soccer ball", Body = @"FIFA-approved size and weight", Created = new DateTime(2012, 9, 18), Category = Update },
                new Post { ID = 4, Title = @"Corner flags", Body = @"Give your playing field that professional touch", Created = new DateTime(2012, 9, 20), Category = Kurs },
                new Post { ID = 5, Title = @"Stadium", Body = @"Flat-packed 35,000 seat stadium", Created = new DateTime(2012, 9, 21), Category = Update },
                new Post { ID = 6, Title = @"Thinking cap", Body = @"Improve your brain efficiency by 75%", Created = new DateTime(2012, 9, 19), Category = News },
                new Post { ID = 7, Title = @"Unsteady chair", Body = @"Secretly give your opponent a disadvantage", Created = new DateTime(2012, 9, 21), Category = Kurs },
                new Post { ID = 8, Title = @"Human Chess Board", Body = @"A fun game for the whole family", Created = new DateTime(2012, 9, 23), Category = News },
                new Post { ID = 9, Title = @"Bling-bling King", Body = @"Gold-plated, diamond-studded King", Created = new DateTime(2012, 9, 25), Category = Standard },

            };
            posts.ForEach(s => context.Posts.Add(s));
            context.SaveChanges();

            var accounts = new List<Account>
            {
                // All passwords are "password" without the quotes.
                new Account { ID = 1, Email = "admin@varldsklass.com", FirstName = "Admin", LastName = "von Världsklass", Password = "$2a$10$0YOweSok2GRqb0r.AqHb9eR8BKcUEJUEdoabynZoj05R3dM0onEYK", Salt = "$2a$10$0YOweSok2GRqb0r.AqHb9e", Administrator = true, CreatedDate = DateTime.Now },
                new Account { ID = 2, Email = "bokare@varldsklass.com", FirstName = "Bokare", LastName = "von Världsklass", Password = "$2a$10$0YOweSok2GRqb0r.AqHb9eR8BKcUEJUEdoabynZoj05R3dM0onEYK", Salt = "$2a$10$0YOweSok2GRqb0r.AqHb9e", Administrator = false, CreatedDate = DateTime.Now }
            };

            accounts.ForEach(a => context.Accounts.Add(a));
            context.SaveChanges();
        }

        
    }
}