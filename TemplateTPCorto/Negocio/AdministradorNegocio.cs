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
        public void autoCredencial(OperacionCambioCredencial operacion)
        {
            UsuarioPersistencia usuarioP = new UsuarioPersistencia();
            usuarioP.updateCredencialByLegajo(operacion.Credencial);
            usuarioP.deleteOpCredencialById(operacion.IdOperacion);
        }
        public void autoPersona(OperacionCambioPersona operacion)
        {
            UsuarioPersistencia usuarioP = new UsuarioPersistencia();
            usuarioP.updatePersonaByLegajo(operacion.Persona);
            usuarioP.deleteOpPersonaById(operacion.IdOperacion);
        }
        public List<string> getAllOpPersonas()
        {
            var response = new List<string>();
            UsuarioPersistencia usuarioPersistencia = new UsuarioPersistencia();
            response = usuarioPersistencia.getAllOpPersonas();
            return response;
        }
        public void startFormVerOperaciones()
        {
            var formVerOp = new FormVerOperaciones();
            formVerOp.ShowDialog();
        }
        public void startFormVerOpPersona()
        {
            var formVerOp = new FormVerOpPersona();
            formVerOp.ShowDialog();
        }
    }
}
