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

namespace ProyectoPracticaLP2.Controllers
{
    public class ClientesController : Controller
    {
        private ProyectoPracticaLP2Entities db = new ProyectoPracticaLP2Entities();

        // GET: Clientes
        public ActionResult Index()
        {
            IEnumerable<Clientes> clientes = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://localhost:64099/api/");
                var responseTask = httpClient.GetAsync("Clientes");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Clientes>>();
                    readTask.Wait();
                    clientes = readTask.Result;
                }
                else
                {
                    clientes = Enumerable.Empty<Clientes>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View(clientes);
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            Clientes Clientes = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:64099/api/");
                var responseTask = client.GetAsync("Clientes/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Clientes>();
                    readTask.Wait();

                    Clientes = readTask.Result;
                }
            }

            if (Clientes == null)
            {
                return HttpNotFound();
            }

            return View(Clientes);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,apellido,email,telefono,identificacion,direccion")] Clientes Clientes)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://localhost:64099/api/Clientes/");

                //HTTP POST
                var postTask = httpClient.PostAsJsonAsync("Clientes", Clientes);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(Clientes);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Clientes Clientes = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:64099/api/");
                var responseTask = client.GetAsync("Clientes/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Clientes>();
                    readTask.Wait();

                    Clientes = readTask.Result;
                }
            }

            if (Clientes == null)
            {
                return HttpNotFound();
            }

            return View(Clientes);
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,apellido,email,telefono,identificacion,direccion")] Clientes Clientes)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"http://localhost:64099/api/clientes/{Clientes.id}");
                var putTask = client.PutAsJsonAsync(client.BaseAddress, Clientes);

                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(Clientes);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Clientes Clientes = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:64099/api/");
                var responseTask = client.GetAsync("Clientes/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Clientes>();
                    readTask.Wait();

                    Clientes = readTask.Result;
                }
            }

            if (Clientes == null)
            {
                return HttpNotFound();
            }
            return View(Clientes);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) // TODO: With api
        {
            Clientes Clientes = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"http://localhost:64099/api/clientes/{id}");
                var deleteTask = client.DeleteAsync(client.BaseAddress);

                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(Clientes);
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
