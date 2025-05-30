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
        /// <summary>
        /// Obtiene de la table de intentos de log in el numero de veces
        /// que se repite un legajo dado de la credencial que se da
        /// </summary>
        /// <param name="theCredencial">Credencial de usuario valida</param>
        /// <returns></returns>
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
        /// <summary>
        /// Dado un legajo obtiene las personas asociadas en la tabla de personas
        /// y devuelve la primera.
        /// </summary>
        /// <param name="legajo">Legajo de persona valida</param>
        public Persona getPersonaByLegajo(string legajo)
        {
            Persona aPersona = null;
            var rawResponse = dataBaseUtils.BuscarRegistro(legajo, 0, tablePersona);
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

        private List<string> getValueFromResponse(List<string> rawResponse, int indexOfResponse)
        {
            var response = new List<string>();
            if(rawResponse.Count > 0)
            {
                response = rawResponse[indexOfResponse].Split(';').ToList();
            }
            return response;
        }
        /// <summary>
        /// Dado un numero de legajo busca el perfil asoaciado 
        /// y devuelve la fila de la tabla perfiles de ese legajo.
        /// Si no encuentra devuelve una lista vacia.
        /// </summary>
        /// <param name="legajo">Numero de legajo asociado a una persona</param>
        private List<string> getPerfilListRow(string legajo)
        {
            var response = new List<string>();
            var responseQuery = dataBaseUtils.BuscarRegistro(legajo, 0, tableUsuarioPerfil);
            var perfilNumber = getValueFromResponse(responseQuery, 0);//obtengo la fila de usuario-perfil
            if (perfilNumber.Count > 0)//Si existe la fila de usuario-perfil, busco la fila 
            {
                var rawPerfiles = dataBaseUtils.BuscarRegistro(perfilNumber[1], 0, tablePerfil);//la fila de perfil
                response = getValueFromResponse(rawPerfiles, 0);
            }
            return response;
        }
        /// <summary>
        /// Dado un perfil busca los roles de este y los inlculye en el Perfil.
        /// Si no encuentra da de valor una lista vacia de roles al perfil.
        /// </summary>
        /// <param name="unPerfil"></param>
        private void getRolesPerfil(Perfil unPerfil)
        {
            unPerfil.Roles = new List<Rol>();
            var responseSecondQuery = dataBaseUtils.BuscarRegistro(unPerfil.Id, 0, tablePerfilRol);
            for (int i = 0; i < responseSecondQuery.Count; i++)// por cada fila raw de perfil-rol
            {
                var rowPerfilRol = getValueFromResponse(responseSecondQuery, i);
                var responseThirdQuery = dataBaseUtils.BuscarRegistro(rowPerfilRol[1], 0, tableRol);
                var rowRol = getValueFromResponse(responseThirdQuery, 0);
                if (rowRol.Count > 0)
                {
                    var obtainedRol = new Rol(responseThirdQuery[0]);
                    unPerfil.Roles.Add(obtainedRol);
                }
            }
        }
        /// <summary>
        /// Dado un legajo busca el perfil asociado a este y sus roles
        /// Si no lo encuentra devuelve null
        /// </summary>
        /// <param name="legajo">Legajo asociado a persona</param>
        public Perfil getPerfilByLegajo(string legajo)
        {
            Perfil obtainedPerfil = null;
            var perfilString = getPerfilListRow(legajo);
            if (perfilString.Count > 0)//si obtuve un perfil
            {
                obtainedPerfil = new Perfil(perfilString);
                getRolesPerfil(obtainedPerfil);
            }
            return obtainedPerfil;
        }

        /// <summary>
        /// Obtiene de la tabla credenciales todos los legajos que encuentra y
        /// devuelve en un lista
        /// Si no encuentra devuleve una lista vacia.
        /// </summary>
        public List<string> getAllLegajosFromCredenciales()
        {
            var response = new List<string>();
            var rawResult = dataBaseUtils.getAllRegistro(tableCredenciales);
            
            foreach (string oneLine in rawResult)
            {
                var splitedLine = oneLine.Split(';');
                if (splitedLine.Count() > 0)
                {
                    response.Add(splitedLine[0]);
                }
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
        
        public void addOpCredencial(Credencial aCredencial)
        {
            var idOpNew = 0;
            var idPerfil = getPerfilByLegajo(aCredencial.Legajo);
            idOpNew = getBiggestNumber(tableOpCambioCredencial, 0, idOpNew);
            idOpNew = getBiggestNumber(tableOpCambioPersona, 0, idOpNew);
            idOpNew = getBiggestNumber(tableAutorizacion, 0, idOpNew);
            idOpNew++;
            var newOp = new OperacionCambioCredencial(aCredencial, idOpNew.ToString(), idPerfil.Id);
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
        /// <summary>
        /// Elimina de la table de log in intentos todos los intentos de un legajo determinado
        /// por la credencial dada.
        /// </summary>
        /// <param name="theCredencial">Credencial de usuario válida</param>
        public bool deleteAttemptsByLegajo(Credencial theCredencial)
        {
            var result = dataBaseUtils.BorrarRegistro(tableLogInIntentos, 0, theCredencial.Legajo);
            return result;
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
