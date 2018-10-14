using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using uStore.DATA.EF;

namespace uStore.UI.MVC.Controllers
{
    public class WishlistController : Controller
    {
        private uStoreEntities db = new uStoreEntities();
        public ActionResult Index()
        {
            var userid = User.Identity.GetUserId();
            var wishlist = db.WishlistItems
                .Where(w => w.UserID == userid)
                .ToList();
            return View(wishlist);
            
        }
        public ActionResult Add(int id)
        {
            WishlistItem item = new WishlistItem();
            item.ProductID = id;
            item.UserID = User.Identity.GetUserId();
            item.DateAdded = DateTime.Now;

            db.WishlistItems.Add(item);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var product = db.WishlistItems.Find(id);
            db.WishlistItems.Remove(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
