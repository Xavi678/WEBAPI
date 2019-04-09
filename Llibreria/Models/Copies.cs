using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Llibreria.Models
{

   

    public class Copies
    {
       

      

        [Key]
        [Column(Order = 0)]
        public int IDmovie { set; get; }
        [Key]
        [Column(Order = 1)]
        public int numCopia { set; get; }
        public bool invalida { set; get; }
        public Motiu motiu { set; get; }
        public string eMotiu { set; get; }
        public Copies()
        {
          
        }

        public Copies(int dmovie, int v1, bool v2)
        {
            this.IDmovie = dmovie;
            this.numCopia = v1;
            this.invalida = v2;
        }

        public enum Motiu
        {
            Actiu,
           No_retornat,
           Espatllat,
           Error,
           Perdua,
           Altres
        }
    }

   

   
}