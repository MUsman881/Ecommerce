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
    using System.Web;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.Carts = new HashSet<Cart>();
            this.featureproducts = new HashSet<featureproduct>();
            this.orderdetails = new HashSet<orderdetail>();
            this.saleproducts = new HashSet<saleproduct>();
        }
    
        public int pid { get; set; }
        public int catid { get; set; }
        public string pname { get; set; }
        public double price { get; set; }
        public string pdes { get; set; }
        public string picone { get; set; }
        public string pictwo { get; set; }
        public int stock { get; set; }

        public HttpPostedFileBase imgpath { get; set; }
        public HttpPostedFileBase imgpathdes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<featureproduct> featureproducts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<orderdetail> orderdetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saleproduct> saleproducts { get; set; }
    }
}
