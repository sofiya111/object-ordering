using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace лаб_9
{
    public partial class Form1 : Form
    {
        List<string> variant = new List<string>();
        List<string> krit = new List<string>();
        List<double> suma = new List<double>();
        List<double> suma2 = new List<double>();
        List<double> best = new List<double>();
        List<double> step = new List<double>();
        int coun;
        public Form1()
        {
            InitializeComponent();
            dataGridView1.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();
            button7.Hide();
            button8.Hide();
            button6.Enabled = false;
            coun = 0;
        }

        //ввод вариантов
        private void button1_Click(object sender, EventArgs e)
        {
            int v = (int)(numericUpDown1.Value);
            for (int i = 0; i < v; i++)
            {
                Form2 f = new Form2();
                f.Owner = this;
                f.ShowDialog();
                variant.Add(data.Value);
            }
        }
        //ввод критериев
        private void button2_Click(object sender, EventArgs e)
        {
            int v = (int)(numericUpDown2.Value);
            for (int i = 0; i < v; i++)
            {
                Form3 f = new Form3();
                f.Owner = this;
                f.ShowDialog();
                krit.Add(data.Value);
            }
        }
        //ввод результатов попарных сравнений
        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Show();
            label5.Show();
            label6.Show();
            label7.Show();
            button7.Show();
            button8.Show();
            dataGridView1.ColumnCount = variant.Count;
            dataGridView1.RowCount = variant.Count;
            for (int i = 0; i < variant.Count; i++)
            {
                for (int j = 0; j < variant.Count; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = 0;
                }
            }
            for (int i=0;i< variant.Count; i++)
            {
                dataGridView1.Columns[i].HeaderText = variant[i];
                dataGridView1.Rows[i].HeaderCell.Value = variant[i];
                dataGridView1.Rows[i].Cells[i].Value = 1;
            }
            dataGridView1.RowHeadersWidth = 80;
            label7.Text = krit[coun];
        }
        //просмотр результатов многокритериального сравнения
        private void button4_Click(object sender, EventArgs e)
        {
            double[,] mas1 = new double[variant.Count, variant.Count];
            double[,] mas2 = new double[krit.Count, variant.Count];
            double sum = new double();
            for (int i = 0; i < variant.Count; i++)
            {
                for (int j = 0; j < variant.Count; j++)
                {
                    mas1[i, j] = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                }
            }
            for (int i = 0; i < variant.Count; i++)
            {
                for (int p = 0; p < variant.Count; p++)
                {
                    sum += mas1[p, i];
                }
                suma.Add(1 / sum);
                sum = 0;
            }
            int k = 0;
            for (int i = 0; i < krit.Count; i++)
            {
                for (int j = 0; j < variant.Count; j++)
                {
                    mas2[i, j] = suma[k];
                    k++;
                }
            }
            if (radioButton2.Checked == true)
            {
                for (int i = 0; i < krit.Count; i++)
                {
                    for (int j = 0; j < variant.Count; j++)
                    {
                        mas2[i, j] = Math.Pow(mas2[i, j], listbestKrit.Value[i]);
                    }
                }
                k = 0;
                for (int i = 0; i < krit.Count; i++)
                {
                    for (int j = 0; j < variant.Count; j++)
                    {
                        suma2.Add(mas2[i, j]);
                        k++;
                    }
                }
            }
            double min = Double.MaxValue;
            for (int i = 0; i < variant.Count; i++)
            {
                min = Double.MaxValue;
                for (int j = 0; j < krit.Count; j++)
                {
                    if (mas2[j, i] < min)
                    {
                        min = mas2[j, i];
                    }
                }
                best.Add(min);
            }
            if (radioButton1.Checked == true)
            {
                lis.Value = suma;
            }
            else
            {
                lis.Value = suma2;
            }
            listbest.Value = best;
            listvar.Value = variant;
            listkrit.Value = krit;
            Hide();
            Form4 f = new Form4();
            f.Owner = this;
            f.ShowDialog();
            Close();
        }
        //формирование массива
        private void button7_Click(object sender, EventArgs e)
        {
            int c = 0;
            double[,] mas = new double[variant.Count, variant.Count];
            for (int i = 0; i < variant.Count; i++)
            {
                for (int j = 0; j < variant.Count; j++)
                {
                    mas[i, j] = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                    if (mas[i, j] != 0)
                    {
                        c++;
                    }
                }
            }
            if (c < 2 * variant.Count - 1)
            {
                MessageBox.Show("Мало данных! Нельзя сформировать матрицу парных сравнений");
            }
            else
            {
                for (int i = 0; i < variant.Count; i++)
                {
                    for (int j = 0; j < variant.Count; j++)
                    {
                        if (mas[i, j] == 0)
                        {
                            mas[i, j] = 1 / mas[j, i];
                            mas[i, j] = mas[0, j] / mas[0, i];
                        }
                    }
                }
                for (int i = 0; i < variant.Count; i++)
                {
                    for (int j = 0; j < variant.Count; j++)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = Math.Round(mas[i, j], 4);
                    }
                }
            }
        }
        //переход к следующему критерию (сохранение массива и очистка таблицы)
        private void button8_Click(object sender, EventArgs e)
        {
            coun++;
            if (coun >= krit.Count-1)
            {
                button8.Enabled = false;
            }
            double[,] mas1 = new double[variant.Count, variant.Count];
            double sum = new double();
            for (int i = 0; i < variant.Count; i++)
            {
                for (int j = 0; j < variant.Count; j++)
                {
                    mas1[i, j] = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                }
            }
            for (int i = 0; i < variant.Count; i++)
            {
                for (int k = 0; k < variant.Count; k++)
                {
                    sum += mas1[k, i];
                }
                suma.Add(1 / sum);
                sum = 0;
            }
            button3_Click(sender,e);
        }
        //сравнение критериев
        private void button6_Click(object sender, EventArgs e)
        {
            listkrit.Value = krit;
            Form5 f = new Form5();
            f.Owner = this;
            f.ShowDialog();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                button6.Enabled = true;
            }
            else
            {
                button6.Enabled = false;
            }
        }
        //очистка
        private void button5_Click(object sender, EventArgs e)
        {
            suma.Clear();
            variant.Clear();
            krit.Clear();
            best.Clear();
            step.Clear();
            dataGridView1.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();
            button7.Hide();
            button8.Hide();
            coun = 0;
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
        }
    }
}