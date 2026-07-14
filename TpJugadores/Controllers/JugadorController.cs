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
        private JugadorView _jugadorView;
        private EquipoView _equipoView;

        public JugadorController(Torneo torneo, IRepository<Jugador> repo)
        {
            _torneo = torneo;
            _view = new TorneoView();
            _repo = repo;
            _jugadorView = new JugadorView();
            _equipoView = new EquipoView();
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
            string nombreJugador = _jugadorView.PedirNombreJugador();

            if (string.IsNullOrWhiteSpace(nombreJugador))
            {
                _view.MostrarError("El nombre no puede estar vacío");
                return;
            }


            if (!nombreJugador.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            {
                _view.MostrarError("El nombre solo puede tener letras");
                return;
            }

            //  Pedir Posición usando la Vista
            string posicion = _jugadorView.PedirPosicion();

            Console.WriteLine();

            // Pedir Edad usando la Vista 
            int edad;
            while (true)
            {
                edad = _jugadorView.PedirEdad();

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
                numero = _jugadorView.PedirNumero();

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

            // Se guarda el nombre del equipo dentro del jugador.
            // Así cuando el programa vuelva a abrirse, sabemos a qué equipo pertenece.
            nuevoJugador.EquipoNombre = equipoSeleccionado.Nombre;
            // Se agrega al equipo
            equipoSeleccionado.AgregarJugador(nuevoJugador);

           
            // GUARDAR CON ID (IMPORTANTE)
            _repo.Agregar(nuevoJugador); 

            _view.MostrarMensaje("Jugador agregado correctamente");



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

            // Mostrar todos los jugadores con ID
            foreach (Equipo equipo in _torneo.Equipos)
            {
                foreach (Jugador jugador in equipo.Jugadores)
                {
                    _view.LetrasCentradas(
                        $"ID: {jugador.id} - {jugador.Nombre} ({equipo.Nombre})"
                    );
                }
            }

            Console.WriteLine();

            Console.Write("Ingrese ID del jugador a eliminar: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                _view.MostrarError("ID inválido");
                return;
            }

            // Buscar jugador y equipo dueño
            Jugador jugadorEliminar = null;
            Equipo equipoPadre = null;

            foreach (var equipo in _torneo.Equipos)
            {
                jugadorEliminar = equipo.Jugadores.FirstOrDefault(j => j.id == id);

                if (jugadorEliminar != null)
                {
                    equipoPadre = equipo;
                    break;
                }
            }

            if (jugadorEliminar == null)
            {
                _view.MostrarError("Jugador no encontrado");
                return;
            }

            // Eliminar del equipo
            equipoPadre.Jugadores.Remove(jugadorEliminar);

            // Guardar cambios en JSON
            _repo.GuardarTodos(
                _torneo.Equipos.SelectMany(e => e.Jugadores).ToList()
            );

            _view.MostrarMensaje("Jugador eliminado correctamente");
        }


        public void ActualizarJugador()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;

            _view.LetrasCentradas("====================================================");
            _view.LetrasCentradas("              ACTUALIZAR JUGADOR");
            _view.LetrasCentradas("====================================================");

            Console.ResetColor();

            Console.WriteLine();

            // Mostrar todos los jugadores con su ID
            foreach (Equipo equipo in _torneo.Equipos)
            {
                foreach (Jugador jugador in equipo.Jugadores)
                {
                    _view.LetrasCentradas(
                        $"ID: {jugador.id} - {jugador.Nombre} ({equipo.Nombre})"
                    );
                }
            }

            Console.WriteLine();

            Console.Write("Ingrese el ID del jugador: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                _view.MostrarError("ID inválido");
                return;
            }

            // Buscar el jugador por su ID en el archivo JSON.
            Jugador jugadorEdit = _repo.BuscarPorId(id);

            if (jugadorEdit == null)
            {
                _view.MostrarError("Jugador no encontrado");
                return;
            }

            Console.WriteLine();

            // Pedir nuevo nombre
            Console.Write("Nuevo nombre: ");
            string nuevoNombre = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(nuevoNombre))
            {
                jugadorEdit.Nombre = nuevoNombre;
            }

            Console.WriteLine();

            // Pedir nueva posición usando el mismo menú que AgregarJugador
            string nuevaPosicion = _jugadorView.PedirPosicion();
            jugadorEdit.Posicion = nuevaPosicion;

            Console.WriteLine();

            // Pedir nuevo número de camiseta
            int nuevoNumero = _jugadorView.PedirNumero();

            // Verificar que el número no esté repetido dentro del mismo equipo
            Equipo equipoJugador = _torneo.BuscarEquipo(jugadorEdit.EquipoNombre);

            if (equipoJugador != null)
            {
                if (equipoJugador.Jugadores.Any(j =>
                    j.id != jugadorEdit.id &&
                    j.Numero == nuevoNumero))
                {
                    _view.MostrarError("Ese numero de camiseta ya esta ocupado");
                    return;
                }
            }

            jugadorEdit.Numero = nuevoNumero;

            // Guardar los cambios en el JSON
            _repo.Actualizar(jugadorEdit);

            // Recargar los jugadores para reflejar los cambios inmediatamente
            foreach (Equipo equipo in _torneo.Equipos)
            {
                equipo.Jugadores.Clear();
            }

            foreach (Jugador jugador in _repo.LeerTodos())
            {
                Equipo equipo = _torneo.BuscarEquipo(jugador.EquipoNombre);

                if (equipo != null)
                {
                    equipo.AgregarJugador(jugador);
                }
            }

            _view.MostrarMensaje("Jugador actualizado correctamente");
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

            string nombre = _equipoView.PedirNombreEquipo();

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
