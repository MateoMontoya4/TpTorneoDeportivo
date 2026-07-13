using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpJugadores.Models
{
    // Clase que representa un partido
    public class Partido
    {
        

        // Equipo que juega de local
        public Equipo EquipoLocal { get; set; }

        // Equipo que juega de visitante
        public Equipo EquipoVisitante { get; set; }

        // Goles del local
        public int GolesLocal { get; set; }

        // Goles del visitante
        public int GolesVisitante { get; set; }

        // Constructor
        public Partido( Equipo local, Equipo visitante)
        {
            
            EquipoLocal = local;
            EquipoVisitante = visitante;

            GolesLocal = 0;
            GolesVisitante = 0;
        }

        // Guarda el resultado del partido
        public void RegistrarResultado(int golesLocal, int golesVisitante)
        {
            GolesLocal = golesLocal;
            GolesVisitante = golesVisitante;

            if (golesLocal > golesVisitante)
            {
                EquipoLocal.Victorias++;
                EquipoVisitante.Derrotas++;
            }
            else if (golesLocal < golesVisitante)
            {
                EquipoVisitante.Victorias++;
                EquipoLocal.Derrotas++;
            }
            else
            {
                EquipoLocal.Empates++;
                EquipoVisitante.Empates++;
            }
        }

      
    }
}
