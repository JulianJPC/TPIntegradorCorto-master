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
    public partial class FormChangePersona : Form
    {
        public FormChangePersona(List<string> legajos)
        {
            InitializeComponent();
            foreach(string oneLegajo in legajos)
            {
                cmbLegajos.Items.Add(oneLegajo);
            }
            cmbLegajos.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cmbLegajos_DropDownClosed(object sender, EventArgs e)
        {
            SupervisorNegocio supNegocio = new SupervisorNegocio();
            if(cmbLegajos.SelectedItem is string)
            {
                var theLegajo = cmbLegajos.SelectedItem as string;
                Persona onePersona = supNegocio.getPersona(theLegajo);
                if (onePersona != null)
                {
                    txtbNombre.Text = onePersona.Nombre;
                    txtbApellido.Text = onePersona.Apellido;
                    txtbDNI.Text = onePersona.DNI;
                    dtpFechaIngreso.Value = onePersona.FechaIngreso;
                }
                else
                {
                    MessageBox.Show("Error numero de legajo");
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var legajo = cmbLegajos.SelectedItem as string;
            var nombre = txtbNombre.Text;
            var apellido = txtbApellido.Text;
            var dni = txtbDNI.Text;
            var fechaI = dtpFechaIngreso.Value;
            SupervisorNegocio supNegocio = new SupervisorNegocio();
            supNegocio.createPersonaOp(legajo, nombre, apellido, dni, fechaI);
            this.Close();
        }
    }
}
