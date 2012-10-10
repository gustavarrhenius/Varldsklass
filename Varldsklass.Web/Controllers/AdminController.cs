using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Varldsklass.Web.Models;
using Varldsklass.Domain.Entities;
using Varldsklass.Domain.Repositories;
using Varldsklass.Domain.Repositories.Abstract;
using Varldsklass.Web.Infrastructure;



namespace Varldsklass.Web.Controllers
{

    public class AdminController : Controller
    {

        private PostRepository _postRepo;
        private IRepository<Category> _categoryRepo;
        private IAccountRepository _accountRepo;

        public AdminController(PostRepository repo, IRepository<Category> category, IAccountRepository account)
        {
            _postRepo = repo;
            _categoryRepo = category;
            _accountRepo = account;
        }
        //
        // GET: /Admin/

        private bool NotAllowedHere() {
            string email = User.Identity.Name;
            return (_accountRepo.FindAll(u => u.Email == email && u.Administrator).Count() < 1);
        }

        private ActionResult RedirectAway()
        {
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Index()
        {
            if (NotAllowedHere()) return RedirectAway();

            return View();
        }

        [Authorize]
        public ActionResult Courses()
        {
            if (NotAllowedHere()) return RedirectAway();

            return View();
        }

        [Authorize]
        public ActionResult AddCourse()
        {
            if (NotAllowedHere()) return RedirectAway();

            Post post = new Post();
            post.Category = _categoryRepo.FindByID(4);
            return View("AddCourse", post);
        }

        [Authorize]
        public ActionResult CourseList() 
        {
            if (NotAllowedHere()) return RedirectAway();

            List<Post> posts = _postRepo.FindPostsByCategoryID(4).ToList();
            return View(posts);    
        }

        [Authorize]
        [HttpPost]
        public ActionResult SavePost(Post post)
        {
            if (NotAllowedHere()) return RedirectAway();

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

        [Authorize]
        [HttpGet]
        public ActionResult News()
        {
            if (NotAllowedHere()) return RedirectAway();

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult News(News news)
        {
            if (NotAllowedHere()) return RedirectAway();

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

        [Authorize]
        public ActionResult NewsLetter()
        {
            if (NotAllowedHere()) return RedirectAway();

            return View();
        }

        [Authorize]
        public ActionResult Customers()
        {
            if (NotAllowedHere()) return RedirectAway();

            return View();
        }
    }
}
