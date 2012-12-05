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
                new Category { ID = 1, Name = "HLR", Created = new DateTime(2012, 9, 18) },
                new Category { ID = 2, Name = "Lift", Created = new DateTime(2012, 9, 18) },
                new Category { ID = 3, Name = "Mat", Created = new DateTime(2012, 9, 18) },
                new Category { ID = 4, Name = "Lärare", Created = new DateTime(2012, 9, 18) }
            };

            categories.ForEach(s => context.Categories.Add(s));
            context.SaveChanges();

            var post1 = new Post { ID = 1, Title = @"Liftutbildningar", Body = @"Lorum ipsum", Created = new DateTime(2012, 9, 18), Category = categories, postType = 0 };

            List<Event> events = new List<Event> {
                new Event { ID = 1, Title = @"Seminarie 1", Teatcher = @"Lisa Svensson", Created = new DateTime(2012, 9, 18), StartDate = new DateTime(2012, 8, 22), EndDate = new DateTime(2012, 8, 25),Body = "Kursen startar kl 14", City = "Göteborg", Post = post1 },
                new Event { ID = 2, Title = @"Olivmagi med Oliver", Teatcher = @"Pay Cartea", Created = new DateTime(2012, 10, 3), StartDate = new DateTime(2012, 10, 25), EndDate = new DateTime(2012, 11, 7),Body = "Kursen startar när alla är på plats", City = "Hisingen", Post = post1 },
                new Event { ID = 3, Title = @"How to be bad", Teatcher = @"Glass Joe", Created = new DateTime(2012, 9, 18), StartDate = new DateTime(2013, 2, 12), EndDate = new DateTime(2013, 2, 16),Body = "Kursen startar kl 13.50", City = "Bronx", Post = post1 }
            };

            events.ForEach(s => context.Events.Add(s));
            context.SaveChanges();

            var posts = new List<Post>
            {
                new Post { ID = 2, Title = @"HlrUtbildningar", Body = @"Protective and fashionable", Created = new DateTime(2012, 9, 19), Category = categories, postType = 0, Events = new List<Event>() },
                new Post { ID = 3, Title = @"Matutbildningar", Body = @"FIFA-approved size and weight", Created = new DateTime(2012, 9, 18), Category = categories, postType = 0, Events = new List<Event>() },
                new Post { ID = 4, Title = @"Lärareutbildningar", Body = @"Give your playing field that professional touch", Created = new DateTime(2012, 9, 20), Category = categories, postType = 0, Events = new List<Event>() },
                new Post { ID = 5, Title = @"Liftutbildningar", Body = @"Flat-packed 35,000 seat stadium", Created = new DateTime(2012, 9, 21), Category = categories, postType = 0, Events = new List<Event>() },
                new Post { ID = 6, Title = @"HlrUtbildningar", Body = @"Improve your brain efficiency by 75%", Created = new DateTime(2012, 9, 19), Category = categories, postType = 0, Events = new List<Event>() },
                new Post { ID = 7, Title = @"Matutbildningar", Body = @"Secretly give your opponent a disadvantage", Created = new DateTime(2012, 9, 21), Category = categories, postType = 0, Events = new List<Event>() },
                new Post { ID = 8, Title = @"Lärareutbildningar", Body = @"A fun game for the whole family", Created = new DateTime(2012, 9, 23), Category = categories, postType = 0, Events = new List<Event>() },
                new Post { ID = 9, Title = @"Om Oss", Body = @"<h4>Om världsklass</h4><p>Oavsett om du vill gå en utbildning som privatperson, egenföretagare eller anställd så har vi lösningen som passar just dig och dina behov.</p><p>Våra kursdeltagare är glada kursdeltagare!</p><p>Vi gör alltid allt vi kan för att du som kursdeltagare ska bli så nöjd som möjligt, när andra utbildare gör en  'bra' utbildning så vill vi dra det ännu längre, ja ända upp till världsklass helt enkelt!</p>
                <p>På de flesta av våra utbildningar så finns möjligheten att gå med på en 'Öppen utbildning' om det inte finns någon datum som passar så skicka in en förfråga om kommande utbildningstillfällen, eller om ni vill ha en egen utbildning som är skräddarsydd efter er verksamhet så fixar vi det!</p> <p>Har du funderingar så hör gärna av dig till oss, dina åsikter är viktiga för oss! </p><p>VÄRLDSKLASS.COM</p><p>Telefon: 0320 - 109 39</p>", Created = new DateTime(2012, 9, 25), postType = 1, Events = null },
                new Post { ID = 10, Title = @"Frågor och svar", Body = @"Några frågor", Created = new DateTime(2012, 9, 25), postType = 1, Events = null },

            };
            //posts.ForEach(s => context.Posts.Add(s));
            foreach (var post in posts.ToList())
            {
                //post.Events.Add(firstEvent);
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
                new Account { ID = 2, Email = "bokare@varldsklass.com", FirstName = "Bokare", LastName = "von Världsklass", Password = "$2a$10$0YOweSok2GRqb0r.AqHb9eR8BKcUEJUEdoabynZoj05R3dM0onEYK", Salt = "$2a$10$0YOweSok2GRqb0r.AqHb9e", Administrator = false, CreatedDate = DateTime.Now },
                new Account { ID = 3, Email = "johan@varldsklass.com", FirstName = "Johan", LastName = "Axéll", Password = "$2a$10$.tMzg8Rnef.IllIleQN82uBYgRjtilGPigPAToVr9dRf31orUKnoS", Salt = "$2a$10$.tMzg8Rnef.IllIleQN82u", Administrator = true, CreatedDate = DateTime.Now }
            };

            accounts.ForEach(a => context.Accounts.Add(a));
            context.SaveChanges();

            var question = new List<Question>();
            question.ForEach(q => context.Questions.Add(q));

            var attendants = new List<Attendant>
            {
                new Attendant { ID = 1, FirstName="Oliver", LastName="Cartea", Email="paycartea@gmail.com", EventID=1, BookerID=2},
                new Attendant { ID = 2, FirstName="Barbra", LastName="Streisand", Email="ouououooo@barbrastreisand.com", EventID=1, BookerID=2}
            };

            attendants.ForEach(a => context.Attendants.Add(a));
            context.SaveChanges();

            var evaluations = new List<Question>
            {
                new Question { ID = 1, EventID = 1, Food = 5, Teacher = 5, Location = 5, Overall = 5, Opinion = "Johan är bäst i världen! Världsklass!" },
                new Question { ID = 2, EventID = 1, Food = 4, Teacher = 3, Location = 4, Overall = 0, Opinion = "ja har inga synpnukter............." },
                new Question { ID = 3, EventID = 1, Food = 0, Teacher = 1, Location = 1, Overall = 1, Opinion = "Jag ville verkligen bara gå." },
                new Question { ID = 4, EventID = 1, Food = 4, Teacher = 4, Location = 4, Overall = 2, Opinion = "" },
                new Question { ID = 5, EventID = 2, Food = 5, Teacher = 0, Location = 0, Overall = 0, Opinion = "Hängde inte riktigt med på undervisningen, men maten var mycket bra." },
                new Question { ID = 6, EventID = 2, Food = 2, Teacher = 5, Location = 5, Overall = 5, Opinion = "" },
                new Question { ID = 7, EventID = 2, Food = 2, Teacher = 5, Location = 5, Overall = 5, Opinion = "" },
                new Question { ID = 8, EventID = 3, Food = 0, Teacher = 0, Location = 0, Overall = 5, Opinion = "" },
            };

            evaluations.ForEach(e => context.Questions.Add(e));
            context.SaveChanges();


            // Make attendant email-addresses unique
            context.Database.ExecuteSqlCommand("ALTER TABLE dbo.Attendants ADD CONSTRAINT EventEmail UNIQUE NONCLUSTERED ( EventID, Email )");

            var popularCourses = new List<PopularCourse> {
                new PopularCourse { ID = 1, CourseOne = 1, CourseTwo = 2, CourseThree = 3, CourseFour = 4 }
            };

            popularCourses.ForEach(p => context.PopularCourses.Add(p));
            context.SaveChanges();
        }
    }
}
