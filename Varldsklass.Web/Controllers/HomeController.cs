using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Varldsklass.Domain.Repositories;
using Varldsklass.Domain.Entities;

namespace Varldsklass.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            // Generiskt Repository - Här skapas ett repository för Category
            // Repositoryt kräver typer som implementerar IEntity
            Repository<Category> categoryRepo = new Repository<Category>();

            // Samtliga metoder som finns med i det generiska repositoriet
            var categories = categoryRepo.FindAll();

            var filteredCategories = categoryRepo.FindAll(c => c.Name.Contains("sport"));

            var category = categoryRepo.FindByID(1);

            category.Name = "New Name!";
            categoryRepo.Save(category);

            categoryRepo.Delete(category);


            PostRepository productRepo = new PostRepository();

            var products = productRepo.FindAll(); // + övriga "grund"-metoder

            // Metoder implementerade i ProductRepository:
            var productsForCategory = productRepo.FindProductsByCategoryID(0);

            var productsWithEmptyName = productRepo.FindAll(PostRepository
                                                            .FilterProductsWithEmptyDescription);


            return View();
        }
    }
}
