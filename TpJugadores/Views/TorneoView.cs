using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpJugadores.Views
{
    public class TorneoView
    {
        public void MostrarBienvenida()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("========================================");
            Console.WriteLine("      SISTEMA DE TORNEO DE FUTBOL");
            Console.WriteLine("========================================");

            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("Trabajo Practico N° 2");
            Console.WriteLine("Arquitectura MVC");
            Console.WriteLine();
            Console.WriteLine("Presione una tecla para comenzar...");

            Console.ReadKey();
        }

        // Muestra el menu principal
        public string MostrarMenu()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║         TORNEO APERTURA 2026         ║");
            Console.WriteLine("╚══════════════════════════════════════╝");

            Console.ResetColor();

            Console.WriteLine();

            Console.WriteLine($" {DateTime.Now:dd/MM/yyyy HH:mm}");

            Console.WriteLine();

            Console.WriteLine("[1] Agregar equipo");
            Console.WriteLine("[2] Listar equipos");
            Console.WriteLine("[3] Buscar equipo");
            Console.WriteLine("[4] Agregar jugador");
            Console.WriteLine("[5] Registrar partido");
            Console.WriteLine("[6] Mostrar campeon");
            Console.WriteLine("[7] Tabla de posiciones");
            Console.WriteLine("[8] Ver jugadores");
            Console.WriteLine("[9] Estadisticas");
            Console.WriteLine("[10] Top 3 equipos");
            Console.WriteLine("[0] Salir");

            Console.WriteLine();

            Console.Write("Seleccione una opción: ");

            return Console.ReadLine();
        }

        // Muestra mensajes normales
        // Muestra mensajes de exito en verde

        public void MostrarMensaje(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine();
            Console.WriteLine("✔ " + mensaje);

            Console.ResetColor();
        }


        // Muestra mensajes de error
        // Muestra errores en rojo
        public void MostrarError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine();
            Console.WriteLine("✖ " + error);

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

        // Pide nombre del jugador
        public string PedirNombreJugador()
        {
            string nombre;

            while (true)
            {
                Console.Write("Nombre del jugador: ");
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
        // Espera una tecla para continuar
        public void Pausa()
        {
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.WriteLine("Presione una tecla para continuar...");

            Console.ResetColor();

            Console.ReadKey();
        }

    }
}
