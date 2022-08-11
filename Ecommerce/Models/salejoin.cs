using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class salejoin
    {
        public int pid { get; set; }
        public string pname { get; set; }
        public double price { get; set; }
        public string picone { get; set; }

        public System.DateTime lastdate { get; set; }
    }
}