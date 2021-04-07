using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biblioteca
{
    public partial class BuscarLlibre : Form
    {
        public string textBusqueda = "";
        public BuscarLlibre()
        {
            InitializeComponent();
        }
        private void BuscarLlibre_Load(object sender, EventArgs e)
        {
            tbCadenaBusqueda.Focus();
        }
        private void BuscarLlibre_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
        /*
         * BOTONS
         */
        private void btBuscar_Click(object sender, EventArgs e)
        {
            textBusqueda = tbCadenaBusqueda.Text;
        }
        private void tbCadenaBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                textBusqueda = tbCadenaBusqueda.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
