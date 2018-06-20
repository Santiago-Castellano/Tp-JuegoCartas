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

        public enum ResultadoMano
        {
            Ganojugador1,
            Ganojugador2,
            CartaEspecial
        }
        public enum TipoResultado
        {
            Normal = 0,
            Amarilla = 1,
            Roja = 2
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

        private int DeterminaPosicionAtributo(Carta carta, string atributoseleccionado)
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

        private ResultadoMano CompararCartas(Carta cartauno, Carta cartados, int atributo)
        {
            ResultadoMano resultado;
            if (cartauno.Tipo == Carta.TipoCarta.Normal && cartados.Tipo == Carta.TipoCarta.Normal)
            {
                if (cartauno.Atributos[atributo].Valor < cartados.Atributos[atributo].Valor)
                {
                    resultado = ResultadoMano.Ganojugador2;
                    this.IdGanadorMano = this.JugadorDos.ConecctionID;
                    this.IdPerdedorMano = this.JugadorUno.ConecctionID;
                }
                else
                {
                    resultado = ResultadoMano.Ganojugador1;
                    this.IdGanadorMano = this.JugadorUno.ConecctionID;
                    this.IdPerdedorMano = this.JugadorDos.ConecctionID;
                }
            }
            else
            {
                resultado = ResultadoMano.CartaEspecial;
            }
            return resultado;
        }

        

        public TipoResultado Cantar(string atributoseleccionado)
        {
            TipoResultado Devolver;
            ResultadoMano resultado;
            
            Carta cartaUno = this.JugadorUno.Cartas.First();
            Carta cartaDos = this.JugadorDos.Cartas.First();

            int atributocantado = this.DeterminaPosicionAtributo(cartaUno, atributoseleccionado);
            resultado = this.CompararCartas(cartaUno, cartaDos, atributocantado);

            switch (resultado)
            {
                case ResultadoMano.CartaEspecial:
                    {
                        Devolver = this.ActualizarMazoEspecial(cartaUno, cartaDos);
                    }
                    break;
                case ResultadoMano.Ganojugador1:
                    {
                        this.ActualizaMazosNormal(1, cartaUno, cartaDos);
                        Devolver = TipoResultado.Normal;
                    }
                    break;
                default:
                    {
                        this.ActualizaMazosNormal(2, cartaUno, cartaDos);
                        Devolver = TipoResultado.Normal;
                    }
                    break;
            }

            this.ActualizarEstadoPartida();
            return Devolver;
        }

        private void ActualizaMazosNormal(int ganador, Carta cartauno, Carta cartados)
        {

            this.JugadorDos.Cartas.Remove(cartados);
            this.JugadorUno.Cartas.Remove(cartauno);
            if (ganador == 1)
            {
                this.JugadorUno.Cartas.Add(cartauno);
                this.JugadorUno.Cartas.Add(cartados);
            }
            if (ganador == 2)
            {
                this.JugadorDos.Cartas.Add(cartados);
                this.JugadorDos.Cartas.Add(cartauno);
            }
        }

        private TipoResultado ActualizarMazoEspecial(Carta cartauno, Carta cartados)
        {
            TipoResultado resultado = 0;
            this.JugadorUno.Cartas.Remove(cartauno);
            this.JugadorDos.Cartas.Remove(cartados);
            switch (cartauno.Tipo)
            {
                case Carta.TipoCarta.Roja:
                    {
                        JugadorUno.Cartas.Add(cartados);
                        var aux = this.JugadorDos.Cartas.First();
                        this.JugadorDos.Cartas.Remove(aux);
                        this.JugadorUno.Cartas.Add(aux);
                        resultado = TipoResultado.Roja;
                        this.IdGanadorMano = this.JugadorUno.ConecctionID;
                        this.IdPerdedorMano = this.JugadorDos.ConecctionID;
                        return resultado;
                    }
                    break;
                case Carta.TipoCarta.Amarilla:
                    {
                        if (cartados.Tipo != Carta.TipoCarta.Roja)
                        {
                            JugadorUno.Cartas.Add(cartados);
                            resultado = TipoResultado.Amarilla;
                            this.IdGanadorMano = this.JugadorUno.ConecctionID;
                            this.IdPerdedorMano = this.JugadorDos.ConecctionID;
                            return resultado;
                        }
                    }
                    break;

            }

            switch (cartados.Tipo)
            {
                case Carta.TipoCarta.Roja:
                    {
                        JugadorDos.Cartas.Add(cartauno);
                        var aux = this.JugadorUno.Cartas.First();
                        this.JugadorUno.Cartas.Remove(aux);
                        this.JugadorDos.Cartas.Add(aux);
                        resultado = TipoResultado.Roja;
                        this.IdGanadorMano = this.JugadorDos.ConecctionID;
                        this.IdPerdedorMano = this.JugadorUno.ConecctionID;
                        return resultado;
                    }
                    break;
                case Carta.TipoCarta.Amarilla:
                    {
                        if (cartauno.Tipo != Carta.TipoCarta.Roja)
                        {
                            JugadorDos.Cartas.Add(cartauno);
                            resultado = TipoResultado.Amarilla;
                            this.IdGanadorMano = this.JugadorDos.ConecctionID;
                            this.IdPerdedorMano = this.JugadorUno.ConecctionID;
                            return resultado;
                        }
                    }
                    break;
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
