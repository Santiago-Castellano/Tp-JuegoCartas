using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego.Entidades
{
    public class Partida
    {
        public Jugador JugadorUno { get; set; }
        public Jugador JugadorDos {get; set;}
        public Mazo Mazo { get; set; }

     
       

        public Partida(Mazo mazo, Jugador jugador)
        {
            this.Mazo = mazo;
            this.JugadorUno = jugador;
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

        public void Repartir()
        {
            int cont = 0;

            for (int i = 0; i <= (this.Mazo.Cartas.Count)/2; i++)
            {
                this.JugadorUno.Cartas.Add(this.Mazo.Cartas[i]);
                cont = i;
            }
            cont = cont + 1;
            for (int s = cont; s <= this.Mazo.Cartas.Count; s ++)
            {
                this.JugadorDos.Cartas.Add(this.Mazo.Cartas[s]);
            }

        }



    }
}
