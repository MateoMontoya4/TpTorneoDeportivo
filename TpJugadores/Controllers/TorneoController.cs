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
                        MostrarTabla();
                        break;
                    case "8":
                        VerJugadores();
                        break;
                    case "9":
                        MostrarEstadisticas();
                        break;
                    case "10":
                        MostrarTop3();
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

            string nombreJugador;

            while (true)
            {
                Console.Write("Nombre del jugador: ");
                nombreJugador = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nombreJugador))
                {
                    _view.MostrarError("El nombre no puede estar vacio");
                    continue;
                }

                if (nombreJugador.Any(char.IsDigit))
                {
                    _view.MostrarError("El nombre no puede contener numeros");
                    continue;
                }

                break;
            }
            Console.WriteLine();
            Console.WriteLine("[1] Arquero");
            Console.WriteLine("[2] Defensor");
            Console.WriteLine("[3] Mediocampista");
            Console.WriteLine("[4] Delantero");

            string posicion = "";

            while (true)
            {
                Console.Write("Seleccione posicion: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        posicion = "Arquero";
                        break;

                    case "2":
                        posicion = "Defensor";
                        break;

                    case "3":
                        posicion = "Mediocampista";
                        break;

                    case "4":
                        posicion = "Delantero";
                        break;

                    default:
                        _view.MostrarError("Opcion invalida");
                        continue;
                }

                break;
            }
            Console.WriteLine();

            int edad;

            while (true)
            {
                Console.Write("Edad: ");

                if (!int.TryParse(Console.ReadLine(), out edad))
                {
                    _view.MostrarError("Ingrese un numero valido");
                    continue;
                }

                if (edad < 15 || edad > 50)
                {
                    _view.MostrarError("La edad debe estar entre 15 y 50");
                    continue;
                }

                break;
            }

            Console.WriteLine();

            int numero;

            while (true)
            {
                Console.Write("Numero de camiseta: ");

                if (!int.TryParse(Console.ReadLine(), out numero))
                {
                    _view.MostrarError("Ingrese un numero valido");
                    continue;
                }

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

            if (equipoSeleccionado.Jugadores.Any(j =>
                j.Nombre.ToLower() == nombreJugador.ToLower()))
            {
                _view.MostrarError("Ese jugador ya existe");
                return;
            }

            Jugador nuevoJugador = new Jugador(
                nombreJugador,
                edad,
                numero,
                posicion
            );

            equipoSeleccionado.AgregarJugador(nuevoJugador);

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
            Equipo campeon = _torneo.ObtenerCampeon();

            if (campeon == null)
            {
                _view.MostrarError("No hay equipos");
                return;
            }

            _view.MostrarMensaje(
                $"Campeon actual: {campeon.Nombre}"
            );
        }

        // Muestra todos los equipos y sus puntos
        private void MostrarTabla()
        {
            Console.WriteLine();
            Console.WriteLine("====================================");
            Console.WriteLine("      TABLA DE POSICIONES");
            Console.WriteLine("====================================");
            Console.WriteLine("POS   EQUIPO           PTS");
            Console.WriteLine("------------------------------------");

            int posicion = 1;

            foreach (Equipo equipo in _torneo.Equipos
                .OrderByDescending(e => e.CalcularPuntos()))
            {
                Console.WriteLine(
                    $"{posicion,-5}{equipo.Nombre,-15}{equipo.CalcularPuntos()}");

                posicion++;
            }

            Console.WriteLine("====================================");
        }
        // Registra un partido entre dos equipos
        private void RegistrarPartido()
        {
            Console.WriteLine("=== REGISTRAR PARTIDO ===");

            Console.Write("Equipo local: ");
            string nombreLocal = Console.ReadLine();

            Console.Write("Equipo visitante: ");
            string nombreVisitante = Console.ReadLine();

            if (nombreLocal.ToLower() ==
             nombreVisitante.ToLower())
            {
                _view.MostrarError(
                    "Un equipo no puede jugar contra si mismo");
                return;
            }
            Equipo local = _torneo.BuscarEquipo(nombreLocal);
            Equipo visitante = _torneo.BuscarEquipo(nombreVisitante);

            if (local == null || visitante == null)
            {
                _view.MostrarError("Uno o ambos equipos no existen");
                return;
            }

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

            _view.MostrarMensaje(
                "Partido registrado correctamente"
            );
        }
        private void MostrarTop3()
        {
            var top = _torneo.Equipos
                .OrderByDescending(e => e.CalcularPuntos())
                .Take(3)
                .ToList();

            Console.WriteLine();
            Console.WriteLine("===== TOP 3 DEL TORNEO =====");
            Console.WriteLine();

            if (top.Count > 0)
                Console.WriteLine($"1° {top[0].Nombre} - {top[0].CalcularPuntos()} pts");

            if (top.Count > 1)
                Console.WriteLine($"2° {top[1].Nombre} - {top[1].CalcularPuntos()} pts");

            if (top.Count > 2)
                Console.WriteLine($"3° {top[2].Nombre} - {top[2].CalcularPuntos()} pts");
        }
        private void MostrarEstadisticas()
        {
            string nombre = _view.PedirNombreEquipo();

            Equipo equipo = _torneo.BuscarEquipo(nombre);

            if (equipo == null)
            {
                _view.MostrarError("Equipo no encontrado");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("=================================");
            Console.WriteLine($"ESTADISTICAS DE {equipo.Nombre.ToUpper()}");
            Console.WriteLine("=================================");

            Console.WriteLine($"Victorias : {equipo.Victorias}");
            Console.WriteLine($"Empates   : {equipo.Empates}");
            Console.WriteLine($"Derrotas  : {equipo.Derrotas}");

            Console.WriteLine();

            Console.WriteLine($"Puntos    : {equipo.CalcularPuntos()}");

            Console.WriteLine();

            Console.WriteLine($"% Victorias : {equipo.PorcentajeVictorias():F2}%");
            Console.WriteLine($"% Empates   : {equipo.PorcentajeEmpates():F2}%");
            Console.WriteLine($"% Derrotas  : {equipo.PorcentajeDerrotas():F2}%");

            Console.WriteLine("=================================");
        }
        private void VerJugadores()
        {
            string nombre = _view.PedirNombreEquipo();

            Equipo equipo = _torneo.BuscarEquipo(nombre);

            if (equipo == null)
            {
                _view.MostrarError("Equipo no encontrado");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("=================================");
            Console.WriteLine($"JUGADORES DE {equipo.Nombre}");
            Console.WriteLine("=================================");

            if (equipo.Jugadores.Count == 0)
            {
                Console.WriteLine("No hay jugadores cargados");
                return;
            }

            foreach (Jugador jugador in equipo.Jugadores)
            {
                Console.WriteLine(jugador.ObtenerInfo());
            }
        }

    }
   
}
