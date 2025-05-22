namespace TemplateTPCorto
{
    partial class FormPerfilAdministrador
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.btnVerCredencialOp = new System.Windows.Forms.Button();
            this.btnVerPersonaCambio = new System.Windows.Forms.Button();
            this.btnChangePass = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Administrador";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(56, 33);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(44, 16);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "label2";
            // 
            // btnVerCredencialOp
            // 
            this.btnVerCredencialOp.Location = new System.Drawing.Point(56, 63);
            this.btnVerCredencialOp.Name = "btnVerCredencialOp";
            this.btnVerCredencialOp.Size = new System.Drawing.Size(252, 23);
            this.btnVerCredencialOp.TabIndex = 2;
            this.btnVerCredencialOp.Text = "Ver Operaciones cambio credencial";
            this.btnVerCredencialOp.UseVisualStyleBackColor = true;
            this.btnVerCredencialOp.Click += new System.EventHandler(this.btnVerCredencialOp_Click);
            // 
            // btnVerPersonaCambio
            // 
            this.btnVerPersonaCambio.Location = new System.Drawing.Point(56, 93);
            this.btnVerPersonaCambio.Name = "btnVerPersonaCambio";
            this.btnVerPersonaCambio.Size = new System.Drawing.Size(252, 23);
            this.btnVerPersonaCambio.TabIndex = 3;
            this.btnVerPersonaCambio.Text = "Ver operaciones cambio persona";
            this.btnVerPersonaCambio.UseVisualStyleBackColor = true;
            this.btnVerPersonaCambio.Click += new System.EventHandler(this.btnVerPersonaCambio_Click);
            // 
            // btnChangePass
            // 
            this.btnChangePass.Location = new System.Drawing.Point(59, 123);
            this.btnChangePass.Name = "btnChangePass";
            this.btnChangePass.Size = new System.Drawing.Size(249, 23);
            this.btnChangePass.TabIndex = 4;
            this.btnChangePass.Text = "Cambiar contraseña";
            this.btnChangePass.UseVisualStyleBackColor = true;
            this.btnChangePass.Click += new System.EventHandler(this.btnChangePass_Click);
            // 
            // FormPerfilAdministrador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 180);
            this.Controls.Add(this.btnChangePass);
            this.Controls.Add(this.btnVerPersonaCambio);
            this.Controls.Add(this.btnVerCredencialOp);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.label1);
            this.Name = "FormPerfilAdministrador";
            this.Text = "FormPerfilAdministrador";
            this.Load += new System.EventHandler(this.FormPerfilAdministrador_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnVerCredencialOp;
        private System.Windows.Forms.Button btnVerPersonaCambio;
        private System.Windows.Forms.Button btnChangePass;
    }
}