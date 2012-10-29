using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Varldsklass.Domain.Repositories.Abstract;
using Varldsklass.Domain.Entities;
using Varldsklass.Web.ViewModels;
using Varldsklass.Domain.Repositories;

namespace Varldsklass.Web.Controllers
{
    public class BookController : Controller
    {

        private IRepository<Attendant> _attendantRepo;
        private IRepository<Event> _eventRepo;
        private IAccountRepository _accountRepo;

        public BookController(IRepository<Attendant> attendantRepo, IRepository<Event> eventRepo, IAccountRepository accountRepo)
        {
            _attendantRepo = attendantRepo;
            _eventRepo = eventRepo;
            _accountRepo = accountRepo;
        }

        [Authorize]
        public ActionResult Event(int id = 0)
        {
            BookViewModel model = new BookViewModel();
            model.Event = _eventRepo.FindByID(id);
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Save(BookViewModel model)
        {
            model.Event = _eventRepo.FindByID(model.Event.ID);
            Account booker = _accountRepo.FindAll().Where(u => u.Email == User.Identity.Name).FirstOrDefault();

            List<Attendant> ValidAttendants = new List<Attendant>();
            for (int i = 0; i < model.Attendants.Count; i++)
            {
                if (model.Attendants[i].Email != null && model.Attendants[i].Name != null)
                {
                    model.Attendants[i].BookerID = booker.ID;
                    model.Attendants[i].EventID = model.Event.ID;

                    //model.Attendants[i].Event = _eventRepo.FindByID(model.Event.ID);
                    //model.Attendants[i].Booker = _accountRepo.FindByID(booker.ID);

                    ValidAttendants.Add(model.Attendants[i]);
                }
            }

            if (model.BookerAttends)
            {
                ValidAttendants.Add(new Attendant {
                    Name = booker.FullName,
                    Email = booker.Email,
                    BookerID = booker.ID,
                    EventID = model.Event.ID
                });
            }

            //if (!ModelState.IsValid) return View();

            ValidAttendants.ForEach(delegate(Attendant attendant)
            {
                _attendantRepo.Save(attendant);
            });

            return RedirectToAction("List");
        }

        [Authorize]
        public ActionResult List(int id = 0)
        {
            Dictionary<string, List<Attendant>> eventAttendants = new Dictionary<string, List<Attendant>>();
            Event thisEvent = _eventRepo.FindByID(id);
            foreach( var e in _eventRepo.FindAll() ) {
                List<Attendant> attendants = new List<Attendant>();
                var userAccount = _accountRepo.FindAll().Where(a => a.Email == User.Identity.Name).FirstOrDefault();
                if (userAccount == null)
                {
                    continue;
                }
                var userId = userAccount.ID;
                var eventTitle = e.Title;
                List<Attendant> attendantList = _attendantRepo.FindAll().Where(a => a.BookerID == userId).ToList();
                eventAttendants.Add(eventTitle, attendantList);
            }

            return View(eventAttendants);
        }
    }
}
