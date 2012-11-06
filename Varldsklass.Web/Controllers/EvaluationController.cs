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

namespace Varldsklass.Web.Controllers
{
    public class EvaluationController : Controller
    {
        private IRepository<Event> _eventRepo;
        private IRepository<Question> _questionRepo;
        //
        // GET: /Questionnaire/
        public EvaluationController(IRepository<Event> eventRepo, IRepository<Question> questionRepo)
        {
            _eventRepo = eventRepo;
            _questionRepo = questionRepo;
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

        public ActionResult Statistics(int id)
        {
            StatisticsViewModel viewModel = new StatisticsViewModel();
            List<Question> questions = _questionRepo.FindAll().Where(q => q.Event.PostID == id).ToList();

            viewModel.Teacher = (double)questions.Average(q => q.Teacher);
            viewModel.Food = (double)questions.Average(q => q.Food);
            viewModel.Location = (double)questions.Average(q => q.Location);
            viewModel.Overall = (double)questions.Average(q => q.Overall);

            return View();
        }
    }
}

