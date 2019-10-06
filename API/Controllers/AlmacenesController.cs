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
using Webapp.Models;

namespace API.Controllers
{
    public class AlmacenesController : ApiController
    {
        private ProyectoPracticaLP2Entities db = new ProyectoPracticaLP2Entities();

        // GET: api/Almacenes
        public IQueryable<Almacen> GetAlmacens()
        {
            return db.Almacens;
        }

        // GET: api/Almacenes/5
        [ResponseType(typeof(Almacen))]
        public IHttpActionResult GetAlmacen(int id)
        {
            Almacen almacen = db.Almacens.Find(id);
            if (almacen == null)
            {
                return NotFound();
            }

            return Ok(almacen);
        }

        // PUT: api/Almacenes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAlmacen(int id, Almacen almacen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != almacen.id)
            {
                return BadRequest();
            }

            db.Entry(almacen).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlmacenExists(id))
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

        // POST: api/Almacenes
        [ResponseType(typeof(Almacen))]
        public IHttpActionResult PostAlmacen(Almacen almacen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Almacens.Add(almacen);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = almacen.id }, almacen);
        }

        // DELETE: api/Almacenes/5
        [ResponseType(typeof(Almacen))]
        public IHttpActionResult DeleteAlmacen(int id)
        {
            Almacen almacen = db.Almacens.Find(id);
            if (almacen == null)
            {
                return NotFound();
            }

            db.Almacens.Remove(almacen);
            db.SaveChanges();

            return Ok(almacen);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlmacenExists(int id)
        {
            return db.Almacens.Count(e => e.id == id) > 0;
        }
    }
}