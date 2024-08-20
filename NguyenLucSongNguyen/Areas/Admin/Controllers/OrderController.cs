using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NguyenLucSongNguyen.Context;
namespace NguyenLucSongNguyen.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        // GET: Admin/Order
        WebsiteEcomEntities context= new WebsiteEcomEntities();
        public ActionResult Index()
        {
            var order = context.Orders.ToList();
            return View(order);
        }
        public ActionResult Detail(int id)
        {
            var orderDetail=context.OrderDetails.Where(o=>o.order_id==id).ToList();
            return View(orderDetail);
        }
    }
}