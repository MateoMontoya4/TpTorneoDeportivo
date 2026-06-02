using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TpJugadores.Models;
using TpJugadores.Views;

namespace TpJugadores.Controllers
{
    // Controla toda la logica del sistema
    public class TorneoController
    {
        private Torneo _torneo;

        private TorneoView _view;

        // Constructor
        public TorneoController()
        {
            _torneo = new Torneo("Torneo Apertura");

            _view = new TorneoView();

            _view.MostrarBienvenida();

            CargarDatosIniciales();
        }





        private void CargarDatosIniciales()
        {
            Equipo river = new Equipo("River");
            river.Victorias = 8;
            river.Empates = 2;
            river.Derrotas = 1;

            Equipo boca = new Equipo("Boca");
            boca.Victorias = 7;
            boca.Empates = 1;
            boca.Derrotas = 3;

            Equipo racing = new Equipo("Racing");
            racing.Victorias = 5;
            racing.Empates = 4;
            racing.Derrotas = 2;

            _torneo.AgregarEquipo(river);
            _torneo.AgregarEquipo(boca);
            _torneo.AgregarEquipo(racing);

            river.AgregarJugador(
             new Jugador("Armani", 38, 1, "Arquero"));

            river.AgregarJugador(
                new Jugador("Pezzella", 33, 6, "Defensor"));

            river.AgregarJugador(
                new Jugador("Colidio", 25, 11, "Delantero"));

            boca.AgregarJugador(
                new Jugador("Andrada", 35, 1, "Arquero"));
            boca.AgregarJugador(
                new Jugador("Izquierdoz", 36, 2, "Defensor"));
           boca.AgregarJugador(
                new Jugador("Vazquez", 30, 9, "Delantero"));
            racing.AgregarJugador(
                new Jugador("Chila", 37, 1, "Arquero"));
            racing.AgregarJugador(
                new Jugador("Sigali", 34, 2, "Defensor"));
            racing.AgregarJugador(
                new Jugador("Copetti", 28, 9, "Delantero"));

        }






        // Inicia el menu principal
        public void IniciarMenu()
        {
            string opcion;

            do
            {
                opcion = _view.MostrarMenu();

                switch (opcion)
                {
                    case "1":
                        Console.Clear();
                        AgregarEquipo();
                        break;

                    case "2":
                        Console.Clear();
                        ListarEquipos();
                        break;

                    case "3":
                        Console.Clear();
                        BuscarEquipo();
                        break;

                    case "4":
                        Console.Clear();
                        AgregarJugador();
                        break;

                    case "5":
                        Console.Clear();
                        RegistrarPartido();
                        break;

                    case "6":
                        Console.Clear();
                        MostrarCampeon();
                        break;

                    case "7":
                        Console.Clear();
                        EliminarEquipo();
                        break;
                    case "8":
                        VerJugadores();
                        break;
                    case "9":
                        EliminarJugador();
                        break;
                   

                    case "0":
                        _view.MostrarMensaje("Hasta luego");
                        break;

                    default:
                        _view.MostrarError("Opcion invalida");
                        break;
                }
                if (opcion != "0")
                {
                    _view.Pausa();
                }

            } while (opcion != "0");
        }









        // Agrega un equipo
        private void AgregarEquipo()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;

            _view.LetrasCentradas("====================================================");
            _view.LetrasCentradas("                AGREGAR NUEVO EQUIPO                ");
            _view.LetrasCentradas("====================================================");

            Console.ResetColor();

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            _view.LetrasCentradas("EQUIPOS REGISTRADOS");
            Console.ResetColor();

            Console.WriteLine();

            foreach (Equipo equipo in _torneo.Equipos)
            {
                _view.LetrasCentradas("- " + equipo.Nombre);
            }

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            _view.LetrasCentradas("INGRESE EL NOMBRE DEL NUEVO EQUIPO");
            Console.ResetColor();

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            _view.LetrasCentradas("ESCRIBA 0 PARA VOLVER AL MENU");
            Console.ResetColor();

            Console.WriteLine();


            string nombre;

