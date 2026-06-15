using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpJugadores.Views
{
    public class EquipoView
    {
        public void MostrarError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine();
            Console.WriteLine(error);

            Console.ResetColor();
        }

        // Pide nombre de equipo
        public string PedirNombreEquipo()
        {
            string nombre;

            while (true)
            {
                Console.Write("Nombre del equipo: ");
                nombre = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nombre))
                {
                    MostrarError("El nombre no puede estar vacio");
                    continue;
                }

                if (!nombre.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                {
                    MostrarError("Solo se permiten letras");
                    continue;
                }

                return nombre;
            }
        }
    }
}
