using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebStore.Models
{
    public class ShoppingCartItem
    {
        public int ProductID { get; set; }

        [Display(Name = "Product")]
        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        [Display(Name = "Backorder Status")]
        public bool? onBackOrder { get; set; }


        public ShoppingCartItem(int ProductID, int Quantity, double Price, string ProductName, byte[] Picture, bool BackOrder)
        {
            this.ProductID = ProductID;
            this.Quantity = Quantity;
            this.Price = Price;
            this.ProductName = ProductName;
            this.onBackOrder = BackOrder;
        }
    }
}