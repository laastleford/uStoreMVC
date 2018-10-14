using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using uStore.DATA.EF;

namespace uStore.UI.MVC.Models
{
    public class ShoppingCartViewModel
    {
       
        [Range(1, int.MaxValue)]
        public int Qty { get; set; }
       
        public Product Product { get; set; }

        public ShoppingCartViewModel(int qty, Product product)
        {
            Qty = qty;
            Product = product;
        }
    }
}