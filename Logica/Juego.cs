using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego.Entidades
{
    public class Juego
    {
        private List <Partida> Partidas { get; set; }
        private List <Mazo> Mazos { get; set; }

        public Juego()
        {
            this.Partidas = new List<Partida>();
            this.Mazos = new List<Mazo>();
            this.LeerMazos();
            
        }

        private void LeerMazos()
        {
           var deckFolder = Directory.GetDirectories(@"C:\Users\santi\Desktop\Tp-JuegoCartas\Juego.Web\Mazos");
            foreach (var deck in deckFolder)
            {
                Mazo mazo = new Mazo();

                var lines = File.ReadAllLines(deck + "\\Informacion.txt");
                mazo.Nombre = lines[0];
                int contador = 0;
                char[] delimitador = { '|' };
                var forma = lines[1].Split(delimitador);
                for (int i = 2; i < forma.Count(); i++)
                {
                    mazo.NombreAtributos.Add(forma[i]);
                }
                foreach (var line in lines)
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
                        this.AgregaAtributo(carta, aux, forma);
                        mazo.Cartas.Add(carta);
                    }
                    contador += 1;
                }
                Mazos.Add(mazo);
            }
        }

        public Partida UnirsePartida(string nombrejugador, string coneccionjugador,string nombrepartida)
        {
            Jugador jugadornuevo = new Jugador() {Nombre = nombrejugador, ConecctionID = coneccionjugador };
            var partida = this.Partidas.SingleOrDefault(x => x.Nombre == nombrepartida);
            partida.JugadorDos = jugadornuevo;
            partida.ComenzarJuego();
            return partida;
        }

        public Partida CrearNuevaPartida(string jugadorunonombre,string jugadorunoconeccion, string nombrepartida, string nombremazo)
        {
            var mazoseleccionado = this.Mazos.SingleOrDefault(x => x.Nombre == nombremazo);
            var NuevoJugador = new Jugador() {Nombre = jugadorunonombre, ConecctionID = jugadorunoconeccion };
            Partida partida = new Partida(mazoseleccionado, NuevoJugador) {Nombre = nombrepartida };
            partida.JugadorUno = NuevoJugador;
            Partidas.Add(partida);
            return partida;
        }

        public void EliminarPartida(string nombrepartida)
        {

            var partidaeliminar = this.Partidas.SingleOrDefault(x => x.Nombre == nombrepartida);
            this.Partidas.Remove(partidaeliminar);
        }

        private void AgregaAtributo(Carta carta, string[] linea, string[] formadeatributos)
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
