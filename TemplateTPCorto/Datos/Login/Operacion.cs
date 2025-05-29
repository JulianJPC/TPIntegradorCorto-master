using System;
using System.Collections.Generic;
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

        public void getDataFromCred(Credencial laCredencial, string newEstado, string lAuto)
        {
            _tipoOperacion = "Desbloqueo";
            _estado = newEstado;
            _legajoSolicitante = laCredencial.Legajo;
            _legajoAuto = lAuto;
            _fechaAuto = DateTime.Now;
        }
        public void getDataFromPers(Persona laPers, string newEstado, string lAuto)
        {
            _tipoOperacion = "ModPersona";
            _estado = newEstado;
            _legajoSolicitante = laPers.Legajo;
            _legajoAuto = lAuto;
            _fechaAuto = DateTime.Now;
        }
        public string getRowAutoString()
        {
            return String.Join(";", _idOperacion, _tipoOperacion, _estado, _legajoSolicitante, _fechaSolicitud.ToString("d/M/yyyy"), _legajoAuto, _fechaAuto.ToString("d/M/yyyy"));
        }

    }
}
