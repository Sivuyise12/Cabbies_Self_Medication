using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ImfuyoRanch.Models;
using ImfuyoRanch.Services;

namespace ImfuyoRanch.Controllers
{
    public class OrderReportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Order_Service order_Service;

        public OrderReportsController()
        {
            this.order_Service = new Order_Service();
        }
        // GET: OrderReports
        public ActionResult Index()
        {
            return View(db.OrderReports.ToList().OrderByDescending(x=>x.OrderReportId));
        }

        public ActionResult Order_Details(string id)
        {
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (order_Service.GetOrder(id) != null)
                return View(order_Service.GetOrderDetail(id));
            else
                return RedirectToAction("Not_Found", "Error");
        }

        // GET: OrderReports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderReports orderReports = db.OrderReports.Find(id);
            if (orderReports == null)
            {
                return HttpNotFound();
            }
            return View(orderReports);
        }

        // GET: OrderReports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderReportId,OrderId,DateCreated,OrderAmount,CustomerEmail")] OrderReports orderReports)
        {
            if (ModelState.IsValid)
            {
                db.OrderReports.Add(orderReports);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orderReports);
        }

        // GET: OrderReports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderReports orderReports = db.OrderReports.Find(id);
            if (orderReports == null)
            {
                return HttpNotFound();
            }
            return View(orderReports);
        }

        // POST: OrderReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderReportId,OrderId,DateCreated,OrderAmount,CustomerEmail")] OrderReports orderReports)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderReports).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderReports);
        }

        // GET: OrderReports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderReports orderReports = db.OrderReports.Find(id);
            if (orderReports == null)
            {
                return HttpNotFound();
            }
            return View(orderReports);
        }

        // POST: OrderReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderReports orderReports = db.OrderReports.Find(id);
            db.OrderReports.Remove(orderReports);
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
