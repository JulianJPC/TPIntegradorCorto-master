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
        private List<string> getAllLegajos()
        {
            var response = new List<string>();
            var usuarioPersistencia = new UsuarioPersistencia();
            response = usuarioPersistencia.getAllLegajosFromCredenciales();
            return response;
        }
        public void desbloquearPersona()
        {
            var allLegajos = getAllLegajos();
            var formDesbloquearPersona = new FormDesbloquearPass(allLegajos);
            formDesbloquearPersona.ShowDialog();
        }
        public void startFormChangePersona()
        {
            var allLegajos = getAllLegajos();
            FormChangePersona formChangePersona = new FormChangePersona(allLegajos);
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
                    usuarioPersistencia.updateCredencialByLegajo(modCredencial);
                }
            }
        }
        public Credencial getCredencial(string legajo)
        {
            Credencial response = null;
            var usuarioPersistencia = new UsuarioPersistencia();
            response = usuarioPersistencia.getCredencialByLegajo(legajo);
            return response;
        }
        public void changePassAndLogIn(string legajo, string newPass)
        {
            var usuarioPersistencia = new UsuarioPersistencia();
            var theCredencial = usuarioPersistencia.getCredencialByLegajo(legajo);
            theCredencial.Contrasena = newPass;
            if (theCredencial.passValueTest())
            {
               usuarioPersistencia.unblockUser(theCredencial);
            }
        }

    }
}
