using TpJugadores.Models;
using TpJugadores.Views;
using TpJugadores.Repository;

namespace TpJugadores.Controllers
{
    public class TorneoController
    {
        private Torneo _torneo;
        private TorneoView _view;

        private IRepository<Equipo> _repoEquipos;
        private IRepository<Jugador> _repoJugadores;

        private EquipoController _equipoController;
        private JugadorController _jugadorController;
        private PartidoController _partidoController;

        public TorneoController(IRepository<Equipo> repoEquipos, IRepository<Jugador> repoJugadores)
        {
            _repoEquipos = repoEquipos;
            _repoJugadores = repoJugadores;

            _torneo = new Torneo("Torneo Apertura");

            _view = new TorneoView();

            _equipoController = new EquipoController(_torneo, _repoEquipos);
            _jugadorController = new JugadorController(_torneo, _repoJugadores);
            _partidoController = new PartidoController(_torneo);

            _view.MostrarBienvenida();


            CargarEquipos();     // 👈 PRIMERO
            CargarJugadores();   // 👈 DESPUÉS

        }
        private void CargarEquipos()
        {
            var equipos = _repoEquipos.LeerTodos();

            if (equipos != null && equipos.Count > 0)
            {
                foreach (var e in equipos)
                {
                    _torneo.AgregarEquipo(e);
                }
            }
        }
        private void CargarJugadores()
        {
            var jugadores = _repoJugadores.LeerTodos();

            foreach (var j in jugadores)
            {
                var equipo = _torneo.BuscarEquipo(j.EquipoNombre);

                if (equipo != null)
                {
                    equipo.AgregarJugador(j);
                }
            }
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

            // 🔥 AHORA recién cargás jugadores
            var jugadores = _repoJugadores.LeerTodos();

            foreach (var j in jugadores)
            {
                var equipo = _torneo.BuscarEquipo(j.EquipoNombre);

                if (equipo != null)
                {
                    equipo.AgregarJugador(j);
                }
            }
        }

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
                        _equipoController.AgregarEquipo();
                        break;

                    case "2":
                        Console.Clear();
                        _equipoController.ListarEquipos();
                        break;

                    case "3":
                        Console.Clear();
                        _equipoController.BuscarEquipo();
                        break;

                    case "4":
                        Console.Clear();
                        _jugadorController.AgregarJugador();
                        break;

                    case "5":
                        Console.Clear();
                        _partidoController.RegistrarPartido();
                        break;

                    case "6":
                        Console.Clear();
                        _equipoController.MostrarCampeon();
                        break;

                    case "7":
                        Console.Clear();
                        _equipoController.EliminarEquipo();
                        break;

                    case "8":
                        Console.Clear();
                        _jugadorController.VerJugadores();
                        break;

                    case "9":
                        Console.Clear();
                        _jugadorController.EliminarJugador();
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
    }
}