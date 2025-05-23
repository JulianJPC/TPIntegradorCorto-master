﻿using Datos;
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
        public FormDesbloquearPass(List<string> legajos)
        {
            InitializeComponent();
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
            SupervisorNegocio supNegocio = new SupervisorNegocio();
            supNegocio.changePassAndLogIn(legajo, newPass);
            this.Close();
        }

        private void cmbLegajos_DropDownClosed(object sender, EventArgs e)
        {
            SupervisorNegocio supNegocio = new SupervisorNegocio();
            if (cmbLegajos.SelectedItem is string)
            {
                var theLegajo = cmbLegajos.SelectedItem as string;
                Credencial oneCredencial = supNegocio.getCredencial(theLegajo);
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
