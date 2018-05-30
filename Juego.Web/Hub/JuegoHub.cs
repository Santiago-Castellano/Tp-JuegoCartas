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
            //Clients.Others.agregarPartida(nuevaPartida);
             
            //Clients.Caller.esperarJugador();
        }

        public void UnirsePartida(string usuario, string partida)
        {
            //Clients.All.eliminarPartida(partidaAUnirse.Nombre);

            //Clients.Client(jugador1.ConnectionId).dibujarTablero(jugador1, jugador2, partidaAUnirse.Mazo);
            //Clients.Client(jugador2.ConnectionId).dibujarTablero(jugador1, jugador2, partidaAUnirse.Mazo);

        }

        public void ObtenerPartidas()
        {
            //Clients.Caller.agregarPartidas(partidas);
        }

        public void ObtenerMazos()
        {
            var mazos = Juego.ObtenerMazos();
            var mazosstring = mazos.Select(x => x.Nombre).ToList();
            Clients.Caller.agregarMazos(mazosstring);
        }

        public void Cantar(string idAtributo, string idCarta)
        {
            //if (jugada.connectionIdGanador == Context.ConnectionId)
            //{
            //    Clients.Caller.ganarMano(resultado, false);
            //    Clients.Client(jugada.connectionIdPerdedor).perderMano(resultado, false);
            //
            //}
            //else
            //{
            //    Clients.Client(jugada.connectionIdGanador).ganarMano(resultado, false);
            //    Clients.Caller.perderMano(resultado, false);
            //
            //}
            //if (jugada.finalizoJuego)
            //{
            //    Clients.Caller.ganar();
            //    Clients.Client(jugada.connectionIdPerdedor).perder();
            //}
        }
    }
}