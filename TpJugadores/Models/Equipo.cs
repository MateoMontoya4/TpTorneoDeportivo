using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpJugadores.Models
{
    //La clase Equipo implementa la interfaz IPuntuable para calcular sus puntos
    public class Equipo : IPuntuable
    {
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
        
        // Elimina un jugador de la lista del equipo usando su nombre
        public bool EliminarJugador(string nombreJugador)
        {
            // Busca al jugador comparando los nombres en minúscula para que no haya drama con las mayúsculas
            var jugador = Jugadores.FirstOrDefault(j => j.Nombre.ToLower() == nombreJugador.ToLower());

            if (jugador != null)
            {
                return Jugadores.Remove(jugador); // Lo borra y devuelve true
            }
            return false; // Si no lo encuentra, devuelve false
        }
        // Multiplica las victorias por 3 y le suma los empates para dar el puntaje real
        public int CalcularPuntos()
        {
            return (Victorias * 3) + Empates;
        }

        // Arma un texto básico con el nombre del equipo y sus puntos para mostrar en el menú
        public string MostrarInfo()
        {
            return $"{Nombre} - Puntos: {CalcularPuntos()}";
        }
        // Suma todo para saber cuántos partidos jugó el equipo en total
        public int TotalPartidos()
        {
            return Victorias + Empates + Derrotas;
        }

        // Porcentaje de victorias
        public double PorcentajeVictorias()
        {
            if (TotalPartidos() == 0)
                return 0;

            return (double)Victorias * 100 / TotalPartidos();
        }

        // Saca la cuenta matemática del porcentaje de partidos empatados
        public double PorcentajeEmpates()
        {
            if (TotalPartidos() == 0)
                return 0;

            return (double)Empates * 100 / TotalPartidos();
        }

        // Saca la cuenta matemática del porcentaje de partidos perdidos
        public double PorcentajeDerrotas()
        {
            if (TotalPartidos() == 0)
                return 0;

            return (double)Derrotas * 100 / TotalPartidos();
        }
    }
}
