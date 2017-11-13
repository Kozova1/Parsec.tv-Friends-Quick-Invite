using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace Parsec_Friends_Quick_Invite
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked){
                string args = txt_Args.Text;
                makeFriendFile(ref args);
            }
            else{
                string args = txt_Args.Text;
                string ySID = txt_YourSID.Text;
                makeSelfFile(ref args, ySID);
            }
        }

        void makeSelfFile(ref string args, string ySID) {
            // Create a new WebClient instance.
            string myStringWebResource = null;
            string[] installerCode = { @"bitsadmin.exe /transfer JobName https://s3.amazonaws.com/parsec-build/package/parsec-windows.exe  %TMP%\ParsecInstaller.exe ", @"start /wait %TMP%\ParsecInstaller.exe", @"echo " + args + @">> %AppData%\Parsec\config.txt", @"start "" parsec:server_id=" + ySID,  };
            using (System.IO.StreamWriter file =
           new System.IO.StreamWriter(@"Installer.bat"))
            {
                foreach (string line in installerCode)
                {
                    file.WriteLine(line);
                }
            }
            MessageBox.Show("Share the Installer.bat file located in the directory from which you ran this program with whoever you want to invite");
        }

        void makeFriendFile(ref string args) {
            // Create a new WebClient instance.
            string myStringWebResource = null;
            File.Create("Installer.bat");
            string[] installerCode = { @"bitsadmin.exe /transfer JobName https://s3.amazonaws.com/parsec-build/package/parsec-windows.exe  %TMP%\ParsecInstaller.exe ", @"start /wait %TMP%\ParsecInstaller.exe", @"echo app_host=1 > %AppData%\Parsec\config.txt", @"echo app_host_first_run=0 >> %AppData%\Parsec\config.txt",@"echo " + args + @">> %AppData%\Parsec\config.txt" };
            using (System.IO.StreamWriter file =
           new System.IO.StreamWriter(System.Environment.GetEnvironmentVariable("USERPROFILE") + @".\Installer.bat"))
            {
                foreach (string line in installerCode)
                {
                    file.WriteLine(line);
                }
            }
            MessageBox.Show("Share the Installer.bat file located in the directory from which you ran this program with whoever you want to invite");
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e) { }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            if (txt_sIDToConnect != null){
                Int32.TryParse(txt_sIDToConnect.Text, out int serverid);
                System.Diagnostics.Process.Start("parsec:server_id="+ txt_sIDToConnect.Text + " " + txt_Args.Text);
            }
        }
    }
}
