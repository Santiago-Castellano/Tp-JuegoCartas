using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego.Entidades
{
    public class Carta
    {
        public enum TipoCarta
        {
            Roja,
            Amarilla,
            Normal
        }

        public List<Atributo> Atributos { get; set; }
        public TipoCarta Tipo { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }


        public Carta()
        {
            this.Atributos = new List<Atributo>();
        }
    }
}
