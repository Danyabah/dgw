using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void библиотекаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 bib = new Form3();
            bib.Show();
            this.Hide();
        }

        private void библиотекариToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 bibli = new Form4();
            bibli.Show();
            this.Hide();
        }
    }
}
