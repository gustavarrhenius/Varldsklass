using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Varldsklass.Domain.Repositories;
using Varldsklass.Domain.Entities;
using Varldsklass.Domain.Contexts;
using System.Configuration;
using DotNetOpenAuth.OAuth2;
using DotNetOpenAuth.ApplicationBlock;
using DotNetOpenAuth.ApplicationBlock.Facebook;
using System.Net;
using System.IO;
using Varldsklass.Web.ViewModels;
using Varldsklass.Domain.Repositories.Abstract;
using System.Data.Entity;

namespace Varldsklass.Web.Controllers
{
    public class HomeController : Controller
    {

        private IRepository<Event> _eventRepo;
        private IRepository<Post> _postRepo;
        private IRepository<Category> _categoryRepo;
        private IRepository<PopularCourse> _popularCoursesRepo;

        public HomeController(IRepository<Post> repo, IRepository<Category> category, IRepository<Event> Event, IRepository<PopularCourse> popularCourses)
        {
            _eventRepo = Event;
            _postRepo = repo;
            _categoryRepo = category;
            _popularCoursesRepo = popularCourses;
        }

        public ActionResult Index()
        {
        // Facebook feed
            var facebookAccessToken = ConfigurationManager.AppSettings["facebookAccessToken"];
            FacebookGraph.FacebookPageFeed graph = new FacebookGraph.FacebookPageFeed();
            if (null != facebookAccessToken)
                {
                var request = WebRequest.Create(
                    string.Format(@"https://graph.facebook.com/varldsklass?fields=feed&access_token={0}",
                    Uri.EscapeDataString(facebookAccessToken)));
                using (var response = request.GetResponse())
                    {
                    using (var responseStream = response.GetResponseStream())
                        {
                        graph = DotNetOpenAuth.ApplicationBlock.Facebook.FacebookGraph.FacebookPageFeed.Deserialize(responseStream);

                        }
                    }
            }
        return View(graph.PageFeed.Posts);
        }

        public ActionResult Search(string search) 
        {   
            List<SearchResult> searchResult = new List<SearchResult>();

            if (!string.IsNullOrEmpty(search))
            {
               
                var posts = _postRepo.FindAll().Where(p => p.Title.Contains(search.ToLower())|| p.Body.Contains(search.ToLower())).ToList();
                var events = _eventRepo.FindAll().Where(e => e.Title.Contains(search.ToLower())|| e.Body.Contains(search.ToLower()) || e.Teatcher.Contains(search.ToLower()) || e.City.Contains(search.ToLower())).ToList();
                foreach (var post in posts)
                    {
                    SearchResult searchTest = new SearchResult();
                    searchTest.Title = post.Title;
                    searchTest.Excerpt = post.Body;
                    searchTest.Url = "/Course/CourseSingle/" + post.ID; 
                    searchResult.Add(searchTest);
                    }
                foreach (var Event in events)
                    {
                    SearchResult searchTest = new SearchResult();
                    searchTest.Title = Event.Title;
                    searchTest.Excerpt = Event.Body;
                    searchTest.StartDate = Event.StartDate;
                    searchTest.EndDate = Event.EndDate;
                    searchTest.Url = "/Course/EventSingle/" + Event.ID; 
                    searchResult.Add(searchTest);
                    }
            }
            return View(searchResult);
        }

        public ActionResult About()
        {
            // Generiskt Repository - Här skapas ett repository för Category
            // Repositoryt kräver typer som implementerar IEntity
            Repository<Category> categoryRepo = new Repository<Category>();

            // Samtliga metoder som finns med i det generiska repositoriet
            var categories = categoryRepo.FindAll();

            var filteredCategories = categoryRepo.FindAll(c => c.Name.Contains("sport"));

            var category = categoryRepo.FindByID(1);

            category.Name = "New Name!";
            categoryRepo.Save(category);

            categoryRepo.Delete(category);


            var products = _postRepo.FindAll(); // + övriga "grund"-metoder

            // Metoder implementerade i ProductRepository:
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact(int id)
        {
            var listOfLocations = new Repository<Location>().FindAll().ToList();

            var location = listOfLocations.Where(l => l.ID == id).ToList();

            return View(location);
        }

        public ActionResult Menu()
        {
            MenuViewModel MenuModel = new MenuViewModel()
            {
                Categories = new Repository<Category>().FindAll().ToList(), 
                Locations = new Repository<Location>().FindAll().OrderBy(l => l.City).ToList(),
                Pages = new Repository<Post>().FindAll().Where(p => p.postType == (int)Post.PostType.Page).ToList()
            };
            
            return PartialView(MenuModel);
        }

        public ActionResult Calendar()
        {
            var calendar = _eventRepo.FindAll().Where(d => d.StartDate > DateTime.Now).Include(p => p.Post).Take(5).ToList();

            return PartialView(calendar);
        }

        public ActionResult Popular()
        {
            PopularCoursesViewModel pVM = new PopularCoursesViewModel();
            pVM.Posts = new List<Post>();
            var pc = _popularCoursesRepo.FindAll().FirstOrDefault();
            if (pc.CourseOne != null) {
                var post = _postRepo.FindAll().Where(p => p.ID == pc.CourseOne).Include(i => i.Images).FirstOrDefault();
                pVM.Posts.Add(post);
            } if (pc.CourseTwo != null)
            {
                pVM.Posts.Add(_postRepo.FindAll().Where(p => p.ID == pc.CourseTwo).Include(i => i.Images).FirstOrDefault());
            } if (pc.CourseThree != null)
            {
                pVM.Posts.Add(_postRepo.FindAll().Where(p => p.ID == pc.CourseThree).Include(i => i.Images).FirstOrDefault());
            } if (pc.CourseFour != null)
            {
                pVM.Posts.Add(_postRepo.FindAll().Where(p => p.ID == pc.CourseFour).Include(i => i.Images).FirstOrDefault());
            }
            return PartialView(pVM);
        }
    }
}
