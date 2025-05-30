using Datos.Ventas;
using Newtonsoft.Json;
using Persistencia.WebService.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public class VentaPersistencia
    {
        private Guid idUsuario = new Guid("784c07f2-2b26-4973-9235-4064e94832b5");

        /// <summary>
        /// Toma una lista de Ventas y las manda al api a guardar una por una
        /// Si hay alguna que salga mal las añade en una lista con el nombre del item
        /// Al final devuelve la lista de errores
        /// </summary>
        /// <param name="listVentas">Listado de ventas a realizar</param>
        public List<string> addVentas(List<Venta> listVentas)
        {
            var listMsg = new List<string>();
            foreach(var item in listVentas)
            {
                item.idUsuario = idUsuario;
                var msgOperation = addVenta(item);
                if(msgOperation != "OK")
                {
                    listMsg.Add(msgOperation + " " + item.ToString());
                }
            }
            return listMsg;
        }
        
        /// <summary>
        /// Dada una venta la serializa y postea en el Api.
        /// Devuelve el resultado del proceso
        /// </summary>
        /// <param name="unaVenta">Un objeto de venta</param>
        private string addVenta(Venta unaVenta)
        {
            var response = "";
            var jsonRequest = JsonConvert.SerializeObject(unaVenta);

            HttpResponseMessage responseQuery = WebHelper.Post("/api/Venta/AgregarVenta", jsonRequest);

            response = responseQuery.StatusCode.ToString();
            return response;  
        }
        
    }
}
