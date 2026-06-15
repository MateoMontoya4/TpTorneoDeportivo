using TpJugadores.Models;
using TpJugadores.Views;

namespace TpJugadores.Controllers
{
    public class TorneoController
    {
        private Torneo _torneo;
        private TorneoView _view;

        private EquipoController _equipoController;
        private JugadorController _jugadorController;
        private PartidoController _partidoController;

        public TorneoController()
        {
            _torneo = new Torneo("Torneo Apertura");

            _view = new TorneoView();

            _equipoController = new EquipoController(_torneo);
            _jugadorController = new JugadorController(_torneo);
            _partidoController = new PartidoController(_torneo);

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
                new Jugador("Martiz", 37, 1, "Arquero"));

            racing.AgregarJugador(
                new Jugador("Sigali", 34, 2, "Defensor"));

            racing.AgregarJugador(
                new Jugador("Copetti", 28, 9, "Delantero"));
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