using Datos;
using Persistencia.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public class UsuarioPersistencia
    {
        public List<string> getAllLegajos()
        {
            DataBaseUtils dataBaseUtils = new DataBaseUtils();
            var response = new List<string>();
            var rawResult = dataBaseUtils.getAllRegistro("credenciales.csv");
            foreach(string oneLine in rawResult)
            {
                var splitedLine = oneLine.Split(';');
                response.Add(splitedLine[0]);
            }
            return response;
        }
        public int getPerfilFromLegajo(string legajo)
        {
            DataBaseUtils dataBaseUtils = new DataBaseUtils();
            var response = 0;
            List<String> registros = dataBaseUtils.BuscarRegistro(legajo, 0, "usuario_perfil.csv");
            if (registros.Count > 0)
            {
                var idPerfilString = registros[0].Split(';')[1];
                response = Int32.Parse(idPerfilString);
            }
            return response;
        }
        public Credencial login(String username)
        {
            DataBaseUtils dataBaseUtils = new DataBaseUtils();
            List<String> registros = dataBaseUtils.BuscarRegistro(username, 1, "credenciales.csv");
            Credencial credencial;
            if(registros.Count > 0)
            {
                credencial = new Credencial(registros[0]);
            }
            else
            {
                credencial = null;
            }
            return credencial;
        }
        public void updateCredencial(Credencial theCredencial)
        {
            DataBaseUtils dataBaseUtils = new DataBaseUtils();
            dataBaseUtils.ModificarRegistro(theCredencial.getRowString(), "credenciales.csv", 0, theCredencial.Legajo);
        }
        public Credencial getUserFromLegajo(string legajo)
        {
            DataBaseUtils dataBaseUtils = new DataBaseUtils();
            List<String> registros = dataBaseUtils.BuscarRegistro(legajo, 0, "credenciales.csv");
            Credencial credencial;
            if (registros.Count > 0)
            {
                credencial = new Credencial(registros[0]);
            }
            else
            {
                credencial = null;
            }
            return credencial;
        }
        public void unblockUser(Credencial theCredencial)
        {
            DataBaseUtils dataBaseUtils = new DataBaseUtils();
            dataBaseUtils.ModificarRegistro(theCredencial.getUnblockedRowString(), "credenciales.csv", 0, theCredencial.Legajo);
            dataBaseUtils.BorrarRegistro("login_intentos.csv", 0, theCredencial.Legajo);
            dataBaseUtils.BorrarRegistro("usuario_bloqueado.csv", 0, theCredencial.Legajo);
        }
        public void changeLastLogIn(Credencial theCredencial)
        {
            DataBaseUtils dataBaseUtils = new DataBaseUtils();
            dataBaseUtils.ModificarRegistro(theCredencial.getRowString(), "credenciales.csv",  0, theCredencial.Legajo);
        }
        public void addLogInAttemp(Credencial theCredencial)
        {
            DataBaseUtils dataBaseUtils = new DataBaseUtils();
            dataBaseUtils.AgregarRegistro("login_intentos.csv", String.Join(";", theCredencial.Legajo, DateTime.Now.ToString("d/M/yyyy")));
        }
        public int getLogInAttemps(Credencial theCredencial)
        {
            DataBaseUtils dataBaseUtils = new DataBaseUtils();
            var resultAttemps = dataBaseUtils.BuscarRegistro(theCredencial.Legajo, 0, "login_intentos.csv");
            return resultAttemps.Count;
        }
        public void addBlockedUser(Credencial theCredencial)
        {
            DataBaseUtils dataBaseUtils = new DataBaseUtils();
            dataBaseUtils.AgregarRegistro("usuario_bloqueado.csv", theCredencial.Legajo);
        }
        public bool isBlockedTheUser(Credencial theCredencial)
        {
            var response = false;
            DataBaseUtils dataBaseUtils = new DataBaseUtils();
            var resultQuery = dataBaseUtils.BuscarRegistro(theCredencial.Legajo, 0, "usuario_bloqueado.csv");
            if(resultQuery.Count > 0)
            {
                response = true;
            }
            return response;
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
