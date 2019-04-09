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
using MvcMovie.ViewModels;
using PagedList;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        private MvcContext db = new MvcContext();

        // GET: Movies
        public ActionResult Index(string movieGenre, string searchString, string searchString2,string sortOrder,string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDsortParm = String.IsNullOrEmpty(sortOrder) ? "id" : "";
            ViewBag.TitolsortParm =sortOrder=="Titol" ? "titol_desc" : "Titol";
            ViewBag.DatasortParm = sortOrder == "Data" ? "data_desc" : "Data";
            ViewBag.GeneresortParm = sortOrder == "Genere" ? "data_desc" : "Genere";
            ViewBag.PreusortParm = sortOrder == "Preu" ? "preu_desc" : "Preu";
            ViewBag.IMDBsortParm = sortOrder == "IMDB" ? "imdb_desc" : "IMDB";
            ViewBag.DuradasortParm = sortOrder == "Durada" ? "durada_desc" : "Durada";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var GenreLst = new List<string>();

            var GenreQry = from d in db.Movies
                           orderby d.Genere
                           select d.Genere;

            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.movieGenre = new SelectList(GenreLst);

            var movies = from m in db.Movies
                         select m;
           

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Titol.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(searchString2))
            {
                int temp = int.Parse(searchString2);
                movies = movies.Where(i => i.IMDB.Equals(temp));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genere == movieGenre);
            }

            switch (sortOrder)
            {
                case "id":
                    movies = movies.OrderByDescending(i => i.ID);
                    break;

                case "Titol":
                    movies = movies.OrderBy(i => i.Titol);
                    break;
                case "titol_desc":
                    movies = movies.OrderByDescending(i => i.Titol);
                    break;

                case "Data":
                    movies = movies.OrderBy(i => i.DataEstrena);
                    break;
                case "data_desc":
                    movies = movies.OrderByDescending(i => i.DataEstrena);
                    break;

                case "Genere":
                    movies = movies.OrderBy(i => i.Genere);
                    break;
                case "genere_desc":
                    movies = movies.OrderByDescending(i => i.Genere);
                    break;

                case "Preu":
                    movies = movies.OrderBy(i => i.Preu);
                    break;
                case "preu_desc":
                    movies = movies.OrderByDescending(i => i.Preu);
                    break;

                case "IMDB":
                    movies = movies.OrderBy(i => i.IMDB);
                    break;
                case "IMDB_desc":
                    movies = movies.OrderByDescending(i => i.IMDB);
                    break;

                case "Durada":
                    movies = movies.OrderBy(i => i.Durada);
                    break;
                case "durada_desc":
                    movies = movies.OrderByDescending(i => i.Durada);
                    break;
                default:
                    movies = movies.OrderBy(i => i.ID);
                    break;
            }

            List<MovieIndex> mv = new List<MovieIndex>();
            List<Movie> moviesResult=movies.ToList();
            for(int i = 0; i < moviesResult.Count(); i++)
            {
                var movieId = moviesResult[i].ID;
                var copies = (from c in db.Copies where c.IDmovie == movieId select c).Count();
                MovieIndex mindex = new MovieIndex
                {
                    ID = moviesResult[i].ID,
                    IMDB = moviesResult[i].IMDB,
                    Preu = moviesResult[i].Preu,
                    Genere = moviesResult[i].Genere,
                    Sinopsi = moviesResult[i].Sinopsi,
                    Titol = moviesResult[i].Titol,
                    Cartell = moviesResult[i].Cartell,
                    DataEstrena = moviesResult[i].DataEstrena,
                    Durada = moviesResult[i].Durada,
                    numCopies = copies
                };
                mv.Add(mindex);
            }
           
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(mv.ToPagedList(pageNumber, pageSize));
            
        }


        /*public ActionResult Index2(string movieGenre, string searchString, string searchString2, string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDsortParm = String.IsNullOrEmpty(sortOrder) ? "id" : "";
            ViewBag.TitolsortParm = sortOrder == "Titol" ? "titol_desc" : "Titol";
            ViewBag.DatasortParm = sortOrder == "Data" ? "data_desc" : "Data";
            ViewBag.GeneresortParm = sortOrder == "Genere" ? "data_desc" : "Genere";
            ViewBag.PreusortParm = sortOrder == "Preu" ? "preu_desc" : "Preu";
            ViewBag.IMDBsortParm = sortOrder == "IMDB" ? "imdb_desc" : "IMDB";
            ViewBag.DuradasortParm = sortOrder == "Durada" ? "durada_desc" : "Durada";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var GenreLst = new List<string>();

            var GenreQry = from d in db.Movies
                           orderby d.Genere
                           select d.Genere;

            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.movieGenre = new SelectList(GenreLst);

            var movies = from m in db.Movies
                         select m;


            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Titol.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(searchString2))
            {
                int temp = int.Parse(searchString2);
                movies = movies.Where(i => i.IMDB.Equals(temp));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genere == movieGenre);
            }

            switch (sortOrder)
            {
                case "id":
                    movies = movies.OrderByDescending(i => i.ID);
                    break;

                case "Titol":
                    movies = movies.OrderBy(i => i.Titol);
                    break;
                case "titol_desc":
                    movies = movies.OrderByDescending(i => i.Titol);
                    break;

                case "Data":
                    movies = movies.OrderBy(i => i.DataEstrena);
                    break;
                case "data_desc":
                    movies = movies.OrderByDescending(i => i.DataEstrena);
                    break;

                case "Genere":
                    movies = movies.OrderBy(i => i.Genere);
                    break;
                case "genere_desc":
                    movies = movies.OrderByDescending(i => i.Genere);
                    break;

                case "Preu":
                    movies = movies.OrderBy(i => i.Preu);
                    break;
                case "preu_desc":
                    movies = movies.OrderByDescending(i => i.Preu);
                    break;

                case "IMDB":
                    movies = movies.OrderBy(i => i.IMDB);
                    break;
                case "IMDB_desc":
                    movies = movies.OrderByDescending(i => i.IMDB);
                    break;

                case "Durada":
                    movies = movies.OrderBy(i => i.Durada);
                    break;
                case "durada_desc":
                    movies = movies.OrderByDescending(i => i.Durada);
                    break;
                default:
                    movies = movies.OrderBy(i => i.ID);
                    break;
            }

            List<MovieIndex> mv = new List<MovieIndex>();
            List<Movie> moviesResult = movies.ToList();
            for (int i = 0; i < moviesResult.Count(); i++)
            {
                var movieId = moviesResult[i].ID;
                var copies = (from c in db.Copies where c.IDmovie == movieId select c).Count();
                MovieIndex mindex = new MovieIndex
                {
                    ID = moviesResult[i].ID,
                    IMDB = moviesResult[i].IMDB,
                    Preu = moviesResult[i].Preu,
                    Genere = moviesResult[i].Genere,
                    Sinopsi = moviesResult[i].Sinopsi,
                    Titol = moviesResult[i].Titol,
                    Cartell = moviesResult[i].Cartell,
                    DataEstrena = moviesResult[i].DataEstrena,
                    Durada = moviesResult[i].Durada,
                    numCopies = copies
                };
                mv.Add(mindex);
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            

        }*/


        [HttpPost]
        public ActionResult DelCopies(int id)
        {
            var copies = db.Copies.Where(c => c.IDmovie == id).Select(c => c).ToList().LastOrDefault();
            if (copies != null)
            {
                db.Copies.Remove(copies);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Titol,DataEstrena,Genere,Preu,IMDB,Cartell,Durada")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,ReleaseDate,Genre,Price,IMDB,Cartell,Durada")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("/Clients");
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
