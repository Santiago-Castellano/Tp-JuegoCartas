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
        public string Nombre { get; set; }
        private bool PartidaFinalizada { get; set; }
        public string IdGanadorMano { get; set; }
        public string IdPerdedorMano { get; set; }

        public Partida(Mazo mazo, Jugador jugador)
        {
            this.Mazo = mazo;
            this.JugadorUno = jugador;
            this.PartidaFinalizada = false;
        }

        public enum TipoResultado 
        {
            Normal = 0,
            Amarilla = 1,
            Roja = 2,
            RojaAmarilla = 3,
            Especial = 4
        }
        public bool EsPartidaFinalizada()
        {
            return this.PartidaFinalizada;
        }


        private void ActualizarEstadoPartida()
        {
            if (this.JugadorUno.Cartas.Count == 0)
            {
                this.PartidaFinalizada = true;
            }
            if (this.JugadorDos.Cartas.Count == 0)
            {
                this.PartidaFinalizada = true;
            }
        }

        public void ComenzarJuego()
        {
            if (this.Mazo != null && this.JugadorDos != null)
            {
                this.Mezclar();
                this.Repartir();
            }
        }

        private int DeterminaAtributo(Carta carta, string atributoseleccionado)
        {
            List<string> nombresDeAtributos = new List<string>();
            foreach (Atributo item in carta.Atributos)
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
            return atributocantado;
        }

        private string CompararCartas(Carta cartauno, Carta cartados, int atributo)
        {
            string resultado;
            if (cartauno.Tipo == Carta.TipoCarta.Normal && cartados.Tipo == Carta.TipoCarta.Normal)
            {
                if (cartauno.Atributos[atributo].Valor < cartados.Atributos[atributo].Valor)
                {
                    resultado = "Gano jugador 2";
                    this.IdGanadorMano = this.JugadorDos.ConecctionID;
                    this.IdPerdedorMano = this.JugadorUno.ConecctionID;
                }
                else
                {
                    resultado = "Gano jugador 1";
                    this.IdGanadorMano = this.JugadorUno.ConecctionID;
                    this.IdPerdedorMano = this.JugadorDos.ConecctionID;
                }
            }
            else
            {
                resultado = "Carta especial";
            }
            return resultado;
        }


        public TipoResultado Cantar(string atributoseleccionado, string codigocarta) //ver como hacer control de la carta
        {
            TipoResultado Devolver;
            string resultado; 
            Carta cartaUno = this.JugadorUno.Cartas.First();
            Carta cartaDos = this.JugadorDos.Cartas.First();
            int atributocantado = this.DeterminaAtributo(cartaUno, atributoseleccionado);
            resultado = this.CompararCartas(cartaUno,cartaDos,atributocantado);
            
            switch (resultado)
            {
                case "Carta especial":
                    {
                        Devolver = this.ActualizarMazoEspecial();
                    }
                    break;
                case "Gano jugador 1":
                    {
                        this.ActualizaMazosNormal(1);
                        Devolver = TipoResultado.Normal;
                    }
                    break;
                default:
                    {
                        this.ActualizaMazosNormal(2);
                        Devolver = TipoResultado.Normal;
                    }
                    break;
            }

            this.ActualizarEstadoPartida();
            return Devolver;
        }

        private void ActualizaMazosNormal(int ganador)
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

        private TipoResultado ActualizarMazoEspecial()
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

        private void Mezclar()
        {
            if (this.Mazo != null && this.JugadorDos != null)
            {
                Random random = new Random();
                int rand = -1;
                List<int> orden = new List<int>();
                bool Existe = false;
                for (int i = 0; i < this.Mazo.Cartas.Count; i++)
                {
                    rand = random.Next(0, this.Mazo.Cartas.Count);
                    Existe = false;
                    while (Existe == false)
                    {
                        foreach (var item in orden)
                        {
                            if (rand == item)
                            {
                                Existe = true;
                            }
                        }
                        if (Existe == false)
                        {
                            orden.Add(rand);
                            Existe = true;
                        }
                        else
                        {
                            rand = random.Next(0, this.Mazo.Cartas.Count);
                            Existe = false;
                        }
                    }

                    foreach (var item in orden)
                    {
                        var reacomodado = this.Mazo.Cartas[item];
                        this.Mazo.Cartas.Remove(reacomodado);
                        this.Mazo.Cartas.Add(reacomodado);
                    }


                }
            }
        }

        private void Repartir()
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
