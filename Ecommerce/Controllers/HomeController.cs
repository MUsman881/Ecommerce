using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;

namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        EcomAspMVCEntitie obj = new EcomAspMVCEntitie();

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

        


    }
}