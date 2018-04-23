using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Carta
    {
        public enum TipoCarta
        {
            Roja,
            Amarilla,
            Especial
        }
            
        public List<Atributo> Atributos { get; set; }
        public TipoCarta Tipo { get; set; }
        public string Codigo { get; set; }


        public Carta()
        {
            this.Atributos = new List<Atributo>();
        }
    }
}
