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
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

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

        public ActionResult Secret()
        {
            return View("Secret");
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




        public ActionResult SendLine([Bind(Include = "path")] string path, [Bind(Include = "doctor_id")] string doctor_id)
        {

            string Encrypt()
            {
                try
                {   
                    string textToEncrypt =path;
                    string CryptoKey = doctor_id;
                    string encrypt = "";


                    AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                    SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
                    byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(CryptoKey));
                    byte[] iv = md5.ComputeHash(Encoding.UTF8.GetBytes(CryptoKey));
                    aes.Key = key;
                    aes.IV = iv;

                    byte[] dataByteArray = Encoding.UTF8.GetBytes(textToEncrypt);
                    using (MemoryStream ms = new MemoryStream())
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(dataByteArray, 0, dataByteArray.Length);
                        cs.FlushFinalBlock();
                        encrypt = Convert.ToBase64String(ms.ToArray());
                        return encrypt;
                    }
                    
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
                
            }
            Encrypt();

            var client = new RestClient("https://script.google.com/macros/s/AKfycbyB2u5E72rhN3YcBzjDravC7wgMp1M-DK1ZYpoIkt10jAKafj-rZ-t7tAB8TXsr4TM/exec");
            client.Timeout = 5000;
            var request = new RestRequest(Method.POST);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("msg", Encrypt());
            IRestResponse response = client.Execute(request);

            return RedirectToAction("Index"); 
        }



        [HttpPost]
        public ActionResult Decrypt(Report report)
        {
            string Decrypt()
            {
                try
                {
                    string textToDecrypt = report.text;
                    string CryptoKey = report.key;
                    string decrypt = "";

                    AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                    SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
                    byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(CryptoKey));
                    byte[] iv = md5.ComputeHash(Encoding.UTF8.GetBytes(CryptoKey));
                    aes.Key = key;
                    aes.IV = iv;

                    byte[] dataByteArray = Convert.FromBase64String(textToDecrypt);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(dataByteArray, 0, dataByteArray.Length);
                            cs.FlushFinalBlock();
                            decrypt = Encoding.UTF8.GetString(ms.ToArray());
                            return decrypt;
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message, e.InnerException);
                }

            }
            //Decrypt();
            ViewBag.address = Decrypt();
            return View("Secret");
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
