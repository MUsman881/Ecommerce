using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;
using Newtonsoft.Json;


namespace Ecommerce.Controllers
{
    public class ShopController : Controller
    {
        EcomAspMVCEntitie obj = new EcomAspMVCEntitie();
        // GET: Shop
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ordersuccess(Order oc)
        {
            string unid = DateTime.Now.ToString("hhmmssff");
            string date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");

            ordermain om = new ordermain()
            {
                OrderID = Convert.ToInt32(unid),
                OrderDate = Convert.ToDateTime(date),
                Status = "processing",
                CustomerName = oc.CustomerName,
                CustomerPhone = oc.CustomerPhone,
                CustomerCity = oc.CustomerCity,
                CustomerAddress = oc.CustomerAddress
            };
            
            obj.ordermains.Add(om);
            obj.SaveChanges();

            var CartProductCookie = Request.Cookies["CartProducts"];

            if (CartProductCookie != null)
            {
                var productsIDS = CartProductCookie.Value;
            }

            return View();

        }

        public ActionResult Checkout()
        {
            return View();
        }


        public ActionResult ProductDetails(int? pid)
        {
            ViewBag.pid = pid;
            return View();
        }


        public ActionResult viewdetail(string proid)
        {
            
            List<productdetail> prolist = obj.Database.SqlQuery<productdetail>("select pid, pname, price, pdes ,picone, pictwo from product where product.pid ='"+proid+"'").ToList();

            return Json(prolist.ToList(), JsonRequestBehavior.AllowGet);

        }
        public ActionResult allpro()
        {

            List<salejoin> prolist = obj.Database.SqlQuery<salejoin>("select product.pid, product.pname, product.picone, product.price from Product").ToList();

            return Json(prolist.ToList(), JsonRequestBehavior.AllowGet);
        }

        
    }
}