using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Llibreria.Models
{
    public class Client
    {
        [Key]
        public string NIF { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string Nom { get; set; }
        public string Cognom { get; set; }
        public Guid? token { get; set; }
        public DateTime? dataToken { get; set; }

       
        public string Carrer { get; set; }
        public string Numero { get; set; }
        public string Poblacio { get; set; }
        public string Provincia { get; set; }
        public int CP { get; set; }
        public int Tlf { get; set; }
        public string Correu { get; set; }
        public string Aut { get; set; }

        public Boolean caducat()
        {

            
            if (dataToken.Value.AddDays(1).CompareTo(DateTime.Now) > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

   
    
}