//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class saleproduct
    {
        public int sid { get; set; }
        public int pid { get; set; }
        public System.DateTime lastdate { get; set; }
    
        public virtual Product Product { get; set; }
    }
}