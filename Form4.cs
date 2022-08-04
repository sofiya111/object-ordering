using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace лаб_9
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            Form1 main = this.Owner as Form1;
            dataGridView1.ColumnCount = listvar.Value.Count;
            dataGridView1.RowCount = listkrit.Value.Count;
            for (int i = 0; i < listkrit.Value.Count; i++)
            {
                for (int j = 0; j < listvar.Value.Count; j++)
                {
                    dataGridView1.Columns[j].HeaderText = listvar.Value[j];
                    dataGridView1.Rows[i].HeaderCell.Value = listkrit.Value[i];
                }
            }
            dataGridView1.RowHeadersWidth = 80;
            int k = 0;
            for (int i = 0; i < listkrit.Value.Count; i++)
            {
                for (int j = 0; j < listvar.Value.Count; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = Math.Round(lis.Value[k],4);
                    k++;
                }
            }
            dataGridView2.ColumnCount = listvar.Value.Count;
            for (int i = 0; i < listvar.Value.Count; i++)
            {
                dataGridView2.Columns[i].HeaderText = listvar.Value[i];
                dataGridView2.Rows[0].Cells[i].Value = Math.Round(listbest.Value[i],4);
            }
            var dict = listbest.Value.OrderByDescending(x => x)
                           .Distinct()
                           .Select((x, i) => new { Key = x, Value = i + 1 })
                           .ToDictionary(arg => arg.Key, arg => arg.Value);
            var results = listbest.Value.Select(x => new { Score = x, Place = dict[x] }).ToList();
            string resul = "";
            foreach (var result in results)
            {
                for (int i = 0; i < listvar.Value.Count; i++)
                {
                    if (Math.Round(result.Score,4) == Convert.ToDouble(dataGridView2.Rows[0].Cells[i].Value))
                    {
                        resul = Convert.ToString(dataGridView2.Columns[i].HeaderText);
                    }
                }
                textBox1.AppendText(resul + "-" + Convert.ToString(result.Place) + Environment.NewLine);
            }
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = 1;
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                chart1.Series.Add(Convert.ToString(dataGridView1.Columns[i].HeaderText));
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    chart1.Series[Convert.ToString(dataGridView1.Columns[i].HeaderText)].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    chart1.Series[Convert.ToString(dataGridView1.Columns[i].HeaderText)].BorderWidth = 3;
                    chart1.Series[Convert.ToString(dataGridView1.Columns[i].HeaderText)].Points.AddXY(Convert.ToString(dataGridView1.Rows[j].HeaderCell.Value), dataGridView1.Rows[j].Cells[i].Value);
                }
            }
        }
    }
}
