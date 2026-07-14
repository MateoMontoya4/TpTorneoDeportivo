using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using TpJugadores.Models;

namespace TpJugadores.Repository
{
    public class JsonRepository<T> : IRepository<T> where T : IEntidad
    {
        private readonly string _rutaArchivo;


        // Este método guarda la lista completa en el archivo JSON.
        // Lo usamos desde Agregar, Actualizar y Eliminar para no repetir código.
        private void Persistir(List<T> lista)
        {
            var opts = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            File.WriteAllText(_rutaArchivo, 
            JsonSerializer.Serialize(lista, opts));
        }
        public JsonRepository(string ruta)
        {
            _rutaArchivo = ruta;
        }

        public List<T> LeerTodos()
        {
            if (!File.Exists(_rutaArchivo))
                return new();

            var json = File.ReadAllText(_rutaArchivo);

            return JsonSerializer.Deserialize<List<T>>(json) ?? new();
        }

        public void GuardarTodos(List<T> datos)
        {
            // En lugar de volver a escribir el código de guardado,
            // llamamos al método Persistir y le pasamos la misma lista.
            Persistir(datos);
        }



        
     
        public T? BuscarPorId(int id) // Busca un registro por su Id.
        => LeerTodos().
            FirstOrDefault(x => x.id == id);  // Lee todos los datos del archivo y devuelve el primero que tenga ese Id si no lo encuentra devuelve NULL.




        // Agrega un nuevo objeto al archivo JSON.
        // Si es el primero, le asigna Id = 1.
        // Si ya existen registros, le asigna el siguiente Id disponible.
        public void Agregar(T item)
        {

            var lista = LeerTodos();

           
            item.id = lista.Count > 0        // Genera un Id autoincremental
                ? lista.Max(x => x.id) + 1 
                : 1;

            lista.Add(item);

            Persistir(lista);
        }

        
        
        public void Actualizar(T item) // Busca el objeto por su Id y reemplaza la información anterior.
        {
            var lista = LeerTodos();

           
            var idx = lista.FindIndex(x => x.id == item.id);  // Busca la posición del objeto dentro de la lista usando su Id.

            if (idx >= 0)
            {
                lista[idx] = item;

                
            }

            Persistir(lista);  // Guarda la lista con los cambios realizados.


        }

        
        public void Eliminar(int id) // Elimina un registro del archivo usando su Id.
        {
            var lista = LeerTodos();

            
            lista.RemoveAll(e => e.id == id); // Elimina todos los registros que tengan ese Id.

            Persistir(lista);
        }

    }
}
