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

            var product = context.Products.ToList();
            var category = context.Categories.ToList();
            var brand = context.Brands.Where(b => b.show == true).ToList();
            var viewModel = new HomeModel
            {
                Products = product,
                Categories = category,
                Brands = brand
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