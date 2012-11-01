using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Varldsklass.Domain.Repositories.Abstract;
using Varldsklass.Domain.Entities;
using Varldsklass.Web.ViewModels;
using Varldsklass.Domain.Repositories;
using Varldsklass.Web.Utils;
using System.Net.Mail;
using System.Net;
using System.Configuration;

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

        public ActionResult sendBookingEmail(int id = 0) 
        {
            BookViewModel model = new BookViewModel();
            string bookedAttendants = _attendantRepo.FindByID(id).FullName;
            Event bookedEvent = _eventRepo.FindByID(id);
            MailClient.SendBooking(ConfigurationManager.AppSettings["adminEmail"], bookedEvent, bookedAttendants);
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Save(BookViewModel model)
        {
            model.Event = _eventRepo.FindByID(model.Event.ID);
            Account booker = _accountRepo.FindAll().Where(u => u.Email == User.Identity.Name).FirstOrDefault();

            List<Attendant> ValidAttendants = new List<Attendant>();
            /*for (int i = 0; i < model.Attendants.Count; i++)
            {
                if (model.Attendants[i].Email != null && model.Attendants[i].FirstName != null && model.Attendants[i].LastName != null)
                {
                    model.Attendants[i].BookerID = booker.ID;
                    model.Attendants[i].EventID = model.Event.ID;

                    ValidAttendants.Add(model.Attendants[i]);
                }
            }*/
            model.Attendants.ForEach(delegate(Attendant attendant)
            {
                if (attendant.Email != null && attendant.FirstName != null && attendant.LastName != null)
                {
                    attendant.BookerID = booker.ID;
                    attendant.EventID = model.Event.ID;

                    ValidAttendants.Add(attendant);
                }
            });

            if (model.BookerAttends)
            {
                ValidAttendants.Add(new Attendant {
                    FirstName = booker.FirstName,
                    LastName = booker.LastName,
                    Email = booker.Email,
                    BookerID = booker.ID,
                    EventID = model.Event.ID
                });
            }

            ValidAttendants.ForEach(delegate(Attendant attendant)
            {
                _attendantRepo.Save(attendant);
            });

            return RedirectToAction("List");
        }

        [Authorize]
        public ActionResult List(int id = 0)
        {
            if (id == 0)
            {
                List<Event> eventList = _eventRepo.FindAll().Where(e => e.Attendants.Count > 0).ToList();
                return View("EventList", eventList);
            }
            else
            {
                Event singleEvent = _eventRepo.FindByID(id);
                return View("SingleEvent", singleEvent);
            }
        }
    }
}
