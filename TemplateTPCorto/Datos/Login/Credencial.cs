using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Credencial
    {
        private String _legajo;
        private String _nombreUsuario;
        private String _contrasena;
        private DateTime _fechaAlta;
        private DateTime _fechaUltimoLogin;
        private bool _isFirstLogIn;

        public string Legajo { get => _legajo; set => _legajo = value; }
        public string NombreUsuario { get => _nombreUsuario; set => _nombreUsuario = value; }
        public string Contrasena { get => _contrasena; set => _contrasena = value; }
        public DateTime FechaAlta { get => _fechaAlta; set => _fechaAlta = value; }
        public DateTime FechaUltimoLogin { get => _fechaUltimoLogin; set => _fechaUltimoLogin = value; }
        public bool isFirstLogIn { get => _isFirstLogIn; set => _isFirstLogIn = value; }

        /// <summary>
        /// Toma la fila de la base de datos sin separar de la tabla de credenciales y llena los valores de la Credencial
        /// </summary>
        /// <param name="registro">String separado con comas de la fila de la tabla de autorizaciones</param>
        public Credencial(string registro)
        {
            var datos = registro.Split(';');
            this._legajo = datos[0];
            this._nombreUsuario = datos[1];
            this._contrasena = datos[2];
            this._fechaAlta = DateTime.ParseExact(datos[3], "d/M/yyyy", CultureInfo.InvariantCulture);
            if (datos[4].Length == 0)
            {
                _isFirstLogIn = true;
            }
            else
            {
                this._fechaUltimoLogin = DateTime.ParseExact(datos[4], "d/M/yyyy", CultureInfo.InvariantCulture);
                _isFirstLogIn = false;
            }
        }
        /// <summary>
        /// Si los valores de la credencial son adecuados al contexto
        /// Si lo son devuelve true
        /// Si no lo son devuelve false
        /// </summary>
        public bool passValueTest()
        {
            var response = true;
            var legajoLimit = 0;
            if(!Int32.TryParse(_legajo, out legajoLimit))
            {
                if(legajoLimit <= 0)
                {
                    response = false;
                }
            }
            if(_nombreUsuario.Replace(" ", "") == "")
            {
                response = false;
            }
            if (_contrasena.Replace(" ", "") == "")
            {
                response = false;
            }
            if (_fechaAlta <= new DateTime(1900, 1, 1) || _fechaAlta > DateTime.Now)
            {
                response = false;
            }
            if (_fechaUltimoLogin <= new DateTime(1900, 1, 1) || _fechaUltimoLogin > DateTime.Now)
            {
                response = false;
            }
            return response;
        }
        public Credencial(string legajo, string usuario, string pass, DateTime fechaAlta, DateTime fechaLogIn)
        {
            this._legajo = legajo;
            this._nombreUsuario = usuario;
            this._contrasena = pass;
            this._fechaAlta = fechaAlta;
            this._fechaUltimoLogin = fechaLogIn;
            this._isFirstLogIn = false;
        }
        /// <summary>
        /// Devuleve un string con los valores separados por punto y coma para insertar en la BD
        /// </summary>
        public string getRowString()
        {
            var response = String.Join(";", _legajo, _nombreUsuario, _contrasena, _fechaAlta.ToString("d/M/yyyy"), _fechaUltimoLogin.ToString("d/M/yyyy"));
            return response;
        }
        /// <summary>
        /// Devuelve un string con lo valores separados por punto y coma
        /// para insertar en la BD y se agrega el idPerfil para poder insertar
        /// en la tabla de operaciones de credenciales
        /// </summary>
        /// <param name="idPerfil">IdPerfil asociada a la credencial</param>
        public string getRowString(string idPerfil)
        {
            var response = String.Join(";", _legajo, _nombreUsuario, _contrasena, idPerfil, _fechaAlta.ToString("d/M/yyyy"), _fechaUltimoLogin.ToString("d/M/yyyy"));
            return response;
        }
        /// <summary>
        /// Devuelve un string con lo valores separados por punto y coma para insertar en la BD en la tabla de login intentos
        /// </summary>
        public string getLogInAttempRowString()
        {
            var response = String.Join(";", _legajo, DateTime.Now.ToString("d/M/yyyy"));
            return response;
        }
    }
}
