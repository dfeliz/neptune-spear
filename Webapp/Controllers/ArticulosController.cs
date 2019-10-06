using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Webapp.Models;
using System.Net.Http;

namespace ProyectoPracticaLP2.Controllers
{
    public class ArticulosController : Controller
    {
        private ProyectoPracticaLP2Entities db = new ProyectoPracticaLP2Entities();

        // GET: Articulos
        public ActionResult Index()
        {
            IEnumerable<Articulo> articulos = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://localhost:64099/api/");

                var httpResponse = httpClient.GetAsync("Articulos");
                httpResponse.Wait();

                var result = httpResponse.Result;

                if (result.IsSuccessStatusCode)
                {
                    var httpRead = result.Content.ReadAsAsync<IList<Articulo>>();
                    httpRead.Wait();
                    articulos = httpRead.Result;
                }
                else
                {
                    articulos = Enumerable.Empty<Articulo>();
                    ModelState.AddModelError(string.Empty, "Oops, looks like something went wrong.");
                }
            }

            return View(articulos);
        }

        // GET: Articulos/Details/id
        public ActionResult Details(int? id)
        {
            Articulo articulo = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:64099/api/");

                var httpResponse = client.GetAsync("Articulos/" + id.ToString());
                httpResponse.Wait();

                var result = httpResponse.Result;

                if (result.IsSuccessStatusCode)
                {
                    var httpRead = result.Content.ReadAsAsync<Articulo>();
                    httpRead.Wait();

                    articulo = httpRead.Result;
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
        public ActionResult Create([Bind(Include = "id,nombre,descripcion")] Articulo articulo)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://localhost:64099/api/Clientes/");

                var httpPost = httpClient.PostAsJsonAsync("articulo", articulo);
                httpPost.Wait();

                var result = httpPost.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Oops, looks like something went wrong.");

            return View(articulo);
        }

        // GET: Articulos/Edit/id
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Articulo articulo = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://localhost:64099/api/");

                var httpResponse = httpClient.GetAsync("Articulos/" + id.ToString());
                httpResponse.Wait();

                var result = httpResponse.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Articulo>();
                    readTask.Wait();

                    articulo = readTask.Result;
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
        public ActionResult Edit([Bind(Include = "id,nombre,descripcion")] Articulo articulo)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"http://localhost:64099/api/articulos/{articulo.id}");
               
                var httpPut = httpClient.PutAsJsonAsync(httpClient.BaseAddress, articulo);
                httpPut.Wait();

                var result = httpPut.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(articulo);
        }

        // GET: Articulos/Delete/id
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Articulo articulo = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://localhost:64099/api/");
                var httpResponse = httpClient.GetAsync("Articulos/" + id.ToString());
                httpResponse.Wait();

                var result = httpResponse.Result;
                if (result.IsSuccessStatusCode)
                {
                    var httpRead = result.Content.ReadAsAsync<Articulo>();
                    httpRead.Wait();

                    articulo = httpRead.Result;
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
        public ActionResult DeleteConfirmed(int id)
        {
            Articulo articulo = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"http://localhost:64099/api/articulos/{id}");
               
                var httpDelete = httpClient.DeleteAsync(httpClient.BaseAddress);
                httpDelete.Wait();

                var result = httpDelete.Result;

                if (result.IsSuccessStatusCode)
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
