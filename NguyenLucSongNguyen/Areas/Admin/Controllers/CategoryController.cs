using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using NguyenLucSongNguyen.Context;
using NguyenLucSongNguyen.Controllers;
namespace NguyenLucSongNguyen.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        WebsiteEcomEntities context = new WebsiteEcomEntities();
        public ActionResult Index()
        {
            var category = context.Categories.ToList();
            return View(category);
        }
        public ActionResult Detail(int id)
        {
            var category = context.Categories.Find(id);
            return View(category);
        }
        public ActionResult Create()
        {
            
            
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category objCategory)
        {
            var category = new Category();
            try
            {
                if (objCategory.ImageUpload != null)
                {
                    string fileName = ProductController.GenerateSlug(objCategory.name);
                    string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                    fileName = fileName + extension;
                    //var path = Path.Combine(Server.MapPath("~/Content/images/products"), fileName);
                    category.image = fileName;
                    objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/categories"), fileName));

                }
                category.name = objCategory.name;
                category.show = objCategory.ShowDropdown;
                context.Categories.Add(category);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra: " + ex.Message);
            }


            return View();
        }
        public ActionResult Edit(int id)
        {
            var category = context.Categories.Find(id);
            return View(category);
        }
        [HttpPost]
        public ActionResult Edit(Category objCategory)
        {
            var category=context.Categories.Find(objCategory.id);
            if (objCategory.ImageUpload != null)
            {
                string fileName = ProductController.GenerateSlug(objCategory.name);
                string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                fileName = fileName + extension;
                //var path = Path.Combine(Server.MapPath("~/Content/images/products"), fileName);
                category.image = fileName;
                objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/categories"), fileName));

            }
            category.name = objCategory.name;
            category.show = objCategory.show;
            category.show = objCategory.ShowDropdown;
            context.Entry(category).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
            
        }
        public ActionResult Delete(int id)
        {
            var category = context.Categories.Find(id);
            context.Categories.Remove(category);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}