using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kollokvium_2
{
    public partial class Form1 : Form
    {
        private string folderPath = "";
        private string[] filePaths;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button4.Enabled = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(folderPath))
            {
                MessageBox.Show("Выберите папку для сохранения файлов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int N = (int)numericUpDown1.Value;
            Random rand = new Random();

            double[,] matrixA = new double[N, N];
            double[,] matrixB = new double[N, N];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    matrixA[i, j] = rand.NextDouble();
                    matrixB[i, j] = rand.NextDouble();
                }
            }

            for (int i = 2; i <= N; i++)
            {
                string fileName = $"data_{i}.txt";
                string filePath = Path.Combine(folderPath, fileName);
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine($"Matrix {i} (size {i}x{i}):");
                    WriteMatrixToFile(writer, matrixA, i);
                }
            }

            textBox1.Text = folderPath;

            MessageBox.Show("Файлы успешно сохранены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void WriteMatrixToFile(StreamWriter writer, double[,] matrix, int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    writer.Write(matrix[i, j].ToString("F3") + "\t");
                }
                writer.WriteLine();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                {
                    folderPath = dialog.SelectedPath;
                    textBox1.Text = folderPath;
                }
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                {
                    folderPath = dialog.SelectedPath;
                    textBox1.Text = folderPath;
                    UpdateFilePathsComboBox(); 
                }
            }
            comboBox1.Text = "Выберите файл...";
            button4.Enabled = true;
        }

        private void UpdateFilePathsComboBox()
        {
            if (!string.IsNullOrEmpty(folderPath) && Directory.Exists(folderPath))
            {
                filePaths = Directory.GetFiles(folderPath);
                comboBox1.Items.Clear();
                comboBox1.Items.AddRange(filePaths);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите файл для чтения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string selectedFilePath = comboBox1.SelectedItem.ToString();
            ReadMatrixFromFile(selectedFilePath);
        }

        private void ReadMatrixFromFile(string filePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);

                // Проверяем формат файла: должен начинаться с префикса "Matrix"
                if (lines.Length > 0 && lines[0].StartsWith("Matrix"))
                {
                    listBox1.Items.Clear();

                    foreach (string line in lines)
                    {
                        listBox1.Items.Add(line);
                    }
                }
                else
                {
                    MessageBox.Show("Формат файла не соответствует ожидаемому.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка чтения файла: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //button4.Enabled = comboBox1.SelectedIndex != -1;
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
