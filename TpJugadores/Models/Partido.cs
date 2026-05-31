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
        // Fecha del partido
        public DateTime Fecha { get; set; }

        // Equipo que juega de local
        public Equipo EquipoLocal { get; set; }

        // Equipo que juega de visitante
        public Equipo EquipoVisitante { get; set; }

        // Goles del local
        public int GolesLocal { get; set; }

        // Goles del visitante
        public int GolesVisitante { get; set; }

        // Constructor
        public Partido(DateTime fecha, Equipo local, Equipo visitante)
        {
            Fecha = fecha;
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

        // Devuelve el resultado en texto
        public string MostrarResultado()
        {
            return $"{EquipoLocal.Nombre} {GolesLocal} - {GolesVisitante} {EquipoVisitante.Nombre}";
        }
    }
}
