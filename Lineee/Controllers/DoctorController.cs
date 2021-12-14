using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lineee.Models;

namespace Lineee.Controllers
{
    public class DoctorController : Controller
    {
        private LineNotify1Entities db = new LineNotify1Entities();
        // GET: PatientList
        public ActionResult Index()
        {
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