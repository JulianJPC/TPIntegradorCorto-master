using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Login
{
    public abstract class Operacion
    {
        protected string _idOperacion;
        protected string _tipoOperacion;
        protected DateTime _fechaSolicitud;
        protected string _estado;
        protected string _legajoSolicitante;
        protected string _legajoAuto;
        protected DateTime _fechaAuto;

        public string IdOperacion { get => _idOperacion; set => _idOperacion = value; }
        public DateTime FechaSolicitud { get => _fechaSolicitud; set => _fechaSolicitud = value; }
        public string Estado { get => _estado; set => _estado = value; }
        public string TipoOperacion { get => _tipoOperacion; set => _tipoOperacion = value; }
        /// <summary>
        /// Obtiene el legajo de solicitante, estado de autorizacion, y el tipo de operacion
        /// para insertar en un estado previo a la autorizacion
        /// </summary>
        /// <param name="legajo">Numero de legajo valido</param>
        /// <param name="tipoOp">El tipo de operacion a autorizar</param>
        public void getDataPreAuto(string legajo, string tipoOp)
        {
            _tipoOperacion = tipoOp;
            _estado = "Pendiente";
            _legajoSolicitante = legajo;
        }
        /// <summary>
        /// Obtiene el nuevo estado, legajo de autorizador y la fecha de autorizacion
        /// </summary>
        /// <param name="newEstado">Nuevo estado de la autorizacion</param>
        /// <param name="lAuto">legajo de autorización</param>
        public void getDataForAuto(string newEstado, string lAuto)
        {
            _estado = newEstado;
            _legajoAuto = lAuto;
            _fechaAuto = DateTime.Now;
        }
        /// <summary>
        /// Dada una fila de la base de datos de la tabla de autorizaciones
        /// llena de valores la operacion con estos valores.
        /// </summary>
        /// <param name="registro">Fila de la tabla de autorizaciones</param>
        public void getDataFromAuto(string registro)
        {
            var splitedLine = registro.Split(';');
            _tipoOperacion = splitedLine[1];
            _estado = splitedLine[2];
            _legajoSolicitante = splitedLine[3];
            _fechaSolicitud = DateTime.ParseExact(splitedLine[4], "d/M/yyyy", CultureInfo.InvariantCulture);
            if(splitedLine.Length > 5)
            {
                if(splitedLine[5].Length > 0)
                {
                    _legajoAuto = splitedLine[5];
                }
            }
            if (splitedLine.Length > 6)
            {
                if (splitedLine[6].Length > 0)
                {
                    _fechaAuto = DateTime.ParseExact(splitedLine[6], "d/M/yyyy", CultureInfo.InvariantCulture);
                }
            }
        }
        /// <summary>
        /// Devuelve una fila separada por ';' entre sus elementos.
        /// Si no hay fecha de solicitud devuelve la fila sin numero de legajo y fecha de autorizacion
        /// </summary>
        public string getRowAutoString()
        {
            var response = String.Join(";", _idOperacion, _tipoOperacion, _estado, _legajoSolicitante, _fechaSolicitud.ToString("d/M/yyyy"), "", "");
            if (_fechaAuto > new DateTime(1900, 1, 1))
            {
                response = String.Join(";", _idOperacion, _tipoOperacion, _estado, _legajoSolicitante, _fechaSolicitud.ToString("d/M/yyyy"), _legajoAuto, _fechaAuto.ToString("d/M/yyyy"));
            }
            return response;
        }

    }
}
