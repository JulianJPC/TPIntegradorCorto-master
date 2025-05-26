using Datos;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TemplateTPCorto
{
    public partial class FormPerfilSupervisor : Form
    {
        private Credencial laCredencial { get; set; }
        public FormPerfilSupervisor(Credencial unaCredencial)
        {
            InitializeComponent();
            laCredencial = unaCredencial;
            lblNombre.Text = laCredencial.NombreUsuario;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnModPersona_Click(object sender, EventArgs e)
        {
            SupervisorNegocio supNegocio = new SupervisorNegocio();
            supNegocio.startFormChangePersona();
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            SupervisorNegocio supNegocio = new SupervisorNegocio();
            supNegocio.desbloquearPersona();
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            LoginNegocio loginNegocio = new LoginNegocio();
            var resultChange = loginNegocio.changePassPerfil(laCredencial);
        }
    }
}
