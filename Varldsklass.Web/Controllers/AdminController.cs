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

        public AdminController(PostRepository repo, IRepository<Category> category, IRepository<Location> locationRepo, IAccountRepository account, IRepository<Event> eventRepo)
        {
            _postRepo = repo;
            _categoryRepo = category;
            _locationRepo = locationRepo;
            _accountRepo = account;
            _eventRepo = eventRepo;
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

        public ActionResult FileUpload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                var fullFileName = Path.GetFileName(file.FileName);
                var fileName = Path.GetFileNameWithoutExtension(fullFileName);
                var folder = "~/Content/image-uploads";
                FileInfo fileInfo = new FileInfo(fullFileName);
                if (fileInfo.Extension.ToLower() == ".jpg" || fileInfo.Extension.ToLower() == ".jpeg" ||
                    fileInfo.Extension.ToLower() == ".png" || fileInfo.Extension.ToLower() == ".gif")
                {
                    var path = Path.Combine(Server.MapPath(folder), fullFileName);

                    var num = 2;
                    while (System.IO.File.Exists(path))
                    {
                        fullFileName = fileName + "(" + num + ")" + fileInfo.Extension;
                        path = Path.Combine(Server.MapPath(folder), fullFileName);
                        num++;
                    }

                    if (!System.IO.File.Exists(path))
                    {
                        file.SaveAs(path);

                        ViewData["message"] = "Bilden har blivit uppladdad";
                        return View();
                    }
                }
            }
            ViewData["message"] = "Det gick inte att ladda upp bilden";
            return View();
        }
    }
}