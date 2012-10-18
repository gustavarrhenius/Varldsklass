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
    public class EFDBInitializer : DropCreateDatabaseAlways<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            var categories = new List<Category>
            {
            new Category { ID = 1, Name = "HLR" },
            new Category { ID = 2, Name = "Lift" },
            new Category { ID = 3, Name = "Mat" },
            new Category { ID = 4, Name = "Lärare" }
            };

            categories.ForEach(s => context.Categories.Add(s));
            context.SaveChanges();

            var post1 = new Post { ID = 1, Title = @"Liftutblidningar", Body = @"Lorum ipsum", Created = new DateTime(2012, 9, 18), Category = categories, postType = 0 };

            var firstEvent = new Event { ID = 1, Title = @"Semenarie 1", Teatcher = @"Lisa Svensson", Created = new DateTime(2012, 9, 18), StartDate = new DateTime(2012, 9, 22), EndDate = new DateTime(2012, 9, 24),Body = "Kursen startar kl 14", City = "Göteborg", Post = post1 };
            var events = new List<Event>
            {
                firstEvent,
            };

            events.ForEach(s => context.Events.Add(s));
            context.SaveChanges();

            var posts = new List<Post>
            {
                new Post { ID = 2, Title = @"HlrUtblidningar", Body = @"Protective and fashionable", Created = new DateTime(2012, 9, 19), Category = categories, postType = 0, Events = new List<Event>() },
                new Post { ID = 3, Title = @"Matutblidningar", Body = @"FIFA-approved size and weight", Created = new DateTime(2012, 9, 18), Category = categories, postType = 0, Events = new List<Event>() },
                new Post { ID = 4, Title = @"Lärareutblidningar", Body = @"Give your playing field that professional touch", Created = new DateTime(2012, 9, 20), Category = categories, postType = 0, Events = new List<Event>() },
                new Post { ID = 5, Title = @"Liftutblidningar", Body = @"Flat-packed 35,000 seat stadium", Created = new DateTime(2012, 9, 21), Category = categories, postType = 0, Events = new List<Event>() },
                new Post { ID = 6, Title = @"HlrUtblidningar", Body = @"Improve your brain efficiency by 75%", Created = new DateTime(2012, 9, 19), Category = categories, postType = 0, Events = new List<Event>() },
                new Post { ID = 7, Title = @"Matutblidningar", Body = @"Secretly give your opponent a disadvantage", Created = new DateTime(2012, 9, 21), Category = categories, postType = 0, Events = new List<Event>() },
                new Post { ID = 8, Title = @"Lärareutblidningar", Body = @"A fun game for the whole family", Created = new DateTime(2012, 9, 23), Category = categories, postType = 0, Events = new List<Event>() },
                new Post { ID = 9, Title = @"Frågor och svar", Body = @"Några frågor", Created = new DateTime(2012, 9, 25), postType = 1, Events = new List<Event>() },

            };
            //posts.ForEach(s => context.Posts.Add(s));
            foreach (var post in posts.ToList())
            {
                post.Events.Add(firstEvent);
                context.Posts.Add(post);
            }
            context.SaveChanges();

            var locations = new List<Location>
            {
                new Location { ID = 1, City = @"Borås", Address = @"Albanoliden 5", Zip = @"506 30" },
                new Location { ID = 2, City = @"Göteborg", Address = @"Kungsportsavenyn 10", Zip =  @"410 00" },
                new Location { ID = 3, City = @"Kinna", Address = @"Lövåsgatan 11", Zip = @"511 54" },
                new Location { ID = 4, City = @"Kungsbacka", Address = @"Energigatan 10", Zip = @"434 37" },
            };
            locations.ForEach(s => context.Locations.Add(s));
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
