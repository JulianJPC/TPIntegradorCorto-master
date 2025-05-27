using Datos;
using Persistencia;
using Persistencia.DataBase;
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
        public string buscarIdPerfil(string legajo)
        {
            var response = "";
            UsuarioPersistencia usuarioPersistencia = new UsuarioPersistencia();
            response = usuarioPersistencia.getPerfilByLegajo(legajo);
            return response;
        }
        public string buscarPerfil(string idPerfil)
        {
            var response = "";
            UsuarioPersistencia usuarioPersistencia = new UsuarioPersistencia();
            response = usuarioPersistencia.getPerfilByIdPerfil(idPerfil);
            return response;
        }
        public bool changePassPerfil(Credencial theCredencial)
        {
            UsuarioPersistencia usuarioPersistencia = new UsuarioPersistencia();
            return changePass(theCredencial, usuarioPersistencia);
        }
        public Credencial login(string usuario, string password)
        {
            UsuarioPersistencia usuarioPersistencia = new UsuarioPersistencia();
            var response = usuarioPersistencia.getCredencialByUsername(usuario);
            
            if(response != null)
            {
                response = verificationCredencial(response, password, usuarioPersistencia);
            }
            return response;   
        }
        private Credencial verificationCredencial(Credencial theCredencial, string password, UsuarioPersistencia usuarioP)
        {
            Credencial response = null;

            if (theCredencial.Contrasena.Equals(password))
            {
                var blocked = isBlocked(theCredencial, usuarioP);
                var expired = isExpired(theCredencial);
                var resultChange = true;
                if (!blocked && (expired || theCredencial.isFirstLogIn))
                {
                    resultChange = changePass(theCredencial, usuarioP);
                }
                if (!blocked && resultChange)
                {
                    theCredencial.FechaUltimoLogin = DateTime.Now;
                    usuarioP.updateCredencialByLegajo(theCredencial);
                    response = theCredencial;
                }
            }
            else
            {
                usuarioP.addLogInIntento(theCredencial);
                var attempts = usuarioP.getLogInAttempsByLegajo(theCredencial);
                if (attempts >= 3)
                {
                    usuarioP.addUsuarioBloqueado(theCredencial);
                }
            }
            return response;
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
        private bool isBlocked(Credencial theCredencial, UsuarioPersistencia usuarioP)
        {
            var response = false;
            
            var resultQuery = usuarioP.getUsuariosBloqueadosByLegajo(theCredencial);
            if (resultQuery.Count > 0)
            {
                response = true;
            }
            return response;
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
                usuarioP.updateCredencialByLegajo(credencial);
                response = true;
            }
            return response;
        }
    }
}
