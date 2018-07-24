using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Juego.Entidades
{
    public class Juego
    {
        private List<Partida> Partidas { get; set; }
        private List<Mazo> Mazos { get; set; }

        public Juego()
        {
            this.Partidas = new List<Partida>();
            this.Mazos = new List<Mazo>();
            this.LeerMazos();

        }

        private void LeerMazos()
        {
            var CarpetaMazos = Directory.GetDirectories(@"C:\Users\santi\Desktop\Tp-JuegoCartas\Juego.Web\Mazos");
            foreach (var cartas in CarpetaMazos)
            {
                Mazo mazo = new Mazo();
                this.AgregaCartasEspecialesAlMazo(mazo);
                var Lineas = File.ReadAllLines(cartas + "\\Informacion.txt");
                this.AgregaCartasAlMazo(mazo, Lineas);
                Mazos.Add(mazo);
            }
        }

        private void AgregaCartasAlMazo(Mazo mazo, string[] Lineas)
        {
            mazo.Nombre = Lineas[0];
            int contador = 0;
            char[] delimitador = { '|' };
            var forma = Lineas[1].Split(delimitador);
            for (int i = 2; i < forma.Count(); i++)
            {
                mazo.NombreAtributos.Add(forma[i]);
            }
            foreach (var line in Lineas)
            {
                if (contador > 1)
                {
                    var aux = line.Split(delimitador);
                    Carta carta = new Carta()
                    {
                        Nombre = aux[1],
                        Codigo = aux[0],
                        Tipo = Carta.TipoCarta.Normal
                    };
                    this.AgregaAtributosAUnaCarta(carta, aux, forma);
                    mazo.Cartas.Add(carta);

                }
                contador += 1;
            }
            
        }

        private void AgregaCartasEspecialesAlMazo(Mazo mazo)
        {
            Carta roja = new Carta();
            roja.Atributos.Add(new Atributo() { Nombre = "Cantar" });
            roja.Tipo = Carta.TipoCarta.Roja;
            roja.Nombre = "ROJA";
            roja.Codigo = "roja";
            Carta amarilla = new Carta();
            amarilla.Atributos.Add(new Atributo() { Nombre = "Cantar" });
            amarilla.Tipo = Carta.TipoCarta.Amarilla;
            amarilla.Nombre = "AMARILLA";
            amarilla.Codigo = "amarilla";
            mazo.Cartas.Add(roja);
            mazo.Cartas.Add(amarilla);
        }

        private void AgregaAtributosAUnaCarta(Carta carta, string[] linea, string[] formadeatributos)
        {
            for (int i = 0; i < linea.Count(); i++)
            {
                if (i > 1)
                {
                    Atributo atributo = new Atributo()
                    {
                        Nombre = formadeatributos[i],
                        Valor = Convert.ToDecimal(linea[i]),
                    };
                    carta.Atributos.Add(atributo);
                }
            }
        }

        public Partida UnirsePartida(string nombrejugador, string conexionjugador, string nombrepartida)
        {
            Jugador jugadornuevo = new Jugador() { Nombre = nombrejugador, ConecctionID = conexionjugador };
            var partida = this.Partidas.SingleOrDefault(x => x.Nombre == nombrepartida);
            partida.JugadorDos = jugadornuevo;
            partida.ComenzarJuego();
            return partida;
        }

        public Partida CrearNuevaPartida(string jugadorunonombre, string jugadorunoconexion, string nombrepartida, string nombremazo)
        {
            var mazoseleccionado = this.Mazos.SingleOrDefault(x => x.Nombre == nombremazo);
            var NuevoJugador = new Jugador() { Nombre = jugadorunonombre, ConecctionID = jugadorunoconexion };
            Partida partida = new Partida(mazoseleccionado, NuevoJugador) { Nombre = nombrepartida };
            partida.JugadorUno = NuevoJugador;
            Partidas.Add(partida);
            return partida;
        }

        public void EliminarPartida(string nombrepartida)
        {

            var partidaeliminar = this.Partidas.SingleOrDefault(x => x.Nombre == nombrepartida);
            this.Partidas.Remove(partidaeliminar);
        }

        

        public List<Partida> ObtenerPartidasPendientes()
        {
            List<Partida> partidasPendientes = new List<Partida>();
            partidasPendientes = this.Partidas.Where(x => x.JugadorDos == null).ToList();
            return partidasPendientes;
        }

        public List<Mazo> ObtenerMazos()
        {
            return this.Mazos;
        }

        public Partida ObtenerPartida(string idjugador)
        {
            Partida devolver = this.Partidas.SingleOrDefault(x => x.JugadorUno.ConecctionID == idjugador);
            if (devolver == null)
            {
                devolver = this.Partidas.SingleOrDefault(x => x.JugadorDos.ConecctionID == idjugador);
            }
            return devolver;
        }
    }
}
