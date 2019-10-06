using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Webapp.Models;

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
                // HTTP Client connection properties
                httpClient.BaseAddress = new Uri("http://localhost:64099/api/");

                // GET method
                var httpResponse = httpClient.GetAsync("Articulos");
                httpResponse.Wait();

                // Save HTTP result value
                var result = httpResponse.Result;

                // If the HTTP result is succesfull, read values and store them.
                if (result.IsSuccessStatusCode)
                {
                    var readResponse = result.Content.ReadAsAsync<IList<Articulo>>();
                    readResponse.Wait();

                    articulos = readResponse.Result;
                }
                else
                {
                    articulos = Enumerable.Empty<Articulo>();
                    ModelState.AddModelError(string.Empty, "Oops. Something failed... time to get the coffe mug.");
                }
            }
            return View(articulos);
        }

        // GET: Articulos/Details
        // TODO: This should be managed by the API.
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articulo articulo = db.Articulo.Find(id);
            if (articulo == null)
            {
                return HttpNotFound();
            }
            return View(articulo);
        }

        // GET: Articulos/Create
        public ActionResult Create()
        {
            IEnumerable<Articulo> articulos = null;
            articulos = Enumerable.Empty<Articulo>();
            return View();
        }

        // POST: Articulos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,descripcion,almacen_id")] Articulo articulo)
        {
            using (var client = new HttpClient())
            {
                // HTTP Client connection properties
                client.BaseAddress = new Uri("http://localhost:64099/api/articulos/create");

                // POST method
                var httpPost = client.PostAsJsonAsync<Articulo>("Articulos", articulo);
                httpPost.Wait();

                // Save the HTTP resutl here
                var result = httpPost.Result;

                // If the HTTP result is succesfull, read values and store them.
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Oops. Something failed... time to get the coffe mug.");
            return View(articulo);
        }

        // GET: Articulos/Edit
        public ActionResult Edit(int? id)
        {

            Articulo articulo = null;
            using (var httpClient = new HttpClient())
            {
                // HTTP Client connection properties
                httpClient.BaseAddress = new Uri("http://localhost:64099/api/");

                // GET method
                var httpResponse = httpClient.GetAsync("Articulos/" + id.ToString());
                httpResponse.Wait();

                // Save HTTP result value
                var result = httpResponse.Result;

                // If the HTTP result is succesfull, read values and store them.
                if (result.IsSuccessStatusCode)
                {
                    var readResponse = result.Content.ReadAsAsync<Articulo>();
                    readResponse.Wait();

                    articulo = readResponse.Result;
                }
            }
            return View(articulo);
        }

        // POST: Articulos/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,descripcion,almacen_id")] Articulo articulo)
        {
            using (var httpClient = new HttpClient())
            {
                // HTTP Client connection properties
                httpClient.BaseAddress = new Uri("http://localhost:64099/api/Articulos/Edit/");

                // POST method
                var httpEdit = httpClient.PutAsJsonAsync<Articulo>("Articulos", articulo);
                httpEdit.Wait();
                
                // Save HTTP result value
                var result = httpEdit.Result;
 
                // If the HTTP result is succesfull, read values and store them.
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(articulo);
        }

        // GET: Articulos/Delete
        public ActionResult Delete(int? id)
        {
            using (var client = new HttpClient())
            {
                // HTTP Client connection properties
                client.BaseAddress = new Uri("http://localhost:64099/api/");

                // DELETE method
                var httpDelete = client.DeleteAsync("Articulos/" + id.ToString());
                httpDelete.Wait();

                // Save HTTP result here
                var result = httpDelete.Result;

                // If the HTTP result is succesfull, read values and store them.
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        // POST: Articulos/Delete
        // TODO: This should be managed by the API.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Articulo articulo = db.Articulo.Find(id);
            db.Articulo.Remove(articulo);
            db.SaveChanges();
            return RedirectToAction("Index");
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
