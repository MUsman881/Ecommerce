using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public System.DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerAddress { get; set; }
    }
}