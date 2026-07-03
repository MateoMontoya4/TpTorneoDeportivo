using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpJugadores.Models
{
    // Clase principal que administra el torneo
    public class Torneo
    {
        // Nombre del torneo
        public string Nombre { get; set; }

        // Lista de equipos
        public List<Equipo> Equipos { get; set; }

        // Lista de partidos
        public List<Partido> Partidos { get; set; }

        // Constructor
        public Torneo(string nombre)
        {
            Nombre = nombre;

            // Se crean las listas vacías
            Equipos = new List<Equipo>();
            Partidos = new List<Partido>();
        }

        // Agrega un equipo al torneo
        public void AgregarEquipo(Equipo equipo)
        {
            Equipos.Add(equipo);
        }

        // Agrega un partido al torneo
        public void AgregarPartido(Partido partido)
        {
            Partidos.Add(partido);
        }
        //ELIMNAR UN EQUIPO
        public bool EliminarEquipo(Equipo equipo)
        {
            if (equipo == null) return false;
            return Equipos.Remove(equipo);
        }
        // Busca un equipo por nombre
        public Equipo BuscarEquipo(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return null;

            return Equipos.FirstOrDefault(
                e => e.Nombre.ToLower() == nombre.ToLower()
            );
        }

        // Devuelve el equipo con más puntos
        public Equipo ObtenerCampeon()
        {
            if (Equipos.Count == 0)
                return null;

            return Equipos
                .OrderByDescending(e => e.CalcularPuntos())
                .First();
        }
    }
}
