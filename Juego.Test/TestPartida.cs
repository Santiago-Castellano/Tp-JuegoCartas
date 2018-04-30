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

            var mazoaux = mazo.Cartas.Where(x => Convert.ToInt32(x.Codigo) >= 0);
            
            Partida partida = new Partida(mazo, Jugador.ObtenerJugador("Santi"));
            partida.Mezclar();

            Assert.AreEqual(mazoaux,partida.Mazo.Cartas);

        }
    }
}
