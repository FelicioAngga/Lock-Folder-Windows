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
    public partial class Lock : Form
    {
        string enp = ".{2559a1f2-21d7-11d4-bdaf-00c04f60b9f0}";
        public string  initPath { get; set; }
        public Lock(string[] args)
        {
            InitializeComponent();
            if(args.Length > 0)
                initPath = args[0];
        }

        private void pnlClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLock_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbPass.Text)) return;
            var path = initPath;

            DirectoryInfo d = new DirectoryInfo(path);
            d.MoveTo(path + enp);

            var con = new Connection();
            if(LoadPass().Count <= 0)
            {
                con.Query("INSERT INTO tbl_pass (password, folderName) VALUES (@password, @folderName)");
                con.cmd.Parameters.AddWithValue("@password", HashPassword.hashPassword(tbPass.Text));
                con.cmd.Parameters.AddWithValue("@folderName", path);
                con.NonQueryEx();
            }
            else
            {
                con.Query("UPDATE tbl_pass SET password = @password, folderName = @folderName");
                con.cmd.Parameters.AddWithValue("@password", HashPassword.hashPassword(tbPass.Text));
                con.cmd.Parameters.AddWithValue("@folderName", path);
                con.NonQueryEx();
            }
            con.CloseConnection();

            this.Close();
        }

        private DataRowCollection LoadPass()
        {
            var con = new Connection();
            con.Query("SELECT * FROM tbl_pass");
            con.CloseConnection();
            return con.QueryEx().Rows;
        }

        private void Lock_Load(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(initPath))
            {
                if(initPath.LastIndexOf(".{") == -1)
                {
                }
                else
                {
                    var unlock = new Unlock(initPath);
                    unlock.ShowDialog();
                    this.Close();
                }
            }
        }
    }
}
