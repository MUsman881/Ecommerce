using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class OrderController : Controller
    {
        EcomAspMVCEntities obj = new EcomAspMVCEntities();
        // GET: Order

        [HttpPost]
        public ActionResult ProcessOrder(Order oc)
        {
            string orderid = DateTime.Now.ToString("hhmmssff");
            string date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");
            int userID = Convert.ToInt32(Session["ID"]);
            
           List<Cart> cart = obj.Carts.Where(c => c.ID == userID).ToList();

            //1. save the order in Order main table
            ordermain om = new ordermain()
            {
                OrderID = Convert.ToInt32(orderid),
                OrderDate = Convert.ToDateTime(date),
                Status = "processing",
                CustomerName = oc.CustomerName,
                CustomerPhone = oc.CustomerPhone,
                CustomerCity = oc.CustomerCity,
                CustomerAddress = oc.CustomerAddress,
                EmailAddress = oc.EmailAddress
            };
            obj.ordermains.Add(om);
            obj.SaveChanges();

            //2. save the order details in Order detail table
            foreach (var carts in cart)
            {
                orderdetail od = new orderdetail()
                {
                    OrderID = om.OrderID,
                    pid = carts.pid,
                    qty = carts.Qty,
                    price = carts.Product.price,
                    Bill = carts.Qty * carts.Product.price
                };
                obj.orderdetails.Add(od);
                obj.SaveChanges();
            }

            //3. Remove cart products from Cart table
            obj.Carts.RemoveRange(cart);
            obj.SaveChanges();
            return View("OrderSuccess");
        }

        public ActionResult OrderSuccess()
        {
            return View();
        }

    }
}