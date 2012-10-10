using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Varldsklass.Domain.Repositories;
using Varldsklass.Domain.Repositories.Abstract;
using Varldsklass.Domain.Entities;
using Varldsklass.Web.ViewModels;

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

       /* public ActionResult ListCourses()
        {
            PostIndexViewModel filteredPosts = new PostIndexViewModel
            {
                Posts = _postRepo.FindPostsByCategoryName("Course").ToList()
            };
            return View(filteredPosts);
        }

        public ActionResult News()
        {
            PostIndexViewModel filteredPosts = new PostIndexViewModel
            {
                Posts = _postRepo.FindPostsByCategoryName("News").ToList()
            };
            return View(filteredPosts);
        }*/

    }
}
