using System.Collections.Generic;

namespace Juego.Entidades
{
    public class Jugador
    {
        public string Nombre { get; set; }
        public List<Carta> Cartas { get; set; }
        public string ConecctionID { get; set; }

        public Jugador()
        {
            this.Cartas = new List<Carta>();
        }

    }
}
