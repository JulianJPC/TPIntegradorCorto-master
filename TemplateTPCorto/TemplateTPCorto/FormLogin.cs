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
            this.Text = "LogIn";
        }
        /// <summary>
        /// Funcion correspondiente al boton ingresar, hace el login
        /// </summary>
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            // Obtiene la credencial a través del usuario
            var usuario = txtUsuario.Text;
            var password = txtPassword.Text;
            var credencialDeUsuario = loginNegocio.getCredencialLogIn(usuario);
            var MsgError = "";
            if(credencialDeUsuario == null)
            {
                MsgError = "Error el Usuario no existe.";
            }
            else
            {
                //Si el usuario corresponde a una cuenta verifica si la contraseña es correcta
                MsgError = loginNegocio.verificationCredencial(credencialDeUsuario, password);
            }
            if(MsgError == "Exito")
            {
                MsgError = loginNegocio.removeAttempts(credencialDeUsuario);//remueve intentos en la BD
            }

            if(MsgError == "Exito")//si pasa la verificacion y elimina los intentos de log in
            {
                MessageBox.Show("Exito logIn!!");

                var persona = loginNegocio.buscarPersonaCompleta(credencialDeUsuario);
                if(persona != null)
                {
                    FormPerfiles newFormPerfil = new FormPerfiles(credencialDeUsuario, persona);
                    this.Hide();
                    newFormPerfil.ShowDialog();
                    // cuando se cierra el formPerfil muestra este form
                    txtPassword.Text = "";
                    txtUsuario.Text = "";
                    this.Show();
                }
                else
                {
                    MsgError = "Error al traer la persona de la BD";
                }
            }
            else
            {
                MessageBox.Show(MsgError);
            }
        }
    }
}
