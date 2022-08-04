using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace лаб_9
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            Form1 main = this.Owner as Form1;
            dataGridView1.ColumnCount = listkrit.Value.Count;
            dataGridView1.RowCount = listkrit.Value.Count;
            for (int i = 0; i < listkrit.Value.Count; i++)
            {
                for (int j = 0; j < listkrit.Value.Count; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = 0;
                }
            }
            for (int i = 0; i < listkrit.Value.Count; i++)
            {
                dataGridView1.Columns[i].HeaderText = listkrit.Value[i];
                dataGridView1.Rows[i].HeaderCell.Value = listkrit.Value[i];
                dataGridView1.Rows[i].Cells[i].Value = 1;
            }
            dataGridView1.RowHeadersWidth = 80;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double[,] mas = new double[listkrit.Value.Count, listkrit.Value.Count];
            for (int i = 0; i < listkrit.Value.Count; i++)
            {
                for (int j = 0; j < listkrit.Value.Count; j++)
                {
                    mas[i, j] = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                }
            }
            for (int i = 0; i < listkrit.Value.Count; i++)
            {
                for (int j = 0; j < listkrit.Value.Count; j++)
                {
                    if (mas[i, j] == 0)
                    {
                        mas[i, j] = 1 / mas[j, i];
                        mas[i, j] = mas[0, j] / mas[0, i];
                    }
                }
            }
            for (int i = 0; i < listkrit.Value.Count; i++)
            {
                for (int j = 0; j < listkrit.Value.Count; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = Math.Round(mas[i, j], 4);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<double> sum1 = new List<double>();
            double[,] mas1 = new double[listkrit.Value.Count, listkrit.Value.Count];
            double sum = new double();
            for (int i = 0; i < listkrit.Value.Count; i++)
            {
                for (int j = 0; j < listkrit.Value.Count; j++)
                {
                    mas1[i, j] = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                }
            }
            for (int i = 0; i < listkrit.Value.Count; i++)
            {
                for (int k = 0; k < listkrit.Value.Count; k++)
                {
                    sum += mas1[k, i];
                }
                sum1.Add(1 / sum);
                sum = 0;
            }
            listbestKrit.Value = sum1;
            Close();
        }
    }
}
