using Datos.Ventas;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ProductoNegocio
    {
        public List<Producto> obtenerProductosPorCategoria(CategoriaProductos categoria)
        {
            var prodPersistencia = new ProductoPersistencia();
            var productos = prodPersistencia.obtenerProductosPorCategoria(categoria.Id);
            return productos;
        }
    }
}
