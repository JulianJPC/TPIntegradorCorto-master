using Datos.Ventas;
using Persistencia.WebService.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Persistencia
{
    public class ClientePersistencia
    {
        /// <summary>
        /// Se comunica con la API, obtiene los clientes y si estan bien los deserializa 
        /// y añade a una lista de clientes y devuelve.
        /// Si hay error devuelve la lista vacia.
        /// </summary>
        public List<Cliente> obtenerClientes()
        {
            List<Cliente> listaClientes = new List<Cliente>();

            HttpResponseMessage response = WebHelper.Get("/api/Cliente/GetClientes");

            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                var contentStream = response.Content.ReadAsStringAsync().Result;
                listaClientes = JsonConvert.DeserializeObject<List<Cliente>>(contentStream);
            }

            return listaClientes;
        }

    }
}
