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
            credAdmin = aCredencial;
            adminN = new AdministradorNegocio();
            var idOperaciones = adminN.getAllIdOpCredenciales();
            foreach ( var op in idOperaciones)
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void cmbIdOperaciones_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbIdOperaciones.SelectedItem is string)
            {
                var theIdOperacion = cmbIdOperaciones.SelectedItem as string;
                OperacionCambioCredencial oneOp = adminN.getOperacionCredencial(theIdOperacion);
                if (oneOp != null)
                {
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
        }

        private void btnRechazar_Click(object sender, EventArgs e)
        {
            if (cmbIdOperaciones.SelectedItem is string)
            {
                var theIdOperacion = cmbIdOperaciones.SelectedItem as string;
                var theOp = adminN.getOperacionCredencial(theIdOperacion);
                theOp.getDataFromCred(theOp.Credencial, "Rechazado", credAdmin.Legajo);
                adminN.autoCredencial(theOp, false);
                adminN.deleteOpCredencial(theIdOperacion);
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
                var theOp = adminN.getOperacionCredencial(theIdOperacion);
                theOp.getDataFromCred(theOp.Credencial, "Aprobado", credAdmin.Legajo);
                adminN.autoCredencial(theOp, true);
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
