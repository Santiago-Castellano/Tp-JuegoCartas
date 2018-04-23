using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Partida
    {
        public Jugador JuegadorUno { get; set; }
        public Jugador JugadorDos {get; set;}
        public Mazo Mazo { get; set; }

     
       

        public Partida(Mazo mazo, Jugador jugador)
        {
            this.Mazo = mazo;
            this.JuegadorUno = jugador;
        }


        public void Mezclar()
        {
            for (int i = 0; i < (this.Mazo.Cartas.Count/2); i++)
            {
                Random random1 = new Random(this.Mazo.Cartas.Count + 1);
                Random random2 = new Random(this.Mazo.Cartas.Count + 1);
                while (random1 == random2)
                {
                    random2 = new Random(this.Mazo.Cartas.Count + 1);
                }
                Carta aux = new Carta();
                aux = this.Mazo.Cartas[Convert.ToInt32(random1)];
                this.Mazo.Cartas[Convert.ToInt32(random1)] = this.Mazo.Cartas[Convert.ToInt32(random2)];
                this.Mazo.Cartas[Convert.ToInt32(random2)] = aux;
            }
        }

        



    }
}
