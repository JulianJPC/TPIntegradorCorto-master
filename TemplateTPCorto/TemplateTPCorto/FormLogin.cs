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
        private LoginNegocio loginNegocio { get; set; }
        public FormLogin()
        {
            InitializeComponent();
            loginNegocio = new LoginNegocio();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        /// <summary>
        /// Funcion correspondiente al boton ingresar, hace el login
        /// </summary>
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            // Obtiene la credencial a través del usuario
            var usuario = txtUsuario.Text;
            var password = txtPassword.Text;
            var credencialSinVerif = loginNegocio.getCredencialLogIn(usuario);
            Credencial credencialVerif;
            var MsgError = "";
            if(credencialSinVerif == null)
            {
                MsgError = "Error el Usuario no existe.";
            }
            else
            {
                //Si el usuario corresponde a una cuenta verifica si la contraseña es correcta
                MsgError = loginNegocio.verificationCredencial(credencialSinVerif, password);
            }
            if(MsgError == "Exito")//si pasa la verificacion
            {
                credencialVerif = credencialSinVerif;
                MessageBox.Show("Exito logIn!!");
                FormPerfiles newFormPerfil = new FormPerfiles(credencialVerif);
                this.Hide();
                newFormPerfil.ShowDialog();
                // cuando se cierra el formPerfil muestra este form
                txtPassword.Text = "";
                txtUsuario.Text = "";
                this.Show();
            }
            else
            {
                MessageBox.Show(MsgError);
            }
        }
    }
}
