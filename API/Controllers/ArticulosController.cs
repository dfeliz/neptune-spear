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
    public class ArticulosController : ApiController
    {
        private ProyectoPracticaLP2Entities db = new ProyectoPracticaLP2Entities();

        // GET: api/Articulos
        public IQueryable<Articulo> GetArticulo()
        {
            return db.Articulo;
        }

        // GET: api/Articulos/5
        [ResponseType(typeof(Articulo))]
        public IHttpActionResult GetArticulo(int id)
        {
            Articulo articulo = db.Articulo.Find(id);
            if (articulo == null)
            {
                return NotFound();
            }

            return Ok(articulo);
        }

        // PUT: api/Articulos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArticulo(int id, Articulo articulo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != articulo.id)
            {
                return BadRequest();
            }

            db.Entry(articulo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticuloExists(id))
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

        // POST: api/Articulos
        [ResponseType(typeof(Articulo))]
        public IHttpActionResult PostArticulo(Articulo articulo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Articulo.Add(articulo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = articulo.id }, articulo);
        }

        // DELETE: api/Articulos/5
        [ResponseType(typeof(Articulo))]
        public IHttpActionResult DeleteArticulo(int id)
        {
            Articulo articulo = db.Articulo.Find(id);
            if (articulo == null)
            {
                return NotFound();
            }

            db.Articulo.Remove(articulo);
            db.SaveChanges();

            return Ok(articulo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArticuloExists(int id)
        {
            return db.Articulo.Count(e => e.id == id) > 0;
        }
    }
}