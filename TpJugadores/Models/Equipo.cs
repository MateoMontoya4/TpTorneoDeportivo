using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpJugadores.Models
{
    //La clase Equipo implementa la interfaz IPuntuable para calcular sus puntos
    public class Equipo : IPuntuable, IEntidad
    {
        public int id { get; set; }


        // Nombre del equipo
        public string Nombre { get; set; }

        // Contadores para las estadísticas del torneo
        public int Victorias { get; set; }
        public int Empates { get; set; }
        public int Derrotas { get; set; }


        // La lista donde guardamos a todos los jugadores que pertenecen a este equipo
        public List<Jugador> Jugadores { get; set; }

        // El constructor mete el nombre del equipo y arranca todo en cero y la lista vacía
        public Equipo(string nombre)
        {
            Nombre = nombre;

            Victorias = 0;
            Empates = 0;
            Derrotas = 0;

            // Se crea la lista vacia de jugadores
            Jugadores = new List<Jugador>();
        }

        // Recibe un jugador ya creado y lo mete en la lista del equipo
        public void AgregarJugador(Jugador jugador)
        {
            Jugadores.Add(jugador);
        }
        
        
        // Multiplica las victorias por 3 y le suma los empates para dar el puntaje real
        public int CalcularPuntos()
        {
            return (Victorias * 3) + Empates;
        }


   

    }
}
