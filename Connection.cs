using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlaceHolder
{
    public class Connection
    {
        public SQLiteConnection con;
        public SQLiteCommand cmd;
        SQLiteDataAdapter da;
        DataTable dt;
        DataSet ds;
        private int scollVal = 0;
        private int temp = 0;

        public Connection()
        {
            con = new SQLiteConnection(LoadConnectionString(), false);
            OpenConnection();
        }

        public void OpenConnection()
        {
            if (con.State != System.Data.ConnectionState.Open)
                con.Open();
        }

        public void CloseConnection()
        {
            if (con.State != System.Data.ConnectionState.Closed)
                con.Close();
        }

        public void Query(string query)
        {
            cmd = new SQLiteCommand(query, con);
        }

        public DataTable QueryEx()
        {
            da = new SQLiteDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public void NonQueryEx()
        {
            cmd.ExecuteNonQuery();
        }

        public string NonQueryExRet()
        {
            return cmd.ExecuteScalar().ToString();
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return $@"Data Source= {Application.StartupPath}\Lock.db; Version=3;";
            //return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
