using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using NguyenLucSongNguyen.Context;


namespace NguyenLucSongNguyen.Controllers
{    public class UserController : Controller
    {
        WebsiteEcomEntities obj = new WebsiteEcomEntities();
        
        // GET: User
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User objuser)
        {
            objuser.password = CreateMD5(objuser.password);
            var flagUser = obj.Users.Where(n=> n.email.Equals(objuser.email) && n.password.Equals(objuser.password)).ToList();
            if(flagUser.Count >0)
            {
                Session["fullName"] = flagUser.FirstOrDefault().firstName + " " + flagUser.FirstOrDefault().lastName;
                Session["userId"] = flagUser.FirstOrDefault().userId;

                return RedirectToAction("Index","Home");
            }
            return View();
        }
        
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User objuser)
        {
            try
            {
                objuser.password = CreateMD5(objuser.password);
                obj.Users.Add(objuser);
                obj.SaveChanges();
                return RedirectToAction("Login");
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index","Home");
        }
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                /*return Convert.ToHexString(hashBytes);*/ // .NET 5 +

                // Convert the byte array to hexadecimal string prior to .NET 5
                StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}