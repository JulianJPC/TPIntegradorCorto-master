using Datos;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class LoginNegocio
    {
        public Credencial login(String usuario, String password)
        {
            UsuarioPersistencia usuarioPersistencia = new UsuarioPersistencia();

            Credencial credencial = usuarioPersistencia.login(usuario);

            
            if(credencial == null)
            {
                return null;
            }
            else if (credencial.Contrasena.Equals(password))
            {
                var isBlocked = usuarioPersistencia.isBlockedTheUser(credencial);
                if (isBlocked)
                {
                    return null;
                }
                else
                {
                    bool expired = isExpired(credencial);
                    if (expired)
                    {
                        //var newFormPass = new FormChangePassword(credencial);

                    }
                    credencial.FechaUltimoLogin = DateTime.Now;
                    usuarioPersistencia.changeLastLogIn(credencial);
                    return credencial;
                }
            }
            else
            {
                var attempts = usuarioPersistencia.getLogInAttemps(credencial);
                if (attempts >= 3)
                {
                    usuarioPersistencia.addBlockedUser(credencial);
                }
                else
                {
                    usuarioPersistencia.addLogInAttemp(credencial);
                }
                return null;
            }
            
        }
        private bool isExpired(Credencial credencial)
        {
            var now = DateTime.Now;
            var fechaUltimoLogin = credencial.FechaUltimoLogin;
            
            var diasDesdeUltimoLogin = (now - fechaUltimoLogin).TotalDays;
            

            if (diasDesdeUltimoLogin >= 30 )
            {
                return true;
            }
            return false;
        }
    }
}
