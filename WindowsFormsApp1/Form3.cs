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

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        private string Query = "Select * from Bibl";
        public Form3()
        {
            InitializeComponent();
            DisplayBibl(Query);
            библиотекаToolStripMenuItem.Enabled = false;
        }
 

        private void библиотекариToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form4 biblik = new Form4();
            biblik.Show();
            this.Hide();
        }
     
        int Key = 0;
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Даня\Documents\BiblTbl.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayBibl(string Query)
        {
            // получаем данные в DataSet через SqlDataAdapter
            Con.Open();

            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            //SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            //создаем объект DataSet
            var ds = new DataSet();
            //заполняем DataSet
            sda.Fill(ds);
            //отображаем данные
            dataGridView1.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
