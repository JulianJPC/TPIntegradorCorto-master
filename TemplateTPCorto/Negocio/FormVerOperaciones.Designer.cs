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
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbIdOperaciones = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtbLegajo = new System.Windows.Forms.TextBox();
            this.txtbUsuario = new System.Windows.Forms.TextBox();
            this.txtbPass = new System.Windows.Forms.TextBox();
            this.txtbIdPerfil = new System.Windows.Forms.TextBox();
            this.dtpFechaAlta = new System.Windows.Forms.DateTimePicker();
            this.dtpUltimoLogIn = new System.Windows.Forms.DateTimePicker();
            this.btnRechazar = new System.Windows.Forms.Button();
            this.btnAutorizar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(309, 338);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Volver";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Id Operacion";
            // 
            // cmbIdOperaciones
            // 
            this.cmbIdOperaciones.FormattingEnabled = true;
            this.cmbIdOperaciones.Location = new System.Drawing.Point(141, 38);
            this.cmbIdOperaciones.Name = "cmbIdOperaciones";
            this.cmbIdOperaciones.Size = new System.Drawing.Size(243, 24);
            this.cmbIdOperaciones.TabIndex = 3;
            this.cmbIdOperaciones.DropDownClosed += new System.EventHandler(this.cmbIdOperaciones_DropDownClosed);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(85, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Legajo";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(81, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Usuario";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(58, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Contraseña";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(81, 179);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "idPerfil";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(63, 210);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 16);
            this.label6.TabIndex = 8;
            this.label6.Text = "Fecha Alta";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 247);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 16);
            this.label7.TabIndex = 9;
            this.label7.Text = "Fecha ultimo logIn";
            // 
            // txtbLegajo
            // 
            this.txtbLegajo.Enabled = false;
            this.txtbLegajo.Location = new System.Drawing.Point(141, 83);
            this.txtbLegajo.Name = "txtbLegajo";
            this.txtbLegajo.Size = new System.Drawing.Size(243, 22);
            this.txtbLegajo.TabIndex = 10;
            // 
            // txtbUsuario
            // 
            this.txtbUsuario.Enabled = false;
            this.txtbUsuario.Location = new System.Drawing.Point(141, 112);
            this.txtbUsuario.Name = "txtbUsuario";
            this.txtbUsuario.Size = new System.Drawing.Size(243, 22);
            this.txtbUsuario.TabIndex = 11;
            // 
            // txtbPass
            // 
            this.txtbPass.Enabled = false;
            this.txtbPass.Location = new System.Drawing.Point(141, 144);
            this.txtbPass.Name = "txtbPass";
            this.txtbPass.Size = new System.Drawing.Size(243, 22);
            this.txtbPass.TabIndex = 12;
            // 
            // txtbIdPerfil
            // 
            this.txtbIdPerfil.Enabled = false;
            this.txtbIdPerfil.Location = new System.Drawing.Point(141, 172);
            this.txtbIdPerfil.Name = "txtbIdPerfil";
            this.txtbIdPerfil.Size = new System.Drawing.Size(243, 22);
            this.txtbIdPerfil.TabIndex = 13;
            // 
            // dtpFechaAlta
            // 
            this.dtpFechaAlta.Enabled = false;
            this.dtpFechaAlta.Location = new System.Drawing.Point(141, 203);
            this.dtpFechaAlta.Name = "dtpFechaAlta";
            this.dtpFechaAlta.Size = new System.Drawing.Size(243, 22);
            this.dtpFechaAlta.TabIndex = 14;
            // 
            // dtpUltimoLogIn
            // 
            this.dtpUltimoLogIn.Enabled = false;
            this.dtpUltimoLogIn.Location = new System.Drawing.Point(141, 247);
            this.dtpUltimoLogIn.Name = "dtpUltimoLogIn";
            this.dtpUltimoLogIn.Size = new System.Drawing.Size(243, 22);
            this.dtpUltimoLogIn.TabIndex = 15;
            // 
            // btnRechazar
            // 
            this.btnRechazar.Location = new System.Drawing.Point(297, 296);
            this.btnRechazar.Name = "btnRechazar";
            this.btnRechazar.Size = new System.Drawing.Size(86, 23);
            this.btnRechazar.TabIndex = 16;
            this.btnRechazar.Text = "Rechazar";
            this.btnRechazar.UseVisualStyleBackColor = true;
            this.btnRechazar.Click += new System.EventHandler(this.btnRechazar_Click);
            // 
            // btnAutorizar
            // 
            this.btnAutorizar.Location = new System.Drawing.Point(205, 296);
            this.btnAutorizar.Name = "btnAutorizar";
            this.btnAutorizar.Size = new System.Drawing.Size(75, 23);
            this.btnAutorizar.TabIndex = 17;
            this.btnAutorizar.Text = "Autorizar";
            this.btnAutorizar.UseVisualStyleBackColor = true;
            this.btnAutorizar.Click += new System.EventHandler(this.btnAutorizar_Click);
            // 
            // FormVerOperaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 380);
            this.Controls.Add(this.btnAutorizar);
            this.Controls.Add(this.btnRechazar);
            this.Controls.Add(this.dtpUltimoLogIn);
            this.Controls.Add(this.dtpFechaAlta);
            this.Controls.Add(this.txtbIdPerfil);
            this.Controls.Add(this.txtbPass);
            this.Controls.Add(this.txtbUsuario);
            this.Controls.Add(this.txtbLegajo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbIdOperaciones);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Name = "FormVerOperaciones";
            this.Text = "FormVerOperaciones";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbIdOperaciones;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtbLegajo;
        private System.Windows.Forms.TextBox txtbUsuario;
        private System.Windows.Forms.TextBox txtbPass;
        private System.Windows.Forms.TextBox txtbIdPerfil;
        private System.Windows.Forms.DateTimePicker dtpFechaAlta;
        private System.Windows.Forms.DateTimePicker dtpUltimoLogIn;
        private System.Windows.Forms.Button btnRechazar;
        private System.Windows.Forms.Button btnAutorizar;
    }
}