using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego.Entidades
{
    public class Juego
    {
        public List <Partida> Partidas { get; set; }
        public List <Mazo> Mazos { get; set; }

        public Juego()
        {
            this.Partidas = new List<Partida>();
            this.Mazos = new List<Mazo>();
        }


        public List<Partida> ObtenerPartidasPendientes()
        {
            List<Partida> partidasPendientes = new List<Partida>();
            foreach (Partida item in this.Partidas)
            {
                if (item.JugadorDos == null)
                {
                    partidasPendientes.Add(item);
                }
            }
            return partidasPendientes;
        }

        public List<Mazo> ObtenerMazos()
        {
            return this.Mazos;
        }
    }
}
