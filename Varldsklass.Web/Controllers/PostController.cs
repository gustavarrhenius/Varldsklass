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
        private IEventRepository _eventRepo;
        private IRepository<Post> _postRepo;
        private IRepository<Category> _categoryRepo;
        private IRepository<Image> _imgRepo;

        public PostController(IRepository<Post> repo, IRepository<Category>
        category, IEventRepository Event, IRepository<Image> image)
        {
            _eventRepo = Event;
            _postRepo = repo; _postRepo.Model = _eventRepo.Model;
            _categoryRepo = category; _categoryRepo.Model = _eventRepo.Model;
            _imgRepo = image; _imgRepo.Model = _eventRepo.Model;
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
                    sb.Categories = _categoryRepo.FindAll().ToList();
                    sb.Category = _categoryRepo.FindAll().Where(c => c.ID == id).Include(p => p.Posts).FirstOrDefault();
                }
            } else {
                sb.Post = _postRepo.FindAll().Where(c => c.ID == id).Include(e => e.Events.Where(d=>d.StartDate < DateTime.Now)).FirstOrDefault();
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
                post = _postRepo.FindAll().Where(p => p.ID == id).Include(i => i.Images).FirstOrDefault();
            }
            
            return View(post);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SavePage(Post Post, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                var theOldPost = _postRepo.FindByID(Post.ID);
                if (theOldPost == null)
                    theOldPost = Post;
                if (theOldPost.Images == null)
                    theOldPost.Images = new List<Image>();
                var oldImages = theOldPost.Images.ToList();
                var listOfImagesPaths = form["image"];
                string[] arrayOfImagesPaths = null;
                if (listOfImagesPaths != null)
                    arrayOfImagesPaths = listOfImagesPaths.Split(',');

                oldImages = theOldPost.Images.ToList();
                if (arrayOfImagesPaths != null && arrayOfImagesPaths.Count() > 0)
                {
                    foreach (var addedimg in _imgRepo.FindAll(c =>
                arrayOfImagesPaths.Any(cat => cat == c.ImagePath.ToString()) &&
                !c.Posts.Any(p => p.ID == theOldPost.ID)).ToList())
                        theOldPost.Images.Add(addedimg);
                }
                foreach (var removedimg in oldImages.Where(c => arrayOfImagesPaths
                == null || !arrayOfImagesPaths.Any(cat2 => cat2 == c.ImagePath.ToString())))
                    theOldPost.Images.Remove(removedimg); 

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
