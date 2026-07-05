using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpJugadores.Models
{
    // Acá Jugador hereda los datos de Persona y se conecta con la interfaz IRegistrable
    public class Jugador : Persona, IRegistrable, IEntidad
    {

        // Identificador único del jugador.
        // Lo usa el Repository para buscar, actualizar o eliminar este jugador.
        public int id { get; set; }




        // Guardamos el número de camiseta
        public int Numero { get; set; }
        // posicion dentro de la cancha
        public string Posicion { get; set; }


        // Guarda el nombre del equipo al que pertenece el jugador.
        // Se usa para poder recuperar el jugador cuando se lee el JSON.
        public string EquipoNombre { get; set; }   



        // El constructor pide los datos y con el base se los manda a la clase Persona
        public Jugador(string nombre, int edad, int numero, string posicion) : base(nombre, edad)
        {
            Numero = numero;
            Posicion = posicion;
            
        }

      

        // Este método junta todos los datos del jugador en un solo texto para mostrarlo lindo en pantalla
        public string ObtenerInfo()
        {
            return $"{Nombre} - #{Numero} - {Posicion} ";
        }
    }
}