            while (true)
            {
                Console.Write("Nombre del equipo: ");
                nombre = Console.ReadLine();


                if (nombre == "0")
                {
                    return;
                }

                if (string.IsNullOrWhiteSpace(nombre))
                {
                    _view.MostrarError("El nombre no puede estar vacio");
                    continue;
                }

                if (nombre.Any(char.IsDigit))
                {
                    _view.MostrarError("El nombre no puede contener numeros");
                    continue;
                }

                if (_torneo.BuscarEquipo(nombre) != null)
                {
                    _view.MostrarError("Ese equipo ya existe");
                    continue;
                }

                break;
            }

            Equipo nuevoEquipo = new Equipo(nombre);

            _torneo.AgregarEquipo(nuevoEquipo);

            _view.MostrarMensaje($"El equipo '{nombre}' fue agregado correctamente");
        }









        // Lista los equipos
        private void ListarEquipos()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;

            _view.LetrasCentradas("====================================================");
            _view.LetrasCentradas("                 LISTADO DE EQUIPOS                 ");
            _view.LetrasCentradas("====================================================");

            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine();

            _view.LetrasCentradas("----------------------------------------------------");
            _view.LetrasCentradas(" N°   EQUIPO                     PUNTOS");
            _view.LetrasCentradas("----------------------------------------------------");

            int posicion = 1;

            foreach (Equipo equipo in _torneo.Equipos
                .OrderByDescending(e => e.CalcularPuntos()))
            {
                _view.LetrasCentradas(
                    $" {posicion,-3}  {equipo.Nombre,-25} {equipo.CalcularPuntos(),3}"
                );

                posicion++;
            }

