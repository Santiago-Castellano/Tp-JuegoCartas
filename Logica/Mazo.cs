using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Mazo
    {
        public string Codigo { get; set; }
        public List<Carta> Cartas { get; set; }

        public Mazo()
        {
            this.Cartas = new List<Carta>();
        }
    }
}
