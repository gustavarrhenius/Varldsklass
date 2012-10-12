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

namespace Varldsklass.Web.Controllers
{

    public class AdminController : Controller
    {

        private PostRepository _postRepo;
        private IRepository<Category> _categoryRepo;
        private IAccountRepository _accountRepo;
        private IRepository<Location> _locationRepo;

        public AdminController(PostRepository repo, IRepository<Category> category, IRepository<Location> locationRepo, IAccountRepository account)
        {
            _postRepo = repo;
            _categoryRepo = category;
            _locationRepo = locationRepo;
            _accountRepo = account;
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
        public ActionResult CreateLocation(Location location)
        {
            if (NotAllowedHere()) return RedirectAway();

            if (ModelState.IsValid)
            {
                (new Repository<Location>()).Save(location);

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
        public ActionResult EditLocation(Location location)
        {
            if (NotAllowedHere()) return RedirectAway();
            if (ModelState.IsValid)
            {
                _locationRepo.Save(location);

                return RedirectToAction("ListLocations");
            }

            return View(location);

        }
    }
}