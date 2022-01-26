using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AdvWorksPL.Models
{
    public class ProductModel
    {
        [DisplayName("Product Name")]
        public string ProdName { get; set; }
        [DisplayName("Product Number")]
        public string ProdNum { get; set; }
        [DisplayName("List Price")]
        public decimal ProdListPrice { get; set; }
    }
}