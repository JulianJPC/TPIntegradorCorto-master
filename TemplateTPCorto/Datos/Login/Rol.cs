using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Login
{
    public class Rol
    {
        private string _id;
        private string _descripcion;

        public string Id { get => _id; set => _id = value; }
        public string Descripcion { get => _descripcion; set => _descripcion = value; }

        public Rol(string registro)
        {
            var splitedLine = registro.Split(';');
            this._id = splitedLine[0];
            this._descripcion= splitedLine[1];
        }
    }
}
