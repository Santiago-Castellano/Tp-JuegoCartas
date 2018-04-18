using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Partida
    {
        public Jugador JuegadorUno { get; set; }
        public Jugador JugadorDos {get; set;}
        public Mazo Mazo { get; set; }

        public enum Turnos
        {
            TurnoUno,
            TurnoDos
        }
        public Turnos Turno { get; set; }

        public Partida()
        {
            Turno = Turnos.TurnoUno;
        }

        public void CambioDeTurno()
        {
            if (this.Turno == Turnos.TurnoUno)
            {
                this.Turno = Turnos.TurnoDos;
            }
            else
            {
                this.Turno = Turnos.TurnoUno;
            }
        }
    }
}
