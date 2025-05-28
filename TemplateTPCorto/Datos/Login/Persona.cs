using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Login
{
    public class Persona
    {
        private String _legajo;
        private String _nombre;
        private String _apellido;
        private String _dni;
        private DateTime _fechaIngreso;

        public string Legajo { get => _legajo; set => _legajo = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Apellido { get => _apellido; set => _apellido = value; }
        public string DNI { get => _dni; set => _dni = value; }
        public DateTime FechaIngreso { get => _fechaIngreso; set => _fechaIngreso = value; }

        public Persona(string registro)
        {
            var datos = registro.Split(';');
            this._legajo = datos[0];
            this._nombre = datos[1];
            this._apellido = datos[2];
            this._dni = datos[3];
            this._fechaIngreso = DateTime.ParseExact(datos[4], "d/M/yyyy", CultureInfo.InvariantCulture);
        }
        public Persona(string l, string n, string a, string d, DateTime fI)
        {
            this._legajo = l;
            this._nombre = n;
            this._apellido = a;
            this._dni = d;
            this._fechaIngreso = fI;
        }
        public bool passValueTest()
        {
            var response = true;
            var legajoLimit = 0;
            if (!Int32.TryParse(_legajo, out legajoLimit))
            {
                if (legajoLimit <= 0)
                {
                    response = false;
                }
            }
            if (_nombre.Replace(" ", "") == "")
            {
                response = false;
            }
            if (_apellido.Replace(" ", "") == "")
            {
                response = false;
            }
            if (!Int32.TryParse(_dni, out int placeHolder))
            {
                response = false;
            }
            if (_fechaIngreso <= new DateTime(1900, 1, 1) || _fechaIngreso > DateTime.Now)
            {
                response = false;
            }
            return response;
        }
        public string getRowString()
        {
            var response = String.Join(";", _legajo, _nombre, _apellido, _dni, _fechaIngreso.ToString("d/M/yyyy"));
            return response;
        }
    }
}
