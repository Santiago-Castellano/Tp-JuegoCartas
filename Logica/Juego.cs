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
        public List <Partida> Partidas { get; set; }
        public List <Mazo> Mazos { get; set; }

        public Juego()
        {
            this.Partidas = new List<Partida>();
            var lines = File.ReadAllLines(@"C:\Users\santi\Desktop\Tp-JuegoCartas");
            Mazo mazo = new Mazo() {Nombre = lines[0] };
            int contador = 0;
            char[] delimitador = { '|' };
            var forma = lines[1].Split(delimitador);
            foreach (var line in lines)
            {
               if(contador > 1)
                {
                    var aux = line.Split(delimitador);
                    Carta carta = new Carta()
                    {
                        Nombre =aux[1],
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
    }
}
