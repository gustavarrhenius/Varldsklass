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

        public ActionResult CourseInfo(int id)
        {
            var posts = _postRepo.FindAll().Where(p => p.ID == id).Include(p => p.Events).FirstOrDefault();

            return View(posts);
        }

        public ActionResult Page(int id)
        {
            var post = _postRepo.FindByID(id);

            return View(post);
        }
        public ActionResult Pages()
        {
            var posts = _postRepo.FindAll().Where(p => p.postType == (int)Post.PostType.Page).ToList();

            return View(posts);
        }

        public ActionResult Sidebar(int id, string title)
        {
            SideBarViewModel sb = new SideBarViewModel();
            if (title == "kategori") {
                if (id == 0)
                {
                    sb.Categories = _categoryRepo.FindAll().ToList();
                }
                else
                {
                    sb.Category = _categoryRepo.FindAll().Where(c => c.ID == id).Include(p => p.Posts).FirstOrDefault();
                }
            } else {
                sb.Post = _postRepo.FindAll().Where(c => c.ID == id).Include(e => e.Events).FirstOrDefault();
            }
            return PartialView("_SidebarPartialView", sb);
        }

        public ActionResult AddPage(int id = 0)
        {
            Post post;
            if (id == 0) {
                 post = new Post();
                 post.Created = DateTime.Now;
                 post.postType = (int)Post.PostType.Page;
            } else {
                 post = _postRepo.FindByID(id);
            }
            
            return View(post);
        }

        [HttpPost]
        public ActionResult SavePage(Post Post)
        {
            if (ModelState.IsValid)
            {
                _postRepo.Save(Post);
                // add a message to the viewbag
                TempData["message"] = string.Format("{0} är sparad", Post.Title);
                // return the user to the list
                return RedirectToAction("Pages", "Post");
            }
            else
            {
                // there is something wrong with the data values
                return View("AddPage", Post);
            }
        }

        public ActionResult DeletePage(int id)
        {
            var post = _postRepo.FindByID(id);
            _postRepo.Delete(post);
            TempData["message"] = string.Format("{0} har tagits bort", post.Title);
            return RedirectToAction("Pages");
        }
 

        /*-----------------------------------------
        * Category Controller 
        * -------------------------------------*/
        public ActionResult EditCategory(int id = 0)
        {
            if (id != 0)
            {
                return View("AddCategory", _categoryRepo.FindByID(id));
            }
            else
            {
                return View("AddCategory", new Category());
            }
        }

    }
}
