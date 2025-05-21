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
            this.contraseñaAnterior = credencial.Contrasena;
            

        }


        private void Formchangepassword_Load(object sender, EventArgs e)
        {

        }

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

        internal void Show()
        {
            throw new NotImplementedException();
        }
    }
}
