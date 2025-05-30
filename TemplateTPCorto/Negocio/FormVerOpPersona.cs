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
            this.Text = "Autorizaciones Personas";
            credAdmin = aCredencial;
            adminN = new AdministradorNegocio();
            var idPendinetes = adminN.getAllAutoIdByEstado("ModPersona");
            foreach (var id in idPendinetes)
            {   
                cmbIdOperaciones.Items.Add(id);
            }
            cmbIdOperaciones.DropDownStyle = ComboBoxStyle.DropDownList;
            if (idPendinetes.Count == 0)
            {
                MessageBox.Show("No hay operaciones pendientes de cambio de persona");
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
        /// <summary>
        /// Cuand cambia el combo busca la operacion en la base de datos y
        /// llena el formulario con la info que obtiene.
        /// </summary>
        private void cmbIdOperaciones_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbIdOperaciones.SelectedItem is string)
            {
                var theIdOperacion = cmbIdOperaciones.SelectedItem as string;
                var oneOpRaw = adminN.getOperacion(theIdOperacion);
                if (oneOpRaw != null)
                {
                    var oneOp = (OperacionCambioPersona)oneOpRaw;
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
            else
            {
                MessageBox.Show("Error numero de legajo");
            }
        }
        /// <summary>
        /// Si se rechaza la operacion, busca la operacion del id
        /// obtiene la info del admin y modifica la tabla de autorizaciones para rechazar
        /// </summary>
        private void btnRechazar_Click(object sender, EventArgs e)
        {
            if (cmbIdOperaciones.SelectedItem is string)
            {
                var theIdOperacion = cmbIdOperaciones.SelectedItem as string;
                var theOp = (OperacionCambioPersona)adminN.getOperacion(theIdOperacion);
                theOp.getDataForAuto("Rechazado", credAdmin.Legajo);
                adminN.autoOperacion(theOp);
                MessageBox.Show("La modificación fue rechazada");
                this.Close();
            }
            else
            {
                MessageBox.Show("Error numero de legajo");
            }
        }
        /// <summary>
        /// Si se autoriza los cambios a la persona se busca la operacion se la llena de la informacion
        /// de la persona y el legajo del administrador y se modifica la persona y actualiza
        /// la autorizacion
        /// </summary>
        private void btnAutorizar_Click(object sender, EventArgs e)
        {
            if (cmbIdOperaciones.SelectedItem is string)
            {
                var theIdOperacion = cmbIdOperaciones.SelectedItem as string;
                var theOp = (OperacionCambioPersona)adminN.getOperacion(theIdOperacion);
                theOp.getDataForAuto("Aprobado", credAdmin.Legajo);
                adminN.autoOperacion(theOp);
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
