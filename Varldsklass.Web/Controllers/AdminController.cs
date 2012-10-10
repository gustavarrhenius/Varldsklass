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



namespace Varldsklass.Web.Controllers
{

    public class AdminController : Controller
    {

        private PostRepository _postRepo;
        private IRepository<Category> _categoryRepo;

        public AdminController(PostRepository repo, IRepository<Category> category)
        {
            _postRepo = repo;
            _categoryRepo = category;
        }
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Courses()
        {
            return View();
        }
        

        public ActionResult NewsLetter()
        {
            return View();
        }

        public ActionResult Customers()
        {
            return View();
        }

        public ActionResult ListLocations()
        {
            var listOfLocations = new Repository<Location>().FindAll().ToList();

            return View(listOfLocations);
        }

        public ActionResult ToCreateLocation()
        {
            return RedirectToAction("CreateLocation");
        }

        public ActionResult CreateLocation()
        {
            return View(new Location());
        }

        [HttpPost]
        public ActionResult CreateLocation(Location location)
        {
            if (ModelState.IsValid)
            {
                (new Repository<Location>()).Save(location);

                return RedirectToAction("ListLocations");
            }

            return View(location);
        }

        public ActionResult ToEditLocation(int? id)
        {
            return RedirectToAction("EditLocation", new { Id = id });
        }

        public ActionResult EditLocation(int? id)
        {
            
            EditLocationsViewModel location = new EditLocationsViewModel()
            {
                Locations = new Repository<Location>().FindAll().Where(l => l.ID == id).ToList()
            };

            return View(location);

        }
    }
}
