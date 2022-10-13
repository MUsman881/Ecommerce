using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;
using System.Web.Security;

namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        EcomAspMVCEntities obj = new EcomAspMVCEntities();

        public ActionResult fdatahome()
        {
            List<featurejoin> prolist = obj.Database.SqlQuery<featurejoin>("select Product.pid, Product.pname, Product.price, Product.picone from Product, featureproduct where Product.pid=featureproduct.pid order by Product.pid").ToList();

            return Json(prolist.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult sdatahome()
        {
            List<salejoin> prolist = obj.Database.SqlQuery<salejoin>("select product.pid,product.pname,product.picone,product.price,saleproduct.lastdate from product,saleproduct where product.pid=saleproduct.pid and saleproduct.lastdate>='" + DateTime.Now.ToString() + "'  order by saleproduct.lastdate").ToList();
            return Json(prolist.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            int userid = Convert.ToInt32(Session["ID"]);
            List<Cart> cart = obj.Carts.Where(c => c.ID == userid).ToList();

            if (cart != null)
            {
                Session["cartitems"] = cart.Count();
            }
            else
            {
                Session["cartitems"] = null;
            }

            return View();
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

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Login");
        }
        
        #region login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            User us = obj.Users.Where(u => u.Email == user.Email && u.Password == user.Password).SingleOrDefault();

            if (us != null)
            {
                Session["ID"] = us.ID.ToString();
                Session["userName"] = us.Name.ToString();
                Session["userPass"] = us.Password.ToString();
                ViewBag.username = Session["username"];
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "Invalid Username or Password!";

            }
            return View();
        }
        #endregion

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User register)
        {
            return View();
        }

    }
}