using Datos;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class SupervisorNegocio
    {
        public List<string> getAllLegajos()
        {
            var response = new List<string>();
            UsuarioPersistencia usuarioPersistencia = new UsuarioPersistencia();
            response = usuarioPersistencia.getAllLegajos();
            return response;
        }
        public void desbloquearPersona(List<string> legajos)
        {
            FormDesbloquearPass formDesbloquearPersona = new FormDesbloquearPass(legajos);
            formDesbloquearPersona.ShowDialog();
        }
        public void changePersona(List<string> legajos)
        {
            FormChangePersona formChangePersona = new FormChangePersona(legajos);
            formChangePersona.ShowDialog();
        }
        public void changePersona(string legajos, string user, string pass, DateTime fechaAlta, DateTime fechaLastLogIn)
        {
            var response = true;
            UsuarioPersistencia usuarioPersistencia = new UsuarioPersistencia();
            if (legajos == null || legajos == "")
            {
                response = false;
            }
            if(user == null || user == "")
            {
                response = false;
            }
            if(pass == null || pass == "")
            {
                response = false;
            }
            if(fechaAlta == null)
            {
                response = false;
            }
            if(fechaLastLogIn == null)
            {
                response = false;
            }
            if (response)
            {
                Credencial modCredencial = new Credencial(legajos, user, pass, fechaAlta, fechaLastLogIn);
                if (modCredencial.passValueTest())
                {
                    usuarioPersistencia.updateCredencial(modCredencial);
                }
            }
        }
        public Credencial getCredencial(string legajo)
        {
            Credencial response = null;
            UsuarioPersistencia usuarioPersistencia = new UsuarioPersistencia();
            response = usuarioPersistencia.getUserFromLegajo(legajo);
            return response;
        }
        public void changePassAndLogIn(string legajo, string newPass)
        {
            UsuarioPersistencia usuarioPersistencia = new UsuarioPersistencia();
            Credencial theCredencial = usuarioPersistencia.getUserFromLegajo(legajo);
            theCredencial.Contrasena = newPass;
            if (theCredencial.passValueTest())
            {
               usuarioPersistencia.unblockUser(theCredencial);
            }
        }

    }
}
