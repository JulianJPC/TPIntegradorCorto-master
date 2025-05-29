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
    public partial class FormPerfiles : Form
    {
        private Credencial laCredencial { get; set; }
        public FormPerfiles(Credencial unaCredencial)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            laCredencial = unaCredencial;
            var loginN = new LoginNegocio();
            var idPerfil = loginN.buscarIdPerfil(laCredencial.Legajo);
            var descPerfil = loginN.buscarPerfil(idPerfil);
            lblUsuario.Text = laCredencial.NombreUsuario;
            lblPerfilNombre.Text = descPerfil;
            if(idPerfil == "1")
            {
                btnOperador.Enabled = true;
            }
            else if(idPerfil == "2")
            {
                btnDesbCredencial.Enabled = true;
                btnModPersona.Enabled = true;
            }
            else if(idPerfil == "3")
            {
                btnAutoPersona.Enabled = true;
                btnAutoCred.Enabled = true;
            }
        }

        private void btnModPersona_Click(object sender, EventArgs e)
        {
            var supNegocio = new SupervisorNegocio();
            supNegocio.startFormChangePersona();
        }

        private void btnDesbCredencial_Click(object sender, EventArgs e)
        {
            var supNegocio = new SupervisorNegocio();
            supNegocio.startFormDesCredencial();
        }

        private void btnCambiarPass_Click(object sender, EventArgs e)
        {
            var loginNegocio = new LoginNegocio();
            loginNegocio.changePassPerfil(laCredencial);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAutoPersona_Click(object sender, EventArgs e)
        {
            var adminNegocio = new AdministradorNegocio();
            adminNegocio.startFormVerOpPersona(laCredencial);
        }

        private void btnAutoCred_Click(object sender, EventArgs e)
        {
            var adminNegocio = new AdministradorNegocio();
            adminNegocio.startFormVerOperaciones(laCredencial);
        }
    }
}
