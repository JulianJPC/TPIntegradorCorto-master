using Datos.Login;
using Persistencia.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public class OperacionesPersistencia
    {
        private DataBaseUtils dataBaseUtils { get; set; }
        private string tableOpCambioCredencial { get; set; }
        private string tableOpCambioPersona { get; set; }
        private string tableAutorizacion { get; set; }
        public OperacionesPersistencia()
        {
            dataBaseUtils = new DataBaseUtils();
            tableAutorizacion = "autorizacion.csv";
            tableOpCambioCredencial = "operacion_cambio_credencial.csv";
            tableOpCambioPersona = "operacion_cambio_persona.csv";
        }

        /// <summary>
        /// Dado dos personas una a la que se le quiere modificar datos y otra que es el supervisor
        /// crea una operacion de cambio de persona con la info de estos dos
        /// y añade a la tabla de operaciones de cambio de persona y despues añade a la tabla de autorizacions
        /// una autorizacion en estado pendiente
        /// </summary>
        /// <param name="aPersona">Persona modificada que se quiere para la operacion</param>
        /// <param name="supervisor">Supervisor que solicita la operacion</param>
        public void addOpPersona(Persona aPersona, Persona supervisor)
        {
            var idOpNew = 0;
            idOpNew = getBiggestNumber(tableAutorizacion, 0, idOpNew);
            idOpNew++;
            var newOp = new OperacionCambioPersona(aPersona, idOpNew.ToString());
            newOp.getDataFromPers(supervisor, "Pendiente");
            dataBaseUtils.AgregarRegistro(tableOpCambioPersona, newOp.getRowString());
            dataBaseUtils.AgregarRegistro(tableAutorizacion, newOp.getRowAutoString());
        }
        /// <summary>
        /// Dada una tabla y un numero de columna en que buscar y un numero base,
        /// se va fijando fila por fila si el numero de la columna dada es mayor al numero base
        /// si es mayor lo toma y sigue viendo si hay un numero mayor a ese
        /// al final devuelve el numero mas grande.
        /// Si no encuentra devuleve el numero base.
        /// </summary>
        /// <param name="table">Tabla en que buscar la columna con numeros</param>
        /// <param name="indexNumberColumn">Numero de columna en que buscar</param>
        /// <param name="previousNumber">Numero base para comparar</param>
        private int getBiggestNumber(string table, int indexNumberColumn, int previousNumber)
        {
            var response = previousNumber;
            var allRowsTable = dataBaseUtils.getAllRegistro(table);
            foreach (var row in allRowsTable)
            {
                var idOp = row.Split(';')[indexNumberColumn];
                if (Int32.TryParse(idOp, out int placeHolder))
                {
                    var idOpInt = Int32.Parse(idOp);
                    if (idOpInt > response)
                    {
                        response = idOpInt;
                    }
                }
            }
            return response;
        }
    }
}
