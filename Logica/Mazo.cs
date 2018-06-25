using System.Collections.Generic;

namespace Juego.Entidades
{
    public class Mazo
    {
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
