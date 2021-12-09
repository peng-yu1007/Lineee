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
    }
}