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
        public Jugador JugadorDos { get; set; }
        public Mazo Mazo { get; set; }

        public Partida(Mazo mazo, Jugador jugador)
        {
            this.Mazo = mazo;
            this.JugadorUno = jugador;
        }

        public enum TipoResultado 
        {
            Normal = 0,
            Amarilla = 1,
            Roja = 2,
            RojaAmarilla = 3,
            Especial = 4
        }


        public TipoResultado Cantar(string atributoseleccionado, string codigocarta) //ver como hacer control de la carta
        {
            TipoResultado Devolver = 0;
            int resultado = 0; 
            Carta cartaUno = this.JugadorUno.Cartas.First();
            Carta cartaDos = this.JugadorDos.Cartas.First();
            if (cartaUno.Tipo == Carta.TipoCarta.Normal && cartaDos.Tipo == Carta.TipoCarta.Normal)
            {
                List<string> nombresDeAtributos = new List<string>();
                foreach (Atributo item in cartaUno.Atributos)
                {
                    nombresDeAtributos.Add(item.Nombre);
                }
                int atributocantado = 0;
                int cont = 0;
                foreach (string item in nombresDeAtributos)
                {
                    if (atributoseleccionado == item)
                    {
                        atributocantado = cont;
                    }
                    cont += cont;
                }
                if (cartaUno.Atributos[atributocantado].Valor < cartaDos.Atributos[atributocantado].Valor)
                {
                    resultado = 2;
                }
                else
                {
                    resultado = 1;
                }
            }
            else
            {
                resultado = 0;
            }
            if (resultado == 0)
            {
                Devolver = this.ActualizarMazoEspecial();
            }
            else
            {
                this.ActualizaMazosNormal(resultado);
                Devolver = TipoResultado.Normal;
            }
            return Devolver;
        }

        public void ActualizaMazosNormal(int ganador)
        {
            if (ganador == 1)
            {
                var aux = this.JugadorUno.Cartas.First();
                var aux2 = this.JugadorDos.Cartas.First();
                this.JugadorUno.Cartas.RemoveAt(0);
                this.JugadorUno.Cartas.Add(aux);
                this.JugadorDos.Cartas.RemoveAt(0);
                this.JugadorUno.Cartas.Add(aux2);

            }
            if (ganador == 2)
            {
                var aux = this.JugadorDos.Cartas.First();
                this.JugadorDos.Cartas.RemoveAt(0);
                this.JugadorDos.Cartas.Add(aux);
                aux = this.JugadorUno.Cartas.First();
                this.JugadorUno.Cartas.RemoveAt(0);
                this.JugadorDos.Cartas.Add(aux);
            }
        }

        public TipoResultado ActualizarMazoEspecial()
        {
            TipoResultado resultado = 0;
            var CartaUno = this.JugadorUno.Cartas.First();
            var CartaDos = this.JugadorDos.Cartas.First();
            if (CartaDos.Tipo != Carta.TipoCarta.Especial & CartaUno.Tipo != Carta.TipoCarta.Especial)
            {
                if (CartaUno.Tipo == Carta.TipoCarta.Roja)
                {
                    if (CartaDos.Tipo == Carta.TipoCarta.Amarilla)
                    {
                        this.JugadorUno.Cartas.RemoveAt(0);
                        this.JugadorDos.Cartas.RemoveAt(0);
                        var aux = this.JugadorDos.Cartas.First();
                        this.JugadorDos.Cartas.RemoveAt(0);
                        this.JugadorUno.Cartas.Add(aux);
                        resultado = TipoResultado.RojaAmarilla;
                    }
                    if (CartaDos.Tipo == Carta.TipoCarta.Normal)
                    {
                        this.JugadorUno.Cartas.RemoveAt(0);
                        this.JugadorDos.Cartas.RemoveAt(0);
                        this.JugadorUno.Cartas.Add(CartaDos);
                        var aux = this.JugadorDos.Cartas.First();
                        this.JugadorDos.Cartas.RemoveAt(0);
                        this.JugadorUno.Cartas.Add(aux);
                        resultado = TipoResultado.Roja;
                    }
                }

                if (CartaDos.Tipo == Carta.TipoCarta.Roja)
                {
                    if (CartaUno.Tipo == Carta.TipoCarta.Amarilla)
                    {
                        this.JugadorDos.Cartas.RemoveAt(0);
                        this.JugadorUno.Cartas.RemoveAt(0);
                        var aux = this.JugadorUno.Cartas.First();
                        this.JugadorUno.Cartas.RemoveAt(0);
                        this.JugadorDos.Cartas.Add(aux);
                        resultado = TipoResultado.RojaAmarilla;
                    }
                    if (CartaUno.Tipo == Carta.TipoCarta.Normal)
                    {
                        this.JugadorDos.Cartas.RemoveAt(0);
                        this.JugadorUno.Cartas.RemoveAt(0);
                        this.JugadorDos.Cartas.Add(CartaUno);
                        var aux = this.JugadorUno.Cartas.First();
                        this.JugadorUno.Cartas.RemoveAt(0);
                        this.JugadorDos.Cartas.Add(aux);
                        resultado = TipoResultado.Roja;
                    }
                }
            }
            else
            {
                resultado = TipoResultado.Especial;
                if (CartaUno.Tipo == Carta.TipoCarta.Especial)
                {
                    this.JugadorDos.Cartas.RemoveAt(0);
                    this.JugadorDos.Cartas.Add(CartaDos);
                    var aux = this.JugadorDos.Cartas.Where(x => x.Codigo != "Hola").ToList();
                    aux.OrderBy(x => x.Atributos.First());
                    var mayor = aux.First();
                    this.JugadorDos.Cartas.Remove(mayor);
                    this.JugadorUno.Cartas.Add(mayor);
                }
                if (CartaDos.Tipo == Carta.TipoCarta.Especial)
                {
                    this.JugadorUno.Cartas.RemoveAt(0);
                    this.JugadorUno.Cartas.Add(CartaUno);
                    var aux = this.JugadorUno.Cartas.Where(x => x.Codigo !=  "Hola").ToList();
                    aux.OrderBy(x => x.Atributos.First());
                    var mayor = aux.First();
                    this.JugadorUno.Cartas.Remove(mayor);
                    this.JugadorDos.Cartas.Add(mayor);   
                }
            }
            return resultado;
        }

        public void Mezclar()
        {
            if (this.Mazo != null && this.JugadorDos != null)
            {
                for (int i = 0; i < (this.Mazo.Cartas.Count / 2); i++)
                {
                    Random random1 = new Random();
                    Random random2 = new Random();
                    int rand1 = random1.Next(0, this.Mazo.Cartas.Count + 1);
                    int rand2 = random2.Next(0, this.Mazo.Cartas.Count + 1);
                    while (random1 == random2)
                    {
                        rand2 = random2.Next(0, this.Mazo.Cartas.Count + 1);
                    }
                    Carta aux = new Carta();
                    aux = this.Mazo.Cartas[rand1];
                    this.Mazo.Cartas[rand1] = this.Mazo.Cartas[rand2];
                    this.Mazo.Cartas[rand2] = aux;
                }
            }
        }

        public void Repartir()
        {
            if (this.Mazo != null && this.JugadorDos != null)
            {
                int cont = 0;
                for (int i = 0; i < (this.Mazo.Cartas.Count) / 2; i++)
                {
                    this.JugadorUno.Cartas.Add(this.Mazo.Cartas[i]);
                    cont = i;
                }
                cont = cont + 1;
                for (int s = cont; s < this.Mazo.Cartas.Count; s++)
                {
                    this.JugadorDos.Cartas.Add(this.Mazo.Cartas[s]);
                }
            }

        }



    }
}
