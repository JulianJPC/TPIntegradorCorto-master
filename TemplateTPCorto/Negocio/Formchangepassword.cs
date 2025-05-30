using Datos;
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
    public partial class Formchangepassword : Form
    {
        private string contraseñaAnterior { get; set; }
        public string contraseñaNueva { get; set; }
        



        public Formchangepassword(Credencial credencial)
        {
            InitializeComponent();
            this.Text = "Cambiar contraseña";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.contraseñaAnterior = credencial.Contrasena;
        }

        /// <summary>
        /// Si la contraseña escrita tiene mas de 8 caracteres y es diferente a la contraseña anterior
        /// guarda el valor de la contraseña y cierra el forms.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string contraseña = txtpassword.Text;
            if(contraseña.Length < 8)
            {
                MessageBox.Show("La contraseña debe tener al menos 8 caracteres.");
                return;
            }
            else if (contraseña == contraseñaAnterior)
            {
                MessageBox.Show("La nueva contraseña no puede ser igual a la anterior.");
                return;
            }
           
            else
            {
                this.contraseñaNueva = contraseña;
                MessageBox.Show("Contraseña cambiada con éxito.");
                this.Close();
            }
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.contraseñaNueva = null;
            this.Close();
        }
    }
}
