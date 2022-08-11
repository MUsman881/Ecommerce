using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class productjoin
    {
        public int pid { get; set; }
        public int catid { get; set; }
        public string pname { get; set; }
        public double price { get; set; }
        public string pdes { get; set; }
        public string picone { get; set; }
        public string pictwo { get; set; }
        public int stock { get; set; }
        public string catname { get; set; }

    }
}