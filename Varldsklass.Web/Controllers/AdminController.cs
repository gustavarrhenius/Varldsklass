using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Varldsklass.Web.Models;
using Varldsklass.Domain.Entities;
using Varldsklass.Domain.Repositories;
using Varldsklass.Domain.Repositories.Abstract;
using Varldsklass.Web.ViewModels;
using Varldsklass.Web.Infrastructure;
using System.IO;

namespace Varldsklass.Web.Controllers
{

    public class AdminController : Controller
    {

        private PostRepository _postRepo;
        private IRepository<Category> _categoryRepo;
        private IAccountRepository _accountRepo;
        private IRepository<Location> _locationRepo;
        private IRepository<Event> _eventRepo;
        private IRepository<Image> _imgRepo;


        public AdminController(PostRepository repo, IRepository<Category> category, IRepository<Image> image, IRepository<Location> locationRepo, IAccountRepository account, IRepository<Event> eventRepo)
        {
            _eventRepo = eventRepo;
            _postRepo = repo; _postRepo.Model = _eventRepo.Model;
            _categoryRepo = category; _categoryRepo.Model = _eventRepo.Model;
            _accountRepo = account; _accountRepo.Model = _eventRepo.Model;
            _imgRepo = image; _imgRepo.Model = _eventRepo.Model;
            _locationRepo = locationRepo; _locationRepo.Model = _eventRepo.Model;
        }
        //
        // GET: /Admin/

        private bool NotAllowedHere()
        {
            string email = User.Identity.Name;
            return (_accountRepo.FindAll(u => u.Email == email && u.Administrator).Count() < 1);
        }

        private ActionResult RedirectAway()
        {
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Topbar()
        {
            if (NotAllowedHere())
            {
                return new EmptyResult();
            }
            else
            {
                return PartialView("_AdminTopbar");
            }
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

        [Authorize]
        public ActionResult ListLocations()
        {
            if (NotAllowedHere()) return RedirectAway();
            var listOfLocations = new Repository<Location>().FindAll().ToList();

            return View(listOfLocations);
        }

        [Authorize]
        public ActionResult CreateLocation()
        {
            if (NotAllowedHere()) return RedirectAway();

            return View(new Location());
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateLocation(Location location, FormCollection form)
        {
            if (NotAllowedHere()) return RedirectAway();

            if (ModelState.IsValid)
            {
                var theOldPost = _locationRepo.FindByID(location.ID);
                if (theOldPost == null)
                    theOldPost = location;
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
                _locationRepo.Save(location);

                return RedirectToAction("ListLocations");
            }

            return View(location);
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditLocation(int id)
        {
            if (NotAllowedHere()) return RedirectAway();
            var location = _locationRepo.FindByID(id);

            return View(location);

        }

        [Authorize]
        [HttpPost]
        public ActionResult EditLocation(Location location, FormCollection form)
        {
            if (NotAllowedHere()) return RedirectAway();
            if (ModelState.IsValid)
            {
                var theOldPost = _locationRepo.FindByID(location.ID);
                if (theOldPost == null)
                    theOldPost = location;
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
                _locationRepo.Save(location);

                return RedirectToAction("ListLocations");
            }

            return View(location);

        }

        public ActionResult DeleteLocation(int id)
        {
            _locationRepo.Delete(_locationRepo.FindByID(id));

            return RedirectToAction("ListLocations");
        }

        public ActionResult Menu()
        {
           return PartialView();
        }

        public ActionResult ListAttendants()
        {
            List<Event> attendantList = _eventRepo.FindAll().ToList();
            return View( attendantList );
        }
    }
}
