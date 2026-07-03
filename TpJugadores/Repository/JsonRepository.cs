using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;

namespace TpJugadores.Repository
{
    public class JsonRepository<T> : IRepository<T>
    {
        private readonly string _rutaArchivo;

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
            var opts = new JsonSerializerOptions    
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(datos, opts);

            File.WriteAllText(_rutaArchivo, json);
        }
    }
}
