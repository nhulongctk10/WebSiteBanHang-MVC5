﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LongKhanhMobile.DAL;
using LongKhanhMobile.Models;
using PagedList;

namespace LongKhanhMobile.Areas.Admin.Controllers
{
    public class CommentsController : Controller
    {
        private ShopDbContext db = new ShopDbContext();

        // GET: Admin/Comments
        public ActionResult Index(int? page, int? pageSize)
        {
            var comments = db.Comments.Include(c => c.Product).Include(c => c.Replier).OrderByDescending(c => c.PostedTime.Value.Year).AsQueryable();

            if (!page.HasValue || page.Value<1)
                page = 1;
            if (!pageSize.HasValue || pageSize < 10)
                pageSize = 10;

            ViewBag.CurrentPageSize = page;
            ViewBag.PageSize = new SelectList(new []{10,20,35,50,100}, pageSize);

            return View(comments.ToPagedList(page.Value,pageSize.Value));
        }

        // GET: Admin/Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Admin/Comments/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name");
            ViewBag.AccountId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Admin/Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentId,FullName,Email,Subject,Content,PostedTime,Actived,Status,ReplyContent,ReplyTime,AccountId,ProductId,RowVersion")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", comment.ProductId);
            ViewBag.AccountId = new SelectList(db.Users, "Id", "Email", comment.AccountId);
            return View(comment);
        }

        // GET: Admin/Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", comment.ProductId);
            ViewBag.AccountId = new SelectList(db.Users, "Id", "Email", comment.AccountId);
            return View(comment);
        }

        // POST: Admin/Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentId,FullName,Email,Subject,Content,PostedTime,Actived,Status,ReplyContent,ReplyTime,AccountId,ProductId,RowVersion")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", comment.ProductId);
            ViewBag.AccountId = new SelectList(db.Users, "Id", "Email", comment.AccountId);
            return View(comment);
        }

        // GET: Admin/Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Admin/Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
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
