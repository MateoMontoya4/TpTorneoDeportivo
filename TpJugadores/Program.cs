using TpJugadores.Controllers;

namespace TpJugadores
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Crea el controlador principal
            TorneoController controller =
                new TorneoController();

            // Inicia el menu
            controller.IniciarMenu();
        }
    }
}
