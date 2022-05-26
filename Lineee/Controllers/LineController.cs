using Lineee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lineee.Controllers
{

    [RoutePrefix("api/line")]
    public class LineController : ApiController
    {
        private LineNotify1Entities db = new LineNotify1Entities();

        // GET api/<controller>
        [HttpGet]       
        public IEnumerable<string> Get(string response)
        {
            db.ExaOrders.Add(new ExaOrders
            {
                doctor_id = 22001,
                exam_id = 3011,
                exam_number = 8079,
                order_date = DateTime.Now,
                patient_id = 1003
            }
                ) ;
            ;
            db.SaveChanges();

            return new string[] { "我來到一個島 他叫卡加布列島" };
        }

        // GET api/<controller>/5
        [HttpGet]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]

        public string Post([FromBody] Body body)
        {
            try
            {
                db.Patient.Add(new Patient
                {
                    patient_id = 1234,
                    id_card = "3011",
                    patient_name = body.id,
                    birth_date = DateTime.Now,
                    blood = body.msg,
                    gender = "abc"
                }
                   );
                ;
                db.SaveChanges();

                return "我來到一個島 他叫卡加布列島";
            }
            catch (Exception e)
            {
                db.Patient.Add(new Patient
                {
                    patient_id = 1234,
                    id_card = "3011",
                    patient_name = e.Message,
                    birth_date = DateTime.Now,
                    blood = e.Message,
                    gender = "abc"
                }
                  );
                ;
                db.SaveChanges();

                return "我來到一個島 他叫卡加布列島";
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

    }

    public class Body
    {
        public string id { get; set; }
        public string msg { get; set; }
    }

}