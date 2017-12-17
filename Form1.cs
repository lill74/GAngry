using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GAngry
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "GMAD Path : " + Properties.Settings.Default.Path;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Please select gmad.exe";
            ofd.FileName = "gmad.exe";

            DialogResult dr = ofd.ShowDialog();


            if (dr == DialogResult.OK)
            {
                textBox1.Text = "GMAD Path : " + Properties.Settings.Default.Path;
                Properties.Settings.Default.Path = ofd.FileName;
                Properties.Settings.Default.Save();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(Path.GetExtension(Properties.Settings.Default.Path) != ".exe")
            {
                textBox1.Text = "Wrong GMAD setup";
                return;
            }
                if (radioButton1.Checked)
                {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Please select GMA file";

                DialogResult dr = ofd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    textBox1.Text = Excute("extract -file " + ofd.FileName);
                }
            }
                else if (radioButton2.Checked)
                {
                FolderBrowserDialog ofd = new FolderBrowserDialog();

                DialogResult dr = ofd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    textBox1.Text = Excute("create -folder " + ofd.SelectedPath);
                }
            }
                else
                {
                    MessageBox.Show("Please check Buttons!");
                }
        }

        public static string Excute(string arg)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = Properties.Settings.Default.Path;
            startInfo.Arguments = arg;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process processTemp = new Process();
            processTemp.StartInfo = startInfo;
            processTemp.EnableRaisingEvents = true;
            try
            {
                processTemp.Start();
            }
            catch (Exception e)
            {
                throw;
            }
            return processTemp.StandardOutput.ReadToEnd();
        }

    }
}
