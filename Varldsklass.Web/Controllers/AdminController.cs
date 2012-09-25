using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lektion13.Web.Controllers
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
        public ActionResult News()
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


    }
}
