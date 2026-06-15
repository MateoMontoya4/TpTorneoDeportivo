using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpJugadores.Views
{
    public class PartidoView
    {
        // Pide los goles del equipo local
        public int PedirGolesLocal()
        {
            int goles;

            Console.Write("Goles del local: ");

            while (!int.TryParse(Console.ReadLine(), out goles))
            {
                MostrarError("Ingrese un numero valido");
                Console.Write("Goles del local: ");
            }

            return goles;
        }




        // Pide los goles del equipo visitante
        public int PedirGolesVisitante()
        {
            int goles;

            Console.Write("Goles del visitante: ");

            while (!int.TryParse(Console.ReadLine(), out goles))
            {
                MostrarError("Ingrese un numero valido");
                Console.Write("Goles del visitante: ");
            }

            return goles;
        }
        public void MostrarError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine();
            Console.WriteLine(error);

            Console.ResetColor();
        }
    }
}
