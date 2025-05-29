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

namespace Negocio
{
    public partial class FormDesbloquearPass : Form
    {
        private SupervisorNegocio supNegocio { get; set; }
        public FormDesbloquearPass(List<string> legajos)
        {
            InitializeComponent();
            supNegocio = new SupervisorNegocio();
            this.StartPosition = FormStartPosition.CenterScreen;
            foreach (string oneLegajo in legajos)
            {
                cmbLegajos.Items.Add(oneLegajo);
            }
            cmbLegajos.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            var legajo = cmbLegajos.SelectedItem as string;
            var newPass = txtbPass.Text;
            supNegocio.createCredOp(legajo, newPass);
            MessageBox.Show("El desbloqueo fue enviado a autorizar.");
            this.Close();
        }

        private void cmbLegajos_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbLegajos.SelectedItem is string)
            {
                var theLegajo = cmbLegajos.SelectedItem as string;
                var oneCredencial = supNegocio.getCredencial(theLegajo);
                if (oneCredencial != null)
                {
                    txtbUsuario.Text = oneCredencial.NombreUsuario;
                    txtbPass.Text = oneCredencial.Contrasena;
                }
                else
                {
                    MessageBox.Show("Error numero de legajo");
                }
            }
        }
    }
}
