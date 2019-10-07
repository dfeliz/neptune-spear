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
    public class ClientesController : ApiController
    {
        private ProyectoPracticaLP2Entities db = new ProyectoPracticaLP2Entities();

        // GET: api/Clientes
        public IQueryable<Clientes> GetClientes()
        {
            return db.Clientes;
        }

        // GET: api/Clientes/5
        [ResponseType(typeof(Clientes))]
        public IHttpActionResult GetCliente(int id)
        {
            Clientes Clientes = db.Clientes.Find(id);
            if (Clientes == null)
            {
                return NotFound();
            }

            return Ok(Clientes);
        }

        // PUT: api/Clientes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCliente(int id, Clientes Clientes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Clientes.id)
            {
                return BadRequest();
            }

            db.Entry(Clientes).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        // POST: api/Clientes
        [ResponseType(typeof(Clientes))]
        public IHttpActionResult PostCliente(Clientes Clientes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Clientes.Add(Clientes);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Clientes.id }, Clientes);
        }

        // DELETE: api/Clientes/5
        [ResponseType(typeof(Clientes))]
        public IHttpActionResult DeleteCliente(int id)
        {
            Clientes Clientes = db.Clientes.Find(id);
            if (Clientes == null)
            {
                return NotFound();
            }

            db.Clientes.Remove(Clientes);
            db.SaveChanges();

            return Ok(Clientes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClienteExists(int id)
        {
            return db.Clientes.Count(e => e.id == id) > 0;
        }
    }
}