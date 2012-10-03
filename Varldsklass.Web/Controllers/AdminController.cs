using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Varldsklass.Web.Models;
using Varldsklass.Domain.Entities;
using Varldsklass.Domain.Repositories;
using Varldsklass.Domain.Repositories.Abstract;



namespace Varldsklass.Web.Controllers
{

    public class AdminController : Controller
    {

        private PostRepository _postRepo;
        private IRepository<Category> _categoryRepo;

        public AdminController(PostRepository repo, IRepository<Category> category)
        {
            _postRepo = repo;
            _categoryRepo = category;
        }
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Courses()
        {
            return View();
        }

        public ActionResult AddCourse()
        {
            Post post = new Post();
            post.Category = _categoryRepo.FindByID(4);
            return View("AddCourse", post);
        }

        public ActionResult CourseList() 
        {
            List<Post> posts = _postRepo.FindPostsByCategoryID(4).ToList();
            return View();    
        }

        [HttpPost]
        public ActionResult SavePost(Post post)
        {
            if (ModelState.IsValid)
            {
                post.Category = _categoryRepo.FindByID(post.Category.ID);
                _postRepo.SavePost(post);
                // add a message to the viewbag
                TempData["message"] = string.Format("{0} has been saved", post.Title);
                // return the user to the list
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(post);
            }
        }

        [HttpGet]
        public ActionResult News()
        {
            return View();
        }

        [HttpPost]
        public ActionResult News(News news)
        {
            news = new News();

            string headline = news.Headline;
            string body = "<h3>" + news.Ingress + "</h3>\n<p>" + news.Text + "</p>";

            if (ModelState.IsValid)
            {
                // redirect to NewsSave.cshtml
                return View("NewsSaved", news);
            }
            else
            {
                // there is a validation error - redisplay the form
                return View();
            }
        }

        

        public ActionResult NewsLetter()
        {
            return View();
        }

        public ActionResult Customers()
        {
            return View();
        }
    }
}
