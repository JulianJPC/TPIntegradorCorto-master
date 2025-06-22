using Datos;
using Datos.Login;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TemplateTPCorto
{
    public partial class FormPerfiles : Form
    {
        private Credencial laCredencial { get; set; }
        private Persona laPersona { get; set; }
        private SupervisorNegocio supervisorNegocio { get; set; }
        private AdministradorNegocio administradorNegocio { get; set; }
        private VentasNegocio ventasNegocio { get; set; }
        public FormPerfiles(Credencial unaCredencial, Persona unaPersona)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            laCredencial = unaCredencial;
            laPersona = unaPersona;
            supervisorNegocio = new SupervisorNegocio();
            administradorNegocio = new AdministradorNegocio();
            ventasNegocio = new VentasNegocio();
            lblUsuario.Text = laCredencial.NombreUsuario;
            lblPerfilNombre.Text = laPersona.Perfil.Descripcion;
            this.Text = "Acciones";

            btnCambiarPass.Enabled = true;
            btnCambiarPass.Visible = true;
            foreach (var oneRol in laPersona.Perfil.Roles)//Activa opciones dependiendo del rol
            {
                if(oneRol.Id == "1")
                {
                    btnModPersona.Enabled = true;
                    btnModPersona.Visible = true;
                }
                else if(oneRol.Id == "2")
                {
                    btnAutoPersona.Enabled = true;
                    btnAutoPersona.Visible = true;
                }
                else if(oneRol.Id == "3")
                {
                    btnDesbCredencial.Enabled = true;
                    btnDesbCredencial.Visible = true;
                }
                else if(oneRol.Id == "4")
                {
                    btnAutoCred.Enabled = true;
                    btnAutoCred.Visible = true;
                }
                else if( oneRol.Id == "5")
                {
                    btnCargarVenta.Enabled = true;
                    btnCargarVenta.Visible = true;
                }
            }
        }

        private void btnModPersona_Click(object sender, EventArgs e)
        {
            supervisorNegocio.startFormChangePersona(laPersona);
        }

        private void btnDesbCredencial_Click(object sender, EventArgs e)
        {
            supervisorNegocio.startFormDesCredencial(laPersona);
        }

        private void btnCambiarPass_Click(object sender, EventArgs e)
        {
            var loginNegocio = new LoginNegocio();
            loginNegocio.changePass(laCredencial);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAutoPersona_Click(object sender, EventArgs e)
        {
            administradorNegocio.startFormVerOpPersona(laCredencial);
        }

        private void btnAutoCred_Click(object sender, EventArgs e)
        {
            administradorNegocio.startFormVerOperaciones(laCredencial);
        }

        private void btnCargarVenta_Click(object sender, EventArgs e)
        {
            ventasNegocio.startFormVentas(laCredencial);
        }
    }
}
