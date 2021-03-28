using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreFrontLab.UI.MVC.Models;

namespace StoreFrontLab.UI.MVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
            var shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];

            if (shoppingCart == null || shoppingCart.Count == 0)
            {
                //user either hasn't put anything in the car or has removed all items. Or the session has expired
                //set the cart to an empty object. (Can still send that object to the view, where you can't send a null object to the view)
                shoppingCart = new Dictionary<int, CartItemViewModel>();

                //create a message about the empty cart
                ViewBag.Message = "There are no items currently in your cart. Time to get shopping!";
            }
            else
            {
                ViewBag.Message = null; //explicitly clears out that viewbag variable
            }

            return View(shoppingCart);
        }

        public ActionResult UpdateCart(int productID, int qty)
        {
            //get the cart out of session and into a local variable
            Dictionary<int, CartItemViewModel> shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];

            //next we need to target the correct cartItem using the bookId/Key. 
            shoppingCart[productID].Qty = qty;

            //update session with new cart info
            Session["cart"] = shoppingCart;

            return RedirectToAction("Index");
        }
        
        public ActionResult RemoveFromCart(int id)
        {
            //get access to the cart by unboxing from session
            Dictionary<int, CartItemViewModel> shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];

            shoppingCart.Remove(id);

            //if all items were removed, give user a message

            //This doesn't work because ViewBag Info does not persist through a RedirectToAction
            //Even if it does persist, we reassign ViewBag.Message in the index.
            //if (shoppingCart.Count == 0)
            //{
            //    ViewBag.Message = "You have removed all items from your cart.";
            //}

            Session["cart"] = shoppingCart;

            return RedirectToAction("Index");
        }

    }//end class
}//end namespace