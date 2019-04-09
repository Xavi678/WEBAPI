using Llibreria.Context;
using Llibreria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace ApiVideoClub.Controllers
{
    public class ValuesController : ApiController
    {
        private MvcContext db = new MvcContext();
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet]
        [ResponseType(typeof(Guid))]
        public IHttpActionResult Autenticacio(string login,string password)
        {
           
            var client = db.Clients.Where(x => x.login.Equals(login)).Select(x => x).FirstOrDefault();

            if (client == null)
            {
                return this.NotFound();
            }

            if (client.password.Equals(password))
            {
                client.dataToken= DateTime.Now;
                Guid id = Guid.NewGuid();

                client.token = id;

                db.SaveChanges();

                return this.Ok(id);
            }
            else
            {
                return this.NotFound();
            }

               

        }

        [HttpGet]
        [ResponseType(typeof(List<Movie>))]
        public IHttpActionResult obtenirPelicules(string id)
        {
           
            //var movies = db.Clients.Where(m=>m.)
            var client = db.Clients.Where(i => i.token.ToString().Equals(id)).Select(i => i).FirstOrDefault();

            if (client != null)
            {
                if (client.caducat())
                {
                    return this.NotFound();
                }
                var idMovie = db.Lloguers.Where(m => m.ClientID.Equals(client.NIF)).Select(m => m.IDcopies).ToList();

                //List<Movie> pelis=db.Movies.Where(m => m.ID.Equals(idMovie)).Select(m => m).ToList();

                List<Movie> pelis= new List<Movie>();

                foreach (var item in idMovie)
                {

                    pelis.Add(db.Movies.Where(m => m.ID.Equals(item)).Select(m => m).FirstOrDefault());
                }

                return this.Ok(pelis);

            }
            else
            {
                return this.NotFound();
            }
        }

        [HttpGet]
        [ResponseType(typeof(List<Movie>))]
        public IHttpActionResult obtenirPeliculesNoRetornades(string id)
        {

            //var movies = db.Clients.Where(m=>m.)
            var client = db.Clients.Where(i => i.token.ToString().Equals(id)).Select(i => i).FirstOrDefault();

            if (client != null)
            {
                if (client.caducat())
                {
                    return this.NotFound();
                }
                var movies = db.Lloguers.Where(m => m.ClientID.Equals(client.NIF) && m.DataReal==null).Select(m => m.IDcopies).ToList();

                //List<Movie> pelis=db.Movies.Where(m => m.ID.Equals(idMovie)).Select(m => m).ToList();

                List<Movie> pelis = new List<Movie>();

                foreach (var item in movies)
                {

                    pelis.Add(db.Movies.Where(m => m.ID.Equals(item)).Select(m => m).FirstOrDefault());
                }

                return this.Ok(pelis);

            }
            else
            {
                return this.NotFound();
            }
        }

        [HttpPost]
        public IHttpActionResult canviarPassword(string id,string novaPass)
        {
            if (novaPass == null)
            {
                return this.BadRequest();
            }
            //var movies = db.Clients.Where(m=>m.)
            var client = db.Clients.Where(i => i.token.ToString().Equals(id)).Select(i => i).FirstOrDefault();

            if (client != null)
            {
                if (client.caducat())
                {
                    return this.NotFound();
                }

                //var movies = db.Lloguers.Where(m => m.ClientID.Equals(client.NIF) && m.DataReal == null).Select(m => m).ToList();
                client.password = novaPass;
                //List<Movie> pelis=db.Movies.Where(m => m.ID.Equals(idMovie)).Select(m => m).ToList();

                db.SaveChanges();

                return this.Ok();

            }
            else
            {
                return this.NotFound();
            }
        }

        [HttpGet]
        public IHttpActionResult tancarSessio(string id)
        {

            //var movies = db.Clients.Where(m=>m.)
            var client = db.Clients.Where(i => i.token.ToString().Equals(id)).Select(i => i).FirstOrDefault();

            if (client != null)
            {
                if (client.caducat())
                {
                    return this.NotFound();
                }
                //var movies = db.Lloguers.Where(m => m.ClientID.Equals(client.NIF) && m.DataReal == null).Select(m => m).ToList();
                client.token = null;
                //List<Movie> pelis=db.Movies.Where(m => m.ID.Equals(idMovie)).Select(m => m).ToList();

                db.SaveChanges();

                return this.Ok();

            }
            else
            {
                return this.NotFound();
            }
        }






        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
