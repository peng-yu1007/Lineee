using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lineee.Models;

namespace Lineee.Controllers
{
    public class ExaOrdersController : Controller
    {
        private LineNotify1Entities db = new LineNotify1Entities();

        // GET: ExaOrders
        public ActionResult Index()
        {
            var exaOrders = db.ExaOrders.Include(e => e.Doctor).Include(e => e.Exam).Include(e => e.Patient);
            return View(exaOrders.ToList());
        }

        // GET: ExaOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExaOrders exaOrders = db.ExaOrders.Find(id);
            if (exaOrders == null)
            {
                return HttpNotFound();
            }
            return View(exaOrders);
        }

        // GET: ExaOrders/Create
        public ActionResult Create(string patient_id)
        {
            ViewBag.doctor_id = new SelectList(db.Doctor, "doctor_id", "doctor_name");
            ViewBag.exam_id = new SelectList(db.Exam, "exam_id", "exam_name");
            ViewBag.patient_id = patient_id;
            return View();
        }

        public ActionResult CreateSave([Bind(Include = "exam_number,patient_id,exam_id,doctor_id,order_date")] ExaOrders exaOrders)
        {
                db.ExaOrders.Add(exaOrders);
                db.SaveChanges();
                return RedirectToAction("Index");
            

        }

        // POST: ExaOrders/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]

        public ActionResult Create(string id_card,[Bind(Include = "exam_number,patient_id,exam_id,doctor_id,order_date")] ExaOrders exaOrders)
        {
          
            ViewBag.doctor_id = new SelectList(db.Doctor, "doctor_id", "doctor_name", exaOrders.doctor_id);
            ViewBag.exam_id = new SelectList(db.Exam, "exam_id", "exam_name", exaOrders.exam_id);
            ViewBag.patient_id = new SelectList(db.Patient,"patient_id","id_card",exaOrders.patient_id);
            ViewBag.id_card = id_card;

            exaOrders.order_date = DateTime.Now.Date;

            return View(exaOrders);
        }

      

        // GET: ExaOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExaOrders exaOrders = db.ExaOrders.Find(id);
            if (exaOrders == null)
            {
                return HttpNotFound();
            }
            ViewBag.doctor_id = new SelectList(db.Doctor, "doctor_id", "doctor_name", exaOrders.doctor_id);
            ViewBag.exam_id = new SelectList(db.Exam, "exam_id", "exam_name", exaOrders.exam_id);
            ViewBag.patient_id = new SelectList(db.Patient, "patient_id", "id_card", exaOrders.patient_id);
            return View(exaOrders);
        }

        // POST: ExaOrders/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "exam_number,exam_id,patient_id,doctor_id,order_date")] ExaOrders exaOrders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exaOrders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.doctor_id = new SelectList(db.Doctor, "doctor_id", "doctor_name", exaOrders.doctor_id);
            ViewBag.exam_id = new SelectList(db.Exam, "exam_id", "exam_name", exaOrders.exam_id);
            ViewBag.patient_id = new SelectList(db.Patient, "patient_id", "id_card", exaOrders.patient_id);
            return View(exaOrders);
        }

        // GET: ExaOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExaOrders exaOrders = db.ExaOrders.Find(id);
            if (exaOrders == null)
            {
                return HttpNotFound();
            }
            return View(exaOrders);
        }

        // POST: ExaOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExaOrders exaOrders = db.ExaOrders.Find(id);
            db.ExaOrders.Remove(exaOrders);
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
