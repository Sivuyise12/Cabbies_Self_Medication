using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ImfuyoRanch.Models;
using Microsoft.AspNet.Identity;
using Shopping;

namespace ImfuyoRanch.Controllers
{
    public class ItemRatingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ItemRatings
        public ActionResult Index(int? id = null)
        {

            var itemRatings = db.ItemRatings.Include(i => i.Item);
            if (id == null)
            {
                return View(itemRatings.ToList());
            }
            else{
                return View(itemRatings.ToList().Where(x=>x.ItemCode==id).ToList());
            }
           
        }

        // GET: ItemRatings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemRating itemRating = db.ItemRatings.Find(id);
            if (itemRating == null)
            {
                return HttpNotFound();
            }
            return View(itemRating);
        }

        // GET: ItemRatings/Create
        public ActionResult Create(int id, string orderID)
        {
            Session["id"] = id;
            Session["orderID"] = orderID;
            ViewBag.ItemCode = new SelectList(db.Items, "ItemCode", "Name");
            return View();
        }

        // POST: ItemRatings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemRatingID,CustomerEmail,ItemCode,Comment,Rating")] ItemRating itemRating)
        {
            var id = Convert.ToInt32(Session["id"]);
            var orderID = Session["orderID"].ToString();
            Item item = db.Items.Find(id);
            if (ModelState.IsValid)
            {
                if (itemRating.Rating > 0)
                {
                    if (itemRating.Rating > 10)
                    {
                        itemRating.Rating = 10;
                    }
                    itemRating.ItemCode = id;
                    itemRating.CustomerEmail = User.Identity.GetUserName();
                    item.ReviewId += 1;
                    db.ItemRatings.Add(itemRating);
                    db.SaveChanges();
                    return RedirectToAction("Order_Details", "Orders", new { id = orderID });
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Rating ");
                    ViewBag.ItemCode = new SelectList(db.Items, "ItemCode", "Name", itemRating.ItemCode);
                    return View(itemRating);
                }
            }
            ViewBag.ItemCode = new SelectList(db.Items, "ItemCode", "Name", itemRating.ItemCode);
            return View(itemRating);

        }

        // GET: ItemRatings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemRating itemRating = db.ItemRatings.Find(id);
            if (itemRating == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemCode = new SelectList(db.Items, "ItemCode", "Name", itemRating.ItemCode);
            return View(itemRating);
        }

        // POST: ItemRatings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemRatingID,CustomerEmail,ItemCode,Comment,Rating")] ItemRating itemRating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemRating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemCode = new SelectList(db.Items, "ItemCode", "Name", itemRating.ItemCode);
            return View(itemRating);
        }

        // GET: ItemRatings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemRating itemRating = db.ItemRatings.Find(id);
            if (itemRating == null)
            {
                return HttpNotFound();
            }
            return View(itemRating);
        }

        // POST: ItemRatings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemRating itemRating = db.ItemRatings.Find(id);
            db.ItemRatings.Remove(itemRating);
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
