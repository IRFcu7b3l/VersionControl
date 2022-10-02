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
using UserMaintenance1._0.Entities;

namespace UserMaintenance1._0
{
    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User> ();
        public Form1()
        {
            InitializeComponent();
            label1.Text = Resource1.FullName;
            button1.Text = Resource1.Add;
            button2.Text = Resource1.Write;             
            listBox1.DataSource=users;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "FullName";
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var u = new User();
            u.FullName = textBox1.Text;
            users.Add(u);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "csv";
            sfd.Filter = "Comma Separated Values (*.csv) | *.csv)"; 
            sfd.InitialDirectory = Application.StartupPath;
            if (sfd.ShowDialog() == DialogResult.OK) return;
            using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                foreach (var s in users)
                {
                    sw.Write(s.id);
                    sw.Write(";");
                    sw.Write(s.FullName);
                    sw.Write(";");
                    sw.WriteLine();
                }
                
        }
    }
}
