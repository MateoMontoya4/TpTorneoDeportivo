using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TpJugadores.Models;

namespace TpJugadores.Repository
{
    // El Repository solo puede trabajar con clases que implementen IEntidad.
    // De esta manera todas tienen un Id y pueden buscarse, actualizarse o eliminarse.
    public interface IRepository<T> where T : IEntidad
    {
        List<T> LeerTodos();
        void GuardarTodos(List<T> datos);

      
        // Busca un objeto por su Id.
        T? BuscarPorId(int id);

        // Agrega un nuevo objeto.
        void Agregar(T item);

        // Actualiza un objeto existente.
        void Actualizar(T item);

        // Elimina un objeto usando su Id.
        void Eliminar(int id);
    }
}
