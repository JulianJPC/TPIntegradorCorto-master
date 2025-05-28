using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Login
{
    public class Autorizacion
    {
        private string _idOperacion;
        private string _tipoOperacion;
        private string _estado;
        private string _legajoSolicitante;
        private DateTime _fechaSolicitud;
        private string _legajoAutorizador;
        private DateTime _fechaAutorizacion;

        public string IdOperacion { get => _idOperacion; set => _idOperacion = value; }
        public string TipoOperacion { get => _tipoOperacion; set => _tipoOperacion = value; }
        public string Estado { get => _estado; set => _estado = value; }
        public string LegajoSolicitante { get => _legajoSolicitante; set => _legajoSolicitante = value; }
        public DateTime FechaSolicitud { get => _fechaSolicitud; set => _fechaSolicitud = value; }
        public string LegajoAutorizador { get => _legajoAutorizador; set => _legajoAutorizador = value; }
        public DateTime FechaAutorizacion { get => _fechaAutorizacion; set => _fechaAutorizacion = value; }


    }
}
