using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LongKhanhMobile.DAL;
using LongKhanhMobile.Models;

namespace LongKhanhMobile.Controllers
{
    public class ProductsController : Controller
    {
        private ShopDbContext db = new ShopDbContext();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.ProductProfiles).Include(p => p.Supplier);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult ProductDetail(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Product product = db.Products.Find(id);
            //if (product == null)
            //{
            //    return HttpNotFound();
            //}
            return View(new Product());
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.ProductProfiles, "ProductProfileId", "ProductProfileId");
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,Name,Alias,Thumbnail,ProductCode,ShortIntro,Description,QtyPerUnit,Price,Quantity,Discount,SupplierId,Actived,RowVersion")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.ProductProfiles, "ProductProfileId", "ProductProfileId", product.ProductId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "Name", product.SupplierId);
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
            ViewBag.ProductId = new SelectList(db.ProductProfiles, "ProductProfileId", "ProductProfileId", product.ProductId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "Name", product.SupplierId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,Name,Alias,Thumbnail,ProductCode,ShortIntro,Description,QtyPerUnit,Price,Quantity,Discount,SupplierId,Actived,RowVersion")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.ProductProfiles, "ProductProfileId", "ProductProfileId", product.ProductId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "Name", product.SupplierId);
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
