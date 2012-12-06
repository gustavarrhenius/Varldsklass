using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Varldsklass.Domain.Repositories.Abstract;
using Varldsklass.Domain.Entities;
using Varldsklass.Web.ViewModels;
using Varldsklass.Domain.Repositories;
using Varldsklass.Web.Infrastructure;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;

namespace Varldsklass.Web.Controllers
{
    public class BookController : Controller
    {
        private IRepository<Attendant> _attendantRepo;
        private IEventRepository _eventRepo;
        private IAccountRepository _accountRepo;

        public BookController(IRepository<Attendant> attendantRepo, IEventRepository eventRepo, IAccountRepository accountRepo)
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
            model.Booker = booker;
            List<Attendant> ValidAttendants = new List<Attendant>();

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
                bool alreadyExists = (_attendantRepo.FindAll().Where( a => a.EventID == attendant.EventID && a.Email == attendant.Email).Count() > 0);

                if( ! alreadyExists ) {
                    _attendantRepo.Save(attendant);
                }
            });

            model.Attendants = ValidAttendants; // Update model for mail-rendering

            // Send mail to booker
            try
            {
                MailSender("bookingEmailTemplate", model, "olivmagi@gmail.com", "Bokning");
                MailSender("johanBookingEmailTemplate", model, "noreply@varldsklass.com", "Bokning");
            }
            catch (Exception exception)
            {
                TempData["Event"] = model.Event.ID;
                return RedirectToAction("Fail");
            }

            return RedirectToAction("List");
        }

        public ActionResult Fail()
        {
            return View(TempData["Event"]);
        }

        private void MailSender(string templateName, dynamic model, string to, string subject) {
            // Send mail to booker
            SendMail sendMail = new SendMail();
            string body = sendMail.render(templateName, model);
            sendMail.send(to, subject, body);
        }

        [Authorize]
        public ActionResult List(int id = 0)
        {
            int currentUserId = _accountRepo.FindAll().Where(a => a.Email == User.Identity.Name).FirstOrDefault().ID;

            if (id == 0)
            {
                List<Event> eventList = _eventRepo.FindByBooker(currentUserId);
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
