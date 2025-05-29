using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Login
{
    public class OperacionCambioPersona : Operacion
    {
        private Persona _persona;

        public Persona Persona { get => _persona; set => _persona = value; }
        public string IdOperacion { get => _idOperacion; set => _idOperacion = value; }
        public DateTime FechaSolicitud { get => _fechaSolicitud; set => _fechaSolicitud = value; }

        public OperacionCambioPersona(Persona aPersona, string idOp)
        {
            this._persona = aPersona;
            this._idOperacion = idOp;
            this._fechaSolicitud = DateTime.Now;
        }
        public OperacionCambioPersona(string registro)
        {
            var splitedRegistro = registro.Split(';');
            _idOperacion = splitedRegistro[0];
            var fechaIngreso = DateTime.ParseExact(splitedRegistro[5], "d/M/yyyy", CultureInfo.InvariantCulture);
            _fechaSolicitud = DateTime.ParseExact(splitedRegistro[6], "d/M/yyyy", CultureInfo.InvariantCulture);
            _persona = new Persona(splitedRegistro[1], splitedRegistro[2], splitedRegistro[3], splitedRegistro[4], fechaIngreso);
        }
        public string getRowString()
        {
            var response = String.Join(";", _idOperacion, _persona.getRowString(), _fechaSolicitud.ToString("d/M/yyyy"));
            return response;
        }
    }
}
