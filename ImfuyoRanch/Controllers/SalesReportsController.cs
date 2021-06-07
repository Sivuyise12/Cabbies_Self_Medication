using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ImfuyoRanch.Models;

namespace ImfuyoRanch.Controllers
{
    public class SalesReportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SalesReports
        public ActionResult Index()
        {
            var salesReports = db.SalesReports.Include(s => s.Item);
            return View(salesReports.ToList());
        }

        // GET: SalesReports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesReport salesReport = db.SalesReports.Find(id);
            if (salesReport == null)
            {
                return HttpNotFound();
            }
            return View(salesReport);
        }

        // GET: SalesReports/Create
        public ActionResult Create()
        {
            ViewBag.ItemCode = new SelectList(db.Items, "ItemCode", "Name");
            return View();
        }

        // POST: SalesReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalesReportId,ItemCode,NumberOfOrders")] SalesReport salesReport)
        {
            if (ModelState.IsValid)
            {
                db.SalesReports.Add(salesReport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemCode = new SelectList(db.Items, "ItemCode", "Name", salesReport.ItemCode);
            return View(salesReport);
        }

        // GET: SalesReports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesReport salesReport = db.SalesReports.Find(id);
            if (salesReport == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemCode = new SelectList(db.Items, "ItemCode", "Name", salesReport.ItemCode);
            return View(salesReport);
        }

        // POST: SalesReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalesReportId,ItemCode,NumberOfOrders")] SalesReport salesReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesReport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemCode = new SelectList(db.Items, "ItemCode", "Name", salesReport.ItemCode);
            return View(salesReport);
        }

        // GET: SalesReports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesReport salesReport = db.SalesReports.Find(id);
            if (salesReport == null)
            {
                return HttpNotFound();
            }
            return View(salesReport);
        }

        // POST: SalesReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalesReport salesReport = db.SalesReports.Find(id);
            db.SalesReports.Remove(salesReport);
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
