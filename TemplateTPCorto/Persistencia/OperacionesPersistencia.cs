using Datos;
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


        //                                                         GET

        /// <summary>
        /// Dado el id de la operacion busca en el registro y
        /// la autorizacion con el mismo id y cuando lo encuentra
        /// busca en la tabla de tipo de operacion correspondiente el resto de la info
        /// al final agrega al objeto Operacion creado, la info de autorizacion del principio
        /// Si no encunetra devuelve null
        /// </summary>
        /// <param name="idOp">Id de una operacion de cambio de persona</param>
        public Operacion getOperacionById(string idOp)
        {
            Operacion laOperacion = null;
            var rawResponseAuto = dataBaseUtils.BuscarRegistro(idOp, 0, tableAutorizacion);
            if (rawResponseAuto.Count > 0)
            {
                var splitedLine = rawResponseAuto[0].Split(';');
                if (splitedLine[1] == "Desbloqueo")
                {
                    var rawResponse = dataBaseUtils.BuscarRegistro(idOp, 0, tableOpCambioCredencial);
                    if (rawResponse.Count > 0)
                    {
                        laOperacion = new OperacionCambioCredencial(rawResponse[0]);
                    }
                }
                else if (splitedLine[1] == "ModPersona")
                {
                    var rawResponse = dataBaseUtils.BuscarRegistro(idOp, 0, tableOpCambioPersona);
                    if (rawResponse.Count > 0)
                    {
                        laOperacion = new OperacionCambioPersona(rawResponse[0]);
                    }
                }
            }
            if (laOperacion != null)
            {
                laOperacion.getDataFromAuto(rawResponseAuto[0]);
            }
            return laOperacion;
        }
        /// <summary>
        /// Dado el tipo de operacion busca en la tabla de autorizaciones
        /// todas las autorizaciones pendientes
        /// de estas filtra y se queda con las filas que tengan el mismo tipo de operacion 
        /// y devuelve la lista de los id de esas operaciones.
        /// Si no encuentra nada devuelve la lista vacia
        /// </summary>
        /// <param name="tipoOperacion">Posibles nombres de tipo de operacion de la BD</param>
        public List<string> getAllPendientes(string tipoOperacion)
        {
            var rawResponse = dataBaseUtils.BuscarRegistro("Pendiente", 2, tableAutorizacion);
            var onlyOfMyTipe = new List<string>();
            foreach (var oneResponse in rawResponse)
            {
                var splitedLine = oneResponse.Split(';');
                if (splitedLine.Length > 0)
                {
                    if (splitedLine[1] == tipoOperacion)
                    {
                        onlyOfMyTipe.Add(splitedLine[0]);
                    }
                }
            }
            return onlyOfMyTipe;
        }

        //                                                          UPDATE

        /// <summary>
        /// Modifica una fila de autorizacion dada una operacion completa
        /// </summary>
        public void modAuto(Operacion unaOperacion)
        {
            dataBaseUtils.ModificarRegistro(unaOperacion.getRowAutoString(), tableAutorizacion, 0, unaOperacion.IdOperacion);
        }

        //                                                          ADD

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
            newOp.getDataPreAuto(supervisor.Legajo, "ModPersona");
            dataBaseUtils.AgregarRegistro(tableOpCambioPersona, newOp.getRowString());
            dataBaseUtils.AgregarRegistro(tableAutorizacion, newOp.getRowAutoString());
        }
        /// <summary>
        /// Dado una credencial que se quiere desbloquear y el supervisor que lo solicita
        /// crea una operacion de cambio de credencial con la info
        /// y añade a la tabla de operaciones de credenciales y despues añade a la tabla de autorizacions
        /// una autorizacion en estado pendiente
        /// </summary>
        /// <param name="aCredencial">Credencial que se quiere desbloquear</param>
        /// <param name="supervisor">Supervisor que solicita la operacion</param>
        public void addOpCredencial(Credencial aCredencial, string idPerfil, Persona supervisor)
        {
            var idOpNew = 0;
            idOpNew = getBiggestNumber(tableAutorizacion, 0, idOpNew);
            idOpNew++;
            var newOp = new OperacionCambioCredencial(aCredencial, idOpNew.ToString(), idPerfil);
            newOp.getDataPreAuto(supervisor.Legajo, "Desbloqueo");
            dataBaseUtils.AgregarRegistro(tableOpCambioCredencial, newOp.getRowString());
            dataBaseUtils.AgregarRegistro(tableAutorizacion, newOp.getRowAutoString());
        }
        
        //                                                              AUX
        
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
