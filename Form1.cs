using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_Exam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 newForm = new Form3();
            this.Hide();
            newForm.Show();
            newForm.FormClosed += (s, args) => this.Show();
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            // Центрируем label2
            label2.Left = (formWidth - label2.Width) / 2;
            label2.Top = (formHeight) / 2 - 100;

            // Центрируем label3
            label3.Left = (formWidth - label3.Width) / 2;
            label3.Top = label2.Bottom + 20;

            if (formWidth <= 490)
            {
                label3.Visible = false;
            }
            else
            {
                label3.Visible = true;
            }

            if (formWidth <= 370)
            {
                label2.Visible = false;
            }
            else
            {
                label2.Visible = true;
            }

            // Логика для кнопки и логотипа
            if (formWidth <= 410)
            {
                // Кнопка под логотипом
                button1.Top = label1.Bottom;
                button1.Left = (formWidth - button1.Width) / 2;

                // Логотип по центру
                label1.Left = (formWidth - label1.Width) / 2;
            }
            else
            {
                // Кнопка справа от окна
                label1.Left = 20;
                button1.Top = label1.Top;
                button1.Left = formWidth - button1.Width - 20;
            }
        }

    }
}
