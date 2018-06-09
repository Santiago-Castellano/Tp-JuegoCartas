using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego.Entidades
{
    public class Mazo
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public List<Carta> Cartas { get; set; }
        public List<string> NombreAtributos { get; set; }

        public Mazo()
        {
            this.Cartas = new List<Carta>();
            this.NombreAtributos = new List<string>();
        }
    }
}
