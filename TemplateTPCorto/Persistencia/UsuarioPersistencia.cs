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
    }
}
