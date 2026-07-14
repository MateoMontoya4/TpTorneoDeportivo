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

            // Título
            Console.ForegroundColor = ConsoleColor.Red;

            LetrasCentradas("████████╗ ██████╗ ██████╗ ███╗   ██╗███████╗ ██████╗ ");
            LetrasCentradas("╚══██╔══╝██╔═══██╗██╔══██╗████╗  ██║██╔════╝██╔═══██╗");
            LetrasCentradas("   ██║   ██║   ██║██████╔╝██╔██╗ ██║█████╗  ██║   ██║");
            LetrasCentradas("   ██║   ██║   ██║██╔══██╗██║╚██╗██║██╔══╝  ██║   ██║");
            LetrasCentradas("   ██║   ╚██████╔╝██║  ██║██║ ╚████║███████╗╚██████╔╝");
            LetrasCentradas("   ╚═╝    ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═══╝╚══════╝ ╚═════╝ ");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            LetrasCentradas("═══════════════════════════════════════════════════════");
            LetrasCentradas("     GESTION TORNEO DEPORTIVO 2026");
            LetrasCentradas("═══════════════════════════════════════════════════════");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            LetrasCentradas("GESTION DE EQUIPOS");
            LetrasCentradas("GESTION DE JUGADORES");
            LetrasCentradas("REGISTRO DE PARTIDOS");
            LetrasCentradas("TABLA DE POSICIONES");
            LetrasCentradas("ESTADISTICAS DEL TORNEO");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            LetrasCentradas("---------------------------------------------");
            LetrasCentradas($"FECHA: {DateTime.Now:dd/MM/yyyy}");
            LetrasCentradas($"HORA : {DateTime.Now:HH:mm:ss}");
            LetrasCentradas("---------------------------------------------");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            LetrasCentradas(">>> PRESIONE UNA TECLA PARA COMENZAR <<<");

            Console.ResetColor();

            Console.ReadKey();
        }





        // Muestra el menu principal
        public string MostrarMenu()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;

            LetrasCentradas("==============================================================");
            LetrasCentradas("         TORNEO APERTURA 2026             ");
            LetrasCentradas("==============================================================");

            Console.ResetColor();

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            LetrasCentradas($"FECHA: {DateTime.Now:dd/MM/yyyy}    HORA: {DateTime.Now:HH:mm:ss}");
            Console.ResetColor();

            Console.WriteLine();
            LetrasCentradas("******************** MENU PRINCIPAL ********************");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Magenta;
            LetrasCentradas("[1]  AGREGAR EQUIPO");
            LetrasCentradas("[2]  LISTAR EQUIPOS");
            LetrasCentradas("[3]  BUSCAR EQUIPO");
            LetrasCentradas("[4]  AGREGAR JUGADOR");
            LetrasCentradas("[5]  REGISTRAR PARTIDO");
            LetrasCentradas("[6]  MOSTRAR CAMPEON");
            LetrasCentradas("[7] ELIMINAR EQUIPO DEL TORNEO");
            LetrasCentradas("[8]  VER JUGADORES");
            LetrasCentradas("[9]  ELIMINAR JUGADOR DEL TORNEO");
            LetrasCentradas("[10]  ACTUALIZAR EQUIPO");
            LetrasCentradas("[11]  ACTUALIZAR JUGADOR");
            LetrasCentradas("[0]  SALIR");
            Console.ResetColor();

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            LetrasCentradas("ELIJA UNA OPCION DEL MENU");
            Console.ResetColor();

            Console.WriteLine();
            Console.Write("Opcion: ");

            return Console.ReadLine();
        }

        

        

        public void MostrarMensaje(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine();
            Console.WriteLine(mensaje);

            Console.ResetColor();
        }





        // Muestra mensajes de error
        
        public void MostrarError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine();
            Console.WriteLine(error);

            Console.ResetColor();
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




        //esto es para q las letras esten centradas y no todo al costado
        public void LetrasCentradas(string texto)
        {
            int espacios = (Console.WindowWidth - texto.Length) / 2;

            if (espacios < 0)
                espacios = 0;

            Console.WriteLine(new string(' ', espacios) + texto);
        }
       

       



    }
}
