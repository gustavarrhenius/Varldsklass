﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Varldsklass.Domain.Repositories;
using Varldsklass.Domain.Entities;
using Varldsklass.Domain.Contexts;
using System.Configuration;
using DotNetOpenAuth.OAuth2;
using DotNetOpenAuth.ApplicationBlock;
using DotNetOpenAuth.ApplicationBlock.Facebook;
using System.Net;
using System.IO;
using Varldsklass.Web.ViewModels;

namespace Varldsklass.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
        // Facebook feed
            var facebookAccessToken = ConfigurationManager.AppSettings["facebookAccessToken"];
            FacebookGraph.FacebookPageFeed graph = new FacebookGraph.FacebookPageFeed();
            if (null != facebookAccessToken)
                {
                var request = WebRequest.Create(
                    string.Format(@"https://graph.facebook.com/varldsklass?fields=feed&access_token={0}",
                    Uri.EscapeDataString(facebookAccessToken)));
                using (var response = request.GetResponse())
                    {
                    using (var responseStream = response.GetResponseStream())
                        {
                        graph = DotNetOpenAuth.ApplicationBlock.Facebook.FacebookGraph.FacebookPageFeed.Deserialize(responseStream);

                        }
                    }
            }
        return View(graph.PageFeed.Posts);
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
            var productsForCategory = productRepo.FindPostsByCategoryID(0);

            var productsWithEmptyName = productRepo.FindAll(PostRepository
                                                            .FilterProductsWithEmptyDescription);
            return View();
        }

        //public ActionResult FacebookFeed()
        //    {
        //    var facebookAccessToken = ConfigurationManager.AppSettings["facebookAccessToken"];
        //    FacebookGraph.FacebookPageFeed graph = new FacebookGraph.FacebookPageFeed();
        //    if (null != facebookAccessToken)
        //        {
        //        var request = WebRequest.Create(
        //            string.Format(@"https://graph.facebook.com/varldsklass?fields=feed&access_token={0}",
        //            Uri.EscapeDataString(facebookAccessToken)));
        //        using (var response = request.GetResponse())
        //            {
        //            using (var responseStream = response.GetResponseStream())
        //                {
        //                graph = DotNetOpenAuth.ApplicationBlock.Facebook.FacebookGraph.FacebookPageFeed.Deserialize(responseStream);
                        
        //                }
        //            }
        //        }
        //    return View(graph.PageFeed.Posts);
        //}

        public ActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact(int id)
        {
            var listOfLocations = new Repository<Location>().FindAll().ToList();

            var location = listOfLocations.Where(l => l.ID == id).ToList();

            return View(location);
        }

        public ActionResult Menu()
        {
            MenuViewModel listOfLocations = new MenuViewModel()
            {
                Locations = new Repository<Location>().FindAll().OrderBy(l => l.City).ToList()
            };
            
            return PartialView(listOfLocations);
        }
    }
}
