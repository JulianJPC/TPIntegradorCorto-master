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
        
        private string addVenta(Venta unaVenta)
        {
            var response = "";
            var jsonRequest = JsonConvert.SerializeObject(unaVenta);

            HttpResponseMessage responseQuery = WebHelper.Post("/api/Venta/AgregarVenta", jsonRequest);

            if (responseQuery.IsSuccessStatusCode)
            {
                response = responseQuery.StatusCode.ToString();
            }
            else
            {
                response = responseQuery.StatusCode.ToString();
            }
            return response;  
        }
        
    }
}
