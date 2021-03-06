using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ImfuyoRanch.Models;
using ImfuyoRanch.Models.Equipment_Hire;

namespace ImfuyoRanch.Controllers
{
    public class EquipmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Equipments
        public ActionResult Index()
        {
            var equipments = db.Equipments.Include(e => e.EquipmentType);
            return View(equipments.ToList());
        }
           public ActionResult IndexView(int id)
        {
            var equipments = db.Equipments.Include(e => e.EquipmentType);
            return View(equipments.ToList().Where(x=>x.EquipmentTypeId==id));
        }

        // GET: Equipments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment equipment = db.Equipments.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        // GET: Equipments/Create
        public ActionResult Create()
        {
            ViewBag.EquipmentTypeId = new SelectList(db.EquipmentTypes, "EquipmentTypeId", "EquipementTypeName");
            return View();
        }

        // POST: Equipments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EquipmentId,EquipmentTypeId,EquipmentName,Picture,HirePrice,Status")] Equipment equipment, HttpPostedFileBase photoUpload)
        {
            if (ModelState.IsValid)
            {
                byte[] photo = null;
                photo = new byte[photoUpload.ContentLength];
                photoUpload.InputStream.Read(photo, 0, photoUpload.ContentLength);
                equipment.Picture = photo;
                equipment.Status = "Available";
                db.Equipments.Add(equipment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EquipmentTypeId = new SelectList(db.EquipmentTypes, "EquipmentTypeId", "EquipementTypeName", equipment.EquipmentTypeId);
            return View(equipment);
        }

        // GET: Equipments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment equipment = db.Equipments.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            ViewBag.EquipmentTypeId = new SelectList(db.EquipmentTypes, "EquipmentTypeId", "EquipementTypeName", equipment.EquipmentTypeId);
            return View(equipment);
        }

        // POST: Equipments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EquipmentId,EquipmentTypeId,EquipmentName,Picture,HirePrice,Status")] Equipment equipment, HttpPostedFileBase photoUpload)
        {
            if (ModelState.IsValid)
            {
                byte[] photo = null;
                photo = new byte[photoUpload.ContentLength];
                photoUpload.InputStream.Read(photo, 0, photoUpload.ContentLength);
                equipment.Picture = photo;
                db.Entry(equipment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EquipmentTypeId = new SelectList(db.EquipmentTypes, "EquipmentTypeId", "EquipementTypeName", equipment.EquipmentTypeId);
            return View(equipment);
        }

        // GET: Equipments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment equipment = db.Equipments.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        // POST: Equipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Equipment equipment = db.Equipments.Find(id);
            db.Equipments.Remove(equipment);
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
