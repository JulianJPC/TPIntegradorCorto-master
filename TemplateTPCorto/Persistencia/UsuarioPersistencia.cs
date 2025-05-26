using Datos;
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
        // UPDATE
        public void updateCredencialByLegajo(Credencial aCredencial)
        {
            updateRowTable(tableCredenciales, aCredencial.Legajo, 0, aCredencial.getRowString());
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
        // DELETE



        public void unblockUser(Credencial theCredencial)
        {
            updateCredencialByLegajo(theCredencial);
            DeleteRowTable(tableLogInIntentos, theCredencial.Legajo, 0);
            DeleteRowTable(tableUsuarioBloqueado, theCredencial.Legajo, 0);
        }   
        public List<string> getAllOpCredenciales()
        {
            DataBaseUtils dataBaseUtils = new DataBaseUtils();
            var response = dataBaseUtils.getAllRegistro("operacion_cambio_credencial.csv");
            return response;
        }
        public List<string> getAllOpPersonas()
        {
            DataBaseUtils dataBaseUtils = new DataBaseUtils();
            var response = dataBaseUtils.getAllRegistro("operacion_cambio_persona.csv");
            return response;
        }
    }
}
