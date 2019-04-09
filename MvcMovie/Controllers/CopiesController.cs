using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Llibreria.Context;
using Llibreria.Models;

namespace MvcMovie.Controllers
{
    public class CopiesController : Controller
    {
        private MvcContext db = new MvcContext();

        // GET: Copies
        public ActionResult Index(int id)
        {
            ViewBag.ID = id;
            var copies = from c in db.Copies where c.IDmovie == id select c;
            return View(copies.ToList());
        }

        // GET: Copies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Copies copies = db.Copies.Find(id);
            if (copies == null)
            {
                return HttpNotFound();
            }
            return View(copies);
        }

      

        // POST: Copies/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id)
        {
            //var copies = from c in db.Copies where c.IDmovie == id select c;
            var copies = db.Copies.Where(c => c.IDmovie == id).Select(c => c).ToList().LastOrDefault();
            // var copies = (from c in db.Copies where c.IDmovie == movieId select c).
            // Copies copia = copies.ToList().LastOrDefault();
           
            if (copies == null)
            {
                //Copies cp = new Copies(id, 0, false);
                db.Copies.Add(new Copies(id, 0, false));
                db.SaveChanges();
                return RedirectToAction("Index", new { id });
            }
            else
            {
               //int i= copies.numCopia++;
               int i= copies.numCopia;
                i++;
                 //Copies cp = new Copies(id, i, false);
                db.Copies.Add(new Copies(id, i, false));
                db.SaveChanges();
                return RedirectToAction("Index", new { id});
            }




            
               
            
        }
        [HttpPost]
        public ActionResult Elegit(string Motiu, int id, int numCopia)
        {

            var copies = db.Copies.Where(c => c.IDmovie == id && c.numCopia == numCopia).Select(c => c).ToList();
            copies[0].eMotiu = Motiu;
            copies[0].invalida = true;
            db.SaveChanges();
            return RedirectToAction("index",new { id});
        }

        // GET: Copies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Copies copies = db.Copies.Find(id);
            if (copies == null)
            {
                return HttpNotFound();
            }
            return View(copies);
        }

        // POST: Copies/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDmovie,numCopia,invalida,motiu")] Copies copies)
        {
            if (ModelState.IsValid)
            {
                db.Entry(copies).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(copies);
        }

        // GET: Copies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Copies copies = db.Copies.Find(id);
            if (copies == null)
            {
                return HttpNotFound();
            }
            return View(copies);
        }

        // POST: Copies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Copies copies = db.Copies.Find(id);
            db.Copies.Remove(copies);
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
