using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Varldsklass.Web.Models;
using Varldsklass.Domain.Entities;

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
            news = new News();

            string headline = news.Headline;
            string body = "<h3>" + news.Ingress + "</h3>\n<p>" + news.Text + "</p>";

            if (ModelState.IsValid)
            {
                // redirect to NewsSave.cshtml
                return View("NewsSaved", news);
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
