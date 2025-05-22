using Datos;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateTPCorto;

namespace Negocio
{
    public class LoginNegocio
    {
        public int buscarIdPerfil(string legajo)
        {
            var response = 0;
            UsuarioPersistencia usuarioPersistencia = new UsuarioPersistencia();
            response = usuarioPersistencia.getPerfilFromLegajo(legajo);
            return response;
        }
        public bool changePassPerfil(Credencial theCredencial)
        {
            UsuarioPersistencia usuarioPersistencia = new UsuarioPersistencia();
            return changePass(theCredencial, usuarioPersistencia);
        }
        private bool changePass(Credencial credencial, UsuarioPersistencia usuarioP)
        {
            var response = false;
            var newFormPass = new Formchangepassword(credencial);
            newFormPass.ShowDialog();
            var newPass = newFormPass.contraseñaNueva;
            if (newPass != null)
            { 
                credencial.Contrasena = newPass;
                usuarioP.changeLastLogIn(credencial);
                response = true;
            }
            return response;
        }
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
                    if (expired || credencial.isFirstLogIn)
                    {
                        
                        var resultChange = changePass(credencial, usuarioPersistencia);
                        if(!resultChange)
                        {
                            return null;
                        }
                        
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
