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
                AddCourseViewModel vmCategory = new AddCourseViewModel();
                vmCategory.Post = _postRepo.FindByID(id);
                vmCategory.Categories = vmCategory.Post.Category.ToList();
                ViewData["events"] = new SelectList(_categoryRepo.FindAll().ToList(), "ID", "Name");
                return View("AddCourse", vmCategory);
            }
            else
            {
                AddCourseViewModel vmCategory = new AddCourseViewModel();

                vmCategory.Post = new Post();
                vmCategory.Post.Created = DateTime.Now;
                vmCategory.Post.postType = (int)Post.PostType.Course;
                ViewData["events"] = new SelectList(_categoryRepo.FindAll().ToList(), "ID", "Name");
                return View("AddCourse", vmCategory);
            }
        }

        public ActionResult CourseInfo(int id)
        {
            var info = _postRepo.FindByID(id);
            return View(info);
        }

        /* ---- Front View ---- */
        public ActionResult CourseSingle(int id)
        {
            var posts = _postRepo.FindAll().Where(p => p.ID == id).Include(p => p.Events).FirstOrDefault();

            return View(posts);
        }

        [HttpPost]
        public ActionResult SaveCourse(Post post, FormCollection postedForm)
        {
            
            if (ModelState.IsValid)
            {
                var listOfCategoryIDs = postedForm["name"];

                var arrayOfCategoryIDs = listOfCategoryIDs.Split(',');
                 if (arrayOfCategoryIDs.Count() > 0){
                     post.Category = new List<Category>();
                     foreach (var array in arrayOfCategoryIDs) {
                         int x = Convert.ToInt32(array);
                         var cat = _categoryRepo.FindByID(x);
                         cat.Posts.Add(post);
                         _categoryRepo.Save(cat);
                     }
                 }
                _postRepo.Save(post);
                // add a message to the viewbag
                TempData["message"] = string.Format("{0} has been saved", post.Title);
                // return the user to the list
                return RedirectToAction("Index");
            }
            else
            {
                AddCourseViewModel vmCategory = new AddCourseViewModel();
                vmCategory.Post = post;
                if (post.Category != null) {
                    vmCategory.Categories = post.Category.ToList();
                }
                ViewData["events"] = new SelectList(_categoryRepo.FindAll().ToList(), "ID", "Name");
                // there is something wrong with the data values
                return View("AddCourse", vmCategory);
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
        public ActionResult AddEvent(int id = 0)
        {
            ModelState.Clear();
            Event Event = new Event();
            Event.PostID = id;
            Event.Created = DateTime.Now;
            Event.StartDate = DateTime.Now;
            Event.EndDate = DateTime.Now;
            return View("AddEvent", Event);
        }

        public ActionResult EditEvent(int id)
        {

            var Event = _eventRepo.FindByID(id);
            return View("AddEvent", Event);
        }

        [HttpPost]
        public ActionResult SaveEvent(Event Event)
        {
            if (ModelState.IsValid)
            {
                _eventRepo.Save(Event);
                // add a message to the viewbag
                TempData["message"] = string.Format("{0} has been saved", Event.Title);
                // return the user to the list
                return RedirectToAction("Course", new { id = Event.PostID });
            }
            else
            {
                // there is something wrong with the data values
                return View("AddEvent", Event);
            }
        }

        public ActionResult DeleteEvent(int id, int course)
        {
            _eventRepo.Delete(_eventRepo.FindByID(id));
            return RedirectToAction("Course", new { id = course });
        }

        /* ---- Front View ---- */
        public ActionResult EventSingle(int id)
        {
            var Event = _eventRepo.FindAll().Where(p => p.ID == id).Include(p => p.Post).FirstOrDefault();

            return View(Event);
        }


        /*-----------------------------------------
        * Category Controller 
        * -------------------------------------*/
        public ActionResult EditCategory(int id = 0)
        {
            if (id != 0) {
                return View("AddCategory", _categoryRepo.FindByID(id));
            } else {
                Category category = new Category();
                category.Created = DateTime.Now;
                return View("AddCategory", category);
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
                return View("AddCategory", Category);
            }
        }

        public ActionResult DeleteCategory(int id)
        {
            _categoryRepo.Delete(_categoryRepo.FindByID(id));
            return RedirectToAction("Index");
        }

        /* ---- Front View ---- */
        public ActionResult Category(int id)
        {
            Category categories = _categoryRepo.FindAll().Where(c => c.ID == id).Include(p => p.Posts).FirstOrDefault();
            return View(categories);
        }

        /* ---- Front View ---- */
        public ActionResult CategoryList()
        {
            List<Category> categories = _categoryRepo.FindAll().ToList();

            return View(categories);
        }

    }
}
