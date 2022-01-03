using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Lineee.Models;
using RestSharp;

namespace Lineee.Controllers
{
    public class DoctorController : Controller
    {
        private LineNotify1Entities db = new LineNotify1Entities();
        // GET: PatientList
        public ActionResult Index()
        {

            //HttpContent stringContent1 = new StringContent("msg");
            //using (var content = new MultipartFormDataContent())
            //{
            //    content.Add(stringContent1, "QAQ");

            //    var req = new HttpClient();
            //    var test = req.PostAsync($"https://script.google.com/macros/s/AKfycbyB2u5E72rhN3YcBzjDravC7wgMp1M-DK1ZYpoIkt10jAKafj-rZ-t7tAB8TXsr4TM/exec", content);
            //}


            //// Définition des variables qui seront envoyés
            //HttpContent stringContent1 = new StringContent("msg"); // Le contenu du paramètre P1
            ////HttpContent bytesContent = new ByteArrayContent(paramFileBytes);

            //using (var client = new HttpClient())
            //using (var formData = new MultipartFormDataContent())
            //{
            //    formData.Add(stringContent1, "QAQ"); // Le paramètre P1 aura la valeur contenue dans param1String

            //    //  formData.Add(bytesContent, "file2", "file2");
            //    try
            //    {
            //        var response = client.PostAsync($"https://script.google.com/macros/s/AKfycbyB2u5E72rhN3YcBzjDravC7wgMp1M-DK1ZYpoIkt10jAKafj-rZ-t7tAB8TXsr4TM/exec", formData).Result;

            //    }
            //    catch (Exception Error)
            //    {

            //    }
            //    finally
            //    {
            //        client.CancelPendingRequests();
            //    }
            //}

            // post 
            //var client = new RestClient("https://script.google.com/macros/s/AKfycbyB2u5E72rhN3YcBzjDravC7wgMp1M-DK1ZYpoIkt10jAKafj-rZ-t7tAB8TXsr4TM/exec");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.POST);
            //request.AlwaysMultipartFormData = true;
            //request.AddParameter("msg", "QAQ");
            //IRestResponse response = client.Execute(request);


            return View(db.Patient.ToList());
        }

        public ActionResult Choose(string patient_id, string id_card, string patient_name, string birth_date, string blood, string gender)
        {
            ViewBag.patient_id = patient_id;
            ViewBag.id_card = id_card;
            ViewBag.patient_name = patient_name;
            ViewBag.birth_date = birth_date;
            ViewBag.blood = blood;
            ViewBag.gender = gender;
            return View("Choose");
        }


    }
}