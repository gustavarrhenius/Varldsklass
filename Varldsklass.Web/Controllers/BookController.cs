using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Varldsklass.Domain.Repositories.Abstract;
using Varldsklass.Domain.Entities;
using Varldsklass.Web.ViewModels;

namespace Varldsklass.Web.Controllers
{
    public class BookController : Controller
    {

        private IRepository<Attendant> _attendantRepo;
        private IRepository<Event> _eventRepo;

        public BookController(IRepository<Attendant> attendantRepo, IRepository<Event> eventRepo)
        {
            _attendantRepo = attendantRepo;
            _eventRepo = eventRepo;
        }

        public ActionResult Index(int id = 0)
        {
            BookViewModel model = new BookViewModel();
            model.Event = _eventRepo.FindByID(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Save(BookViewModel model)
        {
            if (!ModelState.IsValid) return View();

            model.Event = _eventRepo.FindByID(model.Event.ID);

            model.Attendants.ForEach(delegate(Attendant attendant)
            {
                _attendantRepo.Save(attendant);
            });

            return RedirectToAction("Index");
        }
    }
}
