using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Jugador
    {
        public string Nombre { get; set; }
        private Jugador()
        {
        }

        private static List<Jugador> Jugadores = new List<Jugador>();

        public static Jugador ObtenerJugador(string nombre)
        {
            //Agregar logica de buscar en la lista y retornar
        }
    }
}
