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
    public class ExaOrderDetailsController : Controller
    {
        private LineNotify1Entities db = new LineNotify1Entities();

        // GET: ExaOrderDetails
        public ActionResult Index()
        {
            var exaOrderDetails = db.ExaOrderDetails.Include(e => e.ExaOrders);
            return View(exaOrderDetails.ToList());
        }

        // GET: ExaOrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExaOrderDetails exaOrderDetails = db.ExaOrderDetails.Find(id);
            if (exaOrderDetails == null)
            {
                return HttpNotFound();
            }
            return View(exaOrderDetails);
        }

        // GET: ExaOrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.exam_number = new SelectList(db.ExaOrders, "exam_number", "exam_number");
            return View();
        }

        // POST: ExaOrderDetails/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "details_id,exam_number,exam_details")] ExaOrderDetails exaOrderDetails)
        {
            if (ModelState.IsValid)
            {
                db.ExaOrderDetails.Add(exaOrderDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.exam_number = new SelectList(db.ExaOrders, "exam_number", "exam_number", exaOrderDetails.exam_number);
            return View(exaOrderDetails);
        }

        // GET: ExaOrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExaOrderDetails exaOrderDetails = db.ExaOrderDetails.Find(id);
            if (exaOrderDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.exam_number = new SelectList(db.ExaOrders, "exam_number", "exam_number", exaOrderDetails.exam_number);
            return View(exaOrderDetails);
        }

        // POST: ExaOrderDetails/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "details_id,exam_number,exam_details")] ExaOrderDetails exaOrderDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exaOrderDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.exam_number = new SelectList(db.ExaOrders, "exam_number", "exam_number", exaOrderDetails.exam_number);
            return View(exaOrderDetails);
        }

        // GET: ExaOrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExaOrderDetails exaOrderDetails = db.ExaOrderDetails.Find(id);
            if (exaOrderDetails == null)
            {
                return HttpNotFound();
            }
            return View(exaOrderDetails);
        }

        // POST: ExaOrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExaOrderDetails exaOrderDetails = db.ExaOrderDetails.Find(id);
            db.ExaOrderDetails.Remove(exaOrderDetails);
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
