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
    public partial class Form3 : Form
    {
        //486
        public Form3()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 newForm = new Form1();
            this.Hide();
            newForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form4 newForm = new Form4();
            this.Hide();
            newForm.Show();
        }

        private void Form3_Resize(object sender, EventArgs e)
        {
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            // Логика для кнопки и логотипа
            if (formWidth <= 486)
            {
                // Кнопка под логотипом
                button1.Top = label1.Bottom;
                button1.Left = (formWidth - button1.Width) / 2;

                // Кнопка под логотипом
                button4.Top = button1.Bottom;
                button4.Left = (formWidth - button4.Width) / 2;

                // Логотип по центру
                label1.Left = (formWidth - label1.Width) / 2;
            }
            else
            {
                // Кнопка справа от окна
                label1.Left = 20;
                button1.Top = label1.Top;
                button1.Left = formWidth - button1.Width - 20;
                // Кнопка справа от окна
                button4.Top = label1.Top;
                button4.Left = formWidth - button4.Width - 130;
            }
        }
    }
}
