﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Webapp.Models;
using EntityState = System.Data.Entity.EntityState;

namespace Webapp.Controllers
{
    public class AlmacenesController : Controller
    {
        private ProyectoPracticaLP2Entities db = new ProyectoPracticaLP2Entities();

        // GET: Almacenes
        public ActionResult Index()
        {
            return View(db.Almacens.ToList());
        }

        // GET: Almacenes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Almacen almacen = db.Almacens.Find(id);
            if (almacen == null)
            {
                return HttpNotFound();
            }
            return View(almacen);
        }

        // GET: Almacenes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Almacenes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,direccion")] Almacen almacen)
        {
            if (ModelState.IsValid)
            {
                db.Almacens.Add(almacen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(almacen);
        }

        // GET: Almacenes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Almacen almacen = db.Almacens.Find(id);
            if (almacen == null)
            {
                return HttpNotFound();
            }
            return View(almacen);
        }

        // POST: Almacenes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,direccion")] Almacen almacen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(almacen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(almacen);
        }

        // GET: Almacenes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Almacen almacen = db.Almacens.Find(id);
            if (almacen == null)
            {
                return HttpNotFound();
            }
            return View(almacen);
        }

        // POST: Almacenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Almacen almacen = db.Almacens.Find(id);
            db.Almacens.Remove(almacen);
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
