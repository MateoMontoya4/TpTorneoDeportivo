using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpJugadores.Models
{
    public abstract class Persona
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }

        //constructor
        public Persona (string nombre, int edad)
        {
            Nombre = nombre;
            Edad = edad;
        }
        
       
    }
}
