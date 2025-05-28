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
        public FormVerOperaciones()
        {
            InitializeComponent();
            AdministradorNegocio adminN = new AdministradorNegocio();
            var idOperaciones = adminN.getAllIdOpCredenciales();
            foreach ( var op in idOperaciones)
            {
                cmbIdOperaciones.Items.Add(op.ToString());
            }
            cmbIdOperaciones.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void cmbIdOperaciones_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbIdOperaciones_DropDownClosed(object sender, EventArgs e)
        {
            var admNegocio = new AdministradorNegocio();
            if (cmbIdOperaciones.SelectedItem is string)
            {
                var theIdOperacion = cmbIdOperaciones.SelectedItem as string;
                OperacionCambioCredencial oneOp = admNegocio.getOperacionCredencial(theIdOperacion);
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
            var adminNegocio = new AdministradorNegocio();
            if (cmbIdOperaciones.SelectedItem is string)
            {
                var theIdOperacion = cmbIdOperaciones.SelectedItem as string;
                adminNegocio.deleteOpCredencial(theIdOperacion);
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
                var theOp = adminNegocio.getOperacionCredencial(theIdOperacion);

                adminNegocio.autoCredencial(theOp);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error numero de legajo");
            }
        }
    }
}
