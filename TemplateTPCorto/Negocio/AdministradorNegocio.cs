using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class AdministradorNegocio
    {
        public List<string> getAllOpCredenciales()
        {
            var response = new List<string>();
            UsuarioPersistencia usuarioPersistencia = new UsuarioPersistencia();
            response = usuarioPersistencia.getAllOpCredenciales();
            return response;
        }
        public List<string> getAllOpPersonas()
        {
            var response = new List<string>();
            UsuarioPersistencia usuarioPersistencia = new UsuarioPersistencia();
            response = usuarioPersistencia.getAllOpPersonas();
            return response;
        }
    }
}
