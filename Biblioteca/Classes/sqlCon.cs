using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using System.Linq.Expressions;

namespace Biblioteca.Classes
{    
    public class SqlCon
    {
        /*
         * VARIABLES CLASSE
         */
        OleDbConnection con = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = Resources/Biblioteca2020.accdb");
        /*
         * CONSULTES
         */
        //RETORNA UN DATAGRID
        public bool GetData(string sqlQuery, ref DataTable dt)
        {
            try 
            {
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlQuery;
                cmd.ExecuteNonQuery();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                //dataGrid.DataSource = dt;
                con.Close();
                return true;
            }
            catch (Exception e)
            {
                con.Close();
                MessageBox.Show(e.Message, "Error en l'aplicatiu.",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        // RETORNA L'ID DE L'AUTOR
        public int GetIdAutor(string sqlQuery)
        {
            int IdAutor = 0;
            try
            {
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlQuery;
                IdAutor = (Int32)cmd.ExecuteScalar();
                con.Close();
                return IdAutor;
            }
            catch (Exception e)
            {
                con.Close();
                MessageBox.Show(e.Message, "Error en l'aplicatiu.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return IdAutor;
            }
        }
        // RETORNA EL ID DE LA TAULA ESTIL EN FUNCIÓ D'UN ESTIL
        public int GetIdEstil(string estil)
        {
            try
            {
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT IdEstil FROM ESTIL WHERE Estil='"+ estil + "'";
                int id = (Int32)cmd.ExecuteScalar();
                con.Close();
                return id;
            }
            catch (Exception e)
            {
                con.Close();
                MessageBox.Show(e.Message, "Error en l'aplicatiu.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }
        /*
         * INSERT / UPDATE / DELETE
         */
        // FUNCIÓ GENÈRICA DE MODIFICACIÓ DE TAULA SENSE RETORN       
        public int InsUpdDel(string sqlQuery)
        {
            try
            {
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlQuery;
                cmd.ExecuteNonQuery();
                con.Close();
                return 1;
            }
            catch (Exception e)
            {
                con.Close();
                MessageBox.Show(e.Message, "Error en l'aplicatiu.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }
        // INSERTAR AUTOR PER PARAMETRE RECUPERANT EL ID ASSIGNAT PER LA BBDD
        public int InsertAutor(string sqlQuery, string nom, string cognoms, string nacionalitat, DateTime data, string comentaris)
        {
            try
            {
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlQuery;
                cmd.Parameters.Add("p1", OleDbType.VarChar, 50).Value = nom;
                cmd.Parameters.Add("p2", OleDbType.VarChar, 50).Value = cognoms;
                cmd.Parameters.Add("p3", OleDbType.VarChar, 50).Value = nacionalitat;
                cmd.Parameters.Add("p4", OleDbType.DBDate).Value = data;
                cmd.Parameters.Add("p5", OleDbType.VarChar, 50).Value = comentaris;
                //
                cmd.ExecuteNonQuery();
                //
                cmd.CommandText = "SELECT @@IDENTITY FROM AUTORS";
                int idAutor = (Int32)cmd.ExecuteScalar();
                con.Close();
                return idAutor;
            }
            catch (Exception e)
            {
                con.Close();
                MessageBox.Show(e.Message, "Error en l'aplicatiu.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }
        // INSERTAR LLIBRE PER PARAMETRE RECUPERANT EL ID ASSIGNAT PER LA BBDD
        public int InsertLlibre(string sqlQuery, string titol, DateTime any, string editor, string col, int numEdicio, string tipusCoberta, DateTime dataCompra, int numPag, string comentaris, int idIdioma, int idLocal)
        {
            try
            {
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlQuery;
                //
                cmd.Parameters.Add("p1", OleDbType.VarChar, 50).Value = titol;
                cmd.Parameters.Add("p2", OleDbType.DBDate).Value = any;
                cmd.Parameters.Add("p3", OleDbType.VarChar, 50).Value = editor;
                cmd.Parameters.Add("p4", OleDbType.VarChar, 50).Value = col;
                cmd.Parameters.Add("p5", OleDbType.Integer).Value = numEdicio;
                cmd.Parameters.Add("p6", OleDbType.VarChar, 50).Value = tipusCoberta;
                cmd.Parameters.Add("p7", OleDbType.DBDate).Value = dataCompra;
                cmd.Parameters.Add("p8", OleDbType.Integer).Value = numPag;
                cmd.Parameters.Add("p9", OleDbType.VarChar, 50).Value = comentaris;
                cmd.Parameters.Add("p10", OleDbType.Integer).Value = idIdioma;
                cmd.Parameters.Add("p11", OleDbType.Integer).Value = idLocal;
                //
                cmd.ExecuteNonQuery();
                //
                cmd.CommandText = "SELECT @@IDENTITY FROM LLIBRE";
                int idLlibre = (Int32)cmd.ExecuteScalar();
                con.Close();
                return idLlibre;
            }
            catch (Exception e)
            {
                con.Close();
                MessageBox.Show(e.Message, "Error en l'aplicatiu.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }
        // ACTUALITZAR AUTOR PER PARAMETRE
        public int UpdateAutor(string sqlQuery, string nom, string cognoms, string nacionalitat, DateTime data, string comentaris)
        {
            try
            {
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlQuery;
                cmd.Parameters.Add("p1", OleDbType.VarChar, 50).Value = nom;
                cmd.Parameters.Add("p2", OleDbType.VarChar, 50).Value = cognoms;
                cmd.Parameters.Add("p3", OleDbType.VarChar, 50).Value = nacionalitat;
                cmd.Parameters.Add("p4", OleDbType.DBDate).Value = data;
                cmd.Parameters.Add("p5", OleDbType.VarChar, 50).Value = comentaris;
                //
                cmd.ExecuteNonQuery();
                con.Close();
                return 1;
            }
            catch (Exception e)
            {
                con.Close();
                MessageBox.Show(e.Message, "Error en l'aplicatiu.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }
        // ACTUALITZAR LLIBRE PER PARAMETRE
        public int UpdateLlibre(string sqlQuery, string titol, DateTime any, string editor, string col, int numEdicio, string tipusCoberta, DateTime dataCompra, int numPag, string comentaris, int idIdioma, int idLocal)
        {
            try
            {
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlQuery;
                cmd.Parameters.Add("p1", OleDbType.VarChar, 50).Value = titol;
                cmd.Parameters.Add("p2", OleDbType.DBDate).Value = any;
                cmd.Parameters.Add("p3", OleDbType.VarChar, 50).Value = editor;
                cmd.Parameters.Add("p4", OleDbType.VarChar, 50).Value = col;
                cmd.Parameters.Add("p5", OleDbType.Integer).Value = numEdicio;
                cmd.Parameters.Add("p6", OleDbType.VarChar, 50).Value = tipusCoberta;
                cmd.Parameters.Add("p7", OleDbType.DBDate).Value = dataCompra;
                cmd.Parameters.Add("p8", OleDbType.Integer).Value = numPag;
                cmd.Parameters.Add("p9", OleDbType.VarChar, 50).Value = comentaris;
                cmd.Parameters.Add("p10", OleDbType.Integer).Value = idIdioma;
                cmd.Parameters.Add("p11", OleDbType.Integer).Value = idLocal;
                //
                cmd.ExecuteNonQuery();
                con.Close();
                return 1;
            }
            catch (Exception e)
            {
                con.Close();
                MessageBox.Show(e.Message, "Error en l'aplicatiu.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }
    }
}

