using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Juego.Entidades;


namespace Juego.Web.Hubs
{
    public class JuegoHub : Hub
    {
        private static Entidades.Juego Juego = new Entidades.Juego();

        public void CrearPartida(string usuario, string partida, string mazo)
        {

            // Notifico a los otros usuarios de la nueva partida.
            var aux = Juego.CrearNuevaPartida(usuario, Context.ConnectionId, partida, mazo);

            Clients.Others.agregarPartida(new
            {
                Mazo = aux.Mazo.Nombre,
                Nombre = aux.Nombre,
                Usuario = aux.JugadorUno.Nombre
            });

            Clients.Caller.esperarJugador();
        }

        public void UnirsePartida(string usuario, string partida)
        {
            var partidanueva = Juego.UnirsePartida(usuario, Context.ConnectionId, partida);
            Clients.All.eliminarPartida(partidanueva.Nombre);
            Clients.Client(partidanueva.JugadorUno.ConecctionID).dibujarTablero(new { Cartas = partidanueva.JugadorUno.Cartas.Select(x => new { Codigo = x.Codigo, Nombre = x.Nombre }), Nombre = partidanueva.JugadorUno.Nombre }, new { Cartas = partidanueva.JugadorDos.Cartas.Select(x => new { Codigo = x.Codigo, Nombre = x.Nombre }), Nombre = partidanueva.JugadorDos.Nombre }, new { Nombre = partidanueva.Mazo.Nombre, NombreAtributos = partidanueva.Mazo.NombreAtributos });
            Clients.Client(partidanueva.JugadorDos.ConecctionID).dibujarTablero(new { Cartas = partidanueva.JugadorUno.Cartas.Select(x => new { Codigo = x.Codigo, Nombre = x.Nombre }), Nombre = partidanueva.JugadorUno.Nombre }, new { Cartas = partidanueva.JugadorDos.Cartas.Select(x => new { Codigo = x.Codigo, Nombre = x.Nombre }), Nombre = partidanueva.JugadorDos.Nombre }, new { Nombre = partidanueva.Mazo.Nombre, NombreAtributos = partidanueva.Mazo.NombreAtributos });

        }

        public void ObtenerPartidas()
        {
            Clients.Caller.agregarPartidas(Juego.ObtenerPartidasPendientes().Select(x => new
            {
                Mazo = x.Mazo.Nombre,
                Nombre = x.Nombre,
                Usuario = x.JugadorUno.Nombre
            }));
        }

        public void ObtenerMazos()
        {
            var mazos = Juego.ObtenerMazos();
            List<string> mazosstring = mazos.Select(x => x.Nombre).ToList();
            Clients.Caller.agregarMazos(mazosstring);
        }

        public void DefinirGanador(string idGanador,string idperdedor, Partida.TipoResultado resultado)
        {
            switch (resultado)
            {
                case Partida.TipoResultado.Normal:
                    {
                        Clients.Client(idGanador).ganarMano();
                        Clients.Client(idperdedor).perderMano();
                    }
                    break;
                case Partida.TipoResultado.Amarilla:
                    {
                        Clients.Client(idGanador).ganarManoPorTarjetaAmarilla();
                        Clients.Client(idperdedor).perderManoPorTarjetaAmarilla();
                    }
                    break;
                case Partida.TipoResultado.Roja:
                    {
                        Clients.Client(idGanador).ganarManoPorTarjetaRoja();
                        Clients.Client(idperdedor).perderManoPorTarjetaRoja();

                    }
                    break;
            }
        }

        public void Cantar(string idAtributo, string idCarta)
        {
            var partida = Juego.ObtenerPartida(Context.ConnectionId);
            var resultado = partida.Cantar(idAtributo);
            this.DefinirGanador(partida.IdGanadorMano, partida.IdPerdedorMano, resultado);
            if (partida.EsPartidaFinalizada())
            {
                Clients.Caller.ganar();
                Clients.Client(partida.IdPerdedorMano).perder();
            }
        }
    }
}