using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;


namespace 程式白名單
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<string> L = new List<string>();
        List<string> L2 = new List<string>();
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                FileStream fs = new FileStream(System.AppDomain.CurrentDomain.BaseDirectory + "whitelist.txt", FileMode.CreateNew);
                fs.Close();
            }
            catch
            {

            }
            
            Process[] process = Process.GetProcesses();
            L.AddRange(File.ReadLines(System.AppDomain.CurrentDomain.BaseDirectory + "whitelist.txt"));
            foreach (Process p in process)
            {
                if (p.MainWindowHandle != IntPtr.Zero && !L.Contains(p.ProcessName))
                    listBox1.Items.Add(p.ProcessName);
                else if (L.Contains(p.ProcessName)&&!L2.Contains(p.ProcessName))
                {
                    listBox2.Items.Add(p.ProcessName + "         locked");
                    L2.Add(p.ProcessName);
                }
            }
        }
    

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                listBox2.Items.Add(listBox1.SelectedItem);
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
            
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
                {
                    listBox1.Items.Add(listBox2.SelectedItem);
                    listBox2.Items.Remove(listBox2.SelectedItem);
                }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            using (StreamWriter write = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + "whitelist.txt", true))
            {
                foreach (string LB in listBox2.Items)
                {
                    if (!L2.Contains(LB))
                    {
                        write.WriteLine(LB);
                    }
                }
                write.Close();
            }
            L.Clear();
            L.AddRange(File.ReadLines(System.AppDomain.CurrentDomain.BaseDirectory + "whitelist.txt"));
            Process[] process = Process.GetProcesses();
            foreach (Process p in process)
            {
                if (p.MainWindowHandle != IntPtr.Zero && !L.Contains(p.ProcessName))
                {
                    p.Kill();
                }
            }


        }
    }
}
