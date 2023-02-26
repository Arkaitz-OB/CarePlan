using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CarePlan.Data;
using CarePlan.Models;

namespace CarePlan.Controllers
{
    public class PlansController : ApiController
    {
        private CarePlanContext db = new CarePlanContext();

        /// <summary>
        /// Returns existing care plans.
        /// </summary>
        /// <example>GET: api/Plans</example>
        public IQueryable<Plan> GetPlans()
        {
            return db.Plans;
        }

        /// <summary>
        /// Returns a care plan.
        /// </summary>
        /// <param name="id">Identifier of the plan.</param>
        /// <example>GET: api/Plans/5</example>
        [ResponseType(typeof(Plan))]
        public IHttpActionResult GetPlan(int id)
        {
            Plan plan = db.Plans.Find(id);
            if (plan == null)
                return NotFound();

            return Ok(plan);
        }

        /// <summary>
        /// Updates a care plan.
        /// </summary>
        /// <param name="id">Identifier of the plan.</param>
        /// <param name="plan">Modified plan.</param>
        /// <example>PUT: api/Plans/5</example>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlan(int id, Plan plan)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            string message = ValidateRules(plan);
            if (!string.IsNullOrEmpty(message)) return BadRequest(message);

            if (id != plan.Id)
                return BadRequest();

            db.Entry(plan).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Plans.Count(e => e.Id == id) == 0)
                    return NotFound();
                else
                    throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Creates a care plan.
        /// </summary>
        /// <param name="plan">Plan to create.</param>
        /// <example>POST: api/Plans</example>
        [ResponseType(typeof(Plan))]
        public IHttpActionResult PostPlan(Plan plan)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            string message = ValidateRules(plan);
            if (!string.IsNullOrEmpty(message)) return BadRequest(message);

            db.Plans.Add(plan);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = plan.Id }, plan);
        }

        /// <summary>
        /// Deletes a care plan.
        /// </summary>
        /// <param name="id">Identifier of the plan.</param>
        /// <example>DELETE: api/Plans/5</example>
        [ResponseType(typeof(Plan))]
        public IHttpActionResult DeletePlan(int id)
        {
            Plan plan = db.Plans.Find(id);

            if (plan == null)
                return NotFound();

            db.Plans.Remove(plan);
            db.SaveChanges();

            return Ok(plan);
        }

        private string ValidateRules(Plan plan)
        {
            System.Collections.Generic.List<string> errors = new System.Collections.Generic.List<string>();

            if (plan.Completed == true)
            {
                if (plan.EndDate == null)
                    errors.Add("The EndDate field is required when the Completed field is true.");
                else
                {
                    if (plan.EndDate < plan.StartDate)
                        errors.Add("The EndDate field cannot be before the StartDate field.");
                }

                if (plan.Outcome == null)
                    errors.Add("The Outcome field is required when the Completed field is true.");
            }
            else
            {   // Ensure data integrity. Or alternatively set error message.
                plan.EndDate = null;
                plan.Outcome = null;
            }

            return string.Join(System.Environment.NewLine, errors);
        }

    }

}