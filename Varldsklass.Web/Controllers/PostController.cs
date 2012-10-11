using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Varldsklass.Domain.Repositories;
using Varldsklass.Domain.Repositories.Abstract;
using Varldsklass.Domain.Entities;
using Varldsklass.Web.ViewModels;
using System.Data.Entity;

namespace Varldsklass.Web.Controllers
{
    public class PostController : Controller
    {
        private IRepository<Event> _eventRepo;
        private IRepository<Post> _postRepo;
        private IRepository<Category> _categoryRepo;

        public PostController(IRepository<Post> repo, IRepository<Category> category, IRepository<Event> Event)
        {
            _eventRepo = Event;
            _postRepo = repo;
            _categoryRepo = category;
        }

        //
        // GET: /Product/

        public ActionResult Index()
        {
            PostIndexViewModel posts = new PostIndexViewModel
            {
                Posts = _postRepo.FindAll().ToList()
            };

            return View(posts);
        }

        public ActionResult ListCourses()
        {
            List<Post> posts = _postRepo.FindAll().Where(p => p.postType == (int)Post.PostType.Course).ToList();
            return View(posts);
        }

        public ActionResult CourseInfo(int id)
        {
            var posts = _postRepo.FindAll().Where(p => p.ID == id).Include(p => p.Events).FirstOrDefault();

            return View(posts);
        }
    }
}
