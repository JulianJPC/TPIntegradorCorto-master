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
    public partial class FormVerOperaciones : Form
    {
        private Credencial credAdmin { get; set; }
        public bool closeWindow { get; set; }
        private AdministradorNegocio adminN { get; set; }
        public FormVerOperaciones(Credencial aCredencial)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Autorizaciones desbloqueos";
            credAdmin = aCredencial;
            adminN = new AdministradorNegocio();
            var idPendinetes = adminN.getAllAutoIdByEstado("Desbloqueo");
            foreach ( var id in idPendinetes)
            {
                cmbIdOperaciones.Items.Add(id);
            }
            cmbIdOperaciones.DropDownStyle = ComboBoxStyle.DropDownList;
            if (idPendinetes.Count == 0)
            {
                MessageBox.Show("No hay operaciones pendientes de desbloqueo de cuentas");
                closeWindow = true;
            }
            else
            {
                closeWindow = false;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Cuando cambia el legajo en el combo, busca en la base de datos
        /// la operacion realacionada con ese id y la devuleve.
        /// </summary>
        private void cmbIdOperaciones_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbIdOperaciones.SelectedItem is string)
            {
                var theIdOperacion = cmbIdOperaciones.SelectedItem as string;
                var oneOpRaw = adminN.getOperacion(theIdOperacion);
                if (oneOpRaw != null)
                {
                    var oneOp = (OperacionCambioCredencial)oneOpRaw;
                    txtbLegajo.Text = oneOp.Credencial.Legajo;
                    txtbUsuario.Text = oneOp.Credencial.NombreUsuario;
                    txtbPass.Text = oneOp.Credencial.Contrasena;
                    txtbIdPerfil.Text = oneOp.IdPerfil;
                    dtpFechaAlta.Value = oneOp.Credencial.FechaAlta;
                    dtpUltimoLogIn.Value = oneOp.Credencial.FechaUltimoLogin;
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRechazar_Click(object sender, EventArgs e)
        {
            if (cmbIdOperaciones.SelectedItem is string)
            {
                var theIdOperacion = cmbIdOperaciones.SelectedItem as string;
                var theOp = (OperacionCambioCredencial)adminN.getOperacion(theIdOperacion);
                theOp.getDataForAuto("Rechazado", credAdmin.Legajo);
                adminN.autoOperacion(theOp);
                MessageBox.Show("El desbloque fue rechazado");
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
                var theOp = (OperacionCambioCredencial)adminN.getOperacion(theIdOperacion);
                theOp.getDataForAuto("Aprobado", credAdmin.Legajo);
                adminN.autoOperacion(theOp);
                MessageBox.Show("El desbloque fue aprobado");
                this.Close();
            }
            else
            {
                MessageBox.Show("Error numero de legajo");
            }
        }
    }
}
