using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biblioteca.Classes
{
    public partial class Estils : Form
    {
        public List<string> nousEstils = new List<string>();
        // 
        public Estils(DataTable dtEstils, bool afegir, List<string> EstilsRel)
        {
            InitializeComponent();
            //
            decimal numEstils = dtEstils.Rows.Count;
            decimal div = numEstils / 3;
            int numFilesEstils = (int)Math.Ceiling(div);
            int fila = 0;
            int col = 0;
            //
            string nomEstil = "";
            //
            for (int i = 0; i < dtEstils.Rows.Count; i++)
            {
                nomEstil = dtEstils.Rows[i][1].ToString();
                CheckBox cboxEStil = new CheckBox();
                cboxEStil.Tag = i.ToString();
                cboxEStil.Text = nomEstil;
                cboxEStil.AutoSize = true;
                cboxEStil.CheckedChanged += new EventHandler(CheckBox_CheckChanged);
                cboxEStil.BackColor = Color.LightSalmon;
                //
                if (col < 1)
                {
                    cboxEStil.Location = new Point(10, (fila * 70) + 10); //vertical
                    fila++;
                    panelEstils1.Controls.Add(cboxEStil);
                    if (fila > numFilesEstils - 1)
                    {
                        col++;
                        fila = 0;
                    }
                }
                else
                {
                    if (col < 2)
                    {
                        cboxEStil.Location = new Point(10, (fila * 70) + 10); //horizontal
                        fila++;
                        panelEstils2.Controls.Add(cboxEStil);
                        if (fila > numFilesEstils - 1)
                        {
                            col++;
                            fila = 0;
                        }
                    }
                    else
                    {
                        if (col < 3)
                        {
                            cboxEStil.Location = new Point(10, (fila * 70) + 10); //horizontal
                            fila++;
                            panelEstils3.Controls.Add(cboxEStil);
                            if (fila > numFilesEstils - 1)
                            {
                                col++;
                                fila = 0;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ep! Hem comptat malament el número d'estils; pot ser que en faltin!!", "Comptador Estils", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            // Mirem si estem afegint estils a un nou element - sense estils encara - o si estem modificant - ja te algun/s estils
            if (afegir == false)
            //if(true)
            {
                // Comprovem quins checkboxs haurien d'estar marcats segons BBDD
                foreach (CheckBox c in this.panelEstils1.Controls)
                {
                    // Busquem quins estils té l'autor
                    foreach (string estil in EstilsRel)
                    {
                        if (c.Text == estil)
                        {
                            c.Checked = true;
                        }
                    }
                }
                foreach (CheckBox c in this.panelEstils2.Controls)
                {
                    // Busquem quins estils té l'autor
                    foreach (string estil in EstilsRel)
                    {
                        if (c.Text == estil)
                        {
                            c.Checked = true;
                        }
                    }
                }
                foreach (CheckBox c in this.panelEstils3.Controls)
                {
                    // Busquem quins estils té l'autor
                    foreach (string estil in EstilsRel)
                    {
                        if (c.Text == estil)
                        {
                            c.Checked = true;
                        }
                    }
                }
            }
        }
        private void btEstilsAcceptar_Click(object sender, EventArgs e)
        {
            //List<string> nousEstils = new List<string>();
            nousEstils.Clear();
            //
            // Comprovem quins checkboxs estan marcats
            foreach (CheckBox c in this.panelEstils1.Controls)
            {
                if (c.Checked == true)
                {
                    nousEstils.Add(c.Text);   
                }
                
            }
            foreach (CheckBox c in this.panelEstils2.Controls)
            {
                if (c.Checked == true)
                {
                    nousEstils.Add(c.Text);
                }
            }
            foreach (CheckBox c in this.panelEstils3.Controls)
            {
                if (c.Checked == true)
                {
                    nousEstils.Add(c.Text);
                }
            }

            //Biblio.EstilsAutorSelec = nousEstils;
            this.DialogResult = DialogResult.OK;
            this.Close();
            //this.Dispose();
        }
        private void btEstilsCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            //this.Close();
            this.Dispose();
        }
        protected void CheckBox_CheckChanged(object sender, EventArgs e)
        {
            CheckBox chk = (sender as CheckBox);
            if (chk.Checked)
            {
                chk.BackColor = Color.LightGreen;
            }
            else
            {
                chk.BackColor = Color.LightSalmon;
            }
        }
    }
}
