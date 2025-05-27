namespace TemplateTPCorto
{
    partial class FormPerfiles
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
            this.lblPerfilNombre = new System.Windows.Forms.Label();
            this.lblUs = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.btnModPersona = new System.Windows.Forms.Button();
            this.btnAutoPersona = new System.Windows.Forms.Button();
            this.btnDesbCredencial = new System.Windows.Forms.Button();
            this.btnAutoCred = new System.Windows.Forms.Button();
            this.btnOperador = new System.Windows.Forms.Button();
            this.btnCambiarPass = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Perfil:";
            // 
            // lblPerfilNombre
            // 
            this.lblPerfilNombre.AutoSize = true;
            this.lblPerfilNombre.Location = new System.Drawing.Point(103, 12);
            this.lblPerfilNombre.Name = "lblPerfilNombre";
            this.lblPerfilNombre.Size = new System.Drawing.Size(44, 16);
            this.lblPerfilNombre.TabIndex = 1;
            this.lblPerfilNombre.Text = "label2";
            // 
            // lblUs
            // 
            this.lblUs.AutoSize = true;
            this.lblUs.Location = new System.Drawing.Point(39, 29);
            this.lblUs.Name = "lblUs";
            this.lblUs.Size = new System.Drawing.Size(57, 16);
            this.lblUs.TabIndex = 2;
            this.lblUs.Text = "Usuario:";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(103, 29);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(44, 16);
            this.lblUsuario.TabIndex = 3;
            this.lblUsuario.Text = "label2";
            // 
            // btnModPersona
            // 
            this.btnModPersona.Enabled = false;
            this.btnModPersona.Location = new System.Drawing.Point(42, 92);
            this.btnModPersona.Name = "btnModPersona";
            this.btnModPersona.Size = new System.Drawing.Size(238, 23);
            this.btnModPersona.TabIndex = 4;
            this.btnModPersona.TabStop = false;
            this.btnModPersona.Text = "Modificar persona";
            this.btnModPersona.UseVisualStyleBackColor = true;
            this.btnModPersona.Click += new System.EventHandler(this.btnModPersona_Click);
            // 
            // btnAutoPersona
            // 
            this.btnAutoPersona.Enabled = false;
            this.btnAutoPersona.Location = new System.Drawing.Point(42, 121);
            this.btnAutoPersona.Name = "btnAutoPersona";
            this.btnAutoPersona.Size = new System.Drawing.Size(238, 23);
            this.btnAutoPersona.TabIndex = 5;
            this.btnAutoPersona.Text = "Autorizar Modificar Persona";
            this.btnAutoPersona.UseVisualStyleBackColor = true;
            this.btnAutoPersona.Click += new System.EventHandler(this.btnAutoPersona_Click);
            // 
            // btnDesbCredencial
            // 
            this.btnDesbCredencial.Enabled = false;
            this.btnDesbCredencial.Location = new System.Drawing.Point(42, 150);
            this.btnDesbCredencial.Name = "btnDesbCredencial";
            this.btnDesbCredencial.Size = new System.Drawing.Size(238, 23);
            this.btnDesbCredencial.TabIndex = 6;
            this.btnDesbCredencial.Text = "Desbloquear Credencial";
            this.btnDesbCredencial.UseVisualStyleBackColor = true;
            this.btnDesbCredencial.Click += new System.EventHandler(this.btnDesbCredencial_Click);
            // 
            // btnAutoCred
            // 
            this.btnAutoCred.Enabled = false;
            this.btnAutoCred.Location = new System.Drawing.Point(42, 179);
            this.btnAutoCred.Name = "btnAutoCred";
            this.btnAutoCred.Size = new System.Drawing.Size(238, 23);
            this.btnAutoCred.TabIndex = 7;
            this.btnAutoCred.Text = "Autorizar Desbloquear Credencial";
            this.btnAutoCred.UseVisualStyleBackColor = true;
            this.btnAutoCred.Click += new System.EventHandler(this.btnAutoCred_Click);
            // 
            // btnOperador
            // 
            this.btnOperador.Enabled = false;
            this.btnOperador.Location = new System.Drawing.Point(42, 208);
            this.btnOperador.Name = "btnOperador";
            this.btnOperador.Size = new System.Drawing.Size(238, 23);
            this.btnOperador.TabIndex = 8;
            this.btnOperador.Text = "Operador";
            this.btnOperador.UseVisualStyleBackColor = true;
            // 
            // btnCambiarPass
            // 
            this.btnCambiarPass.Location = new System.Drawing.Point(42, 238);
            this.btnCambiarPass.Name = "btnCambiarPass";
            this.btnCambiarPass.Size = new System.Drawing.Size(238, 23);
            this.btnCambiarPass.TabIndex = 9;
            this.btnCambiarPass.Text = "Cambiar Contraseña";
            this.btnCambiarPass.UseVisualStyleBackColor = true;
            this.btnCambiarPass.Click += new System.EventHandler(this.btnCambiarPass_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(42, 311);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(238, 23);
            this.btnSalir.TabIndex = 10;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // FormPerfiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 347);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnCambiarPass);
            this.Controls.Add(this.btnOperador);
            this.Controls.Add(this.btnAutoCred);
            this.Controls.Add(this.btnDesbCredencial);
            this.Controls.Add(this.btnAutoPersona);
            this.Controls.Add(this.btnModPersona);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.lblUs);
            this.Controls.Add(this.lblPerfilNombre);
            this.Controls.Add(this.label1);
            this.Name = "FormPerfiles";
            this.Text = "FormPerfiles";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPerfilNombre;
        private System.Windows.Forms.Label lblUs;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Button btnModPersona;
        private System.Windows.Forms.Button btnAutoPersona;
        private System.Windows.Forms.Button btnDesbCredencial;
        private System.Windows.Forms.Button btnAutoCred;
        private System.Windows.Forms.Button btnOperador;
        private System.Windows.Forms.Button btnCambiarPass;
        private System.Windows.Forms.Button btnSalir;
    }
}