            _view.LetrasCentradas("----------------------------------------------------");
            _view.LetrasCentradas($" Total de equipos registrados: {_torneo.Equipos.Count}");
            _view.LetrasCentradas("----------------------------------------------------");
        }











        // Busca un equipo por nombre
        private void BuscarEquipo()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;

            _view.LetrasCentradas("====================================================");
            _view.LetrasCentradas("                  BUSCAR EQUIPO");
            _view.LetrasCentradas("====================================================");

            Console.ResetColor();

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            _view.LetrasCentradas("EQUIPOS DISPONIBLES");
            Console.ResetColor();

            Console.WriteLine();

            foreach (Equipo equipo in _torneo.Equipos)
            {
                _view.LetrasCentradas("- " + equipo.Nombre);
            }

            Console.WriteLine();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            _view.LetrasCentradas("INGRESE EL NOMBRE DEL EQUIPO");
            Console.ResetColor();

            Console.WriteLine();

            Console.Write("Nombre del equipo: ");
            string nombre = Console.ReadLine();

            Equipo encontrado = _torneo.BuscarEquipo(nombre);

            if (encontrado == null)
            {
                _view.MostrarError("Equipo no encontrado");
                return;
            }

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Green;

            _view.LetrasCentradas("====================================================");
            _view.LetrasCentradas($"        INFORMACION DE {encontrado.Nombre.ToUpper()}");
            _view.LetrasCentradas("====================================================");

            Console.ResetColor();

            Console.WriteLine();

            _view.LetrasCentradas($"Nombre   : {encontrado.Nombre}");
            _view.LetrasCentradas($"Puntos   : {encontrado.CalcularPuntos()}");
            _view.LetrasCentradas($"Victorias: {encontrado.Victorias}");
            _view.LetrasCentradas($"Empates  : {encontrado.Empates}");
            _view.LetrasCentradas($"Derrotas : {encontrado.Derrotas}");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            _view.LetrasCentradas("====================================================");
            Console.ResetColor();
        }









        // Agrega un jugador a un equipo
        private void AgregarJugador()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;

            _view.LetrasCentradas("====================================================");
            _view.LetrasCentradas("                 AGREGAR JUGADOR");
            _view.LetrasCentradas("====================================================");

            Console.ResetColor();

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            _view.LetrasCentradas("EQUIPOS DISPONIBLES");
            Console.ResetColor();

            Console.WriteLine();

            foreach (Equipo equipo in _torneo.Equipos)
            {
                _view.LetrasCentradas("- " + equipo.Nombre);
            }

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            _view.LetrasCentradas("ESCRIBA 0 PARA VOLVER AL MENU");
            Console.ResetColor();

            Console.WriteLine();

            Console.Write("Nombre del equipo: ");
            string nombreEquipo = Console.ReadLine();

            if (nombreEquipo == "0")
                return;

            Equipo equipoSeleccionado = _torneo.BuscarEquipo(nombreEquipo);

            if (equipoSeleccionado == null)
            {
                _view.MostrarError("Equipo no encontrado");
                return;
            }

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Green;
            _view.LetrasCentradas($"AGREGANDO JUGADOR A {equipoSeleccionado.Nombre.ToUpper()}");
            Console.ResetColor();

            Console.WriteLine();

            // Pedir Nombre del Jugador usando la Vista
            string nombreJugador = _view.PedirNombreJugador();

            //  Pedir Posición usando la Vista
            string posicion = _view.PedirPosicion();

            Console.WriteLine();

           // Pedir Edad usando la Vista 
            int edad;
            while (true)
            {
                edad = _view.PedirEdad();

                if (edad < 15 || edad > 50)
                {
                    _view.MostrarError("La edad debe estar entre 15 y 50");
                    continue;
                }
                break;
            }

            Console.WriteLine();

            // Pedir Número usando la Vista
            int numero;
            while (true)
            {
                numero = _view.PedirNumero();

                if (numero < 1 || numero > 99)
                {
                    _view.MostrarError("El numero debe estar entre 1 y 99");
                    continue;
                }

                if (equipoSeleccionado.Jugadores.Any(j => j.Numero == numero))
                {
                    _view.MostrarError("Ese numero ya esta ocupado");
                    continue;
                }
                break;
            }

            // Validación de jugador repetido por nombre
            if (equipoSeleccionado.Jugadores.Any(j =>
                j.Nombre.ToLower() == nombreJugador.ToLower()))
            {
                _view.MostrarError("Ese jugador ya existe");
                return;
            }

            // Se crea el objeto
            Jugador nuevoJugador = new Jugador(
                nombreJugador,
                edad,
                numero,
                posicion
            );

            // Se agrega al equipo
            equipoSeleccionado.AgregarJugador(nuevoJugador);

            // Mensaje de éxito en la pantalla
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            _view.LetrasCentradas("========================================");
            _view.LetrasCentradas("JUGADOR AGREGADO CORRECTAMENTE");
            _view.LetrasCentradas($"Nombre: {nombreJugador}");
            _view.LetrasCentradas($"Equipo: {equipoSeleccionado.Nombre}");
            _view.LetrasCentradas($"Posicion: {posicion}");
            _view.LetrasCentradas($"Camiseta N° {numero}");
            _view.LetrasCentradas("========================================");
            Console.ResetColor();
        }










        // Muestra el equipo con mas puntos
        private void MostrarCampeon()
        {
            Console.Clear();

            Equipo campeon = _torneo.ObtenerCampeon();

            if (campeon == null)
            {
                _view.MostrarError("No hay equipos cargados");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;

            _view.LetrasCentradas("       ___________      ");
            _view.LetrasCentradas("      '._==_==_=_.'     ");
            _view.LetrasCentradas("      .-\\:      /-.    ");
            _view.LetrasCentradas("     | (|:.     |) |    ");
            _view.LetrasCentradas("      '-|:.     |-'     ");
            _view.LetrasCentradas("        \\::.    /      ");
            _view.LetrasCentradas("         '::. .'        ");
            _view.LetrasCentradas("           ) (          ");
            _view.LetrasCentradas("         _.' '._        ");
            _view.LetrasCentradas("       `\"\"\"\"\"\"\"`        ");

            Console.WriteLine();

            _view.LetrasCentradas("====================================================");
            _view.LetrasCentradas("                 CAMPEON ACTUAL                      ");
            _view.LetrasCentradas("====================================================");

            Console.ResetColor();

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;

            _view.LetrasCentradas($"EQUIPO: {campeon.Nombre.ToUpper()}");
            _view.LetrasCentradas($"PUNTOS: {campeon.CalcularPuntos()}");

            Console.WriteLine();

            _view.LetrasCentradas($"VICTORIAS: {campeon.Victorias}");
            _view.LetrasCentradas($"EMPATES: {campeon.Empates}");
            _view.LetrasCentradas($"DERROTAS: {campeon.Derrotas}");

            Console.ResetColor();

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            _view.LetrasCentradas("LIDER DEL TORNEO APERTURA 2026");
            Console.ResetColor();
        }









        // elimnar un equipo del torneo
        private void EliminarEquipo()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;

            _view.LetrasCentradas("====================================================");
            _view.LetrasCentradas("                ELIMINAR EQUIPO                     ");
            _view.LetrasCentradas("====================================================");

            Console.ResetColor();

            Console.WriteLine();

            foreach (Equipo equipo in _torneo.Equipos)
            {
                _view.LetrasCentradas("- " + equipo.Nombre);
            }

            Console.WriteLine();

            string nombre = _view.PedirNombreEquipo();

            Equipo equipoEliminar = _torneo.BuscarEquipo(nombre);

            if (equipoEliminar == null)
            {
                _view.MostrarError("Equipo no encontrado");
                return;
            }

            _torneo.EliminarEquipo(equipoEliminar);

            _view.MostrarMensaje(
                $"El equipo {equipoEliminar.Nombre} fue eliminado correctamente");
        }










        private void RegistrarPartido()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;

            _view.LetrasCentradas("====================================================");
            _view.LetrasCentradas("               REGISTRAR PARTIDO");
            _view.LetrasCentradas("====================================================");

            Console.ResetColor();

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            _view.LetrasCentradas("EQUIPOS DISPONIBLES");
            Console.ResetColor();

            Console.WriteLine();

            foreach (Equipo equipo in _torneo.Equipos)
            {
                _view.LetrasCentradas("- " + equipo.Nombre);
            }

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            _view.LetrasCentradas("ESCRIBA 0 PARA VOLVER AL MENU");
            Console.ResetColor();

            Console.WriteLine();

            Console.Write("Equipo local: ");
            string nombreLocal = Console.ReadLine();

            if (nombreLocal == "0")
                return;

            Console.Write("Equipo visitante: ");
            string nombreVisitante = Console.ReadLine();

            if (nombreVisitante == "0")
                return;

            if (nombreLocal.ToLower() == nombreVisitante.ToLower())
            {
                _view.MostrarError("Un equipo no puede jugar contra si mismo");
                return;
            }

            Equipo local = _torneo.BuscarEquipo(nombreLocal);
            Equipo visitante = _torneo.BuscarEquipo(nombreVisitante);

            if (local == null || visitante == null)
            {
                _view.MostrarError("Uno o ambos equipos no existen");
                return;
            }

            Console.WriteLine();

            int golesLocal = _view.PedirGolesLocal();
            int golesVisitante = _view.PedirGolesVisitante();

            Partido partido = new Partido(
                DateTime.Now,
                local,
                visitante
            );

            partido.RegistrarResultado(
                golesLocal,
                golesVisitante
            );

            _torneo.AgregarPartido(partido);

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine();
            _view.LetrasCentradas("PARTIDO REGISTRADO CORRECTAMENTE");
            _view.LetrasCentradas($"{local.Nombre} {golesLocal} - {golesVisitante} {visitante.Nombre}");

            Console.WriteLine();

            if (golesLocal > golesVisitante)
            {
                _view.LetrasCentradas($"{local.Nombre} SUMO 3 PUNTOS");
                _view.LetrasCentradas($"{visitante.Nombre} SUMO 0 PUNTOS");
            }
            else if (golesLocal < golesVisitante)
            {
                _view.LetrasCentradas($"{visitante.Nombre} SUMO 3 PUNTOS");
                _view.LetrasCentradas($"{local.Nombre} SUMO 0 PUNTOS");
            }
            else
            {
                _view.LetrasCentradas($"{local.Nombre} SUMO 1 PUNTO");
                _view.LetrasCentradas($"{visitante.Nombre} SUMO 1 PUNTO");
            }

            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;

           
            Console.ResetColor();
        }







        
        private void EliminarJugador()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;

            _view.LetrasCentradas("====================================================");
            _view.LetrasCentradas("               ELIMINAR JUGADOR                     ");
            _view.LetrasCentradas("====================================================");

            Console.ResetColor();

            Console.WriteLine();

            foreach (Equipo equipo in _torneo.Equipos)
            {
                _view.LetrasCentradas("- " + equipo.Nombre);
            }

            Console.WriteLine();

            string nombreEquipo = _view.PedirNombreEquipo();

            Equipo equipoSeleccionado = _torneo.BuscarEquipo(nombreEquipo);

            if (equipoSeleccionado == null)
            {
                _view.MostrarError("Equipo no encontrado");
                return;
            }

            Console.WriteLine();

            if (equipoSeleccionado.Jugadores.Count == 0)
            {
                _view.MostrarError("Ese equipo no tiene jugadores");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            _view.LetrasCentradas("JUGADORES DISPONIBLES");
            Console.ResetColor();

            Console.WriteLine();

            foreach (Jugador jugador in equipoSeleccionado.Jugadores)
            {
                _view.LetrasCentradas(
                    $"{jugador.Nombre} - #{jugador.Numero}");
            }

            Console.WriteLine();

            Console.Write("Nombre del jugador a eliminar: ");
            string nombreJugador = Console.ReadLine();

            Jugador jugadorEliminar =
                equipoSeleccionado.Jugadores.FirstOrDefault(j =>
                j.Nombre.ToLower() == nombreJugador.ToLower());

            if (jugadorEliminar == null)
            {
                _view.MostrarError("Jugador no encontrado");
                return;
            }

            
            equipoSeleccionado.EliminarJugador(nombreJugador);

            _view.MostrarMensaje(
                $"Jugador {jugadorEliminar.Nombre} eliminado correctamente");
        
        }






    
        private void VerJugadores()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;

            _view.LetrasCentradas("====================================================");
            _view.LetrasCentradas("              CONSULTA DE JUGADORES                 ");
            _view.LetrasCentradas("====================================================");

            Console.ResetColor();

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            _view.LetrasCentradas("EQUIPOS DISPONIBLES");
            Console.ResetColor();

            Console.WriteLine();

            foreach (Equipo equipo in _torneo.Equipos)
            {
                _view.LetrasCentradas("- " + equipo.Nombre);
            }

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            _view.LetrasCentradas("INGRESE EL NOMBRE DEL EQUIPO");
            Console.ResetColor();

            Console.WriteLine();

            string nombre = _view.PedirNombreEquipo();

            Equipo equipoSeleccionado = _torneo.BuscarEquipo(nombre);

            if (equipoSeleccionado == null)
            {
                _view.MostrarError("Equipo no encontrado");
                return;
            }

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;

            _view.LetrasCentradas("====================================================");
            _view.LetrasCentradas($"PLANTEL DE {equipoSeleccionado.Nombre.ToUpper()}   ");
            _view.LetrasCentradas("====================================================");

            Console.ResetColor();

            Console.WriteLine();

            if (equipoSeleccionado.Jugadores.Count == 0)
            {
                _view.MostrarError("No hay jugadores cargados");
                return;
            }

            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine(" N°   JUGADOR                POSICION      CAMISETA" );
            Console.WriteLine("----------------------------------------------------");

            int contador = 1;

            foreach (Jugador jugador in equipoSeleccionado.Jugadores)
            {
                Console.WriteLine(
                    $" {contador,-4} {jugador.Nombre,-20} {jugador.Posicion,-12} #{jugador.Numero}");
                contador++;
            }

            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine($" Total de jugadores: {equipoSeleccionado.Jugadores.Count}");
            Console.WriteLine("----------------------------------------------------");
        }

    }
   
}
