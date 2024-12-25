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

namespace OOP_Exam
{
    public partial class FormEdit : Form
    {
        private int recordId;
        private static string connectionString = "Server=localhost;Database=forOOP;Trusted_Connection=True;";
        public FormEdit(int id, string name, string type, decimal price)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            // Сохраняем ID записи
            recordId = id;

            // Заполняем текстбоксы
            textBoxName.Text = name;
            textBoxType.Text = type;
            textBoxPrice.Text = price.ToString();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            // Проверяем введенные данные
            string name = textBoxName.Text.Trim();
            string type = textBoxType.Text.Trim();
            string priceText = textBoxPrice.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(type) || string.IsNullOrEmpty(priceText))
            {
                MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(priceText, out decimal price))
            {
                MessageBox.Show("Цена должна быть числом!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Обновляем запись в базе данных
            string query = "UPDATE components SET name = @name, type = @type, price = @price WHERE id = @id";

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
                        command.Parameters.AddWithValue("@id", recordId);

                        int rowsUpdated = command.ExecuteNonQuery();
                        if (rowsUpdated > 0)
                        {
                            MessageBox.Show("Данные успешно обновлены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при обновлении данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
