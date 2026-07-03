using TpJugadores.Controllers;
using TpJugadores.Models;
using TpJugadores.Repository;

namespace TpJugadores
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IRepository<Equipo> repoEquipos =
               new JsonRepository<Equipo>("equipos.json");

            IRepository<Jugador> repoJugadores =
                new JsonRepository<Jugador>("jugadores.json");

            TorneoController controller =
                new TorneoController(repoEquipos, repoJugadores);

            controller.IniciarMenu();
        }
    }
}
