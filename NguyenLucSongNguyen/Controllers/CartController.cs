using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NguyenLucSongNguyen.Models;
using NguyenLucSongNguyen.Context;

namespace NguyenLucSongNguyen.Controllers
{
    public class CartController : Controller
    {
        WebsiteEcomEntities objwebsiteEcomEntities=new WebsiteEcomEntities();
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session["cart"] as List<CartModel>;
            return View(cart);
        }
        public ActionResult AddToCart(int id, int quantity)
        {
            if (Session["cart"] == null)
            {
                List<CartModel> cart = new List<CartModel>();
                cart.Add(new CartModel { Product = objwebsiteEcomEntities.Products.Find(id), Quantity = quantity });
                Session["cart"] = cart;
                Session["count"] = 1;
            }
            else
            {
                List<CartModel> cart = (List<CartModel>)Session["cart"];
                //kiểm tra sản phẩm có tồn tại trong giỏ hàng chưa???
                int index = isExist(id);
                if (index != -1)
                {
                    //nếu sp tồn tại trong giỏ hàng thì cộng thêm số lượng
                    cart[index].Quantity += quantity;
                }
                else
                {
                    //nếu không tồn tại thì thêm sản phẩm vào giỏ hàng
                    cart.Add(new CartModel { Product = objwebsiteEcomEntities.Products.Find(id), Quantity = quantity });
                    //Tính lại số sản phẩm trong giỏ hàng
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                }
                Session["cart"] = cart;
            }
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }
        private int isExist(int id)
        {
            List<CartModel> cart = (List<CartModel>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.id.Equals(id))
                    return i;
            return -1;
        }
        //xóa sản phẩm khỏi giỏ hàng theo id
        public ActionResult Remove(int Id)
        {
            List<CartModel> li = (List<CartModel>)Session["cart"];
            li.RemoveAll(x => x.Product.id == Id);
            Session["cart"] = li;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            //return Json(new { Message = "Thành công" }, JsonRequestBehavior.AllowGet);
            return RedirectToAction("Index");
        }
        public ActionResult Checkout()
        {
            if (Session["userId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            var cart = Session["cart"] as List<CartModel>;
            Order order= new Order();
            order.user_id =(int)Session["userId"];
            order.created_at=DateTime.Now;
            objwebsiteEcomEntities.Orders.Add(order);
            objwebsiteEcomEntities.SaveChanges();
            foreach(var item in cart)
            {
                OrderDetail detail = new OrderDetail();
                detail.order_id = order.id;
                detail.product_id=item.Product.id;
                detail.quantity=item.Quantity;
                detail.price = item.Product.price;
                objwebsiteEcomEntities.OrderDetails.Add(detail);
                
            }
            objwebsiteEcomEntities.SaveChanges();
            Session.Remove("cart");
            return RedirectToAction("Index","Home");
        }
    }
}