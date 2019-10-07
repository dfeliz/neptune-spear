using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Webapp.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Webapp.Controllers
{
    public class AlmacenesController : Controller
    {
        private ProyectoPracticaLP2Entities db = new ProyectoPracticaLP2Entities();

        // GET: Almacenes
        public async Task<ActionResult> Index()
        {
            IEnumerable<Almacenes> almacenes = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:64099/api/");

                HttpResponseMessage response = await client.GetAsync("Almacenes");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<IList<Almacenes>>();
                    almacenes = result;
                }
                else
                {
                    almacenes = Enumerable.Empty<Almacenes>();
                    ModelState.AddModelError(string.Empty, "Oops, looks like something went wrong.");
                }
            }

            return View(almacenes);
        }

        // GET: Almacenes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Almacenes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Almacenes almacen)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:64099/api/Articulos/");

                HttpResponseMessage response = await client.PostAsJsonAsync("almacenes", almacen);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Oops, looks like something went wrong.");

            return View(almacen);
        }

        // GET: Almacenes/Edit/id
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Almacenes almacen = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:64099/api/");

                HttpResponseMessage response = await client.GetAsync("Almacenes/" + id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<Almacenes>();
                    almacen = result;
                }
            }

            if (almacen == null)
            {
                return HttpNotFound();
            }

            return View(almacen);
        }

        // POST: Almacenes/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Almacenes almacen)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"http://localhost:64099/api/articulos/{almacen.id}");

                HttpResponseMessage response = await client.PutAsJsonAsync(client.BaseAddress, almacen);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(almacen);
        }

        // GET: Almacenes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Almacenes almacen = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"http://localhost:64099/api/almacenes/{id}");

                HttpResponseMessage response = await client.GetAsync(client.BaseAddress);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<Almacenes>();
                    almacen = result;
                }
            }

            if (almacen == null)
            {
                return HttpNotFound();
            }
            return View(almacen);
        }

        // POST: Almacenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Almacenes almacen = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"http://localhost:64099/api/almacenes/{id}");

                HttpResponseMessage response = await client.DeleteAsync(client.BaseAddress);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(almacen);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
