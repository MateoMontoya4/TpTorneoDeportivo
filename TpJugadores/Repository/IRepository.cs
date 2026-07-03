using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpJugadores.Repository
{
    public interface IRepository<T>
    {
        List<T> LeerTodos();
        void GuardarTodos(List<T> datos);
    }
}
