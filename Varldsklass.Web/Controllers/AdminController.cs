using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Varldsklass.Web.Models;

namespace Varldsklass.Web.Controllers
{
    public class AdminController : Controller
    {
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
        [HttpGet]
        public ActionResult News()
        {
            return View();
        }

        [HttpPost]
        public ActionResult News(News news)
        {
            if (ModelState.IsValid)
            {
                // TODO: Email guestResponse to the part organizer
                return View("OK", news);
            }
            else
            {
                // there is a validation error - redisplay the form
                return View();
            }
        }
        public ActionResult NewsLetter()
        {
            return View();
        }

        public ActionResult Customers()
        {
            return View();
        }
    }
}
