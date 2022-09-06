using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;
using Newtonsoft.Json;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Ecommerce.Controllers
{
    [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]

    public class ShopController : Controller
    {
        EcomAspMVCEntities obj = new EcomAspMVCEntities();

        // GET: Shop
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult Cart()
        {
            if(Session["ID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            return View();
        }


        [HttpPost]
        public ActionResult AddtoCart(string proid, string qty)
        {
            int pid = Convert.ToInt32(proid);
            int Qty = Convert.ToInt32(qty);
            int userID = Convert.ToInt32(Session["ID"]);
            Cart cart = obj.Carts.Where(c => c.pid == pid && c.ID == userID).SingleOrDefault();

            if (cart == null)
            {
                Cart c = new Cart();
                c.pid = pid;
                c.ID = userID;
                c.Qty = Qty;

                obj.Carts.Add(c);
                obj.SaveChanges();

                return Json(new { success = true, message = "Product added to Cart" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                cart.Qty = Qty;
                obj.Entry(cart).State = EntityState.Modified;
                obj.SaveChanges();
                return Json(new { success = true, message = "Product quantity updated" }, JsonRequestBehavior.AllowGet);
            }

        }



        public ActionResult ProductDetails(int? pid)
        {
            ViewBag.pid = pid;
            return View();
        }

        public ActionResult viewdetail(string proid)
        {

            List<productdetail> prolist = obj.Database.SqlQuery<productdetail>("select pid, pname, price, pdes ,picone, pictwo from product where product.pid ='" + proid + "'").ToList();

            return Json(prolist.ToList(), JsonRequestBehavior.AllowGet);

        }

        public ActionResult allpro()
        {

            List<salejoin> prolist = obj.Database.SqlQuery<salejoin>("select product.pid, product.pname, product.picone, product.price from Product").ToList();

            return Json(prolist.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult viewCart()
        {
            int userid = Convert.ToInt16(Session["ID"]);

            List<ViewCart> viewcart = obj.Database.SqlQuery<ViewCart>("select Cart.ID, Cart.CartID, Product.pid, Product.pname, Product.picone, Product.price, Qty, Product.price*Qty as Bill from Product, Cart where Product.pid = Cart.pid and Cart.ID = '" + userid + "'").ToList();

            return Json(new { data = viewcart.ToList() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult orderCart()
        {
            int userid = Convert.ToInt16(Session["ID"]);

            List<ViewCart> viewcart = obj.Database.SqlQuery<ViewCart>("select Cart.ID, Cart.CartID, Product.pid, Product.pname, Product.picone, Product.price, Qty, Product.price*Qty as Bill from Product, Cart where Product.pid = Cart.pid and Cart.ID = '" + userid + "'").ToList();

            return Json(viewcart.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult removeCart(string proid)
        {
            int pid = Convert.ToInt32(proid);

            int userID = Convert.ToInt32(Session["ID"]);

            var cart = obj.Carts.Where(x => x.pid == pid && x.ID == userID).FirstOrDefault();

            obj.Carts.Remove(cart);
            obj.SaveChanges();

            return Json(new { success = true, message = "Product remove from Cart." }, JsonRequestBehavior.AllowGet);
        }
    }
}