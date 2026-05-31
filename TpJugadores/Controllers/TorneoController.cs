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
            string nombre = _view.PedirNombreEquipo();

            Equipo equipo = new Equipo(nombre);

            _torneo.AgregarEquipo(equipo);

            _view.MostrarMensaje("Equipo agregado correctamente");
        }

        // Lista los equipos
        private void ListarEquipos()
        {
            foreach (Equipo equipo in _torneo.Equipos)
            {
                _view.MostrarMensaje(
                    equipo.MostrarInfo()
                );
            }
        }

        // Busca un equipo por nombre
        private void BuscarEquipo()
        {
            string nombre = _view.PedirNombreEquipo();

            Equipo equipo = _torneo.BuscarEquipo(nombre);

            if (equipo != null)
            {
                _view.MostrarMensaje(
                    equipo.MostrarInfo()
                );
            }
            else
            {
                _view.MostrarError(
                    "Equipo no encontrado"
                );
            }
        }
        // Agrega un jugador a un equipo
        private void AgregarJugador()
        {
            string nombreEquipo = _view.PedirNombreEquipo();

            Equipo equipo = _torneo.BuscarEquipo(nombreEquipo);

            if (equipo == null)
            {
                _view.MostrarError("Equipo no encontrado");
                return;
            }

            string nombreJugador = _view.PedirNombreJugador();
            int edad = _view.PedirEdad();
            int numero = _view.PedirNumero();
            string posicion = _view.PedirPosicion();

            Jugador jugador = new Jugador(
                nombreJugador,
                edad,
                numero,
                posicion
            );

            equipo.AgregarJugador(jugador);

            _view.MostrarMensaje("Jugador agregado correctamente");
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
            Console.WriteLine("=================================");
            Console.WriteLine("POS   EQUIPO           PUNTOS");
            Console.WriteLine("=================================");

            int posicion = 1;

            foreach (Equipo equipo in _torneo.Equipos
                     .OrderByDescending(e => e.CalcularPuntos()))
            {
                Console.WriteLine(
                    $"{posicion,-5}{equipo.Nombre,-15}{equipo.CalcularPuntos()}"
                );

                posicion++;
            }

            Console.WriteLine("=================================");
        }
        // Registra un partido entre dos equipos
        private void RegistrarPartido()
        {
            Console.WriteLine("=== REGISTRAR PARTIDO ===");

            Console.Write("Equipo local: ");
            string nombreLocal = Console.ReadLine();

            Console.Write("Equipo visitante: ");
            string nombreVisitante = Console.ReadLine();

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
            Console.WriteLine("🏆 TOP 3 DEL TORNEO");
            Console.WriteLine();

            if (top.Count > 0)
                Console.WriteLine($"🥇 {top[0].Nombre} - {top[0].CalcularPuntos()} pts");

            if (top.Count > 1)
                Console.WriteLine($"🥈 {top[1].Nombre} - {top[1].CalcularPuntos()} pts");

            if (top.Count > 2)
                Console.WriteLine($"🥉 {top[2].Nombre} - {top[2].CalcularPuntos()} pts");
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
