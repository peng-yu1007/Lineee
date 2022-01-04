using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Lineee.Models;
using Newtonsoft.Json;
using RestSharp;

namespace Lineee.Controllers
{
    public class ReportsController : Controller
    {
        private LineNotify1Entities db = new LineNotify1Entities();

        // GET: Reports
        public ActionResult Index()
        {
            var report = db.Report.Include(r => r.ExaOrders);
            return View(report.ToList());
        }

        // GET: Reports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Report.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // GET: Reports/Create
        public ActionResult Create(String exam_number)
        {
            ViewBag.exam_number = exam_number;
            return View();
        }

        public ActionResult CreateSave([Bind(Include = "report_id,exam_number,exam_name,exam_details,exam_value,report_date")] Report report)
        {
                db.Report.Add(report);
                db.SaveChanges();
                return RedirectToAction("Index");

        }

        // POST: Reports/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        public ActionResult Create([Bind(Include = "report_id,exam_number,exam_name,exam_details,exam_value,report_date,exam_id")] Report report)
        {
            ViewBag.exam_number = new SelectList(db.ExaOrders, "exam_number", "exam_number", report.exam_number);

            Exam exam = db.Exam.Find(report.exam_id);
            report.exam_name = exam.exam_name;
            ViewBag.exam_name = new SelectList(db.ExaOrders, "exam_name", "exam_name", report.exam_name);

            report.report_date = DateTime.Now.Date;

            return View(report);
        }

        // GET: Reports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Report.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            ViewBag.exam_number = new SelectList(db.ExaOrders, "exam_number", "exam_number", report.exam_number);
            return View(report);
        }

        // POST: Reports/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "report_id,exam_number,exam_name,exam_details,exam_value,report_date")] Report report)
        {
            if (ModelState.IsValid)
            {
                db.Entry(report).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.exam_number = new SelectList(db.ExaOrders, "exam_number", "exam_number", report.exam_number);
            return View(report);
        }

        // GET: Reports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Report.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Report report = db.Report.Find(id);
            db.Report.Remove(report);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Reports/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        public ActionResult SendLine([Bind(Include = "path")] string path)
        {

            var teststr = path.ToString();

            //      $.post('https://script.google.com/macros/s/AKfycbyB2u5E72rhN3YcBzjDravC7wgMp1M-DK1ZYpoIkt10jAKafj-rZ-t7tAB8TXsr4TM/exec',
            //    { msg: '哈囉!!!' },
            //    function(e) {
            //    console.log(e);
            //});


            //content.Add(new StringContent("key1"), "value1", "file1.txt");
            //content.Add(new StringContent("key2"), "value2", "file2.txt");

            //var req = new HttpClient();
            //req.PostAsync("http://localhost:50640/api/multipart", content).Wait();

            var client = new RestClient("https://script.google.com/macros/s/AKfycbyB2u5E72rhN3YcBzjDravC7wgMp1M-DK1ZYpoIkt10jAKafj-rZ-t7tAB8TXsr4TM/exec");
            client.Timeout = 5000;
            var request = new RestRequest(Method.POST);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("msg", path);
            IRestResponse response = client.Execute(request);

            //using (var httpClient = new HttpClient())
            //{
            //    // 改Form-data
            //    string param = JsonConvert.SerializeObject(new { msg = test });
            //    var contentPost = new StringContent(param, Encoding.UTF8, "multipart/form-data");
            //    var path = new Uri($"https://script.google.com/macros/s/AKfycbyB2u5E72rhN3YcBzjDravC7wgMp1M-DK1ZYpoIkt10jAKafj-rZ-t7tAB8TXsr4TM/exec");
            //    var result = httpClient.PostAsync(path, contentPost).Result;

            //    if (result != null)
            //    {
            //        if (!result.IsSuccessStatusCode)
            //        {
            //            string message = $"失敗 回應:{result.ReasonPhrase}";

            //        }

            //        // 200成功
            //        if (result.StatusCode == System.Net.HttpStatusCode.OK)
            //        {
            //            var resultResponseJs = result.Content.ReadAsStringAsync();
            //        }
            //        else
            //        {

            //        }
            //    }
            //    else
            //    {

            //    }
            //}

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
