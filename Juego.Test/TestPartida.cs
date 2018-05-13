using System;
using Juego.Entidades;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Juego.Test
{
    [TestClass]
    public class TestPartida
    {

        [TestMethod]
        public void DeberiaMezclarMazo()
        {

            Mazo mazo = new Mazo();
            for (int i = 0; i < 10; i++)
            {
                mazo.Cartas.Add(new Carta {Codigo = i.ToString()} );
            }

            var mazoaux = mazo.Cartas.Where(x => Convert.ToInt32(x.Codigo) >= 0).ToList();
            
            Partida partida = new Partida(mazo, Jugador.ObtenerJugador("Santi"));
            partida.Mezclar();

            Assert.AreNotEqual(mazoaux,partida.Mazo.Cartas);
        }

        [TestMethod]
        public void DeberiaRepartirCartas()
        {
            Mazo mazo = new Mazo();
            for (int i = 0; i < 10; i++)
            {
                mazo.Cartas.Add(new Carta { Codigo = i.ToString() });
            }

            Partida partida = new Partida(mazo, Jugador.ObtenerJugador("Santi"));
            partida.JugadorDos = Jugador.ObtenerJugador("Mica");

            partida.Repartir();

            bool bandera = false;

            for (int i = 0; i < (partida.JugadorUno.Cartas.Count-1); i++)
            {
                for (int s = (i + 1); s < partida.JugadorUno.Cartas.Count; s++)
                {
                    if (partida.JugadorUno.Cartas[i].Codigo == partida.JugadorUno.Cartas[s].Codigo)
                    {
                        bandera = true;
                    }

                }

            }

            for (int i = 0; i < (partida.JugadorDos.Cartas.Count - 1); i++)
            {
                for (int s = (i + 1); s < partida.JugadorDos.Cartas.Count; s++)
                {
                    if (partida.JugadorDos.Cartas[i].Codigo == partida.JugadorDos.Cartas[s].Codigo)
                    {
                        bandera = true;
                    }

                }

            }

            for (int i = 0; i < (partida.JugadorUno.Cartas.Count - 1); i++)
            {
                for (int s = 0 ; s < partida.JugadorDos.Cartas.Count; s++)
                {
                    if (partida.JugadorUno.Cartas[i].Codigo == partida.JugadorDos.Cartas[s].Codigo)
                    {
                        bandera = true;
                    }

                }

            }

            Assert.AreEqual(bandera, false);
            Assert.AreEqual(partida.JugadorDos.Cartas.Count, partida.JugadorUno.Cartas.Count);
           
        }

        [TestMethod]

        public void DeberiaSerPar()
        {
            Mazo mazo = new Mazo();

            for (int i = 0; i < 10; i++)
            {
                mazo.Cartas.Add(new Carta { Codigo = i.ToString() });
            }

            bool band = false;
            var aux = Math.Pow((-1), (mazo.Cartas.Count));
           if  (aux > 0)
            {
                band = true;
            }
            Assert.AreEqual(band,true);

        }

       [TestMethod]
       public void DeberiaActualizaMazosNormalGanador1()
       {
            Mazo mazo = new Mazo();

            for (int i = 0; i < 10; i++)
            {
                mazo.Cartas.Add(new Carta { Codigo = i.ToString() });
            }

            Partida partida = new Partida(mazo, Jugador.ObtenerJugador("Santi"));
            partida.JugadorDos = Jugador.ObtenerJugador("Mica");

            partida.Repartir();
            partida.ActualizaMazosNormal(1);

            var Jugador2 = (partida.JugadorDos.Cartas.Count+2);
            var Jugador1 = (partida.JugadorUno.Cartas.Count);

            Assert.AreEqual(Jugador1, Jugador2);
           
        }

        [TestMethod]
        public void DeberiaActualizaMazosNormalGanador2()
        {
            Mazo mazo = new Mazo();

            for (int i = 0; i < 10; i++)
            {
                mazo.Cartas.Add(new Carta { Codigo = i.ToString() });
            }

            Partida partida = new Partida(mazo, Jugador.ObtenerJugador("Santi"));
            partida.JugadorDos = Jugador.ObtenerJugador("Mica");

            partida.Repartir();
            partida.ActualizaMazosNormal(2);

            var Jugador2 = (partida.JugadorDos.Cartas.Count);
            var Jugador1 = (partida.JugadorUno.Cartas.Count + 2);

            Assert.AreEqual(Jugador1, Jugador2);

        }

        [TestMethod]
        public void DeberiaActualizaMazoEspecial1()
        { // Jugador 1 con Carta ROJA  y Jugador 2 con Carta NORMAL
            Mazo mazo = new Mazo();

            for (int i = 0; i < 8; i++)
            {
                mazo.Cartas.Add(new Carta { Codigo = i.ToString(),Tipo = Carta.TipoCarta.Normal });
            }
            
            Partida partida = new Partida(mazo, Jugador.ObtenerJugador("Santi"));
            partida.JugadorDos = Jugador.ObtenerJugador("Mica");

            partida.Repartir();
            Carta CartaRoja = new Carta();
            CartaRoja.Tipo = Carta.TipoCarta.Roja;
            partida.JugadorUno.Cartas.Insert(0, CartaRoja);
            Carta CartaNormal = new Carta();
            CartaNormal.Tipo  = Carta.TipoCarta.Normal;
            CartaNormal.Codigo = "8";
            partida.JugadorDos.Cartas.Add(CartaNormal);
            partida.ActualizarMazoEspecial();

            var Jugador2 = (partida.JugadorDos.Cartas.Count + 4 );
            var Jugador1 = (partida.JugadorUno.Cartas.Count);

            Assert.AreEqual(Jugador1,Jugador2);
       
        }

        [TestMethod]
        public void DeberiaActualizaMazoEspecial2()
        // Jugador 1 con Carta ROJA  y Jugador 2 con Carta AMARILLA

        {
            Mazo mazo = new Mazo();

            for (int i = 0; i < 8; i++)
            {
                mazo.Cartas.Add(new Carta { Codigo = i.ToString(), Tipo = Carta.TipoCarta.Normal });
            }

            Partida partida = new Partida(mazo, Jugador.ObtenerJugador("Santi"));
            partida.JugadorDos = Jugador.ObtenerJugador("Mica");
            partida.Repartir();
            Carta CartaRoja = new Carta();
            CartaRoja.Tipo = Carta.TipoCarta.Roja;
            partida.JugadorUno.Cartas.Insert(0, CartaRoja);
            Carta CartaAmarilla = new Carta();
            CartaAmarilla.Tipo = Carta.TipoCarta.Amarilla;
            partida.JugadorDos.Cartas.Insert(0,CartaAmarilla);

            partida.ActualizarMazoEspecial();
            var Jugador2 = (partida.JugadorDos.Cartas.Count + 4);
            var Jugador1 = (partida.JugadorUno.Cartas.Count);

            Assert.AreEqual(Jugador1, Jugador2);
              
        }


        [TestMethod]

        public void DeberiaActualizaMazoEspecial3()
        {  // Jugador 2 con Carta ROJA  y Jugador 1 con Carta NORMAL

            Mazo mazo = new Mazo();

            for (int i = 0; i < 8; i++)
            {
                mazo.Cartas.Add(new Carta { Codigo = i.ToString(), Tipo = Carta.TipoCarta.Normal });
            }

            Partida partida = new Partida(mazo, Jugador.ObtenerJugador("Santi"));
            partida.JugadorDos = Jugador.ObtenerJugador("Mica");
            partida.Repartir();

            Carta CartaRoja = new Carta();
            CartaRoja.Tipo = Carta.TipoCarta.Roja;
            partida.JugadorDos.Cartas.Insert(0, CartaRoja);
            Carta CartaNormal = new Carta();
            CartaNormal.Tipo = Carta.TipoCarta.Normal;
            CartaNormal.Codigo = "8";
            partida.JugadorUno.Cartas.Add(CartaNormal);
            partida.ActualizarMazoEspecial();

            var Jugador1 = (partida.JugadorUno.Cartas.Count + 4);
            var Jugador2 = (partida.JugadorDos.Cartas.Count);
    
            Assert.AreEqual(Jugador1, Jugador2);

        }

        [TestMethod]
        public void DeberiaActualizaMazoEspecial4()
        // Jugador 2 con Carta ROJA  y Jugador 1 con Carta AMARILLA

        {
            Mazo mazo = new Mazo();

            for (int i = 0; i < 8; i++)
            {
                mazo.Cartas.Add(new Carta { Codigo = i.ToString(), Tipo = Carta.TipoCarta.Normal });
            }

            Partida partida = new Partida(mazo, Jugador.ObtenerJugador("Santi"));
            partida.JugadorDos = Jugador.ObtenerJugador("Mica");
            partida.Repartir();
            Carta CartaRoja = new Carta();
            CartaRoja.Tipo = Carta.TipoCarta.Roja;
            partida.JugadorDos.Cartas.Insert(0, CartaRoja);
            Carta CartaAmarilla = new Carta();
            CartaAmarilla.Tipo = Carta.TipoCarta.Amarilla;
            partida.JugadorUno.Cartas.Insert(0, CartaAmarilla);

            partida.ActualizarMazoEspecial();
            var Jugador2 = (partida.JugadorDos.Cartas.Count);
            var Jugador1 = (partida.JugadorUno.Cartas.Count + 4);

            Assert.AreEqual(Jugador1, Jugador2);

        }

        [TestMethod]
        public void DeberiaActualizaMazoEspecial5()
        // Jugador 1 con carta ESPECIAL
        {
            Mazo mazo = new Mazo();

            for (int i = 0; i < 8; i++)
            {
                mazo.Cartas.Add(new Carta { Codigo = i.ToString(), Tipo = Carta.TipoCarta.Normal });
            }

            Partida partida = new Partida(mazo, Jugador.ObtenerJugador("Santi"));
            partida.JugadorDos = Jugador.ObtenerJugador("Mica");

            partida.Repartir();
            Carta CartaEspecial = new Carta();
            CartaEspecial.Tipo = Carta.TipoCarta.Especial;
            partida.JugadorUno.Cartas.Insert(0, CartaEspecial);
            Carta CartaNormal = new Carta();
            CartaNormal.Tipo = Carta.TipoCarta.Normal;
            CartaNormal.Codigo = "8";
            partida.JugadorDos.Cartas.Add(CartaNormal);

             //FALTA TERMINAR
            /*   Buscar el mayor segun el atributo 0 en Jugador 2
                 Llamar al metodo
                 Comparar el mayor con el ultimo elemento de la lista de Jugador 2
           */
partida.ActualizarMazoEspecial();




}


}
}
