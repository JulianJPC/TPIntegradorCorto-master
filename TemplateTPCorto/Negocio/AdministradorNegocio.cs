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
        public List<string> getAllIdOpPersonas()
        {
            var response = new List<string>();
            UsuarioPersistencia usuarioPersistencia = new UsuarioPersistencia();
            response = usuarioPersistencia.getAllIdOpPersonas();
            return response;
        }
        public List<string> getAllIdOpCredenciales()
        {
            var response = new List<string>();
            UsuarioPersistencia usuarioPersistencia = new UsuarioPersistencia();
            response = usuarioPersistencia.getAllIdOpCredenciales();
            return response;
        }
        public OperacionCambioCredencial getOperacionCredencial(string id)
        {
            OperacionCambioCredencial opCambio;
            UsuarioPersistencia usuarioP = new UsuarioPersistencia();
            opCambio = usuarioP.getOperacionCByIdOp(id);
            return opCambio;
        }
        public OperacionCambioPersona getOperacionPersona(string id)
        {
            OperacionCambioPersona opCambio;
            UsuarioPersistencia usuarioP = new UsuarioPersistencia();
            opCambio = usuarioP.getOperacionPByIdOp(id);
            return opCambio;
        }
        public void deleteOpCredencial(string id)
        {
            UsuarioPersistencia usuarioP = new UsuarioPersistencia();
            usuarioP.deleteOpCredencialById(id);
        }
        public void deleteOpPersona(string id)
        {
            UsuarioPersistencia usuarioP = new UsuarioPersistencia();
            usuarioP.deleteOpPersonaById(id);
        }
        public void autoCredencial(OperacionCambioCredencial operacion, bool aproved)
        {
            UsuarioPersistencia usuarioP = new UsuarioPersistencia();
            if (aproved)
            {
                usuarioP.updateCredencialByLegajo(operacion.Credencial);
            }
            usuarioP.deleteOpCredencialById(operacion.IdOperacion);
            usuarioP.addAuto(operacion);
        }
        public void autoPersona(OperacionCambioPersona operacion, bool aproved)
        {
            UsuarioPersistencia usuarioP = new UsuarioPersistencia();
            if (aproved)
            {
                usuarioP.updatePersonaByLegajo(operacion.Persona);
            }
            usuarioP.deleteOpPersonaById(operacion.IdOperacion);
            usuarioP.addAuto(operacion);
        }
        public List<string> getAllOpPersonas()
        {
            var response = new List<string>();
            UsuarioPersistencia usuarioPersistencia = new UsuarioPersistencia();
            response = usuarioPersistencia.getAllOpPersonas();
            return response;
        }
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
