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
    public partial class FormPerfilAdministrador : Form
    {
        private Credencial laCredencial { get; set; }
        public FormPerfilAdministrador(Credencial unaCredencial)
        {
            InitializeComponent();
            laCredencial = unaCredencial;
            lblName.Text = laCredencial.NombreUsuario;
        }

        private void FormPerfilAdministrador_Load(object sender, EventArgs e)
        {

        }

        private void btnVerCredencialOp_Click(object sender, EventArgs e)
        {
            FormVerOperaciones formVerO = new FormVerOperaciones();
            formVerO.ShowDialog();
        }

        private void btnVerPersonaCambio_Click(object sender, EventArgs e)
        {
            FormVerOpPersona formVerO = new FormVerOpPersona();
            formVerO.ShowDialog();
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            LoginNegocio loginNegocio = new LoginNegocio();
            var resultChange = loginNegocio.changePassPerfil(laCredencial);
        }
    }
}
