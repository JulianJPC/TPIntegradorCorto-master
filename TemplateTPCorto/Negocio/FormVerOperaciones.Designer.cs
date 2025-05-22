namespace Negocio
{
    partial class FormVerOperaciones
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.IdOp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.legajo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idPerfil = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaAlta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaUltimoLogIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdOp,
            this.legajo,
            this.NombreUsuario,
            this.pass,
            this.idPerfil,
            this.FechaAlta,
            this.fechaUltimoLogIn});
            this.dataGridView1.Location = new System.Drawing.Point(23, 55);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1051, 358);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // IdOp
            // 
            this.IdOp.HeaderText = "IdOperacion";
            this.IdOp.MinimumWidth = 6;
            this.IdOp.Name = "IdOp";
            this.IdOp.Width = 125;
            // 
            // legajo
            // 
            this.legajo.HeaderText = "legajo";
            this.legajo.MinimumWidth = 6;
            this.legajo.Name = "legajo";
            this.legajo.Width = 125;
            // 
            // NombreUsuario
            // 
            this.NombreUsuario.HeaderText = "Usuario";
            this.NombreUsuario.MinimumWidth = 6;
            this.NombreUsuario.Name = "NombreUsuario";
            this.NombreUsuario.Width = 125;
            // 
            // pass
            // 
            this.pass.HeaderText = "Constraseña";
            this.pass.MinimumWidth = 6;
            this.pass.Name = "pass";
            this.pass.Width = 125;
            // 
            // idPerfil
            // 
            this.idPerfil.HeaderText = "idPerfil";
            this.idPerfil.MinimumWidth = 6;
            this.idPerfil.Name = "idPerfil";
            this.idPerfil.Width = 125;
            // 
            // FechaAlta
            // 
            this.FechaAlta.HeaderText = "Fecha Alta";
            this.FechaAlta.MinimumWidth = 6;
            this.FechaAlta.Name = "FechaAlta";
            this.FechaAlta.Width = 125;
            // 
            // fechaUltimoLogIn
            // 
            this.fechaUltimoLogIn.HeaderText = "fecha Ultimo Log In";
            this.fechaUltimoLogIn.MinimumWidth = 6;
            this.fechaUltimoLogIn.Name = "fechaUltimoLogIn";
            this.fechaUltimoLogIn.Width = 125;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(999, 419);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Volver";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FormVerOperaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1108, 454);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormVerOperaciones";
            this.Text = "FormVerOperaciones";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdOp;
        private System.Windows.Forms.DataGridViewTextBoxColumn legajo;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn pass;
        private System.Windows.Forms.DataGridViewTextBoxColumn idPerfil;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaAlta;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaUltimoLogIn;
        private System.Windows.Forms.Button btnClose;
    }
}