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

namespace OOP_Exam
{
    public partial class Form4 : Form
    {
        private static string connectionString = "Server=localhost;Database=forOOP;Trusted_Connection=True;";
        public Form4()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form4_Load);
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // Наполнение comboBox1
            comboBox1.Items.AddRange(new string[] { "name", "type", "price" });
            comboBox1.SelectedIndex = 0;

            // Загрузка данных в DataGridView
            LoadData();
        }
        private void LoadData()
        {
            string query = "SELECT * FROM components";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 newForm = new Form3();
            this.Hide();
            newForm.Show();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "поиск")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "поиск";
                textBox1.ForeColor = Color.Silver;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "name")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "name";
                textBox2.ForeColor = Color.Silver;
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "type")
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "type";
                textBox3.ForeColor = Color.Silver;
            }
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.Text == "price")
            {
                textBox5.Text = "";
                textBox5.ForeColor = Color.Black;
            }
        }
            
        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                textBox5.Text = "price";
                textBox5.ForeColor = Color.Silver;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Получаем введенный текст и выбранный столбец
            string searchText = textBox1.Text.Trim();
            string selectedColumn = comboBox1.SelectedItem?.ToString();

            // Проверяем, что пользователь выбрал колонку и ввел текст
            if (string.IsNullOrEmpty(searchText) || string.IsNullOrEmpty(selectedColumn))
            {
                MessageBox.Show("Выберите столбец для поиска и введите текст!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Запрос для фильтрации данных
            string query = $"SELECT * FROM components WHERE {selectedColumn} LIKE @searchText";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@searchText", "%" + searchText + "%");

                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при поиске: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Получаем данные из текстбоксов
            string name = textBox2.Text.Trim();
            string type = textBox3.Text.Trim();
            string priceText = textBox5.Text.Trim();

            // Проверяем, что все поля заполнены
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(type) || string.IsNullOrEmpty(priceText))
            {
                MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Преобразуем цену в число
            if (!decimal.TryParse(priceText, out decimal price))
            {
                MessageBox.Show("Цена должна быть числом!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // SQL-запрос для вставки данных
            string query = "INSERT INTO components (name, type, price) VALUES (@name, @type, @price)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@type", type);
                        command.Parameters.AddWithValue("@price", price);

                        // Выполняем запрос
                        int rowsInserted = command.ExecuteNonQuery();
                        if (rowsInserted > 0)
                        {
                            MessageBox.Show("Данные успешно добавлены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Очищаем текстбоксы
                            textBox2.Text = "name";
                            textBox3.Text = "type";
                            textBox5.Text = "price";
                            textBox2.ForeColor = Color.Silver;
                            textBox3.ForeColor = Color.Silver;
                            textBox5.ForeColor = Color.Silver;

                            // Обновляем таблицу
                            LoadData();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при добавлении данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            // Проверяем, выбрана ли строка
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите строку для изменения!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Получаем выбранную строку
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            // Получаем данные из выбранной строки
            int id = Convert.ToInt32(selectedRow.Cells["id"].Value); // Предполагается, что в таблице есть поле id
            string name = selectedRow.Cells["name"].Value.ToString();
            string type = selectedRow.Cells["type"].Value.ToString();
            decimal price = Convert.ToDecimal(selectedRow.Cells["price"].Value);

            // Открываем окно редактирования
            FormEdit formEdit = new FormEdit(id, name, type, price);
            if (formEdit.ShowDialog() == DialogResult.OK)
            {
                // Если изменения подтверждены, перезагружаем данные
                LoadData();
                MessageBox.Show("Данные успешно обновлены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверяем, выделена ли строка
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Получаем ID компонента из выделенной строки
                    int selectedId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);

                    // Удаляем строку из базы данных
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM components WHERE id = @id";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@id", selectedId);
                            command.ExecuteNonQuery();
                        }
                    }

                    // Перезагружаем данные в таблице
                    LoadData();
                    MessageBox.Show("Запись успешно удалена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите строку для удаления.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form4_Resize(object sender, EventArgs e)
        {
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            if (formWidth <= 750)
            {
                textBox1.Top = label1.Bottom;
                textBox1.Left = (formWidth - textBox1.Width) / 2;
                button2.Top = textBox1.Bottom;
                button2.Left = (formWidth - button2.Width) / 2;
                comboBox1.Top = button2.Bottom;
                textBox2.Location = new System.Drawing.Point(12, 120);
                dataGridView1.Location = new System.Drawing.Point(197, 120);
                textBox3.Top = textBox2.Bottom;
                textBox5.Top = textBox3.Bottom;
            }
            else
            {
                button2.Location = new System.Drawing.Point(670, 13);
                button2.Left = formWidth - button2.Width - 20;
                textBox1.Location = new System.Drawing.Point(482, 19);
                textBox1.Left = formWidth - textBox1.Width - 130;
                comboBox1.Top = button2.Top + 5;
                textBox2.Location = new System.Drawing.Point(12, 94);
                dataGridView1.Location = new System.Drawing.Point(197, 86);
                textBox3.Top = textBox2.Top + 30;
                textBox5.Top = textBox3.Top + 30;
            }
        }
    }
}
