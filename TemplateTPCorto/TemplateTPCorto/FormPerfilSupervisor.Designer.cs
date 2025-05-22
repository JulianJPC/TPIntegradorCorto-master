namespace TemplateTPCorto
{
    partial class FormPerfilSupervisor
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
            this.lblNombre = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnModPersona = new System.Windows.Forms.Button();
            this.btnDesbloquear = new System.Windows.Forms.Button();
            this.btnChangePass = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(30, 34);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(44, 16);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Supervisor";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnModPersona
            // 
            this.btnModPersona.Location = new System.Drawing.Point(33, 67);
            this.btnModPersona.Name = "btnModPersona";
            this.btnModPersona.Size = new System.Drawing.Size(181, 23);
            this.btnModPersona.TabIndex = 2;
            this.btnModPersona.Text = "Modificar Persona";
            this.btnModPersona.UseVisualStyleBackColor = true;
            this.btnModPersona.Click += new System.EventHandler(this.btnModPersona_Click);
            // 
            // btnDesbloquear
            // 
            this.btnDesbloquear.Location = new System.Drawing.Point(33, 96);
            this.btnDesbloquear.Name = "btnDesbloquear";
            this.btnDesbloquear.Size = new System.Drawing.Size(181, 23);
            this.btnDesbloquear.TabIndex = 3;
            this.btnDesbloquear.Text = "Desbloquear Credencial";
            this.btnDesbloquear.UseVisualStyleBackColor = true;
            this.btnDesbloquear.Click += new System.EventHandler(this.btnDesbloquear_Click);
            // 
            // btnChangePass
            // 
            this.btnChangePass.Location = new System.Drawing.Point(33, 126);
            this.btnChangePass.Name = "btnChangePass";
            this.btnChangePass.Size = new System.Drawing.Size(181, 23);
            this.btnChangePass.TabIndex = 4;
            this.btnChangePass.Text = "Cambiar contraseña";
            this.btnChangePass.UseVisualStyleBackColor = true;
            this.btnChangePass.Click += new System.EventHandler(this.btnChangePass_Click);
            // 
            // FormPerfilSupervisor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 174);
            this.Controls.Add(this.btnChangePass);
            this.Controls.Add(this.btnDesbloquear);
            this.Controls.Add(this.btnModPersona);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblNombre);
            this.Name = "FormPerfilSupervisor";
            this.Text = "FormPerfilSupervisor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnModPersona;
        private System.Windows.Forms.Button btnDesbloquear;
        private System.Windows.Forms.Button btnChangePass;
    }
}