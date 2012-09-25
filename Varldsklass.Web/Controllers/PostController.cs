using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Varldsklass.Domain.Repositories;
using Varldsklass.Domain.Entities;

namespace Varldsklass.Web.Controllers
{
    public class PostController : Controller
    {
        private IPostRepository _productRepo;
        public PostController(IPostRepository productRepo)
        {
            _productRepo = productRepo;
        }

        //
        // GET: /Product/

        public ActionResult Index()
        {
            // 2 st. Category plockas fram/skapas och används i demo-syfte.
            // Vi kan tänka oss att vi fått dessa som resultat av User Input
            var categoryRepo = new Repository<Category>();
            Category oldCategory = categoryRepo
                                    .FindAll(c => c.Name.Contains("New"))
                                    .OrderByDescending(c => c.ID)
                                    .FirstOrDefault();
            if (null == oldCategory) oldCategory = categoryRepo.FindAll().FirstOrDefault();

            var newCategory = new Category
            {
                Name = string.Format("NewCategory - {0}",
                                     DateTime.UtcNow.ToShortDateString())
            };


            // Nedanstående vill vi inte ha i ProductController - Dags att bryta ut
            //
            // I det här fallet så uppdaterar vi Category för alla produkter med oldCategory till newCategory.
            // Vi skulle kunna tänka oss en större operation här - men detta duger i övningssyfte.
            //
            // Poängen är att detta är logik som inte direkt rör user input/output.
            // Dvs: det bör inte ligga i Controller utan någonstanns i .Domain-projektet.
            var filteredProducts = _productRepo
                                    .FindAll(p => p.CategoryID == oldCategory.ID)
                                    .ToList();
            foreach (var productToUpdate in filteredProducts)
            {
                productToUpdate.Category = newCategory;
                _productRepo.Save(productToUpdate);
            }

            return View();
        }

    }
}
