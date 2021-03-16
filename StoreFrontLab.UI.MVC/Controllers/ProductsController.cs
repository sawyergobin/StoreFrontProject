using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFrontLab.DATA.EF;
using StoreFrontLab.UI.MVC.Utilities;

namespace StoreFrontLab.UI.MVC.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private StoreFrontEntities db = new StoreFrontEntities();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.ProductStatus);
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
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.ProductStatusID = new SelectList(db.ProductStatuses, "ProductStatusID", "StatusName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Name,Description,CategoryID,ProductStatusID,CalorieCount,Price,IsOnSale,DiscountPercentage,ProductImage")] Product product, HttpPostedFileBase productImage)
        {
            if (ModelState.IsValid)
            {

                #region File Upload
                //we'll use this file if the user does not upload an image
                string file = "NoImage.jpg";

                if (productImage != null)
                {
                    file = productImage.FileName;
                    string ext = file.Substring(file.LastIndexOf('.')); //Remember only 1 param in substring means start there and go to the end of the string
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };

                    //check that the uploaded file ext is in our approved list
                    //Check that the file is less than 4mb (the default allowed file size by .NET)
                    if (goodExts.Contains(ext.ToLower()) && productImage.ContentLength <= 4194304) // this is the number of bytes in 4mb
                    {
                        //create a new filename with a GUID
                        file = Guid.NewGuid() + ext;

                        #region Resize Image
                        string savePath = Server.MapPath("~/Content/img/product/"); //Ensure the ending slash here to place it IN the books folder
                        Image convertedImage = Image.FromStream(productImage.InputStream);

                        int maxImageSize = 500; //full size image WIDTH
                        int maxThumbSize = 100; //thumbnail size image WIDTH

                        ImageService.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);


                        #endregion
                    }

                }//end productImage != null
                //no matter what, update the PhotoURL with the value of the file variable. This happens even if they don't upload an image. Grabs the NoImage.png
                product.ProductImage = file;
                #endregion


                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
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
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.ProductStatusID = new SelectList(db.ProductStatuses, "ProductStatusID", "StatusName", product.ProductStatusID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Name,Description,CategoryID,ProductStatusID,CalorieCount,Price,IsOnSale,DiscountPercentage,ProductImage")] Product product, HttpPostedFileBase productImage)
        {
            if (ModelState.IsValid)
            {

                #region File Upload
                string file = product.ProductImage;

                if (productImage != null)
                {
                    file = productImage.FileName;

                    string ext = file.Substring(file.LastIndexOf('.'));

                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };
                    if (goodExts.Contains(ext.ToLower()) && productImage.ContentLength <= 4194304)
                    {
                        file = Guid.NewGuid() + ext;

                        #region Resize Image
                        string savePath = Server.MapPath("~/Content/img/product/");

                        //making an Image Type object from a stream of bites that are coming from the HttpPostedFileBase object
                        //HttpPostedFileBase productImage -> bytes(stream) -> Image -> convertedImage
                        Image convertedImage = Image.FromStream(productImage.InputStream);

                        int maxImageSize = 500;
                        int maxThumbSize = 100;

                        ImageService.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                        #endregion

                        if (product.ProductImage != null && product.ProductImage != "NoImage.jpg")
                        {
                            ImageService.Delete(savePath, product.ProductImage);
                        }
                        product.ProductImage = file;
                    }//end image file is all good

                }//end productImage != null

                #endregion


                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
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

            string path = Server.MapPath("~/Content/img/product/");
            ImageService.Delete(path, product.ProductImage);

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
