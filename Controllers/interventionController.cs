using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using Newtonsoft.Json.Linq;

namespace TodoApi.Controllers
{
    [Route("api/interventions")]
    [ApiController]
    public class interventionController : ControllerBase
    {
        private readonly MysqlContext _context;

        public interventionController(MysqlContext context)
        {
            _context = context;
        }

       
        

        // GET: api/interventions
        [HttpGet]
        public  ActionResult<List<interventions>> GetAll()
        {
            var interventions =  _context.interventions;
            List<interventions> list_intervention = new List<interventions> ();
            
           

            if (interventions == null)
            {
                return NotFound();
            }
            foreach (var intervention in interventions) {
                if(intervention.status== "Pending" && intervention.start_intervention== null ){

                    list_intervention.Add(intervention);
                }



            }
            
            return list_intervention;
            
        }
        

        // PUT: api/interventions/InProgress/id?
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("InProgress/{id}")]
        public async Task<IActionResult> PutInProgress(long id)
        {
            var INTER = _context.interventions.Find (id);
            if (INTER == null) {
                return NotFound ();
            }

            INTER.status = "InProgress";
            INTER.start_intervention = DateTime.Now;

            _context.interventions.Update (INTER);
            _context.SaveChanges ();
             var json = new JObject ();
            json["message"] = "the change of status is well done";
            return Content (json.ToString (), "application/json");
        }

        // PUT: api/interventions/Completed/id?

         [HttpPut("Completed/{id}")]
        public async Task<IActionResult> PutCompleted(long id)
        {
            var INTER = _context.interventions.Find (id);
            if (INTER == null) {
                return NotFound ();
            }

            INTER.status = "Completed";
            INTER.end_intervention = DateTime.Now;

            _context.interventions.Update (INTER);
            _context.SaveChanges ();
             var json = new JObject ();
            json["message"] = "the change of status is well done";
            return Content (json.ToString (), "application/json");
        }

       

    }
}
