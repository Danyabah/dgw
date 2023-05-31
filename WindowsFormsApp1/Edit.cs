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

namespace WindowsFormsApp1
{
    public partial class Edit : Form
    {
        public Edit()
        {
            InitializeComponent();
        }
        private void Clear()
        {
            //очищаем все значения
            textBox1.Text = "";
            textBox2.Text = "";
            numericUpDown1.Value = 0;

            this.Hide();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Даня\Documents\BiblTbl.mdf;Integrated Security=True;Connect Timeout=30");
        private void button1_Click(object sender, EventArgs e)
        {
            //проверка на пустые значения
            if (textBox1.Text == "")
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
                    //создаем команду для обновления
                    SqlCommand cmd = new SqlCommand("Update Users set userName=@UN,userExp=@UE,userPass=@UP where userId=@DKey", Con);
                    //добавляем значения в строку
                    cmd.Parameters.AddWithValue("@UN", textBox1.Text);
                    cmd.Parameters.AddWithValue("@UE", numericUpDown1.Value.ToString());
                    cmd.Parameters.AddWithValue("@UP", textBox2.Text);
                    cmd.Parameters.AddWithValue("@DKey", textBox3.Text);
                    cmd.ExecuteNonQuery();
                    // выполняет sql-выражение и возвращает количество измененных записей.
                    MessageBox.Show("Пользователь изменен");
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
