using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpJugadores.Models
{
    // la clase Jugador hereda de Persona e implementa la interfaz IRegistrable
    public class Jugador : Persona, IRegistrable
    {
        //numero de camiseta del jugador
        public int Numero { get; set; }
        // posicion dentro de la cancha
        public string Posicion { get; set; }

        //cantidad de goles q metio
        public int Goles { get; set; }

        public Jugador(string nombre, int edad, int numero, string posicion) : base(nombre, edad)
        {
            Numero = numero;
            Posicion = posicion;
            Goles = 0;
        }

        // Suma un gol al jugador
        public void MarcarGol()
        {
            Goles++;
        }

        // Devuelve la informacion del jugador
        public string ObtenerInfo()
        {
            return $"{Nombre} - #{Numero} - {Posicion} - Goles: {Goles}";
        }
    }
}
