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
using MvcDemo.Models;

namespace MvcDemo.Controllers
{
    public class TestDBsController : ApiController
    {
        private TestDBContext db = new TestDBContext();

        // GET: api/TestDBs
        public IQueryable<TestDB> GetMovies()
        {
            return db.Movies;
        }

        // GET: api/TestDBs/5
        [ResponseType(typeof(TestDB))]
        public IHttpActionResult GetTestDB(int id)
        {
            TestDB testDB = db.Movies.Find(id);
            if (testDB == null)
            {
                return NotFound();
            }

            return Ok(testDB);
        }

        // PUT: api/TestDBs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTestDB(int id, TestDB testDB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != testDB.ID)
            {
                return BadRequest();
            }

            db.Entry(testDB).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestDBExists(id))
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

        // POST: api/TestDBs
        [ResponseType(typeof(TestDB))]
        public IHttpActionResult PostTestDB(TestDB testDB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Movies.Add(testDB);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = testDB.ID }, testDB);
        }

        // DELETE: api/TestDBs/5
        [ResponseType(typeof(TestDB))]
        public IHttpActionResult DeleteTestDB(int id)
        {
            TestDB testDB = db.Movies.Find(id);
            if (testDB == null)
            {
                return NotFound();
            }

            db.Movies.Remove(testDB);
            db.SaveChanges();

            return Ok(testDB);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TestDBExists(int id)
        {
            return db.Movies.Count(e => e.ID == id) > 0;
        }
    }
}