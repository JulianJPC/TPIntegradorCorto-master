using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Ventas
{
    public class Venta
    {
        private Guid _idCliente;
        private Guid _idUsuario;
        private Guid _idProducto;
        private int _cantidad;
        public Guid idCliente { get => _idCliente; set => _idCliente = value; }
        public Guid idUsuario { get => _idUsuario; set => _idUsuario = value; }
        public Guid idProducto { get => _idProducto; set => _idProducto = value; }
        public int cantidad { get => _cantidad; set => _cantidad = value; }

        public Venta(Guid idCliente, Guid idProducto, int cantidad)
        {
            this._idCliente = idCliente;
            this._idProducto = idProducto;
            this._cantidad = cantidad;
        }
        public override string ToString()
        {
            return _idProducto.ToString() + " Cantidad: " + _cantidad.ToString();
        }

    }
}
