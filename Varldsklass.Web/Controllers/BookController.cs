﻿using System;
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

        public ActionResult Index(int id = 0)
        {
            BookViewModel model = new BookViewModel();
            model.Event = _eventRepo.FindByID(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Save(BookViewModel model)
        {
            model.Event = _eventRepo.FindByID(model.Event.ID);

            for (int i = 0; i < model.Attendants.Count; i++)
            {
                model.Attendants[i].BookerID = _accountRepo.FindAll(u => u.Email == User.Identity.Name).FirstOrDefault().ID;
                model.Attendants[i].EventID = model.Event.ID;
            }

            //if (!ModelState.IsValid) return View();

            //model.Event = _eventRepo.FindByID(model.Event.ID);

            model.Attendants.ForEach(delegate(Attendant attendant)
            {
                _attendantRepo.Save(attendant);
            });

            return RedirectToAction("Success");
        }

        public ActionResult List()
        {
            List<Attendant> attendants = _attendantRepo.FindAll().ToList();
            return View(attendants);
        }
    }
}
