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

            Clients.Others.agregarPartida(new {
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
            Clients.Client(partidanueva.JugadorUno.ConecctionID).dibujarTablero(new { Cartas = partidanueva.JugadorUno.Cartas.Select(x => new { Codigo = x.Codigo, Nombre = x.Nombre }), Nombre = partidanueva.JugadorUno.Nombre }, new { Cartas = partidanueva.JugadorDos.Cartas, Nombre = partidanueva.JugadorDos.Nombre }, new { Nombre = partidanueva.Mazo.Nombre, NombreAtributos = partidanueva.Mazo.NombreAtributos });
            Clients.Client(partidanueva.JugadorDos.ConecctionID).dibujarTablero(new { Cartas = partidanueva.JugadorUno.Cartas.Select(x => new { Codigo = x.Codigo, Nombre = x.Nombre }), Nombre = partidanueva.JugadorUno.Nombre }, new { Cartas = partidanueva.JugadorDos.Cartas, Nombre = partidanueva.JugadorDos.Nombre }, new { Nombre = partidanueva.Mazo.Nombre, NombreAtributos = partidanueva.Mazo.NombreAtributos });

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