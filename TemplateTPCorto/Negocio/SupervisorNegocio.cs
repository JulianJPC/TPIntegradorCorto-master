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
        /// <summary>
        /// Obtiene todos los legajos de la BD y abre el formDesbloquearPass
        /// </summary>
        /// <param name="supervisor">Pesrona que pide el cambio</param>
        public void startFormDesCredencial(Persona supervisor)
        {
            var allLegajos = getAllLegajos();
            var formDesbloquearPersona = new FormDesbloquearPass(allLegajos, supervisor);
            formDesbloquearPersona.ShowDialog();
        }
        /// <summary>
        /// Obtiene todos los legajos de la BD y abre el formChangePersona con esos legajos
        /// </summary>
        /// <param name="supervisor">Pesrona que pide el cambio</param>
        public void startFormChangePersona(Persona supervisor)
        {
            var allLegajos = getAllLegajos();
            var formChangePersona = new FormChangePersona(allLegajos, supervisor);
            formChangePersona.ShowDialog();
        }
        /// <summary>
        /// Dada toda la info del formulario a cambiar de la persona y el supervisor que lo pide,
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
        public string createPersonaOp(string legajo, string nombre, string apellido, string DNI, DateTime fechaIngreso, Persona supervisor)
        {
            var response = "";
            var itsOkValues = true;
            if (legajo == null || legajo == "" || legajo.Contains(';'))
            {
                itsOkValues = false;
            }
            if (nombre == null || nombre == "" || nombre.Contains(';'))
            {
                itsOkValues = false;
            }
            if (apellido == null || apellido == "" || apellido.Contains(';'))
            {
                itsOkValues = false;
            }
            if (DNI == null || DNI == "" || DNI.Contains(';'))
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
                    response = "Valores no permitidos en campos o tienen ';'";
                }
            }
            else
            {
                response = "Valores nulos o vacios";
            }
            return response;
        }

        /// <summary>
        /// Obtiene la credencial asociado a un legajo
        /// </summary>
        /// <param name="legajo">Numero de legajo valido</param>
        public Credencial getCredencial(string legajo)
        {
            return usuarioPersistencia.getCredencialByLegajo(legajo);
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
        /// <summary>
        /// Dado un numero de legajo, una contraseña nueva y una persona supervisor,
        /// Buscal a credencial asociada al legajo y le cambia la contraseña,
        /// se fija si lo valores the la credencial son validos,
        /// si son validos añade la operacion en la base de datos y la autorización
        /// </summary>
        /// <param name="legajo">Número de legajo valido</param>
        /// <param name="newPass">Nueva contraseña de la credencial</param>
        /// <param name="supervisor">Persona que solicita el cambio</param>
        public void createCredOp(string legajo, string newPass, Persona supervisor)
        {
         
            var theCredencial = usuarioPersistencia.getCredencialByLegajo(legajo);
            var perfil = usuarioPersistencia.getPerfilByLegajo(theCredencial.Legajo);
            theCredencial.Contrasena = newPass;
            if (theCredencial.passValueTest())
            {
               operacionesPersistencia.addOpCredencial(theCredencial, perfil.Id, supervisor);
            }
        }

    }
}
