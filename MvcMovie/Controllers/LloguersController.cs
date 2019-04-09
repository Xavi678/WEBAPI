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
    public class LloguersController : Controller
    {
        private MvcContext db = new MvcContext();

        // GET: Lloguers
        public ActionResult Index()
        {
            //var lloguers = db.Lloguers.Include(l => l.Client).Include(l => l.Copies);
            var lloguers = from l in db.Lloguers orderby l.DataInici descending select l ;
            return View(lloguers.ToList());
        }

        public ActionResult UpdateValues(int id)
        {

            return RedirectToAction("Create", id);
        }

        // GET: Lloguers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lloguer lloguer = db.Lloguers.Find(id);
            if (lloguer == null)
            {
                return HttpNotFound();
            }
            return View(lloguer);
        }

        // GET: Lloguers/Create
        public ActionResult Create(int? movie, int? copia, string client, int? lloguer)
        {
            ViewBag.movie = movie;
            ViewBag.copies = copia;
            ViewBag.client = client;

            /*var movies = db.Movies.Select(m => new { m.ID, m.Titol }).ToList();
            var copies = db.Copies.Select(c => new { c.IDmovie, c.numCopia }).ToList();
            var clients = db.Clients.Select(l => new { l.NIF, l.Nom }).ToList();

            ViewBag.llistaMovies = movies;
            ViewBag.llistaCopies = copies;
            ViewBag.llistaClients = clients;*/

            ViewBag.llistaMovies = new SelectList(db.Movies.Select(m => new SelectListItem { Value = m.ID.ToString(), Text = m.Titol }), "Value", "Text");

            ViewBag.llistaClients = new SelectList(db.Clients.Select(l => new SelectListItem { Value = l.NIF, Text = l.Nom }), "Value", "Text");
            if(movie!=null && copia == null)
            {
                ViewBag.llistaCopies = new SelectList(db.Copies.Where(c => c.IDmovie == movie).Select(c => new SelectListItem { Value = c.numCopia.ToString(), Text = c.numCopia.ToString() }), "Value", "Text");
            }else if (lloguer != null) { 
            ViewBag.llistaCopies = new SelectList(db.Copies.Where(c => c.IDmovie == lloguer).Select(c => new SelectListItem { Value = c.numCopia.ToString(), Text = c.numCopia.ToString() }), "Value", "Text");
            }
            else
            {
                
                ViewBag.llistaCopies = new SelectList(db.Copies.Where(c => c.IDmovie == 1).Select(c => new SelectListItem { Value = c.numCopia.ToString(), Text = c.numCopia.ToString() }), "Value", "Text");
            }
            
            Lloguer lloguerr = new Lloguer(movie.GetValueOrDefault(),copia.GetValueOrDefault(),client,DateTime.Now,DateTime.Today.AddDays(7));
 
            //var copies = db.Copies.Where(c => c.IDmovie == id).Select(c => c).ToList().LastOrDefault();
            //ViewBag.ClientID = new SelectList(db.Clients, "NIF", "Nom");
            //ViewBag.IDcopies = new SelectList(db.Copies, "IDmovie", "eMotiu");
            return View(lloguerr);
        }

        // POST: Lloguers/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDlloguer,IDcopies,numCopia,ClientID,DataInici,DataFi,DataReal,Perdut,Amortitzat")] Lloguer lloguer,int? v)
        {
            if (v == 0)
            {


                Create(null,null, null, lloguer.IDcopies);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Lloguers.Add(lloguer);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            //ViewBag.ClientID = new SelectList(db.Clients, "NIF", "Nom", lloguer.ClientID);
            //ViewBag.IDcopies = new SelectList(db.Copies, "IDmovie", "eMotiu", lloguer.IDcopies);
            return View(lloguer);
        }

        // GET: Lloguers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lloguer lloguer = db.Lloguers.Find(id);
            if (lloguer == null)
            {
                return HttpNotFound();
            }
            //ViewBag.ClientID = new SelectList(db.Clients, "NIF", "Nom", lloguer.ClientID);
            //ViewBag.IDcopies = new SelectList(db.Copies, "IDmovie", "eMotiu", lloguer.IDcopies);
            return View(lloguer);
        }

        // POST: Lloguers/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDlloguer,IDcopies,numCopia,ClientID,DataInici,DataFi,DataReal,Perdut,Amortitzat")] Lloguer lloguer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lloguer).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.ClientID = new SelectList(db.Clients, "NIF", "Nom", lloguer.ClientID);
            //ViewBag.IDcopies = new SelectList(db.Copies, "IDmovie", "eMotiu", lloguer.IDcopies);
            return View(lloguer);
        }

        // GET: Lloguers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lloguer lloguer = db.Lloguers.Find(id);
            if (lloguer == null)
            {
                return HttpNotFound();
            }
            return View(lloguer);
        }

        // POST: Lloguers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lloguer lloguer = db.Lloguers.Find(id);
            db.Lloguers.Remove(lloguer);
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
