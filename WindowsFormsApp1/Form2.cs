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
    public partial class Form2 : Form
    {
    
        public Form2()
        {
            InitializeComponent();
           
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Даня\Documents\BiblTbl.mdf;Integrated Security=True;Connect Timeout=30");
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Введите имя и пароль");
            }
            else
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from Users where userName=N'" + textBox1.Text + "' and userPass='" + textBox2.Text + "'", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    Form1 Obj = new Form1();
                    Obj.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Пользователь не найден");
                }
                Con.Close();
               
            }
        }
    }
}
