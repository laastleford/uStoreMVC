using uStore.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using uStore.DATA.EF;
using uStore.DATA.EF.Services;


namespace uStore.UI.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private uStoreEntities db = new uStoreEntities();


        #region Add To Cart
        [HttpPost]
        public ActionResult AddToCart(int qty, int productID)
        {
            //Create the Shell Local Shopping Cart
            Dictionary<int, ShoppingCartViewModel> shoppingCart = null;

            //Check the global shopping cart
            if (Session["cart"] != null)
            {
                //if it has stuff in it, reassign to the local
                shoppingCart = (Dictionary<int, ShoppingCartViewModel>)Session["cart"];
            }
            else
            {
                //create an empty Local Version
                shoppingCart = new Dictionary<int, ShoppingCartViewModel>();
            }

            //get the product being displayed in the view
            Product product = db.Products.Where(x => x.ProductID == productID).FirstOrDefault();
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                //title is valid
                ShoppingCartViewModel item = new ShoppingCartViewModel(qty, product);
                //if the item is already in the cart just increase the qty
                if (shoppingCart.ContainsKey(product.ProductID))
                {
                    shoppingCart[product.ProductID].Qty += qty;
                }
                else //add the item to the cart
                {
                    shoppingCart.Add(product.ProductID, item);
                }
                //now that the item has been added to the local cart, 
                //update the session cart with the new item/qty
                Session["cart"] = shoppingCart;

                Session["confirm"] = string.Format($"{qty} {product.ProductName} " +
                       $"{((qty > 1) ? "were" : "was")} added to your cart.");
            }

            return RedirectToAction("Index", "ShoppingCart");
        }

        #endregion      

        // GET: Products
        public ActionResult Index(string searchTerm)
        {
            if (!String.IsNullOrEmpty(searchTerm))
            {
                var filteredProducts = db.Products.Where(x => x.ProductName.ToLower().Contains(searchTerm.ToLower()) || x.ProductDescription.ToLower().Contains(searchTerm.ToLower()));
            return View(filteredProducts.ToList());
                    }
            var products = db.Products.Include(p => p.ProductStatus);
            return View(products.ToList());
        }

        
        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.ProductStatusID = new SelectList(db.ProductStatuses, "ProductStatusID", "StatusName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,ProductDescription,Price,UnitsInStock,ProductImage,ProductStatusID")] Product product, HttpPostedFileBase imageUpload)
        {
            if (ModelState.IsValid)
            {
                #region imageUpload using Image Service

                //string for default file name
                string fileName = "NoPicAvailable.png";
                if (imageUpload != null)
                {
                    //process image
                    fileName = imageUpload.FileName;
                    //get ext
                    string ext = fileName.Substring(fileName.LastIndexOf("."));
                    //list allowed exts
                    string[] goodExts = { ".jpg", ".jpeg", ".png", ".gif" };
                    //check exts
                    if (goodExts.Contains(ext.ToLower()))
                    {
                        //assign GUID for filename
                        fileName = Guid.NewGuid() + ext;

                        //************************************Image Service Prep to resize image params

                        string savePath = Server.MapPath("~/Content/images/");
                        //convert the uploaded file from HttpPostedFilebase to Image
                        Image convertedImage = Image.FromStream(imageUpload.InputStream);

                        //maxsize
                        int maxSize = 800;
                        //maxthumb
                        int maxThumb = 250;

                        //call ResizeImage() from the class
                        ImageServices.ResizeImage(savePath, fileName, convertedImage, maxSize, maxThumb);
                    }


                }
                //regardless of wether was an upload, update the productimage property of the new record
                product.ProductImage = fileName;
                #endregion

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductStatusID = new SelectList(db.ProductStatuses, "ProductStatusID", "StatusName", product.ProductStatusID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductStatusID = new SelectList(db.ProductStatuses, "ProductStatusID", "StatusName", product.ProductStatusID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,ProductDescription,Price,UnitsInStock,ProductImage,ProductStatusID")] Product product, HttpPostedFileBase imageUpload)
        {
          
                if (ModelState.IsValid)
                {
                    #region imageUpload using Image Service

                    //string for default file name
                    string fileName = "NoPicAvailable.png";
                    if (imageUpload != null)
                    {
                        //process image
                        fileName = imageUpload.FileName;
                        //get ext
                        string ext = fileName.Substring(fileName.LastIndexOf("."));
                        //list allowed exts
                        string[] goodExts = { ".jpg", ".jpeg", ".png", ".gif" };
                        //check exts
                        if (goodExts.Contains(ext.ToLower()))
                        {
                            //assign GUID for filename
                            fileName = Guid.NewGuid() + ext;

                            //************************************Image Service Prep to resize image params

                            string savePath = Server.MapPath("~/Content/images/");
                            //convert the uploaded file from HttpPostedFilebase to Image
                            Image convertedImage = Image.FromStream(imageUpload.InputStream);

                            //maxsize
                            int maxSize = 500;
                            //maxthumb
                            int maxThumb = 100;

                            //call ResizeImage() from the class
                            ImageServices.ResizeImage(savePath, fileName, convertedImage, maxSize, maxThumb);
                        }


                    }
                    //regardless of wether was an upload, update the productimage property of the new record
                    product.ProductImage = fileName;
                    #endregion

                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.ProductStatusID = new SelectList(db.ProductStatuses, "ProductStatusID", "StatusName", product.ProductStatusID);
                return View(product);
            }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
