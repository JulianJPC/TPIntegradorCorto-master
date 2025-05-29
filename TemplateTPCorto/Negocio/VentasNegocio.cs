using Datos;
using Datos.Ventas;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateTPCorto;

namespace Negocio
{
    public class VentasNegocio
    {
        public List<Cliente> obtenerClientes()
        {
            List<Cliente> clientes = new List<Cliente>();

            ClientePersistencia clientePersistencia = new ClientePersistencia();

            clientes = clientePersistencia.obtenerClientes();

            return clientes;
        }
        public string createVenta(List<Venta> listaVentas)
        {
            var response = "Exito al realizar las ventas.";
            var ventPersistente = new VentaPersistencia();
            var msgResult = ventPersistente.addVentas(listaVentas);
            if(msgResult.Count > 0)
            {
                response = "Error en Ventas:\n";
                foreach( var oneMsg in msgResult)
                {
                    response += oneMsg;
                }
            }
            return response;
        }

        public List<CategoriaProductos> obtenerCategoriaProductos()
        {
            List<CategoriaProductos> categoriaProductos = new List<CategoriaProductos>();

            CategoriaProductos p1 = new CategoriaProductos("1", "Audio");
            categoriaProductos.Add(p1);

            CategoriaProductos p2 = new CategoriaProductos("2", "Celulares");
            categoriaProductos.Add(p2);

            CategoriaProductos p3 = new CategoriaProductos("3", "Electro Hogar");
            categoriaProductos.Add(p3);

            CategoriaProductos p4 = new CategoriaProductos("4", "Informática");
            categoriaProductos.Add(p4);

            CategoriaProductos p5 = new CategoriaProductos("5", "Smart TV");
            categoriaProductos.Add(p5);

            return categoriaProductos;
        }
        public void startFormVentas(Credencial laCredencial)
        {
            var newFormVentas = new FormVentas(laCredencial);
            newFormVentas.ShowDialog();
        }

    }
}
