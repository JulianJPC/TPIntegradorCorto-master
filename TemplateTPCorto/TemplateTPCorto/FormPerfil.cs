using Datos;
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
    public partial class FormPerfil : Form
    {
        private Credencial laCredencial{get; set;}

        public FormPerfil(Credencial unaCredencial)
        {
            InitializeComponent();
            laCredencial = unaCredencial;
            lblNombre.Text = laCredencial.NombreUsuario;
        }
    }
}
