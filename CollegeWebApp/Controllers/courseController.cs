using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CollegeWebApp.Models;

namespace CollegeWebApp.Controllers
{
    public class courseController : ApiController
    {
        private DBCollege db = new DBCollege();

        // GET: api/course
        public IQueryable<cours> Getcourses()
        {
            return db.courses;
        }

        // GET: api/course/5
        [ResponseType(typeof(cours))]
        public IHttpActionResult Getcours(long id)
        {
            cours cours = db.courses.Find(id);
            if (cours == null)
            {
                return NotFound();
            }

            return Ok(cours);
        }

        // PUT: api/course/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcours(long id, cours cours)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cours.courseId)
            {
                return BadRequest();
            }

            db.Entry(cours).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!coursExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/course
        [ResponseType(typeof(cours))]
        [AcceptVerbs("POST", "PUT")]
        public IHttpActionResult Postcours(cours cours)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.courses.Add(cours);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cours.courseId }, cours);
        }

        // DELETE: api/course/5
        [ResponseType(typeof(cours))]
        public IHttpActionResult Deletecours(long id)
        {
            cours cours = db.courses.Find(id);
            if (cours == null)
            {
                return NotFound();
            }

            db.courses.Remove(cours);
            db.SaveChanges();

            return Ok(cours);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool coursExists(long id)
        {
            return db.courses.Count(e => e.courseId == id) > 0;
        }
    }
}