using System;
using Juego.Entidades;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Juego.Test
{
    [TestClass]
    public class TestDelJuego
    {

        [TestMethod]
        public void DeberiaComenzarBienLaPartida()
        {
            Mazo mazo = new Mazo();
            for (int i = 0; i < 10; i++)
            {
                mazo.Cartas.Add(new Carta { Codigo = i.ToString() });
            }

            Mazo mazo2 = new Mazo();
            mazo2.Cartas = mazo.Cartas.Where(x => Convert.ToInt32(x.Codigo) >= 0).ToList();

            Partida partida = new Partida(mazo, new Jugador() { Nombre = "Santi" }) { JugadorDos = new Jugador() { Nombre = "Mica" } };
            partida.ComenzarJuego();
            Assert.AreNotEqual(mazo2, partida.Mazo.Cartas);
            Assert.AreEqual(partida.JugadorDos.Cartas.Count, partida.JugadorUno.Cartas.Count);
        }

        [TestMethod]
        public void DeberiaLeerMazos()
        {
            Juego.Entidades.Juego juego = new Entidades.Juego();
            var aux = juego.ObtenerMazos();

            Assert.AreNotEqual(aux.Count, 0);
        }

        [TestMethod]
        public void DeberiaCrearJuegador()
        {
            Jugador jugadorUno = new Jugador() { Nombre = "santi", ConecctionID = "123456" };

            Jugador jugadorDos = new Jugador() { Nombre = "Mica", ConecctionID = "6534" };

            Assert.AreNotEqual(jugadorUno, jugadorDos);

        }

        [TestMethod]
        public void DeberiaObtenerPartidas()
        {
            Juego.Entidades.Juego juego = new Entidades.Juego();
            juego.CrearNuevaPartida("santi", "1234", "partida1", "Mundial");
            var aux = juego.ObtenerPartidasPendientes();

            Assert.AreEqual(aux.Count, 1);

        }

        [TestMethod]
        public void DeberiaObtenerPartida()
        {
            Juego.Entidades.Juego juego = new Entidades.Juego();
            juego.CrearNuevaPartida("santi", "1234", "partida1", "Mundial");
            juego.UnirsePartida("mica", "6789", "partida1");

            var partida = juego.ObtenerPartida("1234");
            var aux = juego.ObtenerPartida("6789");


            Assert.AreEqual(partida.Nombre, "partida1");
            Assert.AreEqual(partida.Mazo.Nombre, "Mundial");
            Assert.AreEqual(partida.JugadorUno.Nombre, "santi");
            Assert.AreEqual(partida, aux);
        }
        [TestMethod]
        public void DeberiaEliminarPartida()
        {
            Juego.Entidades.Juego juego = new Entidades.Juego();
            juego.CrearNuevaPartida("santi", "1234", "partida1", "Mundial");
            juego.UnirsePartida("mica", "6789", "partida1");

            juego.EliminarPartida("partida1");

            Assert.AreEqual(juego.ObtenerPartidasPendientes().Count, 0);
        }
        [TestMethod]
        public void DeberiaCrearCarta()
        {
            Carta carta = new Carta();
            carta.Codigo = "1";
            carta.Nombre = "cartauno";
            carta.Tipo = Carta.TipoCarta.Normal;
            carta.Atributos.Add(new Atributo() { Nombre = "DEF", Valor = 20 });
            Atributo atributo = new Atributo();
            atributo.Nombre = "MED";
            atributo.Valor = 40;
            carta.Atributos.Add(atributo);
            Assert.AreEqual(carta.Codigo, "1");
            Assert.AreEqual(carta.Nombre, "cartauno");
            Assert.AreEqual(carta.Tipo, Carta.TipoCarta.Normal);
            Assert.AreEqual(carta.Atributos.Count, 2);
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
            double aux = Math.Pow((-1), (mazo.Cartas.Count));
            if (aux > 0)
            {
                band = true;
            }
            Assert.AreEqual(band, true);

        }

        [TestMethod]
        public void DeberiaCantarBien_Especial_GugadorDosAmarilla()
        {
            //JUGADOR UNO CON CARTA NORMAL
            Juego.Entidades.Juego juego = new Entidades.Juego();
            juego.CrearNuevaPartida("santi", "1234", "partida1", "Mundial");
            juego.UnirsePartida("mica", "6789", "partida1");

            var partida = juego.ObtenerPartida("1234");

            Carta cartauno = new Carta();
            string nombreatributo1 = partida.JugadorUno.Cartas.First().Atributos.First().Nombre;
            cartauno.Tipo = Carta.TipoCarta.Normal;
            Carta cartados = new Carta();
            cartados.Atributos.Add(new Atributo() { Nombre = nombreatributo1, Valor = 0 });
            cartados.Tipo = Carta.TipoCarta.Amarilla;
            partida.JugadorUno.Cartas.Insert(0, cartauno);
            partida.JugadorDos.Cartas.Insert(0, cartados);

            var resultado = partida.Cantar(nombreatributo1);

            Assert.AreEqual(resultado, Partida.TipoResultado.Amarilla);
        }

        [TestMethod]
        public void DeberiaCantarBien_Especial_GugadorUnoAmarilla()
        {
            //JUGADOR DOS CON NORMAL
            Juego.Entidades.Juego juego = new Entidades.Juego();
            juego.CrearNuevaPartida("santi", "1234", "partida1", "Mundial");
            juego.UnirsePartida("mica", "6789", "partida1");

            var partida = juego.ObtenerPartida("1234");

            Carta cartauno = new Carta();
            string nombreatributo1 = partida.JugadorUno.Cartas.First().Atributos.First().Nombre;
            cartauno.Tipo = Carta.TipoCarta.Amarilla;
            Carta cartados = new Carta();
            cartados.Atributos.Add(new Atributo() { Nombre = nombreatributo1, Valor = 0 });
            cartados.Tipo = Carta.TipoCarta.Normal;
            partida.JugadorUno.Cartas.Insert(0, cartauno);
            partida.JugadorDos.Cartas.Insert(0, cartados);

            var resultado = partida.Cantar(nombreatributo1);

            Assert.AreEqual(resultado, Partida.TipoResultado.Amarilla);
        }

        [TestMethod]
        public void DeberiaCantarBien_Especial_GanadorDosRoja1()
        {
            //JUGADOR UNO CON CARTA NORMAL
            Juego.Entidades.Juego juego = new Entidades.Juego();
            juego.CrearNuevaPartida("santi", "1234", "partida1", "Mundial");
            juego.UnirsePartida("mica", "6789", "partida1");

            var partida = juego.ObtenerPartida("1234");

            Carta cartauno = new Carta();
            string nombreatributo1 = partida.JugadorUno.Cartas.First().Atributos.First().Nombre;
            cartauno.Tipo = Carta.TipoCarta.Normal;
            Carta cartados = new Carta();
            cartados.Atributos.Add(new Atributo() { Nombre = nombreatributo1, Valor = 0 });
            cartados.Tipo = Carta.TipoCarta.Roja;
            partida.JugadorDos.Cartas.Insert(0, cartados);
            partida.JugadorUno.Cartas.Insert(0, cartauno);

            var resultado = partida.Cantar(nombreatributo1);

            Assert.AreEqual(Partida.TipoResultado.Roja, resultado);

        }
        [TestMethod]
        public void DeberiaCantarBien_Especial_GanadorDosRoja2()
        {
            //JUGADOR UNO CON CARTA AMARILLA
            Juego.Entidades.Juego juego = new Entidades.Juego();
            juego.CrearNuevaPartida("santi", "1234", "partida1", "Mundial");
            juego.UnirsePartida("mica", "6789", "partida1");

            var partida = juego.ObtenerPartida("1234");

            Carta cartauno = new Carta();
            string nombreatributo1 = partida.JugadorUno.Cartas.First().Atributos.First().Nombre;
            cartauno.Tipo = Carta.TipoCarta.Amarilla;
            Carta cartados = new Carta();
            cartados.Atributos.Add(new Atributo() { Nombre = nombreatributo1, Valor = 0 });
            cartados.Tipo = Carta.TipoCarta.Roja;
            partida.JugadorDos.Cartas.Insert(0, cartados);
            partida.JugadorUno.Cartas.Insert(0, cartauno);

            var resultado = partida.Cantar(nombreatributo1);

            Assert.AreEqual(Partida.TipoResultado.Roja, resultado);

        }

        [TestMethod]
        public void DeberiaCantarBien_Especial_jugadorUnoRoja1()
        {
            //JUGADOR DOS CON CARTA NORMAL
            Juego.Entidades.Juego juego = new Entidades.Juego();
            juego.CrearNuevaPartida("santi", "1234", "partida1", "Mundial");
            juego.UnirsePartida("mica", "6789", "partida1");
            var partida = juego.ObtenerPartida("1234");
            Carta cartauno = new Carta();
            string nombreatributo1 = partida.JugadorUno.Cartas.First().Atributos.First().Nombre;
            cartauno.Tipo = Carta.TipoCarta.Roja;
            Carta cartados = new Carta();
            cartados.Atributos.Add(new Atributo() { Nombre = nombreatributo1, Valor = 0 });
            cartados.Tipo = Carta.TipoCarta.Normal;
            partida.JugadorUno.Cartas.Insert(0, cartauno);
            partida.JugadorDos.Cartas.Insert(0, cartados);
            partida.JugadorUno.Cartas.RemoveAll(x => x.Tipo != Carta.TipoCarta.Roja);
            var resultado = partida.Cantar(nombreatributo1);

            Assert.AreEqual(resultado, Partida.TipoResultado.Roja);
        }
        [TestMethod]
        public void DeberiaCantarBien_Especial_GugadorUnoRoja2()
        {
            //JUGADOR DOS CON CARTA AMARILLA
            Juego.Entidades.Juego juego = new Entidades.Juego();
            juego.CrearNuevaPartida("santi", "1234", "partida1", "Mundial");
            juego.UnirsePartida("mica", "6789", "partida1");
            var partida = juego.ObtenerPartida("1234");
            Carta cartauno = new Carta();
            string nombreatributo1 = partida.JugadorUno.Cartas.First().Atributos.First().Nombre;
            cartauno.Tipo = Carta.TipoCarta.Roja;
            Carta cartados = new Carta();
            cartados.Atributos.Add(new Atributo() { Nombre = nombreatributo1, Valor = 0 });
            cartados.Tipo = Carta.TipoCarta.Amarilla;
            partida.JugadorUno.Cartas.Insert(0, cartauno);
            partida.JugadorDos.Cartas.Insert(0, cartados);
            partida.JugadorUno.Cartas.RemoveAll(x => x.Tipo != Carta.TipoCarta.Roja);
            var resultado = partida.Cantar(nombreatributo1);

            Assert.AreEqual(resultado, Partida.TipoResultado.Roja);
        }

        [TestMethod]
        public void DeberiaCantarBien_CartasNormales_GanadorUno()
        {
            Juego.Entidades.Juego juego = new Entidades.Juego();
            juego.CrearNuevaPartida("santi", "1234", "partida1", "Mundial");
            juego.UnirsePartida("mica", "6789", "partida1");

            var partida = juego.ObtenerPartida("1234");

            Carta cartauno = new Carta();
            string nombreatributo1 = partida.JugadorUno.Cartas.First().Atributos.First().Nombre;
            cartauno.Atributos.Add(new Atributo() { Nombre = nombreatributo1, Valor = 10 });
            Carta cartados = new Carta();
            cartados.Atributos.Add(new Atributo() { Nombre = nombreatributo1, Valor = 0 });

            partida.JugadorUno.Cartas.Insert(0, cartauno);
            partida.JugadorDos.Cartas.Insert(0, cartados);
            var listauno = partida.JugadorUno.Cartas;
            var listados = partida.JugadorDos.Cartas;
            listauno.Remove(cartauno);
            listados.Remove(cartados);
            listauno.Add(cartauno);
            listauno.Add(cartados);
            var resultado = partida.Cantar(nombreatributo1);

            Assert.AreEqual(partida.JugadorUno.Cartas.Count, listauno.Count);
            Assert.AreEqual(resultado, Partida.TipoResultado.Normal);
        }


        [TestMethod]
        public void DeberiaCantarBien_CartasNormales_GanadorDos()
        {
            Juego.Entidades.Juego juego = new Entidades.Juego();


            juego.CrearNuevaPartida("santi", "1234", "partida1", "Mundial");
            juego.UnirsePartida("mica", "6789", "partida1");

            var partida = juego.ObtenerPartida("1234");

            Carta cartauno = new Carta();
            string nombreatributo1 = partida.JugadorUno.Cartas.First().Atributos.First().Nombre;
            cartauno.Atributos.Add(new Atributo() { Nombre = nombreatributo1, Valor = 0 });
            Carta cartados = new Carta();
            cartados.Atributos.Add(new Atributo() { Nombre = nombreatributo1, Valor = 10 });

            partida.JugadorUno.Cartas.Insert(0, cartauno);
            partida.JugadorDos.Cartas.Insert(0, cartados);
            var listauno = partida.JugadorUno.Cartas;
            var listados = partida.JugadorDos.Cartas;
            listauno.Remove(cartauno);
            listados.Remove(cartados);
            listados.Add(cartados);
            listados.Add(cartauno);
            var aux = partida.Cantar(nombreatributo1);

            Assert.AreEqual(partida.JugadorDos.Cartas.Count, listados.Count);
            Assert.AreEqual(aux, Partida.TipoResultado.Normal);
        }

        [TestMethod]
        public void DeberiaValidarIDganadorUno()
        {
            Juego.Entidades.Juego juego = new Entidades.Juego();


            juego.CrearNuevaPartida("santi", "1234", "partida1", "Mundial");
            juego.UnirsePartida("mica", "6789", "partida1");

            var partida = juego.ObtenerPartida("1234");

            Carta cartauno = new Carta();
            cartauno.Tipo = Carta.TipoCarta.Normal;
            string nombreatributo1 = partida.JugadorUno.Cartas.First().Atributos.First().Nombre;
            cartauno.Atributos.Add(new Atributo() { Nombre = nombreatributo1, Valor = 10 });
            Carta cartados = new Carta();
            cartados.Atributos.Add(new Atributo() { Nombre = nombreatributo1, Valor = 0 });
            cartados.Tipo = Carta.TipoCarta.Normal;
            partida.JugadorUno.Cartas.Insert(0, cartauno);
            partida.JugadorDos.Cartas.Insert(0, cartados);

            var resultado = partida.Cantar(nombreatributo1);

            Assert.AreEqual(resultado, Partida.TipoResultado.Normal);
            Assert.AreEqual(partida.IdGanadorMano, partida.JugadorUno.ConecctionID);
            Assert.AreEqual(partida.IdPerdedorMano, partida.JugadorDos.ConecctionID);

        }
        [TestMethod]
        public void DeberiaValidarIDganadorDos()
        {
            Juego.Entidades.Juego juego = new Entidades.Juego();


            juego.CrearNuevaPartida("santi", "1234", "partida1", "Mundial");
            juego.UnirsePartida("mica", "6789", "partida1");

            var partida = juego.ObtenerPartida("1234");

            Carta cartauno = new Carta();
            cartauno.Tipo = Carta.TipoCarta.Normal;
            string nombreatributo1 = partida.JugadorUno.Cartas.First().Atributos.First().Nombre;
            cartauno.Atributos.Add(new Atributo() { Nombre = nombreatributo1, Valor = 0 });
            Carta cartados = new Carta();
            cartados.Atributos.Add(new Atributo() { Nombre = nombreatributo1, Valor = 10 });
            cartados.Tipo = Carta.TipoCarta.Normal;
            partida.JugadorUno.Cartas.Insert(0, cartauno);
            partida.JugadorDos.Cartas.Insert(0, cartados);

            var resultado = partida.Cantar(nombreatributo1);

            Assert.AreEqual(resultado, Partida.TipoResultado.Normal);
            Assert.AreEqual(partida.IdGanadorMano, partida.JugadorDos.ConecctionID);
            Assert.AreEqual(partida.IdPerdedorMano, partida.JugadorUno.ConecctionID);

        }
        [TestMethod]
        public void DeberiaTerminarPartida_JugadorUnoPierde()
        {

            Juego.Entidades.Juego juego = new Entidades.Juego();
            juego.CrearNuevaPartida("santi", "1234", "partida1", "Mundial");
            juego.UnirsePartida("mica", "6789", "partida1");

            var partida = juego.ObtenerPartida("1234");

            Carta cartauno = new Carta();
            string nombreatributo1 = partida.JugadorUno.Cartas.First().Atributos.First().Nombre;
            cartauno.Atributos.Add(new Atributo() { Nombre = nombreatributo1, Valor = 0 });
            Carta cartados = new Carta();
            cartados.Atributos.Add(new Atributo() { Nombre = nombreatributo1, Valor = 10 });
            cartauno.Codigo = "cartauno";
            cartados.Codigo = "cartados";
            cartauno.Tipo = Carta.TipoCarta.Normal;
            cartados.Tipo = Carta.TipoCarta.Normal;
            partida.JugadorUno.Cartas.Insert(0, cartauno);
            partida.JugadorDos.Cartas.Insert(0, cartados);

            partida.JugadorUno.Cartas.RemoveAll(x => x.Codigo != "cartauno");
            partida.JugadorDos.Cartas.RemoveAll(x => x.Codigo != "cartados");

            partida.Cantar(nombreatributo1);

            Assert.AreEqual(true, partida.EsPartidaFinalizada());
        }

        [TestMethod]
        public void DeberiaTerminarPartida_JugadorDosPierde()
        {

            Juego.Entidades.Juego juego = new Entidades.Juego();
            juego.CrearNuevaPartida("santi", "1234", "partida1", "Mundial");
            juego.UnirsePartida("mica", "6789", "partida1");

            var partida = juego.ObtenerPartida("1234");

            Carta cartauno = new Carta();
            string nombreatributo1 = partida.JugadorUno.Cartas.First().Atributos.First().Nombre;
            cartauno.Atributos.Add(new Atributo() { Nombre = nombreatributo1, Valor = 10 });
            Carta cartados = new Carta();
            cartados.Atributos.Add(new Atributo() { Nombre = nombreatributo1, Valor = 0 });
            cartauno.Codigo = "cartauno";
            cartados.Codigo = "cartados";
            cartauno.Tipo = Carta.TipoCarta.Normal;
            cartados.Tipo = Carta.TipoCarta.Normal;
            partida.JugadorUno.Cartas.Insert(0, cartauno);
            partida.JugadorDos.Cartas.Insert(0, cartados);

            partida.JugadorUno.Cartas.RemoveAll(x => x.Codigo != "cartauno");
            partida.JugadorDos.Cartas.RemoveAll(x => x.Codigo != "cartados");

            partida.Cantar(nombreatributo1);

            Assert.AreEqual(true, partida.EsPartidaFinalizada());
        }

    }
}

