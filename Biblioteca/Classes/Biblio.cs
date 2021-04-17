using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Threading;
using System.Resources;
using Microsoft.VisualBasic;
using Biblioteca.Biblioteca2020DataSetTableAdapters;
using Biblioteca.Classes;
using System.Data.OleDb;


namespace Biblioteca
{
    public partial class Biblio : Form
    {
        /*
         * VARIABLES CLASSE
         */
        public DataTable DtBusquedaLlibre = new DataTable();
        public DataTable DtLlibre = new DataTable();
        public DataTable DtAutor = new DataTable();
        public DataTable DtIdioma = new DataTable();
        public DataTable DtLocalitzacio = new DataTable();
        public DataTable DtEstilLlibre = new DataTable();
        public DataTable DtEstilAutor = new DataTable();
        public DataTable DtEstil = new DataTable();
        public DataTable DtAutorLlibre = new DataTable();
        //
        public bool dtLlibresAct = false;
        public bool dtAutorsAct = false;
        public bool dtIdiomesAct = false;
        public bool dtUbicacionsAct = false;
        public bool dtEstilsAct = false;
        public bool dtEstilsLlibreAct = false;
        public bool sqlResultat = false;
        //
        public bool afegirLlibre = false;
        public bool afegirAutor = false;
        public bool afegirIdioma = false;
        public bool afegirUbicacio = false;
        public bool afegirEstil = false;
        //
        public bool editantLlibre = false;
        public bool editantAutor = false;
        //
        public string tipusFont="";
        public string stringSql = "";
        //
        public List<string> EstilsAutorSelec = new List<string>();
        public List<string> EstilsLlibreSelec = new List<string>();
        //
        public int IdLocalActiu = 0;
        public int cbLocalSelectedIndex = 0;
        const int IdAutorDesconegut = 83;
        const int IdLocalitzacioDesconeguda = 54;
        const int IdIdiomaDesconegut = 10;
        //
        public float dimFont = 5; // afegida inicialització pel portàtil; a la torre no feia falta.
        //
        public SqlCon bbddConn = new SqlCon();
        /*
         * EVENTS
         */
        public Biblio()
        {
            SplashScreen splash = new SplashScreen("resources/BiblioInici.png"); // el putu resources/ <-- s'ha de posar Build Action/ Resources en la imatge adjuntada com a Recurs...
            splash.Show(false);
            //Thread.Sleep(5000); // Pausa per veure l'entrada; el programa no triga tant a carregar.
            splash.Close(TimeSpan.FromMilliseconds(300));
            //
            InitializeComponent();
        }
        private void Biblio_Load(object sender, EventArgs e)
        {
            // CARREGA TAULES
            // *********************** LLIBRES            
            stringSql = "SELECT * FROM LLIBRE ORDER BY Titol";
            bbddConn.GetData(stringSql, ref DtLlibre);
            lbLlibresTotalsBiblio.Text = DtLlibre.Rows.Count.ToString();
            // *********************** AUTORS
            stringSql = "SELECT IdAutor, Nom, Cognoms, Nacionalitat, DataNaixement, Comentaris, Nom &' ' & Cognoms AS NomComplet FROM AUTORS ORDER BY Cognoms";
            bbddConn.GetData(stringSql, ref DtAutor);
            // *********************** IDIOMA
            stringSql = "SELECT * FROM IDIOMA ORDER BY Idioma";
            bbddConn.GetData(stringSql, ref DtIdioma);
            // *********************** LOCALITZACIO
            stringSql = "SELECT * FROM LOCALITZACIO ORDER BY Casa";
            bbddConn.GetData(stringSql, ref DtLocalitzacio);
            // *********************** ESTIL
            stringSql = "SELECT * FROM ESTIL ORDER BY Estil";
            bbddConn.GetData(stringSql, ref DtEstil);
            // *********************** ESTIL LLIBRE
            stringSql = "SELECT * FROM ESTIL_LLIBRE";
            bbddConn.GetData(stringSql, ref DtEstilLlibre);
            // *********************** ESTIL_AUTOR
            stringSql = "SELECT * FROM ESTIL_AUTOR";
            bbddConn.GetData(stringSql, ref DtEstilAutor);
            // *********************** AUTOR_LLIBRE
            stringSql = "SELECT * FROM AUTOR_LLIBRE";
            bbddConn.GetData(stringSql, ref DtAutorLlibre);
            // Posem els camps de data al dia
            anyDateTimePicker.Value = DateTime.Today;
            dataCompraDateTimePicker.Value = DateTime.Today;
            dataNaixementDateTimePicker.Value = DateTime.Today;
            // Carregar Combos
            CarregarCombos();
        }
        private void Biblio_Shown(object sender, EventArgs e)
        {
            dimFont = this.Font.Size;
            tipusFont = this.Font.Name;
            //
            this.Biblio_Resize(this, e);
        }
        private void Biblio_Resize(object sender, EventArgs e)
        {
            // Aquest mètode peta al arrencar el programa en el portàtil; a la torre no. Per això s'inicilalitza la variable en un valor concret que s'haurà de veure quin és.
            int ampleDefecte = 800;
            int altDefecte = 450;
            //
            int ampleNou = this.Size.Width;
            int altNou = this.Size.Height;
            //
            int PropAmple = (ampleNou * 100) / ampleDefecte;
            int PropAlt = (altNou * 100) / altDefecte;
            //
            float dimFontVar = ((dimFont * PropAmple) / 100);
            //
            this.Font = new Font(tipusFont, dimFontVar, FontStyle.Regular);
            tbEstils.Font = new Font(tipusFont, dimFontVar, FontStyle.Regular);
        }
        private void TbPrincipal_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.dtLlibresAct = false;
            this.dtAutorsAct = false;
            this.dtIdiomesAct = false;
            this.dtUbicacionsAct = false;
            this.dtEstilsAct = false;
            this.dtEstilsLlibreAct = false;
            this.sqlResultat = false;
            switch (tbPrincipal.SelectedIndex)
            {
                case 1: // *********************** LLIBRES
                    /*
                    // Carreguem el DataTable al Grid
                    llibreDataGridView.DataSource = DtLlibre;
                    dtLlibresAct = true;
                    // Ocultem les columnes del grid que no ens interessen
                    this.llibreDataGridView.Columns["Any"].Visible = false;
                    this.llibreDataGridView.Columns["Editor"].Visible = false;
                    this.llibreDataGridView.Columns["Colleccio"].Visible = false;
                    this.llibreDataGridView.Columns["NumEdicio"].Visible = false;
                    this.llibreDataGridView.Columns["TipusCoberta"].Visible = false;
                    this.llibreDataGridView.Columns["DataCompra"].Visible = false;
                    this.llibreDataGridView.Columns["NumPagines"].Visible = false;
                    this.llibreDataGridView.Columns["Comentaris"].Visible = false;
                    this.llibreDataGridView.Columns["IdLlibre"].Visible = false;
                    this.llibreDataGridView.Columns["IdIdioma"].Visible = false;
                    this.llibreDataGridView.Columns["IdLocalitzacio"].Visible = false;                    
                    */
                    // Sortim del 'case'
                    break;
                case 2: // *********************** AUTORS
                    // Carreguem el DataTable al Grid
                    autorsDataGridView.DataSource = DtAutor;
                    dtAutorsAct = true;
                    // Ocultem les columnes del grid que no ens interessen
                    this.autorsDataGridView.Columns["IdAutor"].Visible = false;
                    this.autorsDataGridView.Columns["Nacionalitat"].Visible = false;
                    this.autorsDataGridView.Columns["DataNaixement"].Visible = false;
                    this.autorsDataGridView.Columns["Comentaris"].Visible = false;
                    this.autorsDataGridView.Columns["NomComplet"].Visible = false;
                    // Sortim del 'case'
                    break;
                case 3: // *********************** IDIOMES
                        // Carreguem el DataTable al Grid
                    idiomaDataGridView.DataSource = DtIdioma;
                    dtIdiomesAct = true;
                    // Ocultem les columnes del grid que no ens interessen
                    this.idiomaDataGridView.Columns["IdIdioma"].Visible = false;
                    // Sortim del 'case'
                    break;
                case 4: // *********************** UBICACIONS
                    // Carreguem el DataTable al Grid
                    localitzacioDataGridView.DataSource = DtLocalitzacio;
                    dtUbicacionsAct = true;
                    // Ocultem les columnes del grid que no ens interessen
                    this.localitzacioDataGridView.Columns["IdLocalitzacio"].Visible = false;
                    this.localitzacioDataGridView.Columns["Habitacio"].Visible = false;
                    // Sortim del 'case'
                    break;
                case 5: // *********************** ESTILS
                    // Carreguem el DataTable al Grid
                    estilDataGridView.DataSource = DtEstil;
                    dtEstilsAct = true;
                    // Ocultem les columnes del grid que no ens interessen
                    this.estilDataGridView.Columns["IdEstil"].Visible = false;
                    // Sortim del 'case'
                    break;
                case 6: // *********************** LLISTATS
                    // Totals
                    lbTotLlibres.Text = DtLlibre.Rows.Count.ToString();
                    lbTotAutors.Text = DtAutor.Rows.Count.ToString();
                    lbTotIdioma.Text = DtIdioma.Rows.Count.ToString();
                    lbTotUbic.Text = DtLocalitzacio.Rows.Count.ToString();
                    lbTotEstil.Text = DtEstil.Rows.Count.ToString();
                    // Autor
                    string sqlQuery = "SELECT IdAutor, COUNT (IdAutor) AS total FROM AUTOR_LLIBRE GROUP BY IdAutor ORDER BY 2 DESC";
                    int idAutor = bbddConn.GetTotal(sqlQuery);
                    if (idAutor == 0)
                    {
                        MessageBox.Show("Hi ha hagut un problema recuperant la informació.", "Dades Biblioteca.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        sqlQuery = "SELECT Nom & ' ' & Cognoms AS NomComplet FROM AUTORS WHERE IdAutor = " + idAutor;
                        string autor = bbddConn.GetNom(sqlQuery);
                        lbMesUtilAutor.Text = autor;
                    }
                    int NoUtilAutors = 0;
                    foreach (DataRow filaAutor in DtAutor.Rows)
                    {
                        bool existeix = false;
                        foreach (DataRow filaAutorLlibre in DtAutorLlibre.Rows)
                        {
                            if ((Int32)filaAutor["IdAutor"] == (Int32)filaAutorLlibre["IdAutor"])
                            {
                                existeix = true;
                            }
                        }
                        if (existeix == false)
                        {
                            NoUtilAutors++;
                        }
                        /*sqlQuery = "SELECT COUNT (*) FROM AUTOR_LLIBRE WHERE IdAutor = " + (Int32)filaAutor["IdAutor"];
                        int files = bbddConn.GetFiles(sqlQuery);
                        if (files == 0)
                        {
                            NoUtilAutors++;
                        }*/
                    }
                    lbNoUtilAutor.Text = NoUtilAutors.ToString();
                    // Idioma
                    sqlQuery = "SELECT IdIdioma, COUNT (IdIdioma) AS total FROM LLIBRE GROUP BY IdIdioma ORDER BY 2 DESC";
                    int idIdioma = bbddConn.GetTotal(sqlQuery);
                    if (idIdioma == 0)
                    {
                        MessageBox.Show("Hi ha hagut un problema recuperant la informació.", "Dades Biblioteca.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        sqlQuery = "SELECT Idioma FROM IDIOMA WHERE IdIdioma = " + idIdioma;
                        string idioma = bbddConn.GetNom(sqlQuery);
                        lbMesUtilIdioma.Text = idioma;
                    }
                    int NoUtilIdiomes = 0;
                    foreach (DataRow filaIdioma in DtIdioma.Rows)
                    {
                        bool existeix = false;
                        foreach (DataRow filaLlibre in DtLlibre.Rows)
                        {
                            if ((Int32)filaIdioma["IdIdioma"] == (Int32)filaLlibre["IdIdioma"])
                            {
                                existeix = true;
                            }
                       }
                        if (existeix == false)
                        {
                            NoUtilIdiomes++;
                        }
                    }
                    lbNoUtilIdioma.Text = NoUtilIdiomes.ToString();
                    // Ubicacions
                    sqlQuery = "SELECT IdLocalitzacio, COUNT (IdLocalitzacio) AS total FROM LLIBRE GROUP BY IdLocalitzacio ORDER BY 2 DESC";
                    int IdLocalitzacio = bbddConn.GetTotal(sqlQuery);
                    if (idIdioma == 0)
                    {
                        MessageBox.Show("Hi ha hagut un problema recuperant la informació.", "Dades Biblioteca.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        sqlQuery = "SELECT Casa & ' ' & Habitacio AS Ubicacio FROM LOCALITZACIO WHERE IdLocalitzacio = " + IdLocalitzacio;
                        string local = bbddConn.GetNom(sqlQuery);
                        lbMesUtilUbic.Text = local;
                    }
                    int NoUtilLocal = 0;
                    foreach (DataRow filaLocal in DtLocalitzacio.Rows)
                    {
                        bool existeix = false;
                        foreach (DataRow filaLlibre in DtLlibre.Rows)
                        {
                            if ((Int32)filaLocal["IdLocalitzacio"] == (Int32)filaLlibre["IdLocalitzacio"])
                            {
                                existeix = true;
                            }
                        }
                        if (existeix == false)
                        {
                            NoUtilLocal++;
                        }
                    }
                    lbNoUtilUbic.Text = NoUtilLocal.ToString();
                    // Estils
                    
                    sqlQuery = "SELECT IdEstil, SUM(total) AS suma FROM (SELECT A.IdEstil, COUNT(A.IdEstil) AS total FROM ESTIL_AUTOR A GROUP BY A.IdEstil ORDER BY 2 DESC UNION ALL SELECT L.IdEstil, COUNT(L.IdEstil) AS total FROM ESTIL_LLIBRE L GROUP BY L.IdEstil ORDER BY 2 DESC) GROUP BY IdEstil ORDER BY 2 DESC";
                    int IdEstil = bbddConn.GetTotal(sqlQuery);
                    if (IdEstil == 0)
                    {
                        MessageBox.Show("Hi ha hagut un problema recuperant la informació.", "Dades Biblioteca.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        sqlQuery = "SELECT Estil FROM ESTIL WHERE IdEstil = " + IdEstil;
                        string estil = bbddConn.GetNom(sqlQuery);
                        lbMesUtilEstil.Text = estil;
                    }
                    int NoUtilEstils = 0;
                    foreach (DataRow filaEstil in DtEstil.Rows)
                    {
                        bool existeix = false;
                        foreach (DataRow filaEstilLlibre in DtEstilLlibre.Rows)
                        {
                            if ((Int32)filaEstil["IdEstil"] == (Int32)filaEstilLlibre["IdEstil"])
                            {
                                existeix = true;
                            }
                        }
                        foreach (DataRow filaEstilAutor in DtEstilAutor.Rows)
                        {
                            if ((Int32)filaEstil["IdEstil"] == (Int32)filaEstilAutor["IdEstil"])
                            {
                                existeix = true;
                            }
                        }
                        if (existeix == false)
                        {
                            NoUtilEstils++;
                        }
                    }
                    lbNoUtilEstil.Text = NoUtilEstils.ToString();
                    break;
                default:
                    break;
            }
        }
        private void Biblio_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
        private void NumPaginesTextBox_TextChanged(object sender, EventArgs e)
        {
            int parsedValue;
            if (!int.TryParse(numPaginesTextBox.Text, out parsedValue) && (numPaginesTextBox.TextLength > 0))
            {
                MessageBox.Show("Només es permeten nombres en aquest camp.");
                numPaginesTextBox.Clear();
                return;
            }
        }
        private void NumEdicioTextBox_TextChanged(object sender, EventArgs e)
        {
            int parsedValue;
            if (!int.TryParse(numEdicioTextBox.Text, out parsedValue) && (numEdicioTextBox.TextLength > 0))
            {
                MessageBox.Show("Només es permeten nombres en aquest camp.");
                numEdicioTextBox.Clear();
                return;
            }
        }
        private void CbLocalitzacio_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbLocalSelectedIndex = cbLocalitzacio.SelectedIndex;
            DataRowView item = cbLocalitzacio.Items[cbLocalSelectedIndex] as DataRowView;
            if (item  != null)
            {
                habitacioTextBox.Text = item.Row["Habitacio"].ToString();
            }
        }
        /*
         * INICI
         */
        private void BtBuscar_Click(object sender, EventArgs e)
        {
            //Bloquegem el refresc del grid
            dtLlibresAct = false;
            //
            DialogResult dr = new DialogResult();
            BuscarLlibre formBusqueda = new BuscarLlibre();
            dr = formBusqueda.ShowDialog();
            if (dr == DialogResult.OK)
            {
                DtBusquedaLlibre.Clear();
                stringSql = "SELECT * FROM LLIBRE WHERE Titol LIKE '%" + formBusqueda.textBusqueda + "%' ORDER BY Titol;";
                bbddConn.GetData(stringSql, ref DtBusquedaLlibre);
                llibreDataGridView.DataSource = DtBusquedaLlibre;
                //
                // Ocultem les columnes del grid que no ens interessen
                this.llibreDataGridView.Columns["Any"].Visible = false;
                this.llibreDataGridView.Columns["Editor"].Visible = false;
                this.llibreDataGridView.Columns["Colleccio"].Visible = false;
                this.llibreDataGridView.Columns["NumEdicio"].Visible = false;
                this.llibreDataGridView.Columns["TipusCoberta"].Visible = false;
                this.llibreDataGridView.Columns["DataCompra"].Visible = false;
                this.llibreDataGridView.Columns["NumPagines"].Visible = false;
                this.llibreDataGridView.Columns["Comentaris"].Visible = false;
                this.llibreDataGridView.Columns["IdLlibre"].Visible = false;
                this.llibreDataGridView.Columns["IdIdioma"].Visible = false;
                this.llibreDataGridView.Columns["IdLocalitzacio"].Visible = false;
                //
                dtLlibresAct = true;
                lbllibresTrobats.Text = DtBusquedaLlibre.Rows.Count.ToString();
            }
            else if (dr == DialogResult.Cancel)
            {
                // statements executed if boolean-expression-2 is true   
                formBusqueda.Close();
            }
            else
            {
                formBusqueda.Close();
            }
        }
        private void btTots_Click(object sender, EventArgs e)
        {
            //Bloquegem el refresc del grid
            dtLlibresAct = false;
            //
            DtBusquedaLlibre.Clear();
            stringSql = "SELECT * FROM LLIBRE ORDER BY Titol;";
            bbddConn.GetData(stringSql, ref DtBusquedaLlibre);
            llibreDataGridView.DataSource = DtBusquedaLlibre;
            //
            // Ocultem les columnes del grid que no ens interessen
            this.llibreDataGridView.Columns["Any"].Visible = false;
            this.llibreDataGridView.Columns["Editor"].Visible = false;
            this.llibreDataGridView.Columns["Colleccio"].Visible = false;
            this.llibreDataGridView.Columns["NumEdicio"].Visible = false;
            this.llibreDataGridView.Columns["TipusCoberta"].Visible = false;
            this.llibreDataGridView.Columns["DataCompra"].Visible = false;
            this.llibreDataGridView.Columns["NumPagines"].Visible = false;
            this.llibreDataGridView.Columns["Comentaris"].Visible = false;
            this.llibreDataGridView.Columns["IdLlibre"].Visible = false;
            this.llibreDataGridView.Columns["IdIdioma"].Visible = false;
            this.llibreDataGridView.Columns["IdLocalitzacio"].Visible = false;
            //
            dtLlibresAct = true;
            lbllibresTrobats.Text = DtBusquedaLlibre.Rows.Count.ToString();
        }
        private void BtSortir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /*
         * LLIBRES
         */
        private void llibreDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Actualitzem el llibre
            this.RefrescLlibre();
            // Posem el focus en la pestanya llibre
            this.tbPrincipal.SelectedIndex = 1;
        }
        private void LlibreDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            // Actualitzem el llibre
            this.RefrescLlibre();
        }
        private void BtAfegirLlibres_Click(object sender, EventArgs e)
        {
            //Habiltem / Deshabilitem camps.
            this.cbLocalitzacio.Enabled = true;
            this.comentarisTextBox.ReadOnly = false;
            //this.tlpLlibres7.Enabled = true;
            this.tlpLlibres8.Enabled = true;
            this.tplTitol.Enabled = true;
            btCancelarLlibres.Visible = true;
            btGuardarLlibres.Visible = true;
            //
            btAfegirLlibres.Visible = false;
            btModificarLlibres.Visible = false;
            btEliminarLlibres.Visible = false;
            btTornarLlibres.Visible = false;
            //
            llibreDataGridView.Enabled = false;
            // Netegem camps
            netejaCampsLlibre();
            // Portem el punter en el primer camp d'edició.
            titolTextBox.Focus();
            afegirLlibre = true;
            editantLlibre = true;
        }       
        private void BtModificarLlibres_Click(object sender, EventArgs e)
        {
            if (llibreDataGridView.CurrentRow != null)
            {
                //Habiltem / Deshabilitem camps.
                this.cbLocalitzacio.Enabled = true;
                this.comentarisTextBox.ReadOnly = false;
                //this.tlpLlibres7.Enabled = true;
                this.tlpLlibres8.Enabled = true;
                this.tplTitol.Enabled = true;
                btCancelarLlibres.Visible = true;
                btGuardarLlibres.Visible = true;
                //
                btAfegirLlibres.Visible = false;
                btModificarLlibres.Visible = false;
                btEliminarLlibres.Visible = false;
                btTornarLlibres.Visible = false;
                //
                llibreDataGridView.Enabled = false;
                // Portem el punter en el primer camp d'edició.
                titolTextBox.Focus();
                editantLlibre = true;
            }
            else
            {
                MessageBox.Show("No tens cap llibre seleccionat.", "Modificar llibre", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void BtGuardarLlibres_Click(object sender, EventArgs e)
        {
            if ((dtLlibresAct) || (afegirLlibre)) // GUARDAR LLIBRE
            {
                // Assegurem que tenim, com a mínim, títol del llibre.
                if (titolTextBox.Text.Length != 0)
                {
                    // Definim valors per defecte per l'insert.
                    string titol = " ";
                    string editor = " ";
                    string col = " ";
                    int edi = 0;
                    string cob = " ";
                    int numPag = 0;
                    string coment = " ";
                    int idIdioma = 10;
                    int idLocal = 54;
                    int idAutor = 83;
                    // Mirem s'hi s'ha definit el títol
                    if (titolTextBox.Text.Length != 0)
                    {
                        titol = titolTextBox.Text;
                    }
                    // Mirem s'hi s'ha definit l'editor
                    if (editorTextBox.Text.Length != 0)
                    {
                        editor = editorTextBox.Text;
                    }
                    // Mirem s'hi s'ha definit la col·leccio
                    if (colleccioTextBox.Text.Length != 0)
                    {
                        col = colleccioTextBox.Text;
                    }
                    // Mirem s'hi s'ha definit el número d'edició
                    if (numEdicioTextBox.Text.Length != 0)
                    {
                        edi = Int32.Parse(numEdicioTextBox.Text);
                    }
                    // Mirem s'hi s'ha definit el tipus de coberta
                    if (tipusCobertaTextBox.Text.Length != 0)
                    {
                        cob = tipusCobertaTextBox.Text;
                    }
                    // Mirem s'hi s'ha definit el número de pàgines
                    if (numPaginesTextBox.Text.Length != 0)
                    {
                        numPag = Int32.Parse(numPaginesTextBox.Text);
                    }
                    // Mirem s'hi s'ha definit algun comentari
                    if (comentarisTextBox.Text.Length != 0)
                    {
                        coment = comentarisTextBox.Text;
                    }
                    // Mirem s'hi s'ha definit algun idioma i quin id te
                    foreach (DataRow filaIdioma in DtIdioma.Rows)
                    {
                        if (cbIdiomaLlibres.SelectedItem == filaIdioma["Idioma"])
                        {
                            idIdioma = (Int32)filaIdioma["IdIdioma"];
                        }
                    }
                    // Mirem si s'ha seleccionat alguna localitzacio
                    if (cbLocalitzacio.SelectedValue != null)
                    {
                        foreach (DataRow filaLocalitzacio in DtLocalitzacio.Rows)
                        {
                            if ((Int32)cbLocalitzacio.SelectedValue == (Int32)filaLocalitzacio["IdLocalitzacio"])
                            {
                                idLocal = (Int32)filaLocalitzacio["IdLocalitzacio"];
                            }
                        }
                    }
                    // Mirem si s'ha seleccionat algun autor
                    foreach (DataRow filaAutor in DtAutor.Rows)
                    {
                        if (cbAutor.SelectedItem == filaAutor["NomComplet"])
                        {
                            idAutor = (Int32)filaAutor["IdAutor"];
                        }
                    }
                    if (afegirLlibre) // GUARDAR LLIBRE
                    {
                        // Insertem el llibre a la BBDD
                        string sqlQuery = "INSERT INTO LLIBRE (Titol, [Any], Editor, Colleccio, NumEdicio, TipusCoberta, [DataCompra], NumPagines, Comentaris, IdIdioma, IdLocalitzacio) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                        int idLlibre = bbddConn.InsertLlibre(sqlQuery, titol, anyDateTimePicker.Value, editor, col, edi, cob, dataCompraDateTimePicker.Value, numPag, coment, idIdioma, idLocal);
                        // Insertem els estils i l'autor
                        if (idLlibre != 0)
                        {
                            // Insertem l'autor
                            sqlQuery = "INSERT INTO AUTOR_LLIBRE (IdAutor, IdLlibre) VALUES (" + idAutor + ", " + idLlibre + ")";
                            int resultat = bbddConn.InsUpdDel(sqlQuery);
                            if (resultat == 1)
                            {
                                // Insertem estils si n'hi ha
                                if (estilLlibreTextBox.Text.Length != 0)
                                {
                                    bool resultatProv = false;
                                    foreach (string estil in EstilsLlibreSelec)
                                    {
                                        foreach (DataRow filaEstil in DtEstil.Rows)
                                        {
                                            if (filaEstil["Estil"].ToString() == estil)
                                            {
                                                int idEstil = (Int32)filaEstil["IdEstil"];
                                                resultat = bbddConn.InsUpdDel("INSERT INTO ESTIL_LLIBRE (IdEstil, IdLlibre) VALUES (" + idEstil + ", " + idLlibre + ")");
                                                if (resultat == 1)
                                                {
                                                    resultatProv = true;
                                                }
                                                else
                                                {
                                                    resultatProv = false;
                                                }
                                            }
                                        }
                                    }
                                    if (resultatProv)
                                    {
                                        MessageBox.Show("Llibre, autor i estils afegits correctament.", "Insertar Llibre", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Hi ha hagut un problema insertant els estils del llibre.", "Insertar Llibre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Llibre i autor afegits correctament.", "Insertar Llibre", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Hi ha hagut un problema insertant l'autor del llibre.", "Insertar Llibre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Hi ha hagut un problema insertant el llibre.", "Insertar Llibre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else // MODIFICAR LLIBRE
                    {
                        if (llibreDataGridView.CurrentRow != null)
                        {
                            // Recuperem el ID del llibre seleccionat
                            DataGridViewRow filaLlibre = this.llibreDataGridView.Rows[llibreDataGridView.CurrentRow.Index];
                            int idLlibre= (Int32)filaLlibre.Cells[0].Value;
                            // Actualitzem el llibre amb els camps de text
                            string sqlQuery = "UPDATE LLIBRE SET Titol=?, [Any]=?, Editor=?, Colleccio=?, NumEdicio=?, TipusCoberta=?, [DataCompra]=?, NumPagines=?, Comentaris=?, IdIdioma=?, IdLocalitzacio=? WHERE IdLlibre=" + idLlibre;
                            int resultat = bbddConn.UpdateLlibre(sqlQuery, titol, anyDateTimePicker.Value, editor, col, edi, cob, dataCompraDateTimePicker.Value, numPag, coment, idIdioma, idLocal);
                            //
                            if (resultat == 1)
                            {
                                // Eliminem els estils que tenia el llibre
                                resultat = bbddConn.InsUpdDel("DELETE FROM ESTIL_LLIBRE WHERE IdLlibre=" + idLlibre);
                                int resultat2 = bbddConn.InsUpdDel("DELETE FROM AUTOR_LLIBRE WHERE IdLlibre=" + idLlibre);
                                if (resultat == 1 && resultat2 == 1)
                                {
                                    // Insertem l'autor
                                    sqlQuery = "INSERT INTO AUTOR_LLIBRE (IdAutor, IdLlibre) VALUES (" + idAutor + ", " + idLlibre + ")";
                                    resultat = bbddConn.InsUpdDel(sqlQuery);
                                    if (resultat == 1)
                                    {
                                        // Insertem estils si n'hi ha
                                        if (estilLlibreTextBox.Text.Length != 0)
                                        {
                                            bool resultatProv = false;
                                            foreach (string estil in EstilsLlibreSelec)
                                            {
                                                foreach (DataRow filaEstil in DtEstil.Rows)
                                                {
                                                    if (filaEstil["Estil"].ToString() == estil)
                                                    {
                                                        int idEstil = (Int32)filaEstil["IdEstil"];
                                                        resultat = bbddConn.InsUpdDel("INSERT INTO ESTIL_LLIBRE (IdEstil, IdLlibre) VALUES (" + idEstil + ", " + idLlibre + ")");
                                                        if (resultat == 1)
                                                        {
                                                            resultatProv = true;
                                                        }
                                                        else
                                                        {
                                                            resultatProv = false;
                                                        }
                                                    }
                                                }
                                            }
                                            if (resultatProv)
                                            {
                                                MessageBox.Show("Llibre, autor i estils modificats correctament.", "Insertar Llibre", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                            else
                                            {
                                                MessageBox.Show("Hi ha hagut un problema modificant els estils del llibre.", "Insertar Llibre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Llibre i autor modificats correctament.", "Insertar Llibre", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Hi ha hagut un problema modificant l'autor del llibre.", "Insertar Llibre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    if (resultat != 1)
                                    {
                                        MessageBox.Show("Hi ha hagut un problema al modificar els estils del llibre.", "Modificar Llibre", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Hi ha hagut un problema al modificar l'autor del llibre.", "Modificar Llibre", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Hi ha hagut un problema modificant el llibre.", "Modificar Llibre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Fa falta com a mínim el títol del llibre.", "Introduïr / modificar Llibre.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            //Habiltem / Deshabilitem camps.
            this.cbLocalitzacio.Enabled = false;
            this.comentarisTextBox.ReadOnly = true;
            //this.tlpLlibres7.Enabled = false;
            this.tlpLlibres8.Enabled = false;
            this.tplTitol.Enabled = false;
            //
            btAfegirLlibres.Visible = true;
            btModificarLlibres.Visible = true;
            btEliminarLlibres.Visible = true;
            btTornarLlibres.Visible = true;
            //
            btCancelarLlibres.Visible = false;
            btGuardarLlibres.Visible = false;
            //
            llibreDataGridView.Enabled = true;
            // Netegem camps
            netejaCampsLlibre();
            // Actualitzem el grid d'estils
            ActualGridLlibre();
            //
            afegirLlibre = false;
            editantLlibre = false;
        }
        private void BtCancelarLlibres_Click(object sender, EventArgs e)
        {
            llibreDataGridView.CurrentCell = null;
            //
            //Habiltem / Deshabilitem camps.
            this.cbLocalitzacio.Enabled = false;
            this.comentarisTextBox.ReadOnly = true;
            //this.tlpLlibres7.Enabled = false;
            this.tlpLlibres8.Enabled = false;
            this.tplTitol.Enabled = false;
            btCancelarLlibres.Visible = false;
            btGuardarLlibres.Visible = false;
            //
            btAfegirLlibres.Visible = true;
            btModificarLlibres.Visible = true;
            btEliminarLlibres.Visible = true;
            btTornarLlibres.Visible = true;
            //
            llibreDataGridView.Enabled = true;
            // Netegem camps
            netejaCampsLlibre();
            //
            afegirLlibre = false;
            editantLlibre = false;
        }
        private void BtEliminarLlibres_Click(object sender, EventArgs e)
        {
            if (llibreDataGridView.CurrentRow != null)
            {
                dtLlibresAct = false;
                if (MessageBox.Show("Estàs segur que vols eliminar aquest llibre i totes les seves referencies?", "Elimina", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataGridViewRow filaLlibre = this.llibreDataGridView.Rows[llibreDataGridView.CurrentRow.Index];
                    int IdLlibre = (Int32)filaLlibre.Cells[0].Value;
                    // Eliminem totes les relacions del llibre amb l'autor.
                    int resultat = bbddConn.InsUpdDel("DELETE FROM AUTOR_LLIBRE WHERE IdLlibre=" + IdLlibre);
                    if (resultat == 1)
                    {
                        // Eliminem totes les relacions del llibre amb els estils.
                        resultat = bbddConn.InsUpdDel("DELETE FROM ESTIL_LLIBRE WHERE IdLlibre=" + IdLlibre);
                        if (resultat == 1)
                        {
                            // Eliminem el llibre.
                            resultat = bbddConn.InsUpdDel("DELETE FROM LLIBRE WHERE IdLlibre=" + IdLlibre);
                            if (resultat == 1)
                            {
                                MessageBox.Show("Llibre eliminat correctament, així com totes les seves referències a l'autor i als estils.", "Eliminar Llibre", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Hi ha hagut un problema eliminant el registre.", "Eliminar Llibre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Hi ha hagut un problema eliminant els estils del llibre.", "Eliminar Llibre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hi ha hagut un problema eliminant el llibre de l'autor.", "Eliminar Llibre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                llibreDataGridView.CurrentCell = null;
                //
                //Habiltem / Deshabilitem camps.
                this.cbLocalitzacio.Enabled = false;
                this.comentarisTextBox.ReadOnly = true;
                //this.tlpLlibres7.Enabled = false;
                this.tlpLlibres8.Enabled = false;
                this.tplTitol.Enabled = false;
                btCancelarLlibres.Visible = false;
                btGuardarLlibres.Visible = false;
                //
                btAfegirLlibres.Visible = true;
                btModificarLlibres.Visible = true;
                btEliminarLlibres.Visible = true;
                btTornarLlibres.Visible = true;
                //
                llibreDataGridView.Enabled = true;
                // Netegem camps
                netejaCampsLlibre();
                // Actualitzem el grid d'estils
                ActualGridLlibre();
                CarregarCombos();
                //
                afegirLlibre = false;
                editantLlibre = false;
            }
            else
            {
                MessageBox.Show("No tens cap llibre seleccionat.", "Eliminar llibre", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btTornarLlibres_Click(object sender, EventArgs e)
        {
            this.tbPrincipal.SelectedTab = tbInici;
        }
        private void EstilLlibreTextBox_Click(object sender, EventArgs e)
        {
            if (editantLlibre)
            {
                DialogResult dr = new DialogResult();
                Estils formEstils;
                //
                if (afegirLlibre)
                {
                    formEstils = new Estils(DtEstil, true, EstilsLlibreSelec);
                }
                else
                {
                    // Generem la finestra d'estils amb els estils seleccionats.
                    formEstils = new Estils(DtEstil, false, EstilsLlibreSelec);

                
                }
                dr = formEstils.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    // Guardem els estils seleccionats a la classe.
                    EstilsLlibreSelec = formEstils.nousEstils;
                    formEstils.Dispose();
                    // Mostrem els estils en el camp de text.
                    estilLlibreTextBox.Clear();
                    foreach (string estil in EstilsLlibreSelec)
                    {
                        estilLlibreTextBox.AppendText(estil);
                        estilLlibreTextBox.AppendText(Environment.NewLine);
                    }
                }
            }
        }
        /*
         * AUTORS
         */
        private void AutorsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dtAutorsAct && autorsDataGridView.CurrentRow != null)
            {
                netejaCampsAutor();
                DataGridViewRow filaAutor = this.autorsDataGridView.Rows[autorsDataGridView.CurrentRow.Index];
                //AUTORS
                nomTextBox.Text = filaAutor.Cells[1].Value.ToString();
                cognomsTextBox.Text = filaAutor.Cells[2].Value.ToString();
                nacionalitatTextBox.Text = filaAutor.Cells[3].Value.ToString();
                dataNaixementDateTimePicker.Text = filaAutor.Cells[4].Value.ToString();
                comentarisAutorsTextBox.Text = filaAutor.Cells[5].Value.ToString();
                // ESTIL
                estilAutorsTextBox.Clear();
                EstilsAutorSelec.Clear();
                //
                if (filaAutor.Cells[0].Value != DBNull.Value)
                { 
                    if ((Int32)filaAutor.Cells[0].Value != 0)
                    {
                        foreach (DataRow filaEstilAutor in DtEstilAutor.Rows)
                        {
                            if ((Int32)filaAutor.Cells[0].Value == (Int32)filaEstilAutor["IdAutor"])
                            {
                                foreach (DataRow filaEstil in DtEstil.Rows)
                                {
                                    if ((Int32)filaEstilAutor["IdEstil"] == (Int32)filaEstil["IdEstil"])
                                    {
                                        estilAutorsTextBox.AppendText(filaEstil["Estil"].ToString());
                                        estilAutorsTextBox.AppendText(Environment.NewLine);
                                        EstilsAutorSelec.Add(filaEstil["Estil"].ToString());
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void BtAfegirAutors_Click(object sender, EventArgs e)
        {
            //Habiltem / Deshabilitem camps.
            nomTextBox.Enabled = true;
            cognomsTextBox.Enabled = true;
            nacionalitatTextBox.Enabled = true;
            dataNaixementDateTimePicker.Enabled = true;
            comentarisAutorsTextBox.ReadOnly = false;
            //this.tlpAutors7.Enabled = true;
            //
            btAfegirAutors.Visible = false;
            btModificarAutors.Visible = false;
            btEliminarAutors.Visible = false;
            btTornarAutors.Visible = false;
            //
            btCancelarAutors.Visible = true;
            btGuardarAutors.Visible = true;
            //
            autorsDataGridView.Enabled = false;
            // Netegem camps
            netejaCampsAutor();
            // Portem el punter en el primer camp d'edició.
            nomTextBox.Focus();
            //
            afegirAutor = true;
            editantAutor = true;
        }
        private void BtModificarAutors_Click(object sender, EventArgs e)
        {
            if (autorsDataGridView.CurrentRow != null)
            {
                //Habiltem / Deshabilitem camps.
                nomTextBox.Enabled = true;
                cognomsTextBox.Enabled = true;
                nacionalitatTextBox.Enabled = true;
                dataNaixementDateTimePicker.Enabled = true;
                comentarisAutorsTextBox.ReadOnly = false;
                //this.tlpAutors7.Enabled = true;
                //
                btAfegirAutors.Visible = false;
                btModificarAutors.Visible = false;
                btEliminarAutors.Visible = false;
                btTornarAutors.Visible = false;
                //
                btCancelarAutors.Visible = true;
                btGuardarAutors.Visible = true;
                //
                autorsDataGridView.Enabled = false;
                // Portem el punter en el primer camp d'edició.
                nomTextBox.Focus();
                editantAutor = true;
            }
            else
            {
                MessageBox.Show("No tens cap autor seleccionat.", "Modificar Autor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void BtGuardarAutors_Click(object sender, EventArgs e)
        {
            if (dtAutorsAct)
            {
                //Assegurem contigut a nom o cognoms.
                if (nomTextBox.Text.Length != 0 && cognomsTextBox.Text.Length != 0)
                {
                    if (afegirAutor) // GUARDAR AUTOR
                    {
                        // Definim valors per defecte per l'insert.
                        string nom = " ";
                        string cognoms = " ";
                        string nacionalitat = " ";
                        string comentaris = " ";
                        // Mirem si s'ha definit el nom
                        if (nomTextBox.Text.Length != 0)
                        {
                            nom = nomTextBox.Text;
                        }
                        // Mirem si s'ha definit el nom
                        if (cognomsTextBox.Text.Length != 0)
                        {
                            cognoms = cognomsTextBox.Text;
                        }
                        // Mirem si s'ha definit el nom
                        if (nacionalitatTextBox.Text.Length != 0)
                        {
                            nacionalitat = nacionalitatTextBox.Text;
                        }
                        // Mirem si s'ha definit algun comentari
                        if (comentarisAutorsTextBox.Text.Length != 0)
                        {
                            comentaris = comentarisAutorsTextBox.Text;
                        }
                        string sqlQuery = "INSERT INTO AUTORS (Nom, Cognoms, Nacionalitat, DataNaixement, Comentaris) VALUES (?, ?, ?, ?, ?)";
                        int idAutor = bbddConn.InsertAutor(sqlQuery,nom,cognoms,nacionalitat,dataNaixementDateTimePicker.Value,comentaris);
                        if (idAutor != 0)
                        {
                            if (estilAutorsTextBox.Text.Length != 0)
                            {
                                //int idAutor = bbddConn.GetLastIndex("AUTORS");
                                bool resultatProv = false;
                                foreach (string estil in EstilsAutorSelec)
                                {
                                    foreach (DataRow filaEstil in DtEstil.Rows)
                                    {
                                        if (filaEstil["Estil"].ToString() == estil)
                                        {
                                            int idEstil = (Int32)filaEstil["IdEstil"];
                                            int resultat = bbddConn.InsUpdDel("INSERT INTO ESTIL_AUTOR (IdEstil, IdAutor) VALUES (" + idEstil + ", " + idAutor + ")");
                                            if (resultat == 1)
                                            {
                                                resultatProv = true;
                                            }
                                            else
                                            {
                                                resultatProv = false;
                                            }
                                        }
                                    }
                                }
                                if (resultatProv)
                                {
                                    MessageBox.Show("Autor afegit correctament.", "Insertar Autor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Hi ha hagut un problema insertant els estils de l'autor.", "Insertar Autor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Autor afegit correctament.", "Insertar Autor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Hi ha hagut un problema insertant l'autor.", "Insertar Autor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else // MODIFICAR AUTOR
                    {
                        if (autorsDataGridView.CurrentRow != null)
                        {
                            DataGridViewRow filaAutor = this.autorsDataGridView.Rows[autorsDataGridView.CurrentRow.Index];
                            int idAutor = (Int32)filaAutor.Cells[0].Value;
                            //
                            string sqlQuery = "UPDATE AUTORS SET Nom=?, Cognoms=?, Nacionalitat=?, DataNaixement=?, Comentaris=? WHERE IdAutor=" + idAutor; 
                            int resultat = bbddConn.UpdateAutor(sqlQuery, nomTextBox.Text, cognomsTextBox.Text, nacionalitatTextBox.Text, dataNaixementDateTimePicker.Value, comentarisAutorsTextBox.Text);
                            //
                            if (resultat == 1)
                            {
                                resultat = bbddConn.InsUpdDel("DELETE FROM ESTIL_AUTOR WHERE IdAutor=" + idAutor);
                                if (resultat == 1)
                                {
                                    if (estilAutorsTextBox.Text.Length != 0)
                                    {
                                        bool resultatProv = false;
                                        foreach (string estil in EstilsAutorSelec)
                                        {
                                            int idEstil = bbddConn.GetIdEstil(estil);
                                            resultat = bbddConn.InsUpdDel("INSERT INTO ESTIL_AUTOR (IdEstil, IdAutor) VALUES (" + idEstil + ", " + idAutor + ")");                                            
                                            if (resultat == 1)
                                            {
                                                resultatProv = true;
                                            }
                                            else
                                            {
                                                resultatProv = false;
                                            }
                                        }
                                        if (resultatProv)
                                        {
                                            MessageBox.Show("Autor i estils modificats correctament.", "Modificar Autor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else
                                        {
                                            MessageBox.Show("Hi ha hagut un problema al registrar els estils de l'autor.", "Modificar Autor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Autor modificat correctament.", "Modificar Autor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Hi ha hagut un problema al registrar els estils de l'autor.", "Modificar Autor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Hi ha hagut un problema modificant l'autor.", "Modificar Autor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Fa falta com a mínim el nom i el cognom.", "Introduïr / modficar Autor.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            //Habiltem / Deshabilitem camps.
            nomTextBox.Enabled = false;
            cognomsTextBox.Enabled = false;
            nacionalitatTextBox.Enabled = false;
            dataNaixementDateTimePicker.Enabled = false;
            comentarisAutorsTextBox.ReadOnly = true;
            //this.tlpAutors7.Enabled = false;
            //
            btAfegirAutors.Visible = true;
            btModificarAutors.Visible = true;
            btEliminarAutors.Visible = true;
            btTornarAutors.Visible = true;
            //
            btCancelarAutors.Visible = false;
            btGuardarAutors.Visible = false;
            //
            autorsDataGridView.Enabled = true;
            // Netegem camps
            netejaCampsAutor();
            //
            afegirAutor = false;
            editantAutor = false;
            ActualGridAutor();
            CarregarCombos();
        }
        private void BtCancelarAutors_Click(object sender, EventArgs e)
        {
            autorsDataGridView.CurrentCell = null;
            //Habiltem / Deshabilitem camps.
            nomTextBox.Enabled = false;
            cognomsTextBox.Enabled = false;
            nacionalitatTextBox.Enabled = false;
            dataNaixementDateTimePicker.Enabled = false;
            comentarisAutorsTextBox.ReadOnly = true;
            //this.tlpAutors7.Enabled = false;
            //
            btAfegirAutors.Visible = true;
            btModificarAutors.Visible = true;
            btEliminarAutors.Visible = true;
            btTornarAutors.Visible = true;
            //
            btCancelarAutors.Visible = false;
            btGuardarAutors.Visible = false; 
            //
            autorsDataGridView.Enabled = true;
            // Netegem camps
            netejaCampsAutor();
            //
            afegirAutor = false;
            editantAutor = false;
        }
        private void BtEliminarAutors_Click(object sender, EventArgs e)
        {
            if (autorsDataGridView.CurrentRow != null)
            {
                if (dtAutorsAct && autorsDataGridView.CurrentRow != null)
                {
                    if (MessageBox.Show("Estàs segur que vols eliminar aquest autor i totes les seves referències?", "Elimina", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DataGridViewRow filaAutor = this.autorsDataGridView.Rows[autorsDataGridView.CurrentRow.Index];
                        int IdAutor = (Int32)filaAutor.Cells[0].Value;
                        if (IdAutor != 83)
                        {
                            // Eliminem totes les relacions de l'autor amb els llibres.
                            int resultat = bbddConn.InsUpdDel("DELETE FROM AUTOR_LLIBRE WHERE IdAutor=" + IdAutor);
                            if (resultat == 1)
                            {
                                // Eliminem totes les relacions de l'autor amb els estils.
                                resultat = bbddConn.InsUpdDel("DELETE FROM ESTIL_AUTOR WHERE IdAutor=" + IdAutor);
                                if (resultat == 1)
                                {
                                    // Eliminem l'autor.
                                    resultat = bbddConn.InsUpdDel("DELETE FROM AUTORS WHERE IdAutor=" + IdAutor);
                                    if (resultat == 1)
                                    {
                                        MessageBox.Show("Autor eliminat correctament, així com totes les seves referències als llibres i als estils.", "Eliminar Autor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Hi ha hagut un problema eliminant el registre.", "Eliminar Autor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Hi ha hagut un problema eliminant els estils de l'autor.", "Eliminar Autor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Hi ha hagut un problema eliminant l'autor dels llibres.", "Eliminar Autor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No es pot eliminar aquest autor.", "Eliminar Autor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    //Habiltem / Deshabilitem camps.
                    nomTextBox.Enabled = false;
                    cognomsTextBox.Enabled = false;
                    nacionalitatTextBox.Enabled = false;
                    dataNaixementDateTimePicker.Enabled = false;
                    comentarisAutorsTextBox.ReadOnly = true;
                    //this.tlpAutors7.Enabled = false;
                    //
                    btAfegirAutors.Visible = true;
                    btModificarAutors.Visible = true;
                    btEliminarAutors.Visible = true;
                    btTornarAutors.Visible = true;
                    //
                    btCancelarAutors.Visible = false;
                    btGuardarAutors.Visible = false;
                    //
                    autorsDataGridView.Enabled = true;
                    // Netegem camps
                    netejaCampsAutor();
                    // Actualitzem el grid d'estils
                    ActualGridAutor();
                    CarregarCombos();
                    editantAutor = false;
                }
            }
            else
            {
                MessageBox.Show("No tens cap autor seleccionat.", "Eliminar Autor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void BtTornarAutors_Click(object sender, EventArgs e)
        {
            this.tbPrincipal.SelectedTab = tbInici;
        }
        private void estilAutorsTextBox_Click(object sender, EventArgs e)
        {
            if (editantAutor)
            {
                DialogResult dr = new DialogResult();
                Estils formEstils;
                //
                if (afegirAutor)
                {
                    formEstils = new Estils(DtEstil, true, EstilsAutorSelec);
                }
                else
                {
                    // Generem la finestra d'estils amb els estils seleccionats.
                    formEstils = new Estils(DtEstil, false, EstilsAutorSelec);
                }
                dr = formEstils.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    // Guardem els estils seleccionats a la classe.
                    EstilsAutorSelec = formEstils.nousEstils;
                    formEstils.Dispose();
                    // Mostrem els estils en el camp de text.
                    estilAutorsTextBox.Clear();
                    foreach (string estil in EstilsAutorSelec)
                    {
                        estilAutorsTextBox.AppendText(estil);
                        estilAutorsTextBox.AppendText(Environment.NewLine);
                    }
                }
            }
        }
        /*
         * IDIOMES
         */
        private void IdiomaDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dtIdiomesAct && idiomaDataGridView.CurrentRow != null)
            {
                idiomaTextBox.Clear();
                DataGridViewRow filaIdioma = this.idiomaDataGridView.Rows[idiomaDataGridView.CurrentRow.Index];
                idiomaTextBox.Text = filaIdioma.Cells[1].Value.ToString();
            }
        }
        private void BtAfegirIdiomes_Click(object sender, EventArgs e)
        {
            //Habiltem / Deshabilitem camps.
            this.tlpIdioma7.Enabled = true;
            //
            btAfegirIdiomes.Visible = false;
            btModificarIdiomes.Visible = false;
            btEliminarIdiomes.Visible = false;
            btTornarIdiomes.Visible = false;
            //
            btCancelarIdiomes.Visible = true;
            btGuardarIdiomes.Visible = true;
            //
            idiomaDataGridView.Enabled = false;
            // Netegem camps
            idiomaTextBox.Clear();
            // Portem el punter en el primer camp d'edició.
            idiomaTextBox.Focus();
            //
            afegirIdioma = true;
        }
        private void BtModificarIdiomes_Click(object sender, EventArgs e)
        {
            if (idiomaDataGridView.CurrentRow != null)
            {
                //Habiltem / Deshabilitem camps.
                this.tlpIdioma7.Enabled = true;
                //
                btAfegirIdiomes.Visible = false;
                btModificarIdiomes.Visible = false;
                btEliminarIdiomes.Visible = false;
                btTornarIdiomes.Visible = false;
                //
                btCancelarIdiomes.Visible = true;
                btGuardarIdiomes.Visible = true;
                //
                idiomaDataGridView.Enabled = false;
                // Portem el punter en el primer camp d'edició.
                idiomaTextBox.Focus();
            }
            else
            {
                MessageBox.Show("No tens cap idioma seleccionat.", "Modificar Idioma", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void BtGuardarIdiomes_Click(object sender, EventArgs e)
        {
            if (dtIdiomesAct)
            {
                if (idiomaTextBox.Text.Length != 0)
                {
                    if (afegirIdioma) // GUARDAR IDIOMA
                    {
                        int resultat = bbddConn.InsUpdDel("INSERT INTO IDIOMA (Idioma) VALUES ('" + idiomaTextBox.Text + "')");
                        if (resultat == 1)
                        {
                            MessageBox.Show("Idioma insertat correctament.", "Insertar Idioma", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Hi ha hagut un problema insertant l'idioma.", "Insertar Idioma", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else // MODIFICAR IDIOMA
                    {
                        if (idiomaDataGridView.CurrentRow != null)
                        {
                            DataGridViewRow filaIdioma = this.idiomaDataGridView.Rows[idiomaDataGridView.CurrentRow.Index];
                            int IdIdioma = (Int32)filaIdioma.Cells[0].Value;
                            int resultat = bbddConn.InsUpdDel("UPDATE IDIOMA SET Idioma='" + idiomaTextBox.Text + "' WHERE IdIdioma=" + IdIdioma);
                            if (resultat == 1)
                            {
                                MessageBox.Show("Idioma modificat correctament.", "Modificar Idioma", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Hi ha hagut un problema modificant l'idioma.", "Modificar Idioma", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Fa falta com a mínim l'idioma.", "Introduïr / modficar idioma.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            ActualGridIdioma();
            //Habiltem / Deshabilitem camps.
            this.tlpIdioma7.Enabled = false;
            //
            btAfegirIdiomes.Visible = true;
            btModificarIdiomes.Visible = true;
            btEliminarIdiomes.Visible = true;
            btTornarIdiomes.Visible = true;
            //
            btCancelarIdiomes.Visible = false;
            btGuardarIdiomes.Visible = false;
            //
            idiomaDataGridView.Enabled = true;
            // Netegem camps
            idiomaTextBox.Clear();
            //
            afegirIdioma = false;
            CarregarCombos();
        }
        private void BtCancelarIdiomes_Click(object sender, EventArgs e)
        {
            idiomaDataGridView.CurrentCell = null;
            //Habiltem / Deshabilitem camps.
            this.tlpIdioma7.Enabled = false;
            //
            btAfegirIdiomes.Visible = true;
            btModificarIdiomes.Visible = true;
            btEliminarIdiomes.Visible = true;
            btTornarIdiomes.Visible = true;
            //
            btCancelarIdiomes.Visible = false;
            btGuardarIdiomes.Visible = false; 
            //
            idiomaDataGridView.Enabled = true;
            // Netegem camps
            idiomaTextBox.Clear();
            //
            afegirIdioma = false;
        }
        private void BtEliminarIdiomes_Click(object sender, EventArgs e)
        {
            if (idiomaDataGridView.CurrentRow != null)
            {
                if (dtIdiomesAct && idiomaDataGridView.CurrentRow != null)
                {
                    if (MessageBox.Show("Estàs segur que vols eliminar aquest idioma i totes les seves referències?", "Elimina", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DataGridViewRow filaIdioma = this.idiomaDataGridView.Rows[idiomaDataGridView.CurrentRow.Index];
                        int IdIdioma = (Int32)filaIdioma.Cells[0].Value;
                        if (IdIdioma != 10)
                        {

                            // Eliminem l'idioma de tots els llibres
                            int resultat = bbddConn.InsUpdDel("UPDATE LLIBRE SET IdIdioma=" + IdIdiomaDesconegut + " WHERE IdIdioma=" + IdIdioma);
                            if (resultat == 1)
                            {
                                // Eliminem l'idioma
                                resultat = bbddConn.InsUpdDel("DELETE FROM IDIOMA WHERE IdIdioma=" + IdIdioma);
                                if (resultat == 1)
                                {
                                    MessageBox.Show("idioma eliminat correctament, així com totes les seves referències als llibres.", "Eliminar Idioma", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Hi ha hagut un problema eliminant el registre.", "Eliminar Idioma", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Hi ha hagut un problema eliminant l'idioma dels llibres.", "Eliminar Idioma", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No es pot eliminar aquest idioma.", "Eliminar Idioma", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    //Habiltem / Deshabilitem camps.
                    this.tlpIdioma7.Enabled = false;
                    //
                    btAfegirIdiomes.Visible = true;
                    btModificarIdiomes.Visible = true;
                    btEliminarIdiomes.Visible = true;
                    btTornarIdiomes.Visible = true;
                    //
                    btCancelarIdiomes.Visible = false;
                    btGuardarIdiomes.Visible = false;
                    //
                    idiomaDataGridView.Enabled = true;
                    // Netegem camps
                    idiomaTextBox.Clear();
                    // Actualitzem el grid d'estils
                    ActualGridIdioma();
                    CarregarCombos();
                }
            }
            else
            {
                MessageBox.Show("No tens cap idioma seleccionat.", "Eliminar Idioma", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void BtTornarIdiomes_Click(object sender, EventArgs e)
        {
            this.tbPrincipal.SelectedTab = tbInici;
        }
        /*
         * UBICACIONS
         */
        private void localitzacioDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dtUbicacionsAct && localitzacioDataGridView.CurrentRow != null)
            {
                casaTextBox.Clear();
                LlocTextBox.Clear();
                DataGridViewRow filaLocal = this.localitzacioDataGridView.Rows[localitzacioDataGridView.CurrentRow.Index];
                casaTextBox.Text = filaLocal.Cells[1].Value.ToString();
                LlocTextBox.Text = filaLocal.Cells[2].Value.ToString();
            }
        }
        private void btAfegirUbicacions_Click(object sender, EventArgs e)
        {
            //Habiltem / Deshabilitem camps.
            this.tlpUbicacions7.Enabled = true;
            //
            btAfegirUbicacions.Visible = false;
            btModificarUbicacions.Visible = false;
            btEliminarUbicacions.Visible = false;
            btTornarUbicacions.Visible = false;
            //
            btCancelarUbicacions.Visible = true;
            btGuardarUbicacions.Visible = true;
            //
            localitzacioDataGridView.Enabled = false;
            // Netegem camps
            casaTextBox.Clear();
            LlocTextBox.Clear();
            // Portem el punter en el primer camp d'edició.
            casaTextBox.Focus();
            //
            afegirUbicacio = true;
        }
        private void btModificarUbicacions_Click(object sender, EventArgs e)
        {
            if (localitzacioDataGridView.CurrentRow != null)
            {
                //Habiltem / Deshabilitem camps.
                this.tlpUbicacions7.Enabled = true;
                //
                btAfegirUbicacions.Visible = false;
                btModificarUbicacions.Visible = false;
                btEliminarUbicacions.Visible = false;
                btTornarUbicacions.Visible = false;
                //
                btCancelarUbicacions.Visible = true;
                btGuardarUbicacions.Visible = true;
                //
                localitzacioDataGridView.Enabled = false;
                // Portem el punter en el primer camp d'edició.
                casaTextBox.Focus();
            }
            else
            {
                MessageBox.Show("No tens cap ubicació seleccionada.", "Modificar Ubicació", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }
        private void btGuardarUbicacions_Click(object sender, EventArgs e)
        {
            if (dtUbicacionsAct)
            {
                if (casaTextBox.Text.Length != 0)
                {
                    //Assegurem contigut a lloc.
                    string lloc = "";
                    if (LlocTextBox.Text.Length != 0)
                    {
                        lloc = LlocTextBox.Text;
                    }
                    else
                    {
                        lloc = " ";
                    }
                    //
                    if (afegirUbicacio) // GUARDAR UBICACIÓ
                    {
                        int resultat = bbddConn.InsUpdDel("INSERT INTO LOCALITZACIO (Casa, Habitacio) VALUES ('" + casaTextBox.Text + "', '" + lloc.ToString() + "')");
                        if (resultat == 1)
                        {
                            MessageBox.Show("Ubicació insertada correctament.", "Insertar Ubicació", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Hi ha hagut un problema insertant la ubicació.", "Insertar Ubicació", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else // MODIFICAR UBICACIÓ
                    {
                        if (localitzacioDataGridView.CurrentRow != null)
                        {
                            DataGridViewRow filaLocal = this.localitzacioDataGridView.Rows[localitzacioDataGridView.CurrentRow.Index];
                            int IdLocal = (Int32)filaLocal.Cells[0].Value;
                            int resultat = bbddConn.InsUpdDel("UPDATE LOCALITZACIO SET Casa='" + casaTextBox.Text + "', Habitacio='" + lloc.ToString()+ "' WHERE IdLocalitzacio=" + IdLocal);
                            if (resultat == 1)
                            {
                                MessageBox.Show("Ubicació modificada correctament.", "Modificar Ubicació", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Hi ha hagut un problema modificant la ubicació.", "Modificar Ubicació", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Fa falta com a mínim la casa.", "Introduïr / modficar Ubicació.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            ActualGridLocalitzacio();
            //Habiltem / Deshabilitem camps.
            this.tlpUbicacions7.Enabled = false;
            //
            btAfegirUbicacions.Visible = true;
            btModificarUbicacions.Visible = true;
            btEliminarUbicacions.Visible = true;
            btTornarUbicacions.Visible = true;
            //
            btCancelarUbicacions.Visible = false;
            btGuardarUbicacions.Visible = false;
            //
            localitzacioDataGridView.Enabled = true;
            // Netegem camps
            casaTextBox.Clear();
            LlocTextBox.Clear();
            //
            afegirUbicacio= false;
            CarregarCombos();
        }
        private void btCancelarUbicacions_Click(object sender, EventArgs e)
        {
            localitzacioDataGridView.CurrentCell = null;
            //Habiltem / Deshabilitem camps.
            this.tlpUbicacions7.Enabled = false;
            //
            btAfegirUbicacions.Visible = true;
            btModificarUbicacions.Visible = true;
            btEliminarUbicacions.Visible = true;
            btTornarUbicacions.Visible = true;
            //
            btCancelarUbicacions.Visible = false;
            btGuardarUbicacions.Visible = false;
            //
            localitzacioDataGridView.Enabled = true;
            // Netegem camps
            casaTextBox.Clear();
            LlocTextBox.Clear();
            //
            afegirUbicacio = false;
        }
        private void btEliminarUbicacions_Click(object sender, EventArgs e)
        {
            if (localitzacioDataGridView.CurrentRow != null)
            {
                if (dtUbicacionsAct && localitzacioDataGridView.CurrentRow != null)
                {
                    if (MessageBox.Show("Estàs segur que vols eliminar aquesta ubicació i totes les seves referències?", "Elimina", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DataGridViewRow filaLocal = this.localitzacioDataGridView.Rows[localitzacioDataGridView.CurrentRow.Index];
                        int IdLocal = (Int32)filaLocal.Cells[0].Value;
                        if (IdLocal != 54)
                        {
                            // Eliminem la ubicació de tots els llibres
                            int resultat = bbddConn.InsUpdDel("UPDATE LLIBRE SET IdLocalitzacio=" + IdLocalitzacioDesconeguda + " WHERE IdLocalitzacio=" + IdLocal);
                            if (resultat == 1)
                            {
                                // Eliminem la localització
                                resultat = bbddConn.InsUpdDel("DELETE FROM LOCALITZACIO WHERE IdLocalitzacio=" + IdLocal);
                                if (resultat == 1)
                                {
                                    MessageBox.Show("Ubicació eliminada correctament, així com totes les seves referències als llibres.", "Eliminar Ubicació", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Hi ha hagut un problema eliminant el registre.", "Eliminar Ubicació", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Hi ha hagut un problema eliminant la ubicació dels llibres.", "Eliminar Ubicació", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No pots eliminar aquesta ubicació.", "Eliminar Ubicació", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    //Habiltem / Deshabilitem camps.
                    this.tlpUbicacions7.Enabled = false;
                    //
                    btAfegirUbicacions.Visible = true;
                    btModificarUbicacions.Visible = true;
                    btEliminarUbicacions.Visible = true;
                    btTornarUbicacions.Visible = true;
                    //
                    btCancelarUbicacions.Visible = false;
                    btGuardarUbicacions.Visible = false;
                    //
                    localitzacioDataGridView.Enabled = true;
                    // Netegem camps
                    casaTextBox.Clear();
                    LlocTextBox.Clear();
                    // Actualitzem el grid d'estils
                    ActualGridLocalitzacio();
                    CarregarCombos();
                }
            }
            else
            {
                MessageBox.Show("No tens cap ubicació seleccionada.", "Eliminar Ubicació", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btTornarUbicacions_Click(object sender, EventArgs e)
        {
            this.tbPrincipal.SelectedTab = tbInici;
        }
        /*
         * ESTILS
         */
        private void estilDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dtEstilsAct && estilDataGridView.CurrentRow != null)
            {
                estilTextBox.Clear();
                DataGridViewRow filaEstil = this.estilDataGridView.Rows[estilDataGridView.CurrentRow.Index];
                estilTextBox.Text = filaEstil.Cells[1].Value.ToString();
            }
        }
        private void BtAfegirEstils_Click(object sender, EventArgs e)
        {
            //Habiltem / Deshabilitem camps.
            this.tlpEstils7.Enabled = true;
            //
            btAfegirEstils.Visible = false;
            btModificarEstils.Visible = false;
            btEliminarEstils.Visible = false;
            btTornarEstils.Visible = false;
            //
            btCancelarEstils.Visible = true;
            btGuardarEstils.Visible = true;
            //
            estilDataGridView.Enabled = false;
            // Netegem camps
            estilTextBox.Clear();
            // Portem el punter en el primer camp d'edició.
            estilTextBox.Focus();
            //
            afegirEstil = true;
        }
        private void BtModificarEstils_Click(object sender, EventArgs e)
        {
            if (estilDataGridView.CurrentRow != null)
            {
                //Habiltem / Deshabilitem camps.
                this.tlpEstils7.Enabled = true;
                //
                btAfegirEstils.Visible = false;
                btModificarEstils.Visible = false;
                btEliminarEstils.Visible = false;
                btTornarEstils.Visible = false;
                //
                btCancelarEstils.Visible = true;
                btGuardarEstils.Visible = true;
                //
                estilDataGridView.Enabled = false;
                // Portem el punter en el primer camp d'edició.
                estilTextBox.Focus();
            }
            else
            {
                MessageBox.Show("No tens cap estil seleccionat.", "Modificar Estil", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void BtGuardarEstils_Click(object sender, EventArgs e)
        {
            if (dtEstilsAct)
            {
                if (estilTextBox.Text.Length != 0)
                {
                    if (afegirEstil) // GUARDAR ESTIL
                    {
                        int resultat = bbddConn.InsUpdDel("INSERT INTO ESTIL (Estil) VALUES ('" + estilTextBox.Text + "')");
                        if (resultat == 1)
                        {
                            MessageBox.Show("Estil insertat correctament.", "Insertar Estil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Hi ha hagut un problema insertant el registre.", "Insertar Estil", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else // MODIFICAR ESTIL
                    {
                        if (estilDataGridView.CurrentRow != null)
                        {
                            DataGridViewRow filaEstil = this.estilDataGridView.Rows[estilDataGridView.CurrentRow.Index];
                            int IdEstil = (Int32)filaEstil.Cells[0].Value;
                            int resultat = bbddConn.InsUpdDel("UPDATE ESTIL SET Estil='" + estilTextBox.Text + "' WHERE IdEstil=" + IdEstil);
                            if (resultat == 1)
                            {
                                MessageBox.Show("Estil modificat correctament.", "Modificar Estil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Hi ha hagut un problema modificant el registre.", "Modificar Estil", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Fa falta com a mínim l'estil.", "Introduïr / modficar estil.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            ActualGridEstil();
            //Habiltem / Deshabilitem camps.
            this.tlpEstils7.Enabled = false;
            //
            btAfegirEstils.Visible = true;
            btModificarEstils.Visible = true;
            btEliminarEstils.Visible = true;
            btTornarEstils.Visible = true;
            //
            btCancelarEstils.Visible = false;
            btGuardarEstils.Visible = false;
            //
            estilDataGridView.Enabled = true;
            // Netegem camps
            estilTextBox.Clear();
            //
            afegirEstil = false;
            CarregarCombos();
        }
        private void BtCancelarEstils_Click(object sender, EventArgs e)
        {
            estilDataGridView.CurrentCell = null;
            //Habiltem / Deshabilitem camps.
            this.tlpEstils7.Enabled = false;
            //
            btAfegirEstils.Visible = true;
            btModificarEstils.Visible = true;
            btEliminarEstils.Visible = true;
            btTornarEstils.Visible = true;
            //
            btCancelarEstils.Visible = false;
            btGuardarEstils.Visible = false;
            //
            estilDataGridView.Enabled = true;
            // Netegem camps
            estilTextBox.Clear();
            //
            afegirEstil = false;
        }
        private void BtEliminarEstils_Click(object sender, EventArgs e)
        {
            if (estilDataGridView.CurrentRow != null)
            {
                if (dtEstilsAct && estilDataGridView.CurrentRow != null)
                {
                    if (MessageBox.Show("Estàs segur que vols eliminar aquest estil i totes les seves referències?", "Elimina", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DataGridViewRow filaEstil = this.estilDataGridView.Rows[estilDataGridView.CurrentRow.Index];
                        int IdEstil = (Int32)filaEstil.Cells[0].Value;
                        // Eliminem l'estil de tots els llibres
                        int resultat = bbddConn.InsUpdDel("DELETE FROM ESTIL_LLIBRE WHERE IdEstil=" + IdEstil);
                        if (resultat == 1)
                        {
                            // Eliminem l'estil de tots els autors
                            resultat = bbddConn.InsUpdDel("DELETE FROM ESTIL_AUTOR WHERE IdEstil=" + IdEstil);
                            if (resultat == 1)
                            {
                                // Eliminem l'estil
                                resultat = bbddConn.InsUpdDel("DELETE FROM ESTIL WHERE IdEstil=" + IdEstil);
                                if (resultat == 1)
                                {
                                    MessageBox.Show("Estil eliminat correctament, així com totes les seves referències a llibres i autors.", "Eliminar Estil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Hi ha hagut un problema eliminant el registre.", "Eliminar Estil", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Hi ha hagut un problema eliminant l'estil dels autors.", "Eliminar Estil", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Hi ha hagut un problema eliminant l'estil dels llibres.", "Eliminar Estil", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    //Habiltem / Deshabilitem camps.
                    this.tlpEstils7.Enabled = false;
                    //
                    btAfegirEstils.Visible = true;
                    btModificarEstils.Visible = true;
                    btEliminarEstils.Visible = true;
                    btTornarEstils.Visible = true;
                    //
                    btCancelarEstils.Visible = false;
                    btGuardarEstils.Visible = false;
                    //
                    estilDataGridView.Enabled = true;
                    // Netegem camps
                    estilTextBox.Clear();
                    // Actualitzem el grid d'estils
                    ActualGridEstil();
                    CarregarCombos();
                }
            }
            else
            {
                MessageBox.Show("No tens cap estil seleccionat.", "Eliminar Estil", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void BtTornarEstils_Click(object sender, EventArgs e)
        {
            this.tbPrincipal.SelectedTab = tbInici;
        }
        /*
         * LLISTATS
         */
        private void BtTornarLlistats_Click(object sender, EventArgs e)
        {
            this.tbPrincipal.SelectedTab = tbInici;
        }
        /*
         * FUNCIONS 
         */
        private void CarregarCombos()
        {
            // ComboBox Idioma
            cbIdiomaLlibres.DataSource = null;
            cbIdiomaLlibres.Items.Clear();
            cbIdiomaLlibres.Items.Add("");
            foreach (DataRow filaIdioma in DtIdioma.Rows)
            {
                cbIdiomaLlibres.Items.Add(filaIdioma["Idioma"].ToString());
            }
            // ComboBox Localitzacio
            cbLocalitzacio.DataSource = null;
            cbLocalitzacio.Items.Clear();
            cbLocalitzacio.Items.Add("");

            cbLocalitzacio.ValueMember = "IdLocalitzacio";
            cbLocalitzacio.DisplayMember = "Casa";
            cbLocalitzacio.DataSource = DtLocalitzacio;
            //
            cbLocalitzacio.SelectedIndex = cbLocalitzacio.FindStringExact("");
            // Combobox Autor
            cbAutor.DataSource = null;
            cbAutor.Items.Clear();
            cbAutor.Items.Add("");
            foreach (DataRow filaAutor in DtAutor.Rows)
            {
                cbAutor.Items.Add(filaAutor["NomComplet"].ToString());
            }
        }
        private void ActualGridLlibre()
        {
            // Actualitzem el DataTable i el Grid
            dtLlibresAct = false;
            dtEstilsLlibreAct = false;
            //      
            DtLlibre.Clear();
            stringSql = "SELECT * FROM LLIBRE ORDER BY Titol";
            bbddConn.GetData(stringSql,ref DtLlibre);
            //
            DtBusquedaLlibre.Clear();
            //
            DtEstilLlibre.Clear();
            stringSql = "SELECT * FROM ESTIL_LLIBRE";
            bbddConn.GetData(stringSql, ref DtEstilLlibre);
            //
            DtEstilAutor.Clear();
            stringSql = "SELECT * FROM AUTOR_LLIBRE";
            bbddConn.GetData(stringSql, ref DtEstilAutor);
            //
            llibreDataGridView.CurrentCell = null;
            dtEstilsLlibreAct = true;
            dtLlibresAct = true;
            //
            lbLlibresTotalsBiblio.Text = DtLlibre.Rows.Count.ToString();
        }
        private void ActualGridAutor()
        {
            // Actualitzem el DataTable i el Grid
            dtAutorsAct = false;
            DtAutor.Clear();
            stringSql = "SELECT IdAutor, Nom, Cognoms, Nacionalitat, DataNaixement, Comentaris, Nom &' ' & Cognoms AS NomComplet FROM AUTORS ORDER BY Cognoms";
            bbddConn.GetData(stringSql, ref DtAutor);
            //
            DtEstilAutor.Clear();
            stringSql = "SELECT * FROM ESTIL_AUTOR";
            bbddConn.GetData(stringSql, ref DtEstilAutor);
            autorsDataGridView.CurrentCell = null;
            dtAutorsAct = true;
        }
        private void ActualGridIdioma()
        {
            // Actualitzem el DataTable i el Grid
            dtIdiomesAct= false;
            DtIdioma.Clear();
            stringSql = "SELECT * FROM IDIOMA ORDER BY Idioma";
            bbddConn.GetData(stringSql, ref DtIdioma);
            idiomaDataGridView.CurrentCell = null;
            dtIdiomesAct = true;
        }
        private void ActualGridLocalitzacio()
        {
            // Actualitzem el DataTable i el Grid
            dtUbicacionsAct = false;
            DtLocalitzacio.Clear();
            stringSql = "SELECT * FROM LOCALITZACIO ORDER BY Casa";
            bbddConn.GetData(stringSql, ref DtLocalitzacio);
            localitzacioDataGridView.CurrentCell = null;
            dtUbicacionsAct = true;
        }
        private void ActualGridEstil()
        {
            // Actualitzem el DataTable i el Grid
            dtEstilsAct = false;
            DtEstil.Clear();
            stringSql = "SELECT * FROM ESTIL ORDER BY Estil";
            bbddConn.GetData(stringSql, ref DtEstil);
            estilDataGridView.CurrentCell = null;
            dtEstilsAct = true;
        }
        private void RefrescLlibre()
        {
            int IdAutorLlibre = 0;
            string nomAutor = "";
            string idioma = "";
            string casa = "";
            string habitacio = "";
            //
            netejaCampsLlibre();
            //
            if (dtLlibresAct && llibreDataGridView.CurrentRow != null)
            {
                DataGridViewRow filaLlibre = this.llibreDataGridView.Rows[llibreDataGridView.CurrentRow.Index];
                //LLIBRE
                titolTextBox.Text = filaLlibre.Cells[1].Value.ToString();
                anyDateTimePicker.Text = filaLlibre.Cells[2].Value.ToString();
                editorTextBox.Text = filaLlibre.Cells[3].Value.ToString();
                colleccioTextBox.Text = filaLlibre.Cells[4].Value.ToString();
                numEdicioTextBox.Text = filaLlibre.Cells[5].Value.ToString();
                tipusCobertaTextBox.Text = filaLlibre.Cells[6].Value.ToString();
                dataCompraDateTimePicker.Text = filaLlibre.Cells[7].Value.ToString();
                numPaginesTextBox.Text = filaLlibre.Cells[8].Value.ToString();
                comentarisTextBox.Text = filaLlibre.Cells[9].Value.ToString();
                // AUTOR_LLIBRE
                stringSql = "SELECT IdAutor FROM AUTOR_LLIBRE WHERE IdLlibre = " + filaLlibre.Cells[0].Value;
                IdAutorLlibre = bbddConn.GetIdAutor(stringSql);
                // ID AUTOR
                if (IdAutorLlibre != 0)
                {
                    foreach (DataRow filaAutor in DtAutor.Rows)
                    {
                        if (IdAutorLlibre == (Int32)filaAutor["IdAutor"])
                        {
                            nomAutor = filaAutor["NomComplet"].ToString();
                        }
                        // NOM AUTOR
                        cbAutor.SelectedIndex = cbAutor.FindStringExact(nomAutor);
                    }
                }
                // IDIOMA
                if (filaLlibre.Cells[10].Value != DBNull.Value)
                {
                    if ((Int32)filaLlibre.Cells[10].Value != 0)
                    {
                        foreach (DataRow filaIdioma in DtIdioma.Rows)
                        {
                            if ((Int32)filaLlibre.Cells[10].Value == (Int32)filaIdioma["IdIdioma"])
                            {
                                idioma = filaIdioma["Idioma"].ToString();
                            }
                        }
                        // IDIOMA LLIBRE
                        cbIdiomaLlibres.SelectedIndex = cbIdiomaLlibres.FindStringExact(idioma);
                    }
                }
                // ESTIL
                estilLlibreTextBox.Clear();
                EstilsLlibreSelec.Clear();
                if (filaLlibre.Cells[0].Value != DBNull.Value)
                {
                    if ((Int32)filaLlibre.Cells[0].Value != 0)
                    {
                        foreach (DataRow filaEstilLlibre in DtEstilLlibre.Rows)
                        {
                            if ((Int32)filaLlibre.Cells[0].Value == (Int32)filaEstilLlibre["IdLlibre"])
                            {
                                foreach (DataRow filaEstil in DtEstil.Rows)
                                {
                                    if ((Int32)filaEstilLlibre["IdEstil"] == (Int32)filaEstil["IdEstil"])
                                    {
                                        estilLlibreTextBox.AppendText(filaEstil["Estil"].ToString());
                                        estilLlibreTextBox.AppendText(Environment.NewLine);
                                        EstilsLlibreSelec.Add(filaEstil["Estil"].ToString());
                                    }
                                }
                            }
                        }
                    }
                }
                //LOCALITZACIO
                if (filaLlibre.Cells[11].Value != DBNull.Value)
                {
                    if ((Int32)filaLlibre.Cells[11].Value != 0)
                    {
                        foreach (object item in cbLocalitzacio.Items)
                        {
                            DataRowView filaLocalitzacio = item as DataRowView;
                            if (filaLocalitzacio != null)
                            {
                                if ((Int32)filaLocalitzacio["IdLocalitzacio"] == (Int32)filaLlibre.Cells[11].Value)
                                {
                                    casa = filaLocalitzacio["Casa"].ToString();
                                    habitacio = filaLocalitzacio["Habitacio"].ToString();
                                    IdLocalActiu = (Int32)filaLocalitzacio["IdLocalitzacio"];
                                    //
                                    cbLocalSelectedIndex = cbLocalitzacio.Items.IndexOf(item);
                                }
                            }
                            else
                            {
                                // error: item was not of type DataRowView
                            }
                        }
                    }
                }
                // LOCALITZACIO LLIBRE
                cbLocalitzacio.SelectedIndex = cbLocalSelectedIndex;
                habitacioTextBox.Text = habitacio;
            }
        }
        private void netejaCampsLlibre()
        {
            // Netegem camps
            titolTextBox.Clear();
            editorTextBox.Clear();
            colleccioTextBox.Clear();
            numEdicioTextBox.Clear();
            tipusCobertaTextBox.Clear();
            numPaginesTextBox.Clear();
            habitacioTextBox.Clear();
            estilLlibreTextBox.Clear();
            comentarisTextBox.Clear();
            //
            cbAutor.SelectedIndex = cbAutor.FindStringExact("");
            cbIdiomaLlibres.SelectedIndex = cbAutor.FindStringExact("");
            cbLocalitzacio.SelectedIndex = cbLocalitzacio.FindStringExact("");
            //
            anyDateTimePicker.Value = DateTime.Today;
            dataCompraDateTimePicker.Value = DateTime.Today;
        }
        private void netejaCampsAutor()
        {
            // Netegem camps
            nomTextBox.Clear();
            cognomsTextBox.Clear();
            nacionalitatTextBox.Clear();
            dataNaixementDateTimePicker.Value = DateTime.Today;
            estilAutorsTextBox.Clear();
            comentarisAutorsTextBox.Clear();
        }
    }
}