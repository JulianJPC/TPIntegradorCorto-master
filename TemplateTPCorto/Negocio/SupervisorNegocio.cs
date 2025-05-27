using Datos;
using Datos.Login;
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
        public void startFormDesCredencial()
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
        
        public void createPersonaOp(string legajos, string nombre, string apellido, string DNI, DateTime fechaIngreso)
        {
            var response = true;
            UsuarioPersistencia usuarioPersistencia = new UsuarioPersistencia();
            if (legajos == null || legajos == "")
            {
                response = false;
            }
            if(nombre == null || nombre == "")
            {
                response = false;
            }
            if(apellido == null || apellido == "")
            {
                response = false;
            }
            if (DNI == null || DNI == "")
            {
                response = false;
            }
            if (fechaIngreso == null)
            {
                response = false;
            }
            if (response)
            {
                Persona modPersona = new Persona(legajos, nombre, apellido, DNI, fechaIngreso);
                if (modPersona.passValueTest())
                {
                    usuarioPersistencia.addOpPersona(modPersona);
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
        public Persona getPersona(string legajo)
        {
            Persona response = null;
            var usuarioPersistencia = new UsuarioPersistencia();
            response = usuarioPersistencia.getPersonaByLegajo(legajo);
            return response;
        }
        public void createCredOp(string legajo, string newPass)
        {
            var usuarioPersistencia = new UsuarioPersistencia();
            var theCredencial = usuarioPersistencia.getCredencialByLegajo(legajo);
            theCredencial.Contrasena = newPass;
            if (theCredencial.passValueTest())
            {
               usuarioPersistencia.addOpCredencial(theCredencial);
            }
        }

    }
}
