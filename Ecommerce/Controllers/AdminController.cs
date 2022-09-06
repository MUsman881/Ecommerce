using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;
using System.IO;
using System.Data;
using System.Web.Security;
using System.Data.Entity;

namespace Ecommerce.Controllers
{
    public class AdminController : Controller
    {
        EcomAspMVCEntities obj = new EcomAspMVCEntities();

        //get category id and name from database
        public JsonResult GetData()
        {
            var catlist = (from Category in obj.Categories.AsEnumerable()
                           select new
                           {
                               catid = Category.catid,
                               catname = Category.catname,

                           }).Distinct().ToList();
            return Json(new { data = catlist }, JsonRequestBehavior.AllowGet);
        }



        //add category in database
        [HttpPost]
        public ActionResult catin(Category caa)
        {
            if (caa.catid == 0)
            {
                obj.Categories.Add(caa);
                obj.SaveChanges();
                return Json(new { success = true, message = "Category Data save" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                obj.Entry(caa).State = System.Data.Entity.EntityState.Modified;
                obj.SaveChanges();
                return Json(new { success = true, message = "Category Data update" }, JsonRequestBehavior.AllowGet);

            }
        }

        //get category and show in product page
        [HttpGet]
        public JsonResult Getcatdata()
        {
            var catlist = (from Category in obj.Categories.AsEnumerable()
                           select new
                           {
                               catid = Category.catid,
                               catname = Category.catname,
                           }).Distinct().ToList();
            return Json(catlist, JsonRequestBehavior.AllowGet);
        }


        //add product in database

        [HttpPost]
        public ActionResult proin(Product pro)
        {


            string fileName = Path.GetFileNameWithoutExtension(pro.imgpath.FileName);
            string extension = Path.GetExtension(pro.imgpath.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;


            string fileNamedes = Path.GetFileNameWithoutExtension(pro.imgpathdes.FileName);
            string extensiondes = Path.GetExtension(pro.imgpathdes.FileName);
            fileNamedes = fileNamedes + DateTime.Now.ToString("yymmssff") + extensiondes;


            pro.picone = @"/images/productimages/" + fileName;
            pro.pictwo = @"/images/productimages/" + fileNamedes;

            pro.imgpathdes.SaveAs(Path.Combine(Server.MapPath("~/images/productimages/"), fileNamedes));
            pro.imgpath.SaveAs(Path.Combine(Server.MapPath("~/images/productimages/"), fileName));

            
            obj.Products.Add(pro);
            obj.SaveChanges();
            // return Json(new { success = true, message = "Product   Data Save" }, JsonRequestBehavior.AllowGet);
            return View("ProductView");

        }



        //get product data from product, category table
        public ActionResult Getproductdata()
        {
            List<productjoin> prolist = obj.Database.SqlQuery<productjoin>("select pid, pname, price, catname, picone, category.catid, pdes from Product, category where product.catid = category.catid order by pid").ToList();

            return Json(new { data = prolist.ToList() }, JsonRequestBehavior.AllowGet);
        }




        //get feature product data
        public ActionResult Getfproductdata()
        {
            List<productjoin> prolist = obj.Database.SqlQuery<productjoin>("select product.pid,pname,price,catname,picone,category.catid,pdes from product,category where product.catid = category.catid and pid NOT IN (Select pid from featureproduct) order by product.pid").ToList();

            return Json(new { data = prolist.ToList() }, JsonRequestBehavior.AllowGet);
        }

        //get data feature products from database
        public ActionResult Getdatafeature()
        {
            List<featurejoin> prolist = obj.Database.SqlQuery<featurejoin>("select product.pid, product.pname, product.picone from product, featureproduct where product.pid=featureproduct.pid order by product.pid").ToList();

            return Json(new { data = prolist.ToList() }, JsonRequestBehavior.AllowGet);
        }


        //remove feature from feature table
        public ActionResult removefeature(string Proid)
        {
            int spid = Convert.ToInt16(Proid);

            var fp = obj.featureproducts.Where(x => x.pid == spid).First();

            obj.featureproducts.Remove(fp);
            obj.SaveChanges();

            return Json(new { success = true, message = "Product " + Proid + " remove from Feature Product" }, JsonRequestBehavior.AllowGet);
        }


        //add product into feature table
        public ActionResult Getfeature(string Proid)
        {
            if (Proid != null)
            {
                featureproduct fp = new featureproduct();
                fp.fid = 0;
                fp.pid = Convert.ToInt16(Proid);

                obj.featureproducts.Add(fp);
                obj.SaveChanges();

                return Json(new { success = true, message = "Product " + Proid + " add into Feature Product" }, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(new { success = true, message = "Product is not add into Feature Product" }, JsonRequestBehavior.AllowGet);
            }
        }


        //add sale product in a container
        public ActionResult saleinhome(saleproduct sp)
        {
            obj.saleproducts.Add(sp);
            obj.SaveChanges();
            return Json(new { success = true, message = "Sale Product Add" }, JsonRequestBehavior.AllowGet);

        }


        //get product data from category and product table and show in table
        public ActionResult Getprotab()
        {

            List<productjoin> prolist = obj.Database.SqlQuery<productjoin>("select product.pid,pname,price,catname,picone,category.catid,pdes from product,category where product.catid=category.catid  and pid NOT IN (Select pid from saleproduct) order by product.pid").ToList();

            return Json(new { data = prolist.ToList() }, JsonRequestBehavior.AllowGet);
        }



        //show sale products in a table
        public ActionResult Getdatasale()
        {
            List<salejoin> salejoin = obj.Database.SqlQuery<salejoin>("select product.pid, product.pname, product.picone, saleproduct.lastdate from product,saleproduct where product.pid = saleproduct.pid order by saleproduct.lastdate").ToList();

            return Json(new { data = salejoin.ToList() }, JsonRequestBehavior.AllowGet);
        }

        //remove sale product from table
        public ActionResult removesale(string Proid)
        {
            int spid = Convert.ToInt16(Proid);
            var search = obj.saleproducts.Where(x => x.pid == spid).First();

            obj.saleproducts.Remove(search);
            obj.SaveChanges();

            return Json(new { success = true, message = "Product " + Proid + " Remove from Sale Product " }, JsonRequestBehavior.AllowGet);

        }

        //get orders info from ordermainn table for datatable
        public JsonResult viewOrders()
        {
            List<Order> vieworder = obj.Database.SqlQuery<Order>("select OrderID, OrderDate, Status, CustomerName,CustomerPhone from ordermain").ToList();

            return Json(new { data = vieworder.ToList() }, JsonRequestBehavior.AllowGet);
        }

        //get orders main info for detail page
        public JsonResult viewOrdermain(string orderID)
        {
            List<Order> vieworder = obj.Database.SqlQuery<Order>("select OrderID, OrderDate, Status, CustomerName,CustomerPhone,CustomerAddress,CustomerCity from ordermain where OrderID='"+ orderID + "'").ToList();

            return Json(vieworder.ToList(), JsonRequestBehavior.AllowGet);
        }

        //get orderdetails info for detail page
        public JsonResult viewOrderDetail(string orderID)
        {
            List<OrderDetails> orderdetail = obj.Database.SqlQuery<OrderDetails>("select orderdetail.pid, product.pname, qty, orderdetail.price, Bill from orderdetail, product where orderdetail.pid = product.pid and orderdetail.OrderID = '" + orderID + "'").ToList();

            return Json(new { data = orderdetail.ToList() }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ChangeStatus(string orderID, string Status)
        {
            int orderid = Convert.ToInt32(orderID);
            ordermain om = obj.ordermains.Where(o => o.OrderID == orderid).FirstOrDefault();

            om.Status = Status;
            obj.Entry(om).State = EntityState.Modified;
            obj.SaveChanges();

            return Json(new { success = true, message = "Order status update" }, JsonRequestBehavior.AllowGet);
        }
        //views
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(admin adm)
        {
            admin ad = obj.admins.Where(x => x.admin_email == adm.admin_email && x.admin_pass == adm.admin_pass).SingleOrDefault();

            if (ad != null)
            {
                Session["id"] = ad.admin_id.ToString();
                Session["email"] = ad.admin_email.ToString();
                Session["name"] = ad.admin_name.ToString();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "Invalid Username or Password!";
            }

            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Login");
        }

        public ActionResult Index()
        {
            if(Session["id"] == null)
            {
                return RedirectToAction("Login");
            }
           return View();
        }

        public ActionResult OrderDetail(int? orderID)
        {
            ViewBag.orderID = orderID;
            return View();
            
        }
        
        public ActionResult FeatureView()
        {
            return View();
        }

        public ActionResult Categoryview()
        {
            return View();
        }
        public ActionResult Productview()
        {
            return View();
        }
        public ActionResult Orderview()
        {
            return View();
        }
        public ActionResult Saleview()
        {
            return View();
        }
        public ActionResult Report()
        {
            return View();
        }
    }

}