using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlaceHolder
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Classes\\Folder\\shell", true);
            var subkeys = rk.GetSubKeyNames().ToList();
            if(subkeys.Count == 0)
            {
                RegistryKey myfolder = rk.CreateSubKey("Lock/Unlock Folder", true);
                var inner = myfolder.CreateSubKey("command", true);
                inner.SetValue("", $"\"{Application.ExecutablePath}\" \"%1\"");
            }
            foreach (var item in subkeys)
            {
                if(item == "Lock/Unlock Folder")
                {

                }
                else
                {
                    RegistryKey myfolder = rk.CreateSubKey("Lock/Unlock Folder", true);
                    var inner = myfolder.CreateSubKey("command", true);
                    inner.SetValue("", $"\"{Application.ExecutablePath}\" \"%1\"");
                }
            }

            Application.Run(new Lock(args));
        }
    }
}
