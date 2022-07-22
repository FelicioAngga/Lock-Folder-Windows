using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlaceHolder
{
    public partial class Unlock : Form
    {
        public string initPath = "";
        public Unlock(string initPath)
        {
            InitializeComponent();
            this.initPath = initPath;
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            if(LoadPass().Count > 0)
            {
                if(HashPassword.verifyPassword(tbPass.Text, LoadPass()[0]["password"].ToString()))
                {
                    DirectoryInfo d = new DirectoryInfo(initPath);
                    d.MoveTo(LoadPass()[0]["folderName"].ToString());
                    this.Close();
                }
            }
        }

        private DataRowCollection LoadPass()
        {
            var con = new Connection();
            con.Query("SELECT * FROM tbl_pass");
            con.CloseConnection();
            return con.QueryEx().Rows;
        }

        private void pnlClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
