using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using NguyenLucSongNguyen.Context;
namespace NguyenLucSongNguyen.Controllers
{
    public class ProductController : Controller
    {
        WebsiteEcomEntities context = new WebsiteEcomEntities();

        // GET: Product
        public ActionResult Index()
        {
            var product=context.Products.ToList();
            return View(product);
        }
        public ActionResult Index1()
        {
            var product = context.Products.ToList();
            return View(product);
        }
        public ActionResult Detail(int id)
        {
            
            var detail = context.Products.Find(id);
            return View(detail);
        }
        public ActionResult productByCat(int id)
        {
            var product = context.Products.Where(n=>n.category_id == id).ToList();
            return View(product);
        }
        
    }
}