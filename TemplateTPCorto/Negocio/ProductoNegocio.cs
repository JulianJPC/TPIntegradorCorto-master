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
        /// <summary>
        /// Obtiene desde la API los productos de una categoria dada
        /// </summary>
        /// <param name="categoria">Categoria de producto</param>
        public List<Producto> obtenerProductosPorCategoria(CategoriaProductos categoria)
        {
            var prodPersistencia = new ProductoPersistencia();
            var productos = prodPersistencia.obtenerProductosPorCategoria(categoria.Id);
            return productos;
        }
    }
}
