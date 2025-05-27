using Datos;
using Datos.Login;
using Persistencia.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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

        public UsuarioPersistencia()
        {
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
        private List<string> getRowsFromTable(string nameTable, string valueOfColumn, int indexOfColumn)
        {
            var response = new List<string>();
            var dataBaseUtils = new DataBaseUtils();
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
            DataBaseUtils dataBaseUtils = new DataBaseUtils();
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
        public List<string> getUsuariosBloqueadosByLegajo(Credencial aCredencial)
        {
            return getRowsFromTable(tableUsuarioBloqueado, aCredencial.Legajo, 0);
        }
        public Credencial getCredencialByUsername(string username)
        {
            Credencial aCredencial = null;
            var rawResponse = getRowsFromTable(tableCredenciales, username, 1);
            if (rawResponse.Count > 0)
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
            var rawResponse = getRowsFromTable(tableOpCambioCredencial, idOp, 0);
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
                response.Add(splitedLine[0]);
            }
            return response;
        }
        public List<string> getAllOpPersonas()
        {
            var resultQuery = getRowsFromTable(tableOpCambioPersona);
            return resultQuery;
        }
        // UPDATE
        public void updateCredencialByLegajo(Credencial aCredencial)
        {
            updateRowTable(tableCredenciales, aCredencial.Legajo, 0, aCredencial.getRowString());
        }
        public void updatePersonaByLegajo(Persona aPersona)
        {
            updateRowTable(tableCredenciales, aPersona.Legajo, 0, aPersona.getRowString());
        }
        // ADD
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
            var allOperaciones = getRowsFromTable(tableOpCambioPersona);
            var idOpNew = 0;
            
            foreach(var row in allOperaciones)
            {
                var idOp = row.Split(';')[0];
                var idOpInt = Int32.Parse(idOp);
                if(idOpInt > idOpNew)
                {
                    idOpNew = idOpInt;
                }
            }
            idOpNew++;
            var newOp = new OperacionCambioPersona(aPersona, idOpNew.ToString());
            addRowToTable(tableOpCambioPersona, newOp.getRowString());
        }
        public void addOpCredencial(Credencial aCredencial)
        {
            var allOperaciones = getRowsFromTable(tableOpCambioCredencial);
            var idOpNew = 0;
            var idPerfil = getPerfilByLegajo(aCredencial.Legajo);
            foreach (var row in allOperaciones)
            {
                var idOp = row.Split(';')[0];
                var idOpInt = Int32.Parse(idOp);
                if (idOpInt > idOpNew)
                {
                    idOpNew = idOpInt;
                }
            }
            idOpNew++;
            var newOp = new OperacionCambioCredencial(aCredencial, idOpNew.ToString(), idPerfil);
            addRowToTable(tableOpCambioCredencial, newOp.getRowString());
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
