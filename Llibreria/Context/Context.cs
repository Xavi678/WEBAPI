using Llibreria.Models;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Llibreria.Context
{
    public class MvcContext: DbContext
    {

        public MvcContext(): base("MvcContext")
        {
            Database.SetInitializer<MvcContext>(new DropCreateDatabaseIfModelChanges<MvcContext>());
        }
        
            public DbSet<Movie> Movies { get; set; }
              public DbSet<Copies> Copies { get; set; }
          public DbSet<Client> Clients { get; set; }
        public DbSet<Lloguer> Lloguers { get; set; }



    }
}