namespace Biblioteca
{
    partial class BuscarLlibre
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
            this.btCancelar = new System.Windows.Forms.Button();
            this.btBuscar = new System.Windows.Forms.Button();
            this.lbBuscarLlibre = new System.Windows.Forms.Label();
            this.tbCadenaBusqueda = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btCancelar
            // 
            this.btCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancelar.Location = new System.Drawing.Point(468, 55);
            this.btCancelar.Margin = new System.Windows.Forms.Padding(6);
            this.btCancelar.Name = "btCancelar";
            this.btCancelar.Size = new System.Drawing.Size(159, 43);
            this.btCancelar.TabIndex = 3;
            this.btCancelar.Text = "Cancelar";
            this.btCancelar.UseVisualStyleBackColor = true;
            // 
            // btBuscar
            // 
            this.btBuscar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btBuscar.Location = new System.Drawing.Point(468, 6);
            this.btBuscar.Margin = new System.Windows.Forms.Padding(6);
            this.btBuscar.Name = "btBuscar";
            this.btBuscar.Size = new System.Drawing.Size(159, 43);
            this.btBuscar.TabIndex = 2;
            this.btBuscar.Text = "Filtrar";
            this.btBuscar.UseVisualStyleBackColor = true;
            this.btBuscar.Click += new System.EventHandler(this.btBuscar_Click);
            // 
            // lbBuscarLlibre
            // 
            this.lbBuscarLlibre.AutoSize = true;
            this.lbBuscarLlibre.Location = new System.Drawing.Point(25, 13);
            this.lbBuscarLlibre.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbBuscarLlibre.Name = "lbBuscarLlibre";
            this.lbBuscarLlibre.Size = new System.Drawing.Size(386, 29);
            this.lbBuscarLlibre.TabIndex = 4;
            this.lbBuscarLlibre.Text = "Introdueix el paràmetre de búsqueda.";
            // 
            // tbCadenaBusqueda
            // 
            this.tbCadenaBusqueda.Location = new System.Drawing.Point(30, 61);
            this.tbCadenaBusqueda.Margin = new System.Windows.Forms.Padding(6);
            this.tbCadenaBusqueda.Name = "tbCadenaBusqueda";
            this.tbCadenaBusqueda.Size = new System.Drawing.Size(390, 37);
            this.tbCadenaBusqueda.TabIndex = 1;
            this.tbCadenaBusqueda.Tag = "1";
            this.tbCadenaBusqueda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCadenaBusqueda_KeyPress);
            // 
            // BuscarLlibre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 116);
            this.Controls.Add(this.tbCadenaBusqueda);
            this.Controls.Add(this.lbBuscarLlibre);
            this.Controls.Add(this.btBuscar);
            this.Controls.Add(this.btCancelar);
            this.Font = new System.Drawing.Font("Calibri", 18F);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(652, 154);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(652, 154);
            this.Name = "BuscarLlibre";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "5";
            this.Text = "Buscar Llibre";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BuscarLlibre_FormClosed);
            this.Load += new System.EventHandler(this.BuscarLlibre_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btCancelar;
        private System.Windows.Forms.Button btBuscar;
        private System.Windows.Forms.Label lbBuscarLlibre;
        public System.Windows.Forms.TextBox tbCadenaBusqueda;
    }
}