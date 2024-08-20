using NguyenLucSongNguyen.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NguyenLucSongNguyen.Controllers;
namespace NguyenLucSongNguyen.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        WebsiteEcomEntities context = new WebsiteEcomEntities();
        public ActionResult Index()
        {
            var brand = context.Brands.ToList();
            return View(brand);
        }
        public ActionResult Detail(int id)
        {
            var brand = context.Brands.Find(id);
            return View(brand);
        }
        public ActionResult Create()
        {

            
            return View();
        }
        [HttpPost]
        public ActionResult Create(Brand objBrand)
        {
            var brand = new Brand();
            try
            {
                if (objBrand.ImageUpload != null)
                {
                    string fileName = ProductController.GenerateSlug(objBrand.name);
                    string extension = Path.GetExtension(objBrand.ImageUpload.FileName);
                    fileName = fileName + extension;
                    //var path = Path.Combine(Server.MapPath("~/Content/images/products"), fileName);
                    brand.image = fileName;
                    objBrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/brands"), fileName));

                }
                brand.name = objBrand.name;
                brand.show = objBrand.ShowDropdown;
                context.Brands.Add(brand);
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
            var brand = context.Brands.Find(id);
            return View(brand);
        }
        [HttpPost]
        public ActionResult Edit(Brand objbrand)
        {
            var brand = context.Brands.Find(objbrand.id);
            if (objbrand.ImageUpload != null)
            {
                string fileName = ProductController.GenerateSlug(objbrand.name);
                string extension = Path.GetExtension(objbrand.ImageUpload.FileName);
                fileName = fileName + extension;
                //var path = Path.Combine(Server.MapPath("~/Content/images/products"), fileName);
                brand.image = fileName;
                objbrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/brands"), fileName));

            }
            brand.name = objbrand.name;
            brand.show = objbrand.show;
            brand.show = objbrand.ShowDropdown;
            context.Entry(brand).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult Delete(int id)
        {
            var brand = context.Brands.Find(id);
            context.Brands.Remove(brand);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}