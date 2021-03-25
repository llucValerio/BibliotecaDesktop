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
        // *******************************************************************
        // ************************ VARIABLES CLASSE *************************
        // *******************************************************************
        OleDbConnection con = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = BBDD/Biblioteca2020.accdb");
        // *******************************************************************
        // **************************** CONSULTES ****************************
        // *******************************************************************
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
        public int GetLastIndex(string taula)
        {
            try
            {
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT @@IDENTITY FROM " + taula;
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
        public bool GetEstilAutorNou(int idEstil, int idAutor)
        {
            try
            {
                con.Open();
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM ESTIL_AUTOR WHERE IdEstil=" + idEstil + " AND IdAutor=" + idAutor;
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                if (rowsAffected > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                con.Close();
                MessageBox.Show(e.Message, "Error en l'aplicatiu.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        // *******************************************************************
        // ******************* INSERT / UPDATE / DELETE **********************
        // *******************************************************************
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
    }
}
