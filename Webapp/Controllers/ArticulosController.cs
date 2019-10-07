using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Webapp.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProyectoPracticaLP2.Controllers
{
    public class ArticulosController : Controller
    {
        private ProyectoPracticaLP2Entities db = new ProyectoPracticaLP2Entities();

        // GET: Articulos
        public async Task<ActionResult> Index()
        {
            IEnumerable<Articulos> articulos = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:64099/api/");

                HttpResponseMessage response = await client.GetAsync("Articulos");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<IList<Articulos>>();
                    articulos = result;
                }
                else
                {
                    articulos = Enumerable.Empty<Articulos>();
                    ModelState.AddModelError(string.Empty, "Oops, looks like something went wrong.");
                }
            }

            return View(articulos);
        }

        // GET: Articulos/Details/id
        public async Task<ActionResult> Details(int? id)
        {
            Articulos articulo = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:64099/api/");

                HttpResponseMessage response = await client.GetAsync("Articulos/" + id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<Articulos>();
                    articulo = result;
                }
            }

            if (articulo == null)
            {
                return HttpNotFound();
            }

            return View(articulo);
        }

        // GET: Articulos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Articulos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Articulos articulo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:64099/api/Articulos/");

                HttpResponseMessage response = await client.PostAsJsonAsync("articulo", articulo);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Oops, looks like something went wrong.");

            return View(articulo);
        }

        // GET: Articulos/Edit/id
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Articulos articulo = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:64099/api/");

                HttpResponseMessage response = await client.GetAsync("Articulos/" + id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<Articulos>();
                    articulo = result;
                }
            }

            if (articulo == null)
            {
                return HttpNotFound();
            }

            return View(articulo);
        }

        // POST: Articulos/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Articulos articulo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"http://localhost:64099/api/articulos/{articulo.id}");

                HttpResponseMessage response = await client.PutAsJsonAsync(client.BaseAddress, articulo);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(articulo);
        }

        // GET: Articulos/Delete/id
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Articulos articulo = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:64099/api/articulos/{id}");

                HttpResponseMessage response = await client.GetAsync(client.BaseAddress);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<Articulos>();
                    articulo = result;
                }
            }

            if (articulo == null)
            {
                return HttpNotFound();
            }
            return View(articulo);
        }

        // POST: Articulos/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Articulos articulo = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"http://localhost:64099/api/articulos/{id}");

                HttpResponseMessage response = await client.DeleteAsync(client.BaseAddress);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(articulo);
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
