using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Varldsklass.Web.Models;
using Varldsklass.Domain.Entities;
using Varldsklass.Domain.Repositories;
using Varldsklass.Domain.Repositories.Abstract;
using System.Data.Entity;

namespace Varldsklass.Web.Controllers
{
    public class CourseController : Controller
    {
        private IRepository<Event> _eventRepo;
        private IRepository<Post> _postRepo;
        private IRepository<Category> _categoryRepo;

        public CourseController(IRepository<Post> repo, IRepository<Category> category, IRepository<Event> Event)
        {
            _eventRepo = Event;
            _postRepo = repo;
            _categoryRepo = category;
        }


        public ActionResult Index()
        {
            AddCourseViewModel vm = new AddCourseViewModel();
            vm.Posts = _postRepo.FindAll().Where(p => p.postType == (int)Post.PostType.Course).ToList();
            vm.Categories = _categoryRepo.FindAll().ToList();
            return View(vm);
        }

        /*-----------------------------------------
         * Course Controller 
         * -------------------------------------*/
        public ActionResult Course(int id)
        {
            var posts = _postRepo.FindAll();
            return View(posts.Where(p => p.ID == id).Include(p => p.Events).FirstOrDefault());
        }

        public ActionResult EditCourse(int id = 0)
        {
            if (id != 0)
            {
                AddCourseViewModel vm = new AddCourseViewModel();
                vm.Post = _postRepo.FindByID(id);
                vm.Categories = _categoryRepo.FindAll().ToList();
                return View("AddCourse", vm);
            }
            else
            {
                AddCourseViewModel vm = new AddCourseViewModel();
                vm.Post = new Post();
                vm.Post.postType = (int)Post.PostType.Course;
                vm.Categories = _categoryRepo.FindAll().ToList();
                return View("AddCourse", vm);
            }
        }

        public ActionResult CourseList()
        {
            List<Post> posts = _postRepo.FindAll().Where(p => p.postType == (int)Post.PostType.Course).ToList();
            return View(posts);
        }

        public ActionResult CourseInfo(int id)
        {
            var info = _postRepo.FindByID(id);
            return View(info);
        }

        [HttpPost]
        public ActionResult SaveCourse(Post post)
        {
            if (ModelState.IsValid)
            {
                 
                _postRepo.Save(post);
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

        public ActionResult DeleteCourse(int id)
        {
            _postRepo.Delete(_postRepo.FindByID(id));
            return RedirectToAction("Index");
        }


        /*-----------------------------------------
        * Event Controller 
        * -------------------------------------*/
        public ActionResult AddEvent(int id)
        {
            Event Event = new Event();
            Event.Post = _postRepo.FindByID(id);

            return View("AddEvent", Event);
        }

        [HttpPost]
        public ActionResult SaveEvent(Event Event)
        {
            if (ModelState.IsValid)
            {
                var post = _postRepo.FindByID(Event.Post.ID);
                post.Events.Add(Event);
                Event.Post = post;
                Event.PostID = post.ID;
                _eventRepo.Save(Event);
                // add a message to the viewbag
                TempData["message"] = string.Format("{0} has been saved", Event.Title);
                // return the user to the list
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(Event);
            }
        }
        public ActionResult DeleteEvent(int id)
        {
            _eventRepo.Delete(_eventRepo.FindByID(id));
            return RedirectToAction("Index");
        }


        /*-----------------------------------------
        * Category Controller 
        * -------------------------------------*/
        public ActionResult EditCategory(int id = 0)
        {
            if (id != 0) {
                return View("AddCategory", _categoryRepo.FindByID(id));
            } else {
                return View("AddCategory", new Category());
            }
        }

        [HttpPost]
        public ActionResult SaveCategory(Category Category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepo.Save(Category);
                // add a message to the viewbag
                TempData["message"] = string.Format("{0} has been saved", Category.Name);
                // return the user to the list
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(Category);
            }
        }

        public ActionResult DeleteCategory(int id)
        {
            _categoryRepo.Delete(_categoryRepo.FindByID(id));
            return RedirectToAction("Index");
        }

    }
}
