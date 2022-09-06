using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class ViewCart
    {
        public int pid { get; set; }
        public int ID { get; set; }
        public string pname { get; set; }
        public double price { get; set; }
        public string picone { get; set; }
        public int Qty { get; set; }
        public double Bill { get; set; }

    }
}