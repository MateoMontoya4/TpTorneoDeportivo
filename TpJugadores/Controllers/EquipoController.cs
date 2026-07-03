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

            _torneo.AgregarEquipo(nuevoEquipo);

            _view.MostrarMensaje($"El equipo '{nombre}' fue agregado correctamente");

            _torneo.AgregarEquipo(nuevoEquipo);
            _repo.GuardarTodos(_torneo.Equipos);
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
                _view.LetrasCentradas("- " + equipo.Nombre);
            }

            Console.WriteLine();

            string nombre = _view.PedirNombreEquipo();

            Equipo equipoEliminar = _torneo.BuscarEquipo(nombre);

            if (equipoEliminar == null)
            {
                _view.MostrarError("Equipo no encontrado");
                return;
            }

            _torneo.EliminarEquipo(equipoEliminar);

            _view.MostrarMensaje(
                $"El equipo {equipoEliminar.Nombre} fue eliminado correctamente");
            
            _torneo.EliminarEquipo(equipoEliminar);
            _repo.GuardarTodos(_torneo.Equipos);
        }
    }
}
