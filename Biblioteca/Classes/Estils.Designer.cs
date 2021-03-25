namespace Biblioteca.Classes
{
    partial class Estils
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Estils));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelEstils = new System.Windows.Forms.Panel();
            this.panelEstils1 = new System.Windows.Forms.Panel();
            this.panelEstils2 = new System.Windows.Forms.Panel();
            this.panelEstils3 = new System.Windows.Forms.Panel();
            this.btEstilsAcceptar = new System.Windows.Forms.Button();
            this.btEstilsCancelar = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panelEstils.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btEstilsCancelar);
            this.panel1.Controls.Add(this.btEstilsAcceptar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 561);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(900, 100);
            this.panel1.TabIndex = 12;
            // 
            // panelEstils
            // 
            this.panelEstils.Controls.Add(this.panelEstils3);
            this.panelEstils.Controls.Add(this.panelEstils2);
            this.panelEstils.Controls.Add(this.panelEstils1);
            this.panelEstils.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEstils.Location = new System.Drawing.Point(0, 0);
            this.panelEstils.Name = "panelEstils";
            this.panelEstils.Size = new System.Drawing.Size(900, 561);
            this.panelEstils.TabIndex = 13;
            // 
            // panelEstils1
            // 
            this.panelEstils1.AutoScroll = true;
            this.panelEstils1.BackColor = System.Drawing.SystemColors.Control;
            this.panelEstils1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEstils1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.panelEstils1.Location = new System.Drawing.Point(0, 0);
            this.panelEstils1.Margin = new System.Windows.Forms.Padding(7);
            this.panelEstils1.Name = "panelEstils1";
            this.panelEstils1.Size = new System.Drawing.Size(300, 561);
            this.panelEstils1.TabIndex = 7;
            // 
            // panelEstils2
            // 
            this.panelEstils2.AutoScroll = true;
            this.panelEstils2.BackColor = System.Drawing.SystemColors.Control;
            this.panelEstils2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEstils2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.panelEstils2.Location = new System.Drawing.Point(300, 0);
            this.panelEstils2.Name = "panelEstils2";
            this.panelEstils2.Size = new System.Drawing.Size(300, 561);
            this.panelEstils2.TabIndex = 10;
            // 
            // panelEstils3
            // 
            this.panelEstils3.AutoScroll = true;
            this.panelEstils3.BackColor = System.Drawing.SystemColors.Control;
            this.panelEstils3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelEstils3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.panelEstils3.Location = new System.Drawing.Point(600, 0);
            this.panelEstils3.Name = "panelEstils3";
            this.panelEstils3.Size = new System.Drawing.Size(300, 561);
            this.panelEstils3.TabIndex = 12;
            // 
            // btEstilsAcceptar
            // 
            this.btEstilsAcceptar.Location = new System.Drawing.Point(217, 10);
            this.btEstilsAcceptar.Name = "btEstilsAcceptar";
            this.btEstilsAcceptar.Size = new System.Drawing.Size(171, 78);
            this.btEstilsAcceptar.TabIndex = 0;
            this.btEstilsAcceptar.Text = "Acceptar";
            this.btEstilsAcceptar.UseVisualStyleBackColor = true;
            this.btEstilsAcceptar.Click += new System.EventHandler(this.btEstilsAcceptar_Click);
            // 
            // btEstilsCancelar
            // 
            this.btEstilsCancelar.Location = new System.Drawing.Point(515, 10);
            this.btEstilsCancelar.Name = "btEstilsCancelar";
            this.btEstilsCancelar.Size = new System.Drawing.Size(171, 78);
            this.btEstilsCancelar.TabIndex = 1;
            this.btEstilsCancelar.Text = "Cancelar";
            this.btEstilsCancelar.UseVisualStyleBackColor = true;
            this.btEstilsCancelar.Click += new System.EventHandler(this.btEstilsCancelar_Click);
            // 
            // Estils
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 661);
            this.Controls.Add(this.panelEstils);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(7);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(916, 700);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(916, 700);
            this.Name = "Estils";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Estils";
            this.panel1.ResumeLayout(false);
            this.panelEstils.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btEstilsCancelar;
        private System.Windows.Forms.Button btEstilsAcceptar;
        private System.Windows.Forms.Panel panelEstils;
        private System.Windows.Forms.Panel panelEstils3;
        private System.Windows.Forms.Panel panelEstils2;
        private System.Windows.Forms.Panel panelEstils1;
    }
}