using Datos;
using Datos.Login;
using Persistencia;
using Persistencia.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using TemplateTPCorto;

namespace Negocio
{
    public class LoginNegocio
    {
        private UsuarioPersistencia usuarioPersistencia { get; set; }
        public LoginNegocio()
        {
            usuarioPersistencia = new UsuarioPersistencia();
        }
        /// <summary>
        /// Obtiene una persona dada una credencial.
        /// Si no la encuentra devuelve null
        /// </summary>
        /// <param name="laCredencial">Credencial de usuario valido</param>
        public Persona buscarPersonaCompleta(Credencial laCredencial)
        {
            Persona response;
            response = usuarioPersistencia.getPersonaByLegajo(laCredencial.Legajo);
            if(response != null)// si la persona existe obtiene el perfil
            {
                var perfilDePersona = usuarioPersistencia.getPerfilByLegajo(response.Legajo);
                response.Perfil = perfilDePersona;
            }
            return response;
        }
        /// <summary>
        /// Remuevo el registro de intentos de log in en la BD de una credencial dada
        /// </summary>
        /// <param name="theCredencial">Credencial de usuario valido</param>
        public string removeAttempts(Credencial theCredencial)
        {
            var msgError = "Exito";
            var result = usuarioPersistencia.deleteAttemptsByLegajo(theCredencial);
            if (!result)
            {
                msgError = "Error al eliminar los intentos de la base de datos";
            }
            return msgError;
        }
        /// <summary>
        /// Metodo usado en el login, con el usuario y contraseña busca la credencial
        /// De encontrarla la devuelve.
        /// Si no la encuentra lo devuelve null.
        /// </summary>
        /// <param name="usuario">Nombre de usuario de la credencial</param>
        /// <param name="password">Contraseña de usuario de la credencial</param>
        public Credencial getCredencialLogIn(string usuario)
        {
            return usuarioPersistencia.getCredencialByUsername(usuario); 
        }
        /// <summary>
        /// Funcion que dado una credencial y una contraseña se fija si primero la contraseña dada es la de la credencial.
        /// Si no esta bloqueada, se fija si la contraseña es correcta
        /// despues se fija si esta expirada o es su primer log in
        /// de ser eso le pide que cambie la contraseña
        /// de tener exito al cambiar la contraseñá continua
        /// Actualiza la fecha de login da la credencial y la cambia en la base de datos.
        /// De no ser correcta la contraseña añade el intento en la base de datos.
        /// Obtiene todos los intentos de login de esa cuenta y se fija si son mayores o iguales a 3.
        /// De ser mayores o iguales a 3 bloquea la cuenta en la base de datos.
        /// Si no se encuentra con errores devuelve un stirng que dice "Exito".
        /// Si se encuentra con errores devuelve un string describiendo el error.
        /// </summary>
        /// <param name="theCredencial">Credencial sin verificar</param>
        /// <param name="password">Contraseña dada por el usuario sin verificar</param>
        public string verificationCredencial(Credencial theCredencial, string password)
        {
            var msgResult = "Exito";
            var blocked = isBlocked(theCredencial);

            if (blocked)
            {
                msgResult = "Usuario bloqueado";
            }
            else if (theCredencial.Contrasena.Equals(password))
            {
                var expired = isExpired(theCredencial);
                var resultChange = true;
                if(expired || theCredencial.isFirstLogIn)// expirado o primer log in
                {
                    resultChange = changePass(theCredencial);
                }
                else if (!resultChange)// fallo en cambiar contraseña
                {
                    msgResult = "Error al cambiar contraseña log in fallido";
                }

                if(resultChange)//no bloqueada y cambia de contraseña exitoso
                {
                    theCredencial.FechaUltimoLogin = DateTime.Now;
                    var resultUpdate = usuarioPersistencia.updateCredencialByLegajo(theCredencial);
                    if (!resultUpdate)// error al updatear
                    {
                        msgResult = "Error al actualizar fecha log in";
                    }
                }
            }
            else// contraseña incorrecta
            {
                usuarioPersistencia.addLogInIntento(theCredencial);
                var attempts = usuarioPersistencia.getLogInAttempsByLegajo(theCredencial);
                msgResult = $"Contraseña equivocado intento {attempts.ToString()}";
                if (attempts >= 3)
                {
                    usuarioPersistencia.addUsuarioBloqueado(theCredencial);
                    msgResult = "La cuenta se bloqueo hubo 3 intentos fallidos o mas";
                }
            }
            return msgResult;
        }
        /// <summary>
        /// Dada la credencial se fija si el ultimo log in esta dentro de los 
        /// ultimos 29 dias.
        /// Si esta dentro de los 29 días devuelve true.
        /// Si esta entre 30 o mas devuelve false.
        /// </summary>
        /// <param name="credencial">Credencial de usuario del login</param>
        /// <returns></returns>
        private bool isExpired(Credencial credencial)
        {
            var response = false;
            var now = DateTime.Now;
            var fechaUltimoLogin = credencial.FechaUltimoLogin;
            var diasDesdeUltimoLogin = (now - fechaUltimoLogin).TotalDays;//dias entre hoy y el ultimo login
            if (diasDesdeUltimoLogin >= 30 )
            {
                response = true;
            }
            return response;
        }
        /// <summary>
        /// Dada la credencial del login se fija si esta bloqueada.
        /// Se fija en la basa de datos si figura en la tabla de bloqueados
        /// Si esta bloqueado devuleve true.
        /// Si no esta bloqueado devuelve false.
        /// </summary>
        /// <param name="theCredencial">Credencial del usuario del login</param>
        /// <returns></returns>
        private bool isBlocked(Credencial theCredencial)
        {
            var response = false;
            var resultQuery = usuarioPersistencia.getUsuariosBloqueadosByLegajo(theCredencial);
            if (resultQuery.Count > 0)
            {
                response = true;
            }
            return response;
        }
        /// <summary>
        /// Funcion que dada una credencial abre el forms de cambio de contraseña con esta.
        /// Si obtiene una contraseña nueva lo cambia en la credencial y  updatea la base de datos.
        /// Si lo logra cambiar con exito devuelve true.
        /// Si falla devuleve false.
        /// </summary>
        /// <param name="credencial"></param>
        /// <returns></returns>
        public bool changePass(Credencial credencial)
        {
            var response = false;
            var newFormPass = new Formchangepassword(credencial);
            newFormPass.ShowDialog();
            var newPass = newFormPass.contraseñaNueva;
            if (newPass != null)
            {
                credencial.Contrasena = newPass;
                response = usuarioPersistencia.updateCredencialByLegajo(credencial);// resultado del update
            }
            return response;
        }
    }
}
