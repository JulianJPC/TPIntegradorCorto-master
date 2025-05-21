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
    public partial class FormPerfil : Form
    {
        private Credencial laCredencial{get; set;}

        public FormPerfil(Credencial unaCredencial)
        {
            InitializeComponent();
            laCredencial = unaCredencial;
            lblNombre.Text = laCredencial.NombreUsuario;
        }

        private void FormPerfil_Load(object sender, EventArgs e)
        {

        }

        private void btn_changePass_Click(object sender, EventArgs e)
        {
            LoginNegocio loginNegocio = new LoginNegocio();
            var resultChange = loginNegocio.changePassPerfil(laCredencial);
        }
    }
}
