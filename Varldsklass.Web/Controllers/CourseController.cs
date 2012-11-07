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
using Varldsklass.Web.ViewModels;

namespace Varldsklass.Web.Controllers
{
    public class CourseController : Controller
    {
        private IRepository<Event> _eventRepo;
        private IRepository<Post> _postRepo;
        private IRepository<Category> _categoryRepo;
        private IRepository<Attendant> _attendantRepo;
        private IAccountRepository _accountRepo;
        private IRepository<Image> _imgRepo;

        public CourseController(IRepository<Post> repo, IRepository<Category> category, IRepository<Event> Event, IRepository<Image> image, IRepository<Attendant> attendantRepo, IAccountRepository accountRepo)
        {
            _eventRepo = Event;
            _postRepo = repo;
            _categoryRepo = category;
            _attendantRepo = attendantRepo;
            _accountRepo = accountRepo;
			_imgRepo = image;
        }

        private bool NotAllowedHere()
        {
            string email = User.Identity.Name;
            return (_accountRepo.FindAll(u => u.Email == email && u.Administrator).Count() < 1);
        }

        private ActionResult RedirectAway()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Index()
        {
            if (NotAllowedHere()) return RedirectAway();

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
            if (NotAllowedHere()) return RedirectAway();

            var posts = _postRepo.FindAll();
            return View(posts.Where(p => p.ID == id).Include(p => p.Events).FirstOrDefault());
        }

        public ActionResult EventAttendants(int id)
        {
            if (NotAllowedHere()) return RedirectAway();

            Event anEvent = _eventRepo.FindByID(id);
            return View(anEvent);
        }

        [HttpGet]
        public ActionResult EditAttendant(int id)
        {
            if (NotAllowedHere()) return RedirectAway();

            Attendant attendant = _attendantRepo.FindByID(id);
            return View(attendant);
        }

        [HttpPost]
        public ActionResult EditAttendant(Attendant attendant)
        {
            if (NotAllowedHere()) return RedirectAway();

            _attendantRepo.Save(attendant);
            return RedirectToAction("EventAttendants", new { id = attendant.EventID });
        }

        public ActionResult DeleteAttendant(int id)
        {
            if (NotAllowedHere()) return RedirectAway();

            int eventId = _attendantRepo.FindByID(id).Event.ID;
            _attendantRepo.Delete( _attendantRepo.FindByID(id) );
            return RedirectToAction("EventAttendants", new { id = eventId });
        }

        public ActionResult EditCourse(int id = 0)
        {
            if (NotAllowedHere()) return RedirectAway();

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
            if (NotAllowedHere()) return RedirectAway();

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
        [ValidateInput(false)]
        public ActionResult SaveCourse(Post post, FormCollection postedForm)
        {
            if (NotAllowedHere()) return RedirectAway();

            if (ModelState.IsValid)
            {
                var theOldPost = _postRepo.FindByID(post.ID);
                var listOfImagesPaths = postedForm["image"];
                if (listOfImagesPaths != null)
                {
                    var arrayOfImagesPaths = listOfImagesPaths.Split(',');
                    post.Images = _imgRepo.FindAll(c => arrayOfImagesPaths.Any(cat => cat == c.ImagePath.ToString())).ToList();

                    foreach (var cat in theOldPost.Images.Where(c => !arrayOfImagesPaths.Any(cat2 => cat2 == c.ImagePath.ToString())))
                    {
                        cat.Posts.Remove(theOldPost);
                    }
                }
                /*var listOfImagesPaths = postedForm["image"];
                if (listOfImagesPaths != null)
                {
                    var arrayOfImagesPaths = listOfImagesPaths.Split(',');
                    foreach (var path in arrayOfImagesPaths)
                    {
                        var img = _imgRepo.FindAll().Where(p => p.ImagePath == path).FirstOrDefault();
                        img.Posts = new List<Post>();
                        img.Posts.Add(post);
                        _imgRepo.Save(img);
                    }
                }*/

         
                var listOfCategoryIDs = postedForm["name"];
                if (listOfCategoryIDs != null)
                {
                     var arrayOfCategoryIDs = listOfCategoryIDs.Split(',');
                     post.Category = _categoryRepo.FindAll(c => arrayOfCategoryIDs.Any(cat => cat == c.ID.ToString())).ToList();
                     
                     foreach (var cat in theOldPost.Category.Where(c => !arrayOfCategoryIDs.Any(cat2 => cat2 == c.ID.ToString()))) {
                         cat.Posts.Remove(theOldPost);                         
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
            if (NotAllowedHere()) return RedirectAway();

            _postRepo.Delete(_postRepo.FindByID(id));
            return RedirectToAction("Index");
        }


        /*-----------------------------------------
        * Event Controller 
        * -------------------------------------*/
        public ActionResult AddEvent(int id = 0)
        {
            if (NotAllowedHere()) return RedirectAway();

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
            if (NotAllowedHere()) return RedirectAway();

            var Event = _eventRepo.FindByID(id);
            return View("AddEvent", Event);
        }

        [HttpPost]
        public ActionResult SaveEvent(Event Event, FormCollection form)
        {
            if (NotAllowedHere()) return RedirectAway();

            if (ModelState.IsValid)
            {
                var listOfImagesPaths = form["image"];
                if (listOfImagesPaths != null)
                {
                    var arrayOfImagesPaths = listOfImagesPaths.Split(',');
                    foreach (var path in arrayOfImagesPaths)
                    {
                        var img = _imgRepo.FindAll().Where(p => p.ImagePath == path).FirstOrDefault();
                        img.Events = new List<Event>();
                        img.Events.Add(Event);
                        _imgRepo.Save(img);
                    }
                }
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
            if (NotAllowedHere()) return RedirectAway();

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
            if (NotAllowedHere()) return RedirectAway();

            if (id != 0) {
                return View("AddCategory", _categoryRepo.FindAll().Where(c => c.ID == id).Include(i => i.Images).FirstOrDefault());
            } else {
                Category category = new Category();
                category.Created = DateTime.Now;
                return View("AddCategory", category);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveCategory(Category Category, FormCollection form)
        {
            if (NotAllowedHere()) return RedirectAway();

            if (ModelState.IsValid)
            {
                var listOfImagesPaths = form["image"];
                if (listOfImagesPaths != null)
                {
                    var arrayOfImagesPaths = listOfImagesPaths.Split(',');
                    foreach (var path in arrayOfImagesPaths)
                    {
                        var img = _imgRepo.FindAll().Where(p => p.ImagePath == path).FirstOrDefault();
                        img.Category = new List<Category>();
                        img.Category.Add(Category);
                        _imgRepo.Save(img);
                    }
                }
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
            if (NotAllowedHere()) return RedirectAway();

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
