using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Varldsklass.Domain.Repositories;
using Varldsklass.Domain.Entities;
using Varldsklass.Web.ViewModels;

namespace Varldsklass.Web.Controllers
{
    public class PostController : Controller
    {
        private IPostRepository _postRepo;
        public PostController(IPostRepository postRepo)
        {
            _postRepo = postRepo;
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

        public ActionResult ListKurser()
        {
            PostIndexViewModel filteredPosts = new PostIndexViewModel
            {
                Posts = _postRepo.FindPostsByCategoryName("Kurs").ToList()
            };
            return View(filteredPosts);
        }

    }
}
