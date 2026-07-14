using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TpJugadores.Models;
using TpJugadores.Repository;
using TpJugadores.Views;

namespace TpJugadores.Controllers
{
    public class PartidoController
    {
        private Torneo _torneo;
        private TorneoView _view;
        private IRepository<Equipo> _repo;
        private PartidoView _partidoView;

        public PartidoController(Torneo torneo, IRepository<Equipo> repo)
        {
            _torneo = torneo;
            _view = new TorneoView();
            _repo = repo;
            _partidoView = new PartidoView();
        }
        public void RegistrarPartido()
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
            string nombreLocal = Console.ReadLine().Trim();

            if (nombreLocal == "0")
                return;

            Console.Write("Equipo visitante: ");
            string nombreVisitante = Console.ReadLine().Trim();

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

            int golesLocal = _partidoView.PedirGolesLocal();
            int golesVisitante = _partidoView.PedirGolesVisitante();

            Partido partido = new Partido(           
                local,
                visitante
            );

            partido.RegistrarResultado(
                golesLocal,
                golesVisitante
            );

            _torneo.AgregarPartido(partido);

            // Guarda las estadísticas actualizadas de los equipos en el JSON.
            _repo.GuardarTodos(_torneo.Equipos);

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
    }
}
