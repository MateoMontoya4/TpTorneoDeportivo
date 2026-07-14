using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpJugadores.Views
{
    public class JugadorView
    {
        public void MostrarError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine();
            Console.WriteLine(error);

            Console.ResetColor();
        }
        // Pide nombre del jugador
        public string PedirNombreJugador()
        {
            Console.Write("Nombre del jugador: ");

            return Console.ReadLine();
        }



        // Pide edad
        public int PedirEdad()
        {
            int edad;

            Console.Write("Edad: ");

            while (!int.TryParse(Console.ReadLine(), out edad))
            {
                MostrarError("Ingrese un numero valido");
                Console.Write("Edad: ");
            }

            return edad;
        }


        public string PedirPosicion()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Seleccione la posicion:");
                Console.WriteLine("1 - Arquero");
                Console.WriteLine("2 - Defensor");
                Console.WriteLine("3 - Mediocampista");
                Console.WriteLine("4 - Delantero");
                Console.Write("Opcion: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        return "Arquero";

                    case "2":

                        return "Defensor";


                    case "3":
                        return "Mediocampista";

                    case "4":
                        return "Delantero";

                    default:
                        MostrarError("Opcion invalida");
                        break;
                }
            }
        }




        // Pide numero de camiseta
        public int PedirNumero()
        {
            int numero;

            Console.Write("Numero de camiseta: ");

            while (!int.TryParse(Console.ReadLine(), out numero))
            {
                MostrarError("Ingrese un numero valido");
                Console.Write("Numero de camiseta: ");
            }

            return numero;
        }
    }
}
