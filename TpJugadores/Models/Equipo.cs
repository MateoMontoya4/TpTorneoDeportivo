using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpJugadores.Models
{
    public class Equipo : IPuntuable
    {
        // Nombre del equipo
        public string Nombre { get; set; }

        // Cantidad de partidos ganados
        public int Victorias { get; set; }

        // Cantidad de empates
        public int Empates { get; set; }

        // Cantidad de derrotas
        public int Derrotas { get; set; }

        // Lista donde se guardan los jugadores del equipo
        public List<Jugador> Jugadores { get; set; }

        // Constructor
        public Equipo(string nombre)
        {
            Nombre = nombre;

            Victorias = 0;
            Empates = 0;
            Derrotas = 0;

            // Se crea la lista vacia de jugadores
            Jugadores = new List<Jugador>();
        }

        // Agrega un jugador a la lista
        public void AgregarJugador(Jugador jugador)
        {
            Jugadores.Add(jugador);
        }

        // Calcula los puntos del equipo
        public int CalcularPuntos()
        {
            return (Victorias * 3) + Empates;
        }

        // Muestra informacion basica del equipo
        public string MostrarInfo()
        {
            return $"{Nombre} - Puntos: {CalcularPuntos()}";
        }
        // Total de partidos jugados
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

        // Porcentaje de empates
        public double PorcentajeEmpates()
        {
            if (TotalPartidos() == 0)
                return 0;

            return (double)Empates * 100 / TotalPartidos();
        }

        // Porcentaje de derrotas
        public double PorcentajeDerrotas()
        {
            if (TotalPartidos() == 0)
                return 0;

            return (double)Derrotas * 100 / TotalPartidos();
        }
    }
}
