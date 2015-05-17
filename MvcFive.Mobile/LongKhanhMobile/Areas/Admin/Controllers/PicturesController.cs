using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LongKhanhMobile.DAL;
using LongKhanhMobile.Models;
using PagedList;

namespace LongKhanhMobile.Areas.Admin.Controllers
{
    public class PicturesController : Controller
    {
        private ShopDbContext db = new ShopDbContext();

        // GET: Admin/Pictures
        public ActionResult Index(int? page, int? pageSize)
        {
            var pictures = db.Pictures.Include(p => p.Product).AsQueryable();

            if (!page.HasValue || page.Value < 1)
                page = 1;
            if (!pageSize.HasValue || page.Value < 5)
                pageSize = 10;

            ViewBag.PageSize = new SelectList(new[] { 10,20,25,50,100 }, pageSize);
            ViewBag.CurrentPageSize = pageSize;

            var data = pictures.OrderBy(x => x.Caption).ToPagedList(page.Value, pageSize.Value);
            return View(data);
        }

        // GET: Admin/Pictures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picture picture = db.Pictures.Find(id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            return View(picture);
        }

        // GET: Admin/Pictures/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name");
            return View();
        }

        // POST: Admin/Pictures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Picture picture,HttpPostedFileBase upload)
        {
            string imagePath = SaveUploadFile(upload,null);
            if (imagePath == null)
                ModelState.AddModelError("upload image","Bạn chưa chọn hình ảnh nào");

            if (ModelState.IsValid)
            {
                picture.Path = imagePath;

                db.Pictures.Add(picture);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", picture.ProductId);
            return View(picture);
        }

        private string SaveUploadFile(HttpPostedFileBase upload, string oldFilePath)
        {
            if (upload != null && upload.ContentLength > 0)
            {
                var targetFolder = Server.MapPath("~/ProductImages");

                var uniqueFileName = DateTime.Now.Ticks + "_" + upload.FileName;
                var targetFilePath = Path.Combine(targetFolder, uniqueFileName);

                upload.SaveAs(targetFilePath);

                if (!string.IsNullOrEmpty(oldFilePath))
                {
                    oldFilePath = Server.MapPath(oldFilePath);
                    System.IO.File.Delete(oldFilePath);
                }

                return Path.Combine("~/ProductImages", uniqueFileName);
            }
            return oldFilePath;
        }

        // GET: Admin/Pictures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picture picture = db.Pictures.Find(id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", picture.ProductId);
            return View(picture);
        }

        // POST: Admin/Pictures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PictureId,Caption,Path,OrderNo,Actived,ProductId")] Picture picture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(picture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", picture.ProductId);
            return View(picture);
        }

        // GET: Admin/Pictures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picture picture = db.Pictures.Find(id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            return View(picture);
        }

        // POST: Admin/Pictures/Delete/5
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                Picture pt = db.Pictures.Find(id);
                db.Pictures.Remove(pt);
                db.SaveChanges();
                return Json(new
                {
                    success = true,
                    redirectUrl = Url.Action("Index", "Pictures")
                });
            }
            catch (Exception)
            {
                return Json(false);
            }
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
