using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class OrderDetails
    {
        public int pid { get; set; }
        public string pname { get; set; }
        public int qty { get; set; }
        public double price { get; set; }
        public double Bill { get; set; }
    }
}