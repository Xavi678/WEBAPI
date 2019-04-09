using Llibreria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Client.Controllers
{
    public class ConsultesController : Controller
    {
        // GET: Consultes
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ObtenirGuid(string login,string password)
        {
            try
            {
                string resultat = null;
                using (WebClient wc = new WebClient())
                {
                    var json = wc.DownloadString("http://localhost:51460//api/Values/Autenticacio?login=" + login + "&password=" + password);
                    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                    resultat = jsonSerializer.Deserialize<string>(json);

                }

                if (resultat != null)
                {
                    ViewBag.Resultat = resultat;
                }
                else
                {
                    ViewBag.Resultat = "Unauthorized";
                }



                return View();
            }catch(Exception e)
            {
                ViewBag.Resultat = "Client no autoritzat";
                return View();
            }
        }

        public ActionResult ObtenirGuid()
        {
            return View();
        }

        public ActionResult ObtenirPelicules()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ObtenirPelicules(string id)
        {
            try
            {
                List<Movie> movies = null;
                using (WebClient wc = new WebClient())
                {
                    var json = wc.DownloadString("http://localhost:51460//api/Values/obtenirPelicules?id=" + id);
                    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                    movies = jsonSerializer.Deserialize<List<Movie>>(json);

                }





                return View(movies);
            }catch(Exception e)
            {
                ModelState.AddModelError("", "Posi una token correcte");
                return View();
            }
        }

        public ActionResult ObtenirPeliculesNoRetornades()
        {
            //ModelState.AddModelError("", "Posi una token correcte");
            return View();
        }

        [HttpPost]
        public ActionResult ObtenirPeliculesNoRetornades(string id)
        {
            try
            {
                List<Movie> movies = null;
                using (WebClient wc = new WebClient())
                {
                    var json = wc.DownloadString("http://localhost:51460//api/Values/obtenirPeliculesNoRetornades?id=" + id);
                    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                    movies = jsonSerializer.Deserialize<List<Movie>>(json);

                }





                return View(movies);
            }catch(Exception E)
            {
                ModelState.AddModelError("", "Posi una token correcte");
                return View();
            }
        }

        public ActionResult CanviarPassword()
        {
            //ModelState.AddModelError("", "Posi una token correcte");
            return View();
            
        }

        [HttpPost]
        public ActionResult CanviarPassword(string id,string password)
        {
            try
            {
                List<Movie> movies = null;
                using (WebClient wc = new WebClient())
                {
                    wc.QueryString.Add("id", id);
                    wc.QueryString.Add("novaPass", password);
                    //wc.QueryString.Add("parameter3", "parameter 3 value.");

                    var data = wc.UploadValues("http://localhost:51460//api/Values/canviarPassword", "POST", wc.QueryString);


                    var responseString = UnicodeEncoding.UTF8.GetString(data);
                    //var json = wc.DownloadString("http://localhost:51460//api/Values/canviarPassword?id=" + id+"&novaPass="+password);
                    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                    //movies = jsonSerializer.Deserialize<List<Movie>>(json);

                }


                return View("CanviarPassword",null,"Success");
               
            }catch(Exception e)
            {

                return View("CanviarPassword",null,"No s'ha pogut canviar la Contra");
            }
        }


        public ActionResult TancarSessio()
        {

            return View();
        }

        [HttpPost]
        public ActionResult TancarSessio(string id)
        {
            string resposta = null;
            try
            {
                using (WebClient wc = new WebClient())
                {
                    //wc.QueryString.Add("id", id);

                    //wc.QueryString.Add("parameter3", "parameter 3 value.");

                   //var data = wc.UploadValues("http://localhost:51460//api/Values/tancarSessio", "GET", wc.QueryString);
                    var json = wc.DownloadString("http://localhost:51460//api/Values/tancarSessio?id=" + id );

                    //var responseString = UnicodeEncoding.UTF8.GetString(data);
                    //var json = wc.DownloadString("http://localhost:51460//api/Values/canviarPassword?id=" + id+"&novaPass="+password);
                    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                    resposta = jsonSerializer.Deserialize<String>(json);

                }

                return View("TancarSessio", null,"Success");

            }catch(Exception e)
            {
                return View("TancarSessio", null, "Hi ha hagut un problema a l'hora de tancar la sessió");

            }

            //return View();
        }
    }
}