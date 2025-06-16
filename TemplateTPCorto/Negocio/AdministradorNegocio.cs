using Datos;
using Datos.Login;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class AdministradorNegocio
    {
        private UsuarioPersistencia usuarioPersistencia { get; set; }
        private OperacionesPersistencia operacionesPersistencia { get; set; }
        public AdministradorNegocio()
        {
            operacionesPersistencia = new OperacionesPersistencia();
            usuarioPersistencia = new UsuarioPersistencia();
        }
        /// <summary>
        /// Busca en al tabla de autorizaciones todas las autorizaciones de un estado dado.
        /// Saca de cada una solo el id y lo devuelve en lista.
        /// </summary>
        /// <param name="estado">Estado de autorizacion valido</param>
 
        public List<string> getAllAutoIdByEstado(string estado)
        {
            return operacionesPersistencia.getAllPendientes(estado);
        }
        /// <summary>
        /// Busca dado un id de operacion, en la tabla operaciones la info de la persona,
        /// y en la tabla autorizacion la info para completarla
        /// </summary>
        /// <param name="id">Id de operacion valido</param>
        public Operacion getOperacion(string id)
        {
            return operacionesPersistencia.getOperacionById(id);
        }
        
        /// <summary>
        /// Dada una operacion se fija que tipo de operacion es y si esta es aprobada o rechazada
        /// realiza el cambio si esta aprobada y modifica la autorizacion.
        /// Si esta rechazada solo modifica la autorizacion.
        /// </summary>
        /// <param name="operacion">Operacion de cambio de persona valida</param>
        public void autoOperacion(Operacion laOp)
        {
            if(laOp.TipoOperacion == "Desbloqueo")
            {
                var laOpCred = (OperacionCambioCredencial)laOp;
                if(laOp.Estado == "Aprobado")
                {
                    usuarioPersistencia.updateCredencialByLegajo(laOpCred.Credencial);
                }
                operacionesPersistencia.modAuto(laOpCred);
            }
            else if(laOp.TipoOperacion == "ModPersona")
            {
                var laOpPers = (OperacionCambioPersona)laOp;
                if(laOp.Estado == "Aprobado")
                {
                    usuarioPersistencia.updatePersonaByLegajo(laOpPers.Persona);
                }
                operacionesPersistencia.modAuto(laOpPers);
            }
        }
        /// <summary>
        /// Dada la credencial del administrador abre el Form para ver las operaciones de desbloqueo de credenciales
        /// </summary>
        /// <param name="aCredencial">Credencial de administrador</param>
        public void startFormVerOperaciones(Credencial aCredencial)
        {
            var formVerOp = new FormVerOperaciones(aCredencial);
            if (formVerOp.closeWindow)
            {
                formVerOp.Close();
            }
            else
            {
                formVerOp.ShowDialog();
            }
        }
        /// <summary>
        /// Dada la credencial del administrador abre el Form para ver las operaciones de cambio de persona
        /// </summary>
        /// <param name="aCredencial">Credencial de administrador</param>
        public void startFormVerOpPersona(Credencial aCredencial)
        {
            var formVerOp = new FormVerOpPersona(aCredencial);
            if (formVerOp.closeWindow)
            {
                formVerOp.Close();
            }
            else
            {
                formVerOp.ShowDialog();
            }
        }
    }
}
