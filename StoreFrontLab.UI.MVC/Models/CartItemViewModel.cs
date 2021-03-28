using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreFrontLab.DATA.EF; //Allows access to domain models
using System.ComponentModel.DataAnnotations; //allows validation/display metadata annotations
 
namespace StoreFrontLab.UI.MVC.Models
{
    public class CartItemViewModel
    {
        [Range(1, int.MaxValue)] //ensures our cart values are greater than 0 and can't break the int datatype
        public int Qty { get; set; }

        public Product VendingItem { get; set; }

        public CartItemViewModel(int qty, Product vendingItem)
        {
            Qty = qty;
            VendingItem = vendingItem;
        }

    }
}