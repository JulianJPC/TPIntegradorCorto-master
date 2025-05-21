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
    public partial class FormLogin : Form
    {
        private int logInAttemps { get; set; }
        public FormLogin()
        {
            InitializeComponent();
            MessageBox.Show("Ingresar usuario y contraseña");
            logInAttemps = 0;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            String usuario = txtUsuario.Text;
            String password = txtPassword.Text;

            LoginNegocio loginNegocio = new LoginNegocio();
            Credencial credencial = loginNegocio.login(usuario, password);
            if(credencial != null)
            {
                MessageBox.Show("Exito logIn!!");
            }
            else if(credencial == null)
            {
                MessageBox.Show("Está mal usuario o contraseña");
            }
        }
    }
}
