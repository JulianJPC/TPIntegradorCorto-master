using Datos;
using Datos.Login;
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
        private Persona supervisor { get; set; }
        public FormDesbloquearPass(List<string> legajos, Persona unSupervisor)
        {
            InitializeComponent();
            this.Text = "Solicitar desbloqueos";
            supNegocio = new SupervisorNegocio();
            supervisor = unSupervisor;
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

        /// <summary>
        /// Si el numero de legajo es valido, toma la informacion del formulario y al supervisor
        /// y crea la operacion y autorizacion correspondiente.
        /// </summary>
        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            if (cmbLegajos.SelectedItem is string)
            {
                var legajo = cmbLegajos.SelectedItem as string;
                var newPass = txtbPass.Text;
                supNegocio.createCredOp(legajo, newPass, supervisor);
                MessageBox.Show("El desbloqueo fue enviado a autorizar.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Número de legajo no valido.");
            }
                
        }
        /// <summary>
        /// Cuando el combo cambia busca en la base de datos la credencial asociada a ese legajo
        /// y da los valores de esa credencial al formulario.
        /// </summary>
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
                    MessageBox.Show("Error al traer la credencial");
                }
            }
            else
            {
                MessageBox.Show("Error numero de legajo");
            }
        }
    }
}
