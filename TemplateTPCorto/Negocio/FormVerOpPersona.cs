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
    public partial class FormVerOpPersona : Form
    {
        public FormVerOpPersona()
        {
            InitializeComponent();
            AdministradorNegocio adminN = new AdministradorNegocio();
            var idOperaciones = adminN.getAllIdOpCredenciales();
            foreach (var op in idOperaciones)
            {
                cmbIdOperaciones.Items.Add(op.ToString());
            }
            cmbIdOperaciones.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbIdOperaciones_DropDownClosed(object sender, EventArgs e)
        {
            var admNegocio = new AdministradorNegocio();
            if (cmbIdOperaciones.SelectedItem is string)
            {
                var theIdOperacion = cmbIdOperaciones.SelectedItem as string;
                OperacionCambioPersona oneOp = admNegocio.getOperacionPersona(theIdOperacion);
                if (oneOp != null)
                {
                    txtbLegajo.Text = oneOp.Persona.Legajo;
                    txtbNombre.Text = oneOp.Persona.Nombre;
                    txtbApellido.Text = oneOp.Persona.Apellido;
                    txtbDNI.Text = oneOp.Persona.DNI;
                    dtpFechaIngreso.Value = oneOp.Persona.FechaIngreso;
                }
                else
                {
                    MessageBox.Show("Error numero de legajo");
                }
            }
        }

        private void btnRechazar_Click(object sender, EventArgs e)
        {
            var adminNegocio = new AdministradorNegocio();
            if (cmbIdOperaciones.SelectedItem is string)
            {
                var theIdOperacion = cmbIdOperaciones.SelectedItem as string;
                adminNegocio.deleteOpPersona(theIdOperacion);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error numero de legajo");
            }
        }

        private void btnAutorizar_Click(object sender, EventArgs e)
        {
            var adminNegocio = new AdministradorNegocio();
            if (cmbIdOperaciones.SelectedItem is string)
            {
                var theIdOperacion = cmbIdOperaciones.SelectedItem as string;
                var theOp = adminNegocio.getOperacionPersona(theIdOperacion);

                adminNegocio.autoPersona(theOp);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error numero de legajo");
            }
        }
    }
}
