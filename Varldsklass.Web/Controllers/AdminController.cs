using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Varldsklass.Web.Models;
using Varldsklass.Domain.Entities;
using Varldsklass.Domain.Repositories;
using Varldsklass.Domain.Repositories.Abstract;



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

        public ViewResult ListLocations()
        {
            var listOfLocations = new Repository<Location>().FindAll().ToList();

            return View(listOfLocations);
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

        public ViewResult EditLocation()
        {
            
            return View();

        }
    }
}
