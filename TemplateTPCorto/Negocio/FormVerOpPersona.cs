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
            var rawOperaciones = adminN.getAllOpPersonas();
            foreach (var op in rawOperaciones)
            {
                var listValues = op.Split(';').ToList();
                dataGridView1.Rows.Add(listValues[0], listValues[1], listValues[2], listValues[3], listValues[4], listValues[5]);
            }
            dataGridView1.ReadOnly = true;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
