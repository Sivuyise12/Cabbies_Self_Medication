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
using Microsoft.AspNet.Identity;

namespace ImfuyoRanch.Controllers
{
    public class AssignEmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Order_Service order_Service;

        public AssignEmployeesController()
        {
            this.order_Service = new Order_Service();
        }
        // GET: AssignEmployees
        public ActionResult Index()
        {
            var assignEmployees = db.AssignEmployees.Include(a => a.Employee).Include(a => a.Order);
            var userName = User.Identity.GetUserName();
            if (User.IsInRole("Admin"))
            {
                return View(assignEmployees.ToList());
            }
            else
            {
                return View(assignEmployees.ToList().Where(x=>x.Employee.EmployeeEmail==userName));
            }
           
        }

        // GET: AssignEmployees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignEmployee assignEmployee = db.AssignEmployees.Find(id);
            if (assignEmployee == null)
            {
                return HttpNotFound();
            }
            return View(assignEmployee);
        }

        // GET: AssignEmployees/Create
        public ActionResult Create(string id)
        {
            Session["OrderId"] = id;
            ViewBag.EmployeeId = new SelectList(db.Employees.Where(x=>x.EmployeeType=="Driver"), "EmployeeId", "EmployeeName");
            ViewBag.Order_ID = new SelectList(db.Orders, "Order_ID", "Email");
            return View();
        }

        // POST: AssignEmployees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssignEmployeeId,EmployeeId,Order_ID,DateAssigned,Status")] AssignEmployee assignEmployee)
        {
            if (ModelState.IsValid)
            {
                assignEmployee.Order_ID = Session["OrderId"].ToString();
                if (ImfuyoRanchLogic.ImfuyoLogic.CheckOrderIfIsAssigned(assignEmployee.Order_ID))
                {
                    assignEmployee.DateAssigned = DateTime.Now;
                    assignEmployee.Status = "Assigned";
                    db.AssignEmployees.Add(assignEmployee);
                    db.SaveChanges();
                    order_Service.AssignToDriver(assignEmployee.Order_ID);
                    EmailSender.sendDriverEmail(assignEmployee);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Order Already Assigned To Driver");
                    ViewBag.EmployeeId = new SelectList(db.Employees.Where(x => x.EmployeeType == "Driver"), "EmployeeId", "EmployeeName", assignEmployee.EmployeeId);
                    ViewBag.Order_ID = new SelectList(db.Orders, "Order_ID", "Email", assignEmployee.Order_ID);
                    return View(assignEmployee);
                }
            }

            ViewBag.EmployeeId = new SelectList(db.Employees.Where(x => x.EmployeeType == "Driver"), "EmployeeId", "EmployeeName", assignEmployee.EmployeeId);
            ViewBag.Order_ID = new SelectList(db.Orders, "Order_ID", "Email", assignEmployee.Order_ID);
            return View(assignEmployee);
        }

        // GET: AssignEmployees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignEmployee assignEmployee = db.AssignEmployees.Find(id);
            if (assignEmployee == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees.Where(x => x.EmployeeType == "Driver"), "EmployeeId", "EmployeeName", assignEmployee.EmployeeId);
            ViewBag.Order_ID = new SelectList(db.Orders, "Order_ID", "Email", assignEmployee.Order_ID);
            return View(assignEmployee);
        }

        // POST: AssignEmployees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssignEmployeeId,EmployeeId,Order_ID,DateAssigned,Status")] AssignEmployee assignEmployee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignEmployee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees.Where(x => x.EmployeeType == "Driver"), "EmployeeId", "EmployeeName", assignEmployee.EmployeeId);
            ViewBag.Order_ID = new SelectList(db.Orders, "Order_ID", "Email", assignEmployee.Order_ID);
            return View(assignEmployee);
        }

        // GET: AssignEmployees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignEmployee assignEmployee = db.AssignEmployees.Find(id);
            if (assignEmployee == null)
            {
                return HttpNotFound();
            }
            return View(assignEmployee);
        }

        // POST: AssignEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssignEmployee assignEmployee = db.AssignEmployees.Find(id);
            db.AssignEmployees.Remove(assignEmployee);
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
