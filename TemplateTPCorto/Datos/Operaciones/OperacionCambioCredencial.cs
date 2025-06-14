﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Login
{
    public class OperacionCambioCredencial: Operacion
    {
        private Credencial _credencial;
        private string _idPerfil;

        public Credencial Credencial { get => _credencial; set => _credencial = value; }
        public string IdPerfil { get => _idPerfil; set => _idPerfil = value; }

        public OperacionCambioCredencial(Credencial aCredencial, string idOp, string idP)
        {
            this._credencial = aCredencial;
            this._idOperacion = idOp;
            this._idPerfil = idP;
            _fechaSolicitud = DateTime.Now;
        }
        public OperacionCambioCredencial(string registro)
        {
            var splitedRegistro = registro.Split(';');
            _idOperacion = splitedRegistro[0];
            _idPerfil = splitedRegistro[4];
            var fechaAlta = DateTime.ParseExact(splitedRegistro[5], "d/M/yyyy", CultureInfo.InvariantCulture);
            var fechaUltimoLogIn = DateTime.ParseExact(splitedRegistro[6], "d/M/yyyy", CultureInfo.InvariantCulture);
            _credencial = new Credencial(splitedRegistro[1], splitedRegistro[2], splitedRegistro[3], fechaAlta, fechaUltimoLogIn);
             
        }
        public string getRowString()
        {
            var response = String.Join(";", _idOperacion, _credencial.getRowString(_idPerfil));
            return response;
        }
    }
}
