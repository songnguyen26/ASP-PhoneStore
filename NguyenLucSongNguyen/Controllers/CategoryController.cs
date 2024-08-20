using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NguyenLucSongNguyen.Context;
namespace NguyenLucSongNguyen.Controllers
{
    public class CategoryController : Controller
    {
        WebsiteEcomEntities context=new WebsiteEcomEntities();
        // GET: Category
        public ActionResult Index()
        {
            var category=context.Categories.ToList();
            return View(category);
        }
    }
}