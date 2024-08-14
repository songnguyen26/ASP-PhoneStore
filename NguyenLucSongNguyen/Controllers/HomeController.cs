using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NguyenLucSongNguyen.Context;
using NguyenLucSongNguyen.Models;

namespace NguyenLucSongNguyen.Controllers
{
    public class HomeController : Controller
    {
          WebsiteEcomEntities context = new WebsiteEcomEntities();
        public ActionResult Index()
        {

            var product = context.Products
                          .Where(p => p.isOnSale ==1) 
                          .Take(3)            
                          .ToList();
            var category = context.Categories.ToList();

            var viewModel = new HomeModel
            {
                Products = product,
                Categories = category
            };

            return View(viewModel);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult CategoryNav()
        {
            var category = context.Categories.ToList();
            return PartialView("CategoryNav",category);
        }
        
    }
}