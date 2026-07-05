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
    public class EquipoController
    {
        private Torneo _torneo;
        private TorneoView _view;
        private IRepository<Equipo> _repo;

        public EquipoController(Torneo torneo, IRepository<Equipo> repo)
        {
            _torneo = torneo;
            _view = new TorneoView();
            _repo = repo;
        }

        // Agrega un equipo
        public void AgregarEquipo()
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

            // Se agrega el equipo al torneo para poder usarlo durante la ejecución del programa
            _torneo.AgregarEquipo(nuevoEquipo);

            // El Repository se encarga de asignar el Id y guardar el nuevo equipo en el archivo JSON.
            _repo.Agregar(nuevoEquipo);

            _view.MostrarMensaje($"El equipo '{nombre}' fue agregado correctamente");

           
        }
        
        public void ListarEquipos()
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
        public void BuscarEquipo()
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
        // Muestra el equipo con mas puntos
        public void MostrarCampeon()
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
        public void EliminarEquipo()
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
                foreach (Jugador jugador in equipo.Jugadores)
                {
                    _view.LetrasCentradas(
                        $"ID: {jugador.id} - {jugador.Nombre} ({equipo.Nombre})"
                    );
                }
            }

            Console.Write("Ingrese ID del jugador a eliminar: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                _view.MostrarError("ID inválido");
                return;
            }

            // buscar jugador
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

            // eliminar de memoria
            equipoPadre.Jugadores.Remove(jugadorEliminar);

            // 🔥 eliminar del repo (CORRECTO)
            _repo.Eliminar(jugadorEliminar.id);

            _view.MostrarMensaje("Jugador eliminado correctamente");
        }

        public void ActualizarEquipo()
        {
            Console.Clear();

            _view.LetrasCentradas("ACTUALIZAR EQUIPO");

            foreach (var equipo in _torneo.Equipos)
            {
                _view.LetrasCentradas($"ID: {equipo.id} - {equipo.Nombre}");
            }

            Console.Write("Ingrese ID del equipo a modificar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                _view.MostrarError("ID inválido");
                return;
            }

            Equipo equipoEdit = _repo.BuscarPorId(id);

            if (equipoEdit == null)
            {
                _view.MostrarError("Equipo no encontrado");
                return;
            }

            Console.Write("Nuevo nombre: ");
            string nuevoNombre = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(nuevoNombre))
            {
                equipoEdit.Nombre = nuevoNombre;
            }

            //  ACTUALIZA SOLO ESE REGISTRO
            _repo.Actualizar(equipoEdit);

            _torneo.Equipos = _repo.LeerTodos();

            _view.MostrarMensaje("Equipo actualizado correctamente");
        }

    }
}
