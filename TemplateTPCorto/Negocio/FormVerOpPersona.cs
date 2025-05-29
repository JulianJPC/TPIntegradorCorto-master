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
    public partial class FormVerOpPersona : Form
    {
        private Credencial credAdmin { get; set; }
        public bool closeWindow { get; set; }
        private AdministradorNegocio adminN { get; set; }
        public FormVerOpPersona(Credencial aCredencial)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            credAdmin = aCredencial;
            adminN = new AdministradorNegocio();
            var idOperaciones = adminN.getAllIdOpPersonas();
            foreach (var op in idOperaciones)
            {
                cmbIdOperaciones.Items.Add(op.ToString());
            }
            cmbIdOperaciones.DropDownStyle = ComboBoxStyle.DropDownList;
            if (idOperaciones.Count == 0)
            {
                MessageBox.Show("No hay Operaciones");
                closeWindow = true;
            }
            else
            {
                closeWindow = false;
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbIdOperaciones_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbIdOperaciones.SelectedItem is string)
            {
                var theIdOperacion = cmbIdOperaciones.SelectedItem as string;
                OperacionCambioPersona oneOp = adminN.getOperacionPersona(theIdOperacion);
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
            if (cmbIdOperaciones.SelectedItem is string)
            {
                var theIdOperacion = cmbIdOperaciones.SelectedItem as string;
                var theOp = adminN.getOperacionPersona(theIdOperacion);
                theOp.getDataFromPers(theOp.Persona, "Rechazado", credAdmin.Legajo);
                adminN.autoPersona(theOp, false);
                adminN.deleteOpPersona(theIdOperacion);
                MessageBox.Show("La modificación fue rechazada");
                this.Close();
            }
            else
            {
                MessageBox.Show("Error numero de legajo");
            }
        }

        private void btnAutorizar_Click(object sender, EventArgs e)
        {
            if (cmbIdOperaciones.SelectedItem is string)
            {
                var theIdOperacion = cmbIdOperaciones.SelectedItem as string;
                var theOp = adminN.getOperacionPersona(theIdOperacion);
                theOp.getDataFromPers(theOp.Persona, "Aprobado", credAdmin.Legajo);
                adminN.autoPersona(theOp, true);
                MessageBox.Show("La modificación fue aprobada");
                this.Close();
            }
            else
            {
                MessageBox.Show("Error numero de legajo");
            }
        }
    }
}
