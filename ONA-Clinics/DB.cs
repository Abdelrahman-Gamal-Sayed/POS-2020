using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
using System.Data.SQLite;
using System.IO;

namespace ONA_Stores
{
    class DB
    {
        public static string connectionStr = @"Data Source=" + File.ReadAllText("DbConnection.txt").Trim();

        //  public static string connectionStr = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=hoData;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //connection

        SQLiteConnection conn = new SQLiteConnection(connectionStr);
        //queries


        public SQLiteCommand cmd = new SQLiteCommand();

        public void SetCommand(string SQLStatement)
        {
            try
            {

                // cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = SQLStatement;
            }
            catch { } }

        public  bool RunNonQuery(string SQLStatement, string Message = "")
        {
            bool test = false;
            try
            {
                SetCommand(SQLStatement);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Open();
                }
                else if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                //conn.Open();
                //bnfz el queries
                 cmd.ExecuteNonQuery();
                if (Message != "")
                {
                    MessageBox.Show(Message);
                    test = true;
                }
                return test;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
                return test;
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable RunReader(string Selectstatement)
        {
            // return data in tables
            DataTable tbl = new DataTable();
            SetCommand(Selectstatement);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Open();
            }
            else if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                //read from database
                tbl.Load( cmd.ExecuteReader());
                conn.Close();
                return tbl;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
                return tbl;
            }

        }

     

        /*
        private void NumericOnly(System.Object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9.]+");
            e.Handled = reg.IsMatch(e.Text);

        }
        private void NumberOnly(System.Object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9]");
            e.Handled = reg.IsMatch(e.Text);

        }
        */
     
      
    }
}
