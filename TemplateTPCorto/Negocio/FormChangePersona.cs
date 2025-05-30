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
        private SupervisorNegocio supNegocio { get; set; }
        private Persona supervisor {  get; set; }
        public FormChangePersona(List<string> legajos, Persona unSupervisor)
        {
            InitializeComponent();
            this.Text = "Solicitar cambio persona";
            supNegocio = new SupervisorNegocio();
            this.StartPosition = FormStartPosition.CenterScreen;
            foreach (string oneLegajo in legajos)
            {
                cmbLegajos.Items.Add(oneLegajo);
            }
            cmbLegajos.DropDownStyle = ComboBoxStyle.DropDownList;
            this.supervisor = unSupervisor;
        }
        /// <summary>
        /// Cuando cambia el combo, busca en la BD la persona asociada
        /// al nuevo legajo seleccionado y llena el formulario con esa info.
        /// Si no lo encuentra muestra error.
        /// </summary>
        private void cmbLegajos_DropDownClosed(object sender, EventArgs e)
        {
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
        /// <summary>
        /// Cierra el formulario
        /// </summary>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Si el combo esta correcto, toma toda la informacion de los demas boxes
        /// y crea la operacion y autorizacion en las tablas
        /// </summary>
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if(cmbLegajos.SelectedItem is string)
            {
                var legajo = cmbLegajos.SelectedItem as string;
                var nombre = txtbNombre.Text;
                var apellido = txtbApellido.Text;
                var dni = txtbDNI.Text;
                var fechaI = dtpFechaIngreso.Value;
                var messageResult = supNegocio.createPersonaOp(legajo, nombre, apellido, dni, fechaI, supervisor);
                MessageBox.Show(messageResult);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error legajo invalido.");
            }
            
        }
    }
}
