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
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            var usuario = txtUsuario.Text;
            var password = txtPassword.Text;

            var loginNegocio = new LoginNegocio();
            var credencial = loginNegocio.login(usuario, password);
            if(credencial != null)
            {
                MessageBox.Show("Exito logIn!!");
                var idPerfil = loginNegocio.buscarIdPerfil(credencial.Legajo);

                if(idPerfil == "2")
                {
                    FormPerfilSupervisor newPerfilS = new FormPerfilSupervisor(credencial);
                    newPerfilS.FormClosed += (s, args) => Application.Exit();
                    newPerfilS.Show();
                    this.Hide();
                }
                else if (idPerfil == "3")
                {
                    FormPerfilAdministrador newPerfilA = new FormPerfilAdministrador(credencial);
                    newPerfilA.FormClosed += (s, args) => Application.Exit();
                    newPerfilA.Show();
                    this.Hide();
                }
                else
                {
                    FormPerfil thePerfil = new FormPerfil(credencial);
                    thePerfil.FormClosed += (s, args) => Application.Exit();
                    thePerfil.Show();
                    this.Hide();
                }
            }
            else if(credencial == null)
            {
                MessageBox.Show("Está mal usuario o contraseña");
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
