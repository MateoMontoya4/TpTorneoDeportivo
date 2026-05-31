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

            Console.WriteLine($"📅 {DateTime.Now:dd/MM/yyyy HH:mm}");

            Console.WriteLine();

            Console.WriteLine("⚽ [1] Agregar equipo");
            Console.WriteLine("📋 [2] Listar equipos");
            Console.WriteLine("🔎 [3] Buscar equipo");
            Console.WriteLine("👤 [4] Agregar jugador");
            Console.WriteLine("🏟️ [5] Registrar partido");
            Console.WriteLine("🏆 [6] Mostrar campeón");
            Console.WriteLine("📊 [7] Tabla de posiciones");
            Console.WriteLine("👥 [8] Ver jugadores");
            Console.WriteLine("📈 [9] Estadísticas");
            Console.WriteLine("🥇 [10] Top 3 equipos");
            Console.WriteLine("🚪 [0] Salir");

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
            Console.Write("Nombre del equipo: ");
            return Console.ReadLine();
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
            Console.Write("Edad: ");
            return int.Parse(Console.ReadLine());
        }

        // Pide numero de camiseta
        public int PedirNumero()
        {
            Console.Write("Numero de camiseta: ");
            return int.Parse(Console.ReadLine());
        }

        // Pide posicion
        public string PedirPosicion()
        {
            Console.Write("Posicion: ");
            return Console.ReadLine();
        }
        // Pide los goles del equipo local
        public int PedirGolesLocal()
        {
            Console.Write("Goles del local: ");
            return int.Parse(Console.ReadLine());
        }

        // Pide los goles del equipo visitante
        public int PedirGolesVisitante()
        {
            Console.Write("Goles del visitante: ");
            return int.Parse(Console.ReadLine());
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
