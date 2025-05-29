using Datos;
using Datos.Login;
using Persistencia.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Persistencia
{
    public class UsuarioPersistencia
    {
        private string tableAutorizacion { get; set; }
        private string tableCredenciales { get; set; }
        private string tableLogInIntentos { get; set; }
        private string tableOpCambioCredencial { get; set; }
        private string tableOpCambioPersona { get; set; }
        private string tablePerfil { get; set;}
        private string tablePerfilRol { get; set; }
        private string tablePersona { get; set; }
        private string tableRol { get; set; }
        private string tableUsuarioBloqueado { get; set; }
        private string tableUsuarioPerfil { get; set; }
        private DataBaseUtils dataBaseUtils { get; set; }

        public UsuarioPersistencia()
        {
            dataBaseUtils = new DataBaseUtils();
            tableAutorizacion = "autorizacion.csv";
            tableCredenciales = "credenciales.csv";
            tableLogInIntentos = "login_intentos.csv";
            tableOpCambioCredencial = "operacion_cambio_credencial.csv";
            tableOpCambioPersona = "operacion_cambio_persona.csv";
            tablePerfil = "perfil.csv";
            tablePerfilRol = "perfil_rol.csv";
            tablePersona = "persona.csv";
            tableRol = "rol.csv";
            tableUsuarioBloqueado = "usuario_bloqueado.csv";
            tableUsuarioPerfil = "usuario_perfil.csv";
        }
        // BASES
        private void addRowToTable(string nameTable, string rowToAdd)
        {
            var dataBaseUtils = new DataBaseUtils();
            dataBaseUtils.AgregarRegistro(nameTable, rowToAdd);
        }
        /// <summary>
        /// Funcion base para buscar en una tabla las filas  dado un numero de columna y valor sean iguales.
        /// Si encuentra filas las añade en una List<string> y devuelve.
        /// Si no encuntra devuelve la lista vacía.
        /// </summary>
        /// <param name="nameTable">Nombre de la tabla a buscar</param>
        /// <param name="valueOfColumn">Valor a matchear</param>
        /// <param name="indexOfColumn">Numero de columna en que buscar</param>
        /// <returns></returns>
        private List<string> getRowsFromTable(string nameTable, string valueOfColumn, int indexOfColumn)
        {
            var response = new List<string>();
            response = dataBaseUtils.BuscarRegistro(valueOfColumn, indexOfColumn, nameTable);
            return response;
        }
        private List<string> getRowsFromTable(string nameTable)
        {
            var response = new List<string>();
            var dataBaseUtils = new DataBaseUtils();
            response = dataBaseUtils.getAllRegistro(nameTable);
            return response;
        }
        private void updateRowTable(string nameTable, string valueOfColumn, int indexOfColumn, string newRow)
        {
            dataBaseUtils.ModificarRegistro(newRow, nameTable, indexOfColumn, valueOfColumn);
        }
        private void DeleteRowTable(string nameTable, string valueOfColumn, int indexOfColumn)
        {
            DataBaseUtils dataBaseUtils = new DataBaseUtils();
            dataBaseUtils.BorrarRegistro(nameTable, indexOfColumn, valueOfColumn);
        }

        // GET
        public int getLogInAttempsByLegajo(Credencial theCredencial)
        {
            var resultQuery = getRowsFromTable(tableLogInIntentos, theCredencial.Legajo, 0);
            return resultQuery.Count;
        }

        /// <summary>
        /// Dada una credencial busca en la tabla de usuarios bloqueados
        /// si hay filas con el mismo numero de legajo. Las añade en una lista
        /// y devuelve.
        /// Si no encuentra devuelve la lista vacia.
        /// </summary>
        /// <param name="aCredencial">Credencial de usuario llena de valores</param>
        public List<string> getUsuariosBloqueadosByLegajo(Credencial aCredencial)
        {
            return dataBaseUtils.BuscarRegistro(aCredencial.Legajo, 0, tableUsuarioBloqueado);
        }
        /// <summary>
        /// Metodo usuado para buscar en la tabla de Credenciales
        /// la credencial que tenga el mismo nombre de usuario.
        /// Si encuentra credenciales con el mismo nombre de usuario devuelve la primera que encuentra
        /// Si no encuentra credenciales devuelve null
        /// </summary>
        /// <param name="username">Nombre de usuario de la credencial</param>
        public Credencial getCredencialByUsername(string username)
        {
            Credencial aCredencial = null;
            var rawResponse = dataBaseUtils.BuscarRegistro(username, 1, tableCredenciales);
            if (rawResponse.Count > 0)// si encuentra credencial devuelve solo la primera que encuentra
            {
                aCredencial = new Credencial(rawResponse[0]);
            }
            return aCredencial;
        }
        public Credencial getCredencialByLegajo(string legajo)
        {
            Credencial aCredencial = null;
            var rawResponse = getRowsFromTable(tableCredenciales, legajo, 0);
            if (rawResponse.Count > 0)
            {
                aCredencial = new Credencial(rawResponse[0]);
            }
            return aCredencial;
        }
        public Persona getPersonaByLegajo(string legajo)
        {
            Persona aPersona = null;
            var rawResponse = getRowsFromTable(tablePersona, legajo, 0);
            if (rawResponse.Count > 0)
            {
                aPersona = new Persona(rawResponse[0]);
            }
            return aPersona;
        }
        public OperacionCambioCredencial getOperacionCByIdOp(string idOp)
        {
            OperacionCambioCredencial aOperacion = null;
            var rawResponse = getRowsFromTable(tableOpCambioCredencial, idOp, 0);
            if (rawResponse.Count > 0)
            {
                aOperacion = new OperacionCambioCredencial(rawResponse[0]);
            }
            return aOperacion;
        }
        public OperacionCambioPersona getOperacionPByIdOp(string idOp)
        {
            OperacionCambioPersona aOperacion = null;
            var rawResponse = getRowsFromTable(tableOpCambioPersona, idOp, 0);
            if (rawResponse.Count > 0)
            {
                aOperacion = new OperacionCambioPersona(rawResponse[0]);
            }
            return aOperacion;
        }
        public string getPerfilByLegajo(string legajo)
        {
            var response = "";
            var responseQuery = getRowsFromTable(tableUsuarioPerfil, legajo, 0);
            if (responseQuery.Count > 0)
            {
                var idPerfilString = responseQuery[0].Split(';')[1];
                response = idPerfilString;
            }
            return response;
        }
        public string getPerfilByIdPerfil(string idPerfil)
        {
            var response = "";
            var responseQuery = getRowsFromTable(tablePerfil, idPerfil, 0);
            if (responseQuery.Count > 0)
            {
                var descPerfilString = responseQuery[0].Split(';')[1];
                response = descPerfilString;
            }
            return response;
        }
        public List<string> getAllLegajosFromCredenciales()
        {
            var response = new List<string>();
            var rawResult = getRowsFromTable(tableCredenciales);
            foreach (string oneLine in rawResult)
            {
                var splitedLine = oneLine.Split(';');
                response.Add(splitedLine[0]);
            }

            return response;
        }
        public List<string> getAllIdOpCredenciales()
        {
            var response = new List<string>();
            var resultQuery = getRowsFromTable(tableOpCambioCredencial);
            foreach (string oneLine in resultQuery)
            {
                var splitedLine = oneLine.Split(';');
                if (splitedLine[0].Length > 0)
                {
                    response.Add(splitedLine[0]);
                }
            }
            return response;
        }
        public List<string> getAllIdOpPersonas()
        {
            var response = new List<string>();
            var resultQuery = getRowsFromTable(tableOpCambioPersona);
            foreach (string oneLine in resultQuery)
            {
                var splitedLine = oneLine.Split(';');
                if (splitedLine[0].Length > 0)
                {
                    response.Add(splitedLine[0]);
                }
                
            }
            return response;
        }
        public List<string> getAllOpPersonas()
        {
            var resultQuery = getRowsFromTable(tableOpCambioPersona);
            return resultQuery;
        }
        // UPDATE
        /// <summary>
        /// Dada una credencial updatea las tabla de Credenciales en la fila que tenga el mismo numero de 
        /// legajo que el de la credencial.
        /// Si lo logra devuelve true.
        /// Si no devuelve false.
        /// </summary>
        /// <param name="aCredencial">Credencial de usuario llena de valores.</param>
        public bool updateCredencialByLegajo(Credencial aCredencial)
        {
            bool result = dataBaseUtils.ModificarRegistro(aCredencial.getRowString(), tableCredenciales, 0, aCredencial.Legajo);
            return result;
        }
        public void updatePersonaByLegajo(Persona aPersona)
        {
            updateRowTable(tablePersona, aPersona.Legajo, 0, aPersona.getRowString());
        }
        // ADD
        public void addAuto(Operacion op)
        {
            addRowToTable(tableAutorizacion, op.getRowAutoString());
        }
        public void addUsuarioBloqueado(Credencial aCredencial)
        {
            addRowToTable(tableUsuarioBloqueado, aCredencial.Legajo);
        }
        public void addLogInIntento(Credencial aCredencial)
        {
            addRowToTable(tableLogInIntentos, aCredencial.getLogInAttempRowString());
        }
        public void addOpPersona(Persona aPersona)
        {
            var idOpNew = 0;
            idOpNew = getBiggestNumber(tableOpCambioCredencial, 0, idOpNew);
            idOpNew = getBiggestNumber(tableOpCambioPersona, 0, idOpNew);
            idOpNew = getBiggestNumber(tableAutorizacion, 0, idOpNew);
            idOpNew++;
            var newOp = new OperacionCambioPersona(aPersona, idOpNew.ToString());
            addRowToTable(tableOpCambioPersona, newOp.getRowString());
        }
        public void addOpCredencial(Credencial aCredencial)
        {
            var idOpNew = 0;
            var idPerfil = getPerfilByLegajo(aCredencial.Legajo);
            idOpNew = getBiggestNumber(tableOpCambioCredencial, 0, idOpNew);
            idOpNew = getBiggestNumber(tableOpCambioPersona, 0, idOpNew);
            idOpNew = getBiggestNumber(tableAutorizacion, 0, idOpNew);
            idOpNew++;
            var newOp = new OperacionCambioCredencial(aCredencial, idOpNew.ToString(), idPerfil);
            addRowToTable(tableOpCambioCredencial, newOp.getRowString());
        }
        private int getBiggestNumber(string table, int indexNumberColumn, int previousNumber)
        {
            var response = previousNumber;
            var allRowsTable = getRowsFromTable(table);
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
        // DELETE
        public void unblockUser(Credencial theCredencial)
        {
            updateCredencialByLegajo(theCredencial);
            DeleteRowTable(tableLogInIntentos, theCredencial.Legajo, 0);
            DeleteRowTable(tableUsuarioBloqueado, theCredencial.Legajo, 0);
        }   
        public void deleteOpCredencialById(string id)
        {
            DeleteRowTable(tableOpCambioCredencial, id, 0);
        }
        public void deleteOpPersonaById(string id)
        {
            DeleteRowTable(tableOpCambioPersona, id, 0);
        }
    }
}
