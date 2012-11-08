using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Varldsklass.Domain.Entities;
using Varldsklass.Domain.Repositories;
using Varldsklass.Domain.Repositories.Abstract;
using System.Data.Entity;
using Varldsklass.Web.ViewModels;
using Varldsklass.Web.Infrastructure;

namespace Varldsklass.Web.Controllers
{
    public class EvaluationController : Controller
    {
        private IRepository<Event> _eventRepo;
        private IRepository<Question> _questionRepo;
        private IRepository<Attendant> _attRepo;
        //
        // GET: /Questionnaire/
        public EvaluationController(IRepository<Event> eventRepo, IRepository<Question> questionRepo, IRepository<Attendant> attendant)
        {
            _eventRepo = eventRepo;
            _questionRepo = questionRepo;
            _attRepo = attendant;
        }

        public ActionResult SendEvaluation(int id)
        {
            var Event = _eventRepo.FindByID(id);
            BookViewModel model = new BookViewModel();
            model.Attendants = Event.Attendants.ToList();
            model.Event = Event;
            string title = "Utvärdering av " + Event.Title;
            try
            {
                foreach (var attendant in model.Attendants)
                {
                    MailSender("attendantEmailTemplate", model, attendant.Email, title);
                }
            }
            catch (Exception exception)
            {
                TempData["Event"] = model.Event.ID;
                return RedirectToAction("Fail");
            }

            return View("Evaluation", "Evaluation", new { id = Event.ID });
        }

        private void MailSender(string templateName, dynamic model, string to, string subject)
        {
            // Send mail to booker
            SendMail sendMail = new SendMail();
            string body = sendMail.render(templateName, model);
            sendMail.send(to, subject, body);
        }


        public ActionResult EvaluationAttendant(int id)
        {

            var Event = _eventRepo.FindByID(id);

            Question qst = new Question();
            qst.EventID = Event.ID;

            return View(qst);
        }
        public ActionResult Evaluation(int id)
        {
            var Event = _eventRepo.FindByID(id);
            Question qst = new Question();
            qst.EventID = Event.ID;
            return View(qst);
        }

        [HttpPost]
        public ActionResult Save(Question question)
        {
            if (ModelState.IsValid)
            {
                var Event = _eventRepo.FindByID(question.EventID);
              
                _questionRepo.Save(question);
                return RedirectToAction("Course", "Course", new { id = Event.PostID });
            }
            else
            {
                // there is something wrong with the data values
                ViewData["Error"] = "Alla frågor måste besvaras"; 
                return View("Evaluation", question);
            }
        }

        [HttpPost]
        public ActionResult SaveAttendant(Question question)
        {
            if (ModelState.IsValid)
            {
                var Event = _eventRepo.FindByID(question.EventID);
                _questionRepo.Save(question);
                ViewData["Succes"] = "Tack så mycket för din åsikt!";
                return View("Succes");
            }
            else
            {
                // there is something wrong with the data values
                ViewData["Error"] = "Alla frågor måste besvaras";
                return RedirectToAction("EvaluationAttendant", new { id = question.EventID });
            }
        }

        public ActionResult Statistics(int id = 0)
        {
            StatisticsViewModel viewModel = new StatisticsViewModel();
            List<Question> questions;

            if (id > 0)
            {
                questions = _questionRepo.FindAll().Where(q => q.EventID == id).ToList();
            }
            else
            {
                questions = _questionRepo.FindAll().ToList();
            }

            viewModel.Event = _eventRepo.FindByID(id);

            questions.ForEach(delegate(Question question)
            {
                if( IsAvailable(question.Teacher))
                    viewModel.Teacher.Add(question.Teacher.Value);

                if( IsAvailable(question.Location))
                    viewModel.Location.Add(question.Location.Value);

                if( IsAvailable(question.Food))
                    viewModel.Food.Add(question.Food.Value);

                if( IsAvailable(question.Overall))
                    viewModel.Overall.Add(question.Overall.Value);

                viewModel.Opinions.Add( question.Opinion );

            });

            return View(viewModel);
        }

        private bool IsAvailable(int? value)
        {
            if (!value.HasValue) return false;
            if (value.Value < 1) return false;

            return true;
        }
       
    }
}

