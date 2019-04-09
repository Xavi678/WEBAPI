using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Llibreria.Context;
using Llibreria.Models;
using PagedList;


namespace MvcMovie.Controllers
{
    public class ClientsController : Controller
    {
        private MvcContext db = new MvcContext();

        // GET: Clients
        public ActionResult Index(string searchString, string nom,string sortOrder,int? page, string currentFilter)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NifSortParm = String.IsNullOrEmpty(sortOrder) ? "nif" : "";
            ViewBag.NomSortParm = sortOrder == "Nom" ? "nom_desc" : "Nom";
            ViewBag.CognomSortParm = sortOrder == "Cognom" ? "cognom_desc" : "Cognom";
            ViewBag.PoblacioSortParm = sortOrder == "Poblacio" ? "poblacio_desc" : "Poblacio";
            ViewBag.ProvinciaSortParm = sortOrder == "Provincia" ? "provincia_desc" : "Provincia";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;





            var clients = from c in db.Clients select c;

           

            if (!String.IsNullOrEmpty(searchString))
            {
                if (Regex.Matches(searchString, @"[a-zA-Z]").Count!=-1)
                {
                    
                    searchString = searchString.Substring( 0,searchString.Length-1);
                }
                clients = clients.Where(s => s.NIF.StartsWith(searchString));
            }

            if (!String.IsNullOrEmpty(nom))
            {
                clients = clients.Where(s => s.Nom.Contains(nom) || s.Cognom.Contains(nom));
            }

            switch (sortOrder)
            {
                case "nif":
                    clients = clients.OrderByDescending(s => s.NIF);
                    break;

                case "Nom":
                    clients = clients.OrderBy(s => s.Nom);
                    break;

                case "nom_desc":
                    clients = clients.OrderByDescending(s => s.Nom);
                    break;

                case "Cognom":
                    clients = clients.OrderBy(s => s.Cognom);
                    break;

                case "cognom_desc":
                    clients = clients.OrderByDescending(s => s.Cognom);
                    break;

                case "Poblacio":
                    clients = clients.OrderBy(s => s.Poblacio);
                    break;

                case "poblacio_desc":
                    clients = clients.OrderByDescending(s => s.Poblacio);
                    break;

                case "Proivincia":
                    clients = clients.OrderBy(s => s.Provincia);
                    break;

                case "provincia_desc":
                    clients = clients.OrderByDescending(s => s.Provincia);
                    break;
                default:
                    clients = clients.OrderBy(s => s.NIF);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(clients.ToPagedList(pageNumber, pageSize));


        

            
        }

        // GET: Clients/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NIF,Nom,Cognom,Carrer,Numero,Poblacio,Provincia,CP,Tlf,Correu,Aut")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NIF,Nom,Cognom,DataNaixement,Carrer,Numero,Poblacio,Provincia,CP,Tlf,Correu,Aut")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(string id)
        {
            return DeleteConfirmed(id);
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);*/
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
       
        public ActionResult DeleteConfirmed(string id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
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
