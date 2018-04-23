using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego.Entidades
{
    public class Jugador
    {
        public string Nombre { get; set; }
        public List<Carta> Cartas { get; set; }
        private Jugador()
        {
            this.Cartas = new List<Carta>();
        }

        private static List<Jugador> Jugadores = new List<Jugador>();

        public static Jugador ObtenerJugador(string nombre)
        {
            var agregar = Jugadores.SingleOrDefault(x => x.Nombre == nombre);
            if (agregar == null)
            {
                agregar = new Jugador();
                agregar.Nombre = nombre;
            }

            return agregar;
        }
    }
}
