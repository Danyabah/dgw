using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class Add : Form
    {
        private string Query = "Select * from Users";
        public Add()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Даня\Documents\BiblTbl.mdf;Integrated Security=True;Connect Timeout=30");
        private void Clear()
        {
            //очищаем все значения
            textBox1.Text = "";
            textBox2.Text = "";
            numericUpDown1.Value = 0;

           this.Hide();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //проверка на пустые значения
            if (textBox1.Text == "" || textBox2.Text == "" || numericUpDown1.Value == 0)
            {
                MessageBox.Show("Заполните все поля");
            }
            else
            {
                //отлавливаем ошибку
                try
                {
                    //открываем подключение
                    Con.Open();
                    //создаем команду для добавления
                    SqlCommand cmd = new SqlCommand("insert into Users(userName,userPass,userExp)values(@UN,@UP,@UE)", Con);
                    //добавляем значения в строку
                    cmd.Parameters.AddWithValue("@UN", textBox1.Text);
                    cmd.Parameters.AddWithValue("@UP", textBox2.Text);
                    cmd.Parameters.AddWithValue("@UE", numericUpDown1.Value);

                    cmd.ExecuteNonQuery();
                    // выполняет sql-выражение и возвращает количество измененных записей.
                    MessageBox.Show("пользователь добавлен");
                    //закрываем подключение
                    Con.Close();

                    Clear();
               
                   
                }
                catch (Exception err)
                {
                    //показываем ошибку
                    MessageBox.Show(err.Message);
                }
            }
        }
    }
}
