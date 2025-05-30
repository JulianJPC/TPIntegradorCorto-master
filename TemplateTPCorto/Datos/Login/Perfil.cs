using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Login
{
    public class Perfil
    {
        private string _id;
        private string _descripcion;
        private List<Rol> _roles;

        public string Id { get => _id; set => _id = value; }
        public string Descripcion { get => _descripcion; set => _descripcion = value; }
        public List<Rol> Roles { get => _roles; set => _roles = value; }
        public Perfil(List<string> registro)
        {
            this._id = registro[0];
            this._descripcion = registro[1];
        }
    }
}
