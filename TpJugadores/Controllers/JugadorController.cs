using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TpJugadores.Models;
using TpJugadores.Views;
using TpJugadores.Repository;

namespace TpJugadores.Controllers
{
    public class JugadorController
    {
        private Torneo _torneo;
        private TorneoView _view;
        private IRepository<Jugador> _repo;

        public JugadorController(Torneo torneo, IRepository<Jugador> repo)
        {
            _torneo = torneo;
            _view = new TorneoView();
            _repo = repo;
        }
        // Agrega un jugador a un equipo
        public void AgregarJugador()
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
            nuevoJugador.EquipoNombre = equipoSeleccionado.Nombre;
            // Se agrega al equipo
            equipoSeleccionado.AgregarJugador(nuevoJugador);

            //  GUARDAR EN JSON
            _repo.GuardarTodos(
                _torneo.Equipos.SelectMany(e => e.Jugadores).ToList()
            );

           

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
        public void EliminarJugador()
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


        public void VerJugadores()
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
            Console.WriteLine(" N°   JUGADOR                POSICION      CAMISETA");
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
