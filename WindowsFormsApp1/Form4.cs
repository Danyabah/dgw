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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;

namespace WindowsFormsApp1
{
    public partial class Form4 : Form
    {
        private string Query = "Select * from Users";
        public Form4()
        {
            InitializeComponent();
            библиотекариToolStripMenuItem.Enabled = false;
            DisplayUsers(Query);
            изменитьToolStripMenuItem.Enabled = false;
            удалитьToolStripMenuItem.Enabled = false;
            
        }
        int Key = 0;
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Даня\Documents\BiblTbl.mdf;Integrated Security=True;Connect Timeout=30");
        public void DisplayUsers(string Query)
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

        private void библиотекаToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form3 bib = new Form3();
            bib.Show();
            this.Hide();
        }
        private void Add_Click(object sender, EventArgs e)
        {
            var add = new Add();
 
            if (add.ShowDialog(this) == DialogResult.OK)
            {
                DisplayUsers(Query);
            }
        }
        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Click(sender, e);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var edit = new Edit();
            edit.textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            if (edit.textBox1.Text == "")
            {
                изменитьToolStripMenuItem.Enabled = false;
                удалитьToolStripMenuItem.Enabled = false;
            }
            else
            {
                изменитьToolStripMenuItem.Enabled = true;
                удалитьToolStripMenuItem.Enabled = true;
            }
        }
        private void Edit_Click(object sender, EventArgs e)
        {
            var edit = new Edit();

            //заполняем данные
            edit.textBox3.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            edit.textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            edit.textBox2.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            edit.numericUpDown1.Value = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
            if (edit.ShowDialog(this) == DialogResult.OK)
            {

                DisplayUsers(Query);
            }
        }
        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Edit_Click(sender, e);
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            var id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            //если не выбран никто
            if (id == "")
            {
                MessageBox.Show("Выберите пользователя");
            }
            else
            {
                //отлавливаем ошибку
                try
                {
                    //открываем подключение
                    Con.Open();
                    //создаем команду для редактирования
                    SqlCommand cmd = new SqlCommand("Delete from Users where userId=@DKey", Con);
                    //добавляем значения в строку
                    cmd.Parameters.AddWithValue("@DKey", id);
                    cmd.ExecuteNonQuery();
                    // выполняет sql-выражение и возвращает количество измененных записей.
                    MessageBox.Show("Пользователь удален");
                    //закрываем подключение
                    Con.Close();
                    DisplayUsers(Query);
                    id = "";
                }
                catch (Exception err)
                {
                    //показываем ошибку
                    MessageBox.Show(err.Message);
                }
            }
        }
        private Excel.Application GetExcel()
        {
            Excel.Application xlApp;
            Worksheet xlSheet;
            xlApp = new Excel.Application();
            Excel.Workbook wBook;
            wBook = xlApp.Workbooks.Add();
            xlApp.Columns.ColumnWidth = 15;
            xlSheet = wBook.Sheets[1];
            xlSheet.Name = "Пользователи";
            xlSheet.Cells.HorizontalAlignment = 3;
            for (int j = 1; j < dataGridView1.Columns.Count + 1; j++)
            {
                xlApp.Cells[1, j] = dataGridView1.Columns[j - 1].HeaderText;
            }
            return xlApp;
        }
        private void экспортToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show("Экспортировать все?", "Справка", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Yes:
                    {
                        var xlApp = GetExcel();
                        for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                        {
                            for (int j = 0; j < dataGridView1.Columns.Count; j++)
                            {
                                xlApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            }
                        }
                        xlApp.Visible = true;
                        break;
                    };

                case DialogResult.No:
                    {
                        var xlApp = GetExcel();
                        for (int i = 0; i < dataGridView1.SelectedRows.Count - 1; i++)
                        {
                            for (int j = 0; j < dataGridView1.Columns.Count; j++)
                            {
                                xlApp.Cells[i + 2, j + 1] = dataGridView1.SelectedRows[i].Cells[j].Value.ToString();
                            }
                        }
                        xlApp.Visible = true;
                        break;
                    };

                case DialogResult.Cancel:
                    return;
            }
        }
        //private void GetTestName()
        //{
        //    Con.Open();
        //    string Query = "Select * from Users where TestNum=" + TestIdCb.SelectedValue.ToString() + "";
        //    SqlCommand cmd = new SqlCommand(Query, Con);
        //    DataTable dt = new DataTable();
        //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //    sda.Fill(dt);
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        TestNameTb.Text = dr["TestName"].ToString();
        //    }
        //    Con.Close();
        //}
        //private void GetDocId()
        //{
        //    //получаем все id 
        //    Con.Open();
        //    SqlCommand cmd = new SqlCommand("Select DocId from DoctorTbl", Con);
        //    SqlDataReader rdr;
        //    // для получения строк из источника данных. 
        //    rdr = cmd.ExecuteReader();
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("DocId", typeof(int));
        //    dt.Load(rdr);
        //    //фактическое значение
        //    DocIdCb.ValueMember = "DocId";
        //    //задаем в комбобокс
        //    DocIdCb.DataSource = dt;
        //    Con.Close();
        //}
        
    }
}
