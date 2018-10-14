using uStore.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using uStore.DATA.EF;

namespace uStore.UI.MVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        uStoreEntities storeDB = new uStoreEntities();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            var shoppingCart =
                    (Dictionary<int, ShoppingCartViewModel>)Session["cart"];
            if (shoppingCart == null || shoppingCart.Count == 0)
            {
                //new empty shopping cart to pass to the view so it wont fail.
                shoppingCart = new Dictionary<int, ShoppingCartViewModel>();
                ViewBag.Message = "No Items exist in your cart";
            }
            else
            {
                ViewBag.Message = null;
            }

            return View(shoppingCart);
        }

        [HttpPost]
        public ActionResult UpdateCart(int productID, int qty)
        {
            //get the cart from session and hold it in a dictionary
            Dictionary<int, ShoppingCartViewModel> shoppingCart =
                (Dictionary<int, ShoppingCartViewModel>)Session["cart"];

            //update the qty locally - for the item that is selected
            //(row that the update button was clicked in)
            shoppingCart[productID].Qty = qty;

            //return the cart back to session for use
            Session["cart"] = shoppingCart;

            if (shoppingCart.Count == 0)
            {
                ViewBag.Message = "No Items exist in your cart";
            }
            //Reload the Index
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int id)
        {
            //get the cart out of session
            Dictionary<int, ShoppingCartViewModel> shoppingCart =
                (Dictionary<int, ShoppingCartViewModel>)Session["cart"];

            //remove the item
            shoppingCart.Remove(id);

            if (shoppingCart.Count == 0)
            {
                ViewBag.Message = "No Items exist in your cart";
            }

            //reload the index
            return RedirectToAction("Index");
        }
    }
}