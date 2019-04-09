using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Llibreria.Models
{
    public class Lloguer
    {

        public Lloguer()
        {

        }
        public Lloguer(int dcopies, int numCopia, string clientID,DateTime ara,DateTime fi)
        {
            IDcopies = dcopies;
            this.numCopia = numCopia;
            ClientID = clientID;
            DataInici = ara;
            DataFi = fi;
            
        }



        public Lloguer(int dlloguer, int dcopies, int numCopia, string clientID, DateTime dataInici, DateTime dataFi, DateTime dataReal, bool perdut, bool amortitzat)
        {
            IDlloguer = dlloguer;
            IDcopies = dcopies;
            this.numCopia = numCopia;
            ClientID = clientID;
            DataInici = dataInici;
            DataFi = dataFi;
            DataReal = dataReal;
            Perdut = perdut;
            Amortitzat = amortitzat;
        }

        public Lloguer(int dlloguer, int dcopies, int numCopia, Copies copies, string clientID, Client client, DateTime dataInici, DateTime dataFi, DateTime dataReal, bool perdut, bool amortitzat)
        {
            IDlloguer = dlloguer;
            IDcopies = dcopies;
            this.numCopia = numCopia;
            Copies = copies;
            ClientID = clientID;
            Client = client;
            DataInici = dataInici;
            DataFi = dataFi;
            DataReal = dataReal;
            Perdut = perdut;
            Amortitzat = amortitzat;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDlloguer { get; set; }

        [ForeignKey("Copies"), Column(Order = 0)]
        public int IDcopies { get; set; }
        [ForeignKey("Copies"), Column(Order = 1)]
        public int numCopia { get; set; }
        public virtual Copies Copies { get; set; }



        [ForeignKey("Client")]
        public string ClientID { get; set; }


        public virtual Client Client { get; set; }
        [DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime DataInici { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataFi { get; set; }
        [DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DataReal { get; set; }
        public bool Perdut { get; set; }
        public bool Amortitzat { get; set; }




    }
}