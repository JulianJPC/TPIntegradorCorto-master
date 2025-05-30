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

        //                                                                               GET

        /// <summary>
        /// Obtiene de la table de intentos de log in el numero de veces
        /// que se repite un legajo dado de la credencial que se da
        /// </summary>
        /// <param name="theCredencial">Credencial de usuario valida</param>
        /// <returns></returns>
        public int getLogInAttempsByLegajo(Credencial theCredencial)
        {
            var resultQuery = dataBaseUtils.BuscarRegistro(theCredencial.Legajo, 0, tableLogInIntentos);
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
        /// <summary>
        /// Dado un numero de legajo busca en la table de credenciales las filas con ese mismo
        /// numero de legajo y arma una lista. De esa lista obtiene la primera y crea una credencial
        /// y la devuelve. Si no encunetra filas que matchen devuelve null
        /// </summary>
        /// <param name="legajo">Numero de legajo valido</param>
        public Credencial getCredencialByLegajo(string legajo)
        {
            Credencial aCredencial = null;
            var rawResponse = dataBaseUtils.BuscarRegistro(legajo, 0, tableCredenciales);
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
        //                                                                               UPDATE

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
            dataBaseUtils.ModificarRegistro(aPersona.getRowString(), tablePersona, 0, aPersona.Legajo);
        }
        //                                                                               ADD
        public void addUsuarioBloqueado(Credencial aCredencial)
        {
            dataBaseUtils.AgregarRegistro(tableUsuarioBloqueado, aCredencial.Legajo);
        }
        public void addLogInIntento(Credencial aCredencial)
        {
            dataBaseUtils.AgregarRegistro(tableLogInIntentos, aCredencial.getLogInAttempRowString());
        }
        //                                                           DELETE

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
        //                                                              AUX
        private List<string> getValueFromResponse(List<string> rawResponse, int indexOfResponse)
        {
            var response = new List<string>();
            if (rawResponse.Count > 0)
            {
                response = rawResponse[indexOfResponse].Split(';').ToList();
            }
            return response;
        }
    }
}
