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

        public AdminController(PostRepository repo, IRepository<Category> category, IAccountRepository account)
        {
            _postRepo = repo;
            _categoryRepo = category;
            _accountRepo = account;
        }
        //
        // GET: /Admin/

        private bool NotAllowedHere() {
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
        public ActionResult ToCreateLocation()
        {
            if (NotAllowedHere()) return RedirectAway();
            return RedirectToAction("CreateLocation");
        }

		[Authorize]
        public ActionResult CreateLocation()
        {
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
        public ActionResult ToEditLocation(int? id)
        {
            if (NotAllowedHere()) return RedirectAway();
            return RedirectToAction("EditLocation", new { Id = id });
        }

		[Authorize]
        public ActionResult EditLocation(int? id)
        {
            if (NotAllowedHere()) return RedirectAway();            
            EditLocationsViewModel location = new EditLocationsViewModel()
            {
                Locations = new Repository<Location>().FindAll().Where(l => l.ID == id).ToList()
            };

            return View(location);

        }
    }
}
