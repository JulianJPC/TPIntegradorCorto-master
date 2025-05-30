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
        private UsuarioPersistencia usuarioPersistencia { get; set; }
        private OperacionesPersistencia operacionesPersistencia { get; set; }
        public SupervisorNegocio()
        {
            usuarioPersistencia = new UsuarioPersistencia();
            operacionesPersistencia = new OperacionesPersistencia();

        }
        /// <summary>
        /// Obtiene todos los legajos y los devuelve en lista
        /// </summary>
        private List<string> getAllLegajos()
        {
            return usuarioPersistencia.getAllLegajosFromCredenciales();
        }
        public void startFormDesCredencial()
        {
            var allLegajos = getAllLegajos();
            var formDesbloquearPersona = new FormDesbloquearPass(allLegajos);
            formDesbloquearPersona.ShowDialog();
        }
        /// <summary>
        /// Obtiene todos los legajos de la BD y abre el formChangePersona con esos legajos
        /// </summary>
        public void startFormChangePersona(Persona supervisor)
        {
            var allLegajos = getAllLegajos();
            var formChangePersona = new FormChangePersona(allLegajos, supervisor);
            formChangePersona.ShowDialog();
        }
        /// <summary>
        /// Dado toda la info del formulario a cambiar de la persona y el supervisor que lo pide,
        /// primero se fija si lo valores son válidos,
        /// Si lo valores son validos crea un objeto Persona con esos valores nuevos
        /// Se fija en la persona si son valores aceptables,
        /// Si lo son inserta en las tablas de operacion cambio persona y operaciones los cambios
        /// </summary>
        /// <param name="legajo">Numero de legajo de la persona a modificar</param>
        /// <param name="nombre">Nombre modificado</param>
        /// <param name="apellido">Aperllido modificado</param>
        /// <param name="DNI">DNI modificado</param>
        /// <param name="fechaIngreso">Fecha de ingreso modificado</param>
        /// <param name="supervisor">Persona supervisor</param>
        /// <returns></returns>
        public string createPersonaOp(string legajo, string nombre, string apellido, string DNI, DateTime fechaIngreso, Persona supervisor)
        {
            var response = "";
            var itsOkValues = true;
            if (legajo == null || legajo == "")
            {
                itsOkValues = false;
            }
            if(nombre == null || nombre == "")
            {
                itsOkValues = false;
            }
            if(apellido == null || apellido == "")
            {
                itsOkValues = false;
            }
            if (DNI == null || DNI == "")
            {
                itsOkValues = false;
            }
            if (fechaIngreso == null)
            {
                itsOkValues = false;
            }
            if (itsOkValues)
            {
                Persona modPersona = new Persona(legajo, nombre, apellido, DNI, fechaIngreso);
                if (modPersona.passValueTest())
                {
                    operacionesPersistencia.addOpPersona(modPersona, supervisor);
                    response = "El cambio fue enviado a autorizar.";
                }
                else
                {
                    response = "Valores no permitidos en campos";
                }
            }
            else
            {
                response = "Valores nulos o vacios";
            }
            return response;
        }
        public Credencial getCredencial(string legajo)
        {
            Credencial response = null;
            var usuarioPersistencia = new UsuarioPersistencia();
            response = usuarioPersistencia.getCredencialByLegajo(legajo);
            return response;
        }
        /// <summary>
        /// Dado un legajo busca la persona asociada a esta y lo devuelve
        /// Si no lo encuentra devuelve null
        /// </summary>
        /// <param name="legajo">Numero de legajo asociado a una persona</param>
        public Persona getPersona(string legajo)
        {   
            return usuarioPersistencia.getPersonaByLegajo(legajo);
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
