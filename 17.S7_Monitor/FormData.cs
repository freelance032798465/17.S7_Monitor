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

namespace _17.S7_Monitor
{
    public partial class FormData : Form
    {
        public FormData(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        private Form1 form1;
        private FormFilter FormFilter;
        private FormFilterDate FormFilterDate;
        private FormFilterDateTime FormFilterDateTime;
        private FormFilterTime FormFilterTime;

        private void FormData_Load(object sender, EventArgs e)
        {
            FormFilter = new FormFilter();
            FormFilterDate = new FormFilterDate();
            FormFilterDateTime = new FormFilterDateTime();
            FormFilterTime = new FormFilterTime();

            dataGridView1.Rows.Clear();
            var allLogs = form1.db.GetAllLogs();
            foreach (var log in allLogs)
            {
                Console.WriteLine($"{log.ID} | {log.Date:yyyy-MM-dd} {log.Time} | {log.ProductName} | {log.Description}");
                dataGridView1.Rows.Add(log.Date.ToString("yyyy-MM-dd"), log.Time, 
                    log.ProductName, log.ProductCode, log.Description);
            }
        }

        private void ShowFormFilter()
        {
            FormFilter.TopLevel = false;
            FormFilter.FormBorderStyle = FormBorderStyle.None;
            FormFilter.Dock = DockStyle.Fill;

            pn_filter.Controls.Clear();
            pn_filter.Controls.Add(FormFilter);
            FormFilter.Show();
        }
        private void ShowFormFilterDate()
        {
            FormFilterDate.TopLevel = false;
            FormFilterDate.FormBorderStyle = FormBorderStyle.None;
            FormFilterDate.Dock = DockStyle.Fill;

            pn_filter.Controls.Clear();
            pn_filter.Controls.Add(FormFilterDate);
            FormFilterDate.Show();
        }
        private void ShowFormFilterDateTime()
        {
            FormFilterDateTime.TopLevel = false;
            FormFilterDateTime.FormBorderStyle = FormBorderStyle.None;
            FormFilterDateTime.Dock = DockStyle.Fill;

            pn_filter.Controls.Clear();
            pn_filter.Controls.Add(FormFilterDateTime);
            FormFilterDateTime.Show();
        }
        private void ShowFormFilterTime()
        {
            FormFilterTime.TopLevel = false;
            FormFilterTime.FormBorderStyle = FormBorderStyle.None;
            FormFilterTime.Dock = DockStyle.Fill;

            pn_filter.Controls.Clear();
            pn_filter.Controls.Add(FormFilterTime);
            FormFilterTime.Show();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "Date":
                    ShowFormFilterDate();
                    break;
                case "Time":
                    ShowFormFilterTime();
                    break;
                case "DateTime":
                    ShowFormFilterDateTime();
                    break;
                case "Product Name":
                    ShowFormFilter();
                    break;
                case "Product Code":
                    ShowFormFilter();
                    break;
                case "Description":
                    ShowFormFilter();
                    break;
            }

            bt_apply.Visible = true;
        }

        private void bt_csv_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                Console.WriteLine("No data to export.");
                return;
            }

            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "CSV files (*.csv)|*.csv";
                saveDialog.Title = "Save the file CSV";
                saveDialog.FileName = "ExportedData.csv";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveDialog.FileName;

                    StringBuilder sb = new StringBuilder();

                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        sb.Append(dataGridView1.Columns[i].HeaderText);
                        if (i < dataGridView1.Columns.Count - 1)
                            sb.Append(",");
                    }
                    sb.AppendLine();

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                            {
                                sb.Append(row.Cells[i].Value?.ToString().Replace(",", ";"));
                                if (i < dataGridView1.Columns.Count - 1)
                                    sb.Append(",");
                            }
                            sb.AppendLine();
                        }
                    }

                    File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);

                    Console.WriteLine($"Data exported to {filePath}");
                }
            }
        }

        private void bt_apply_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            if (comboBox1.Text == "Date")
            {
                DateTime dateStart = FormFilterDate.dateStart.Value;
                DateTime dateEnd = FormFilterDate.dateEnd.Value;

                var logsRange = form1.db.GetLogsByDateRange(dateStart, dateEnd);

                foreach (var log in logsRange)
                {
                    Console.WriteLine($"{log.ID} | {log.ProductName} | {log.Date:yyyy-MM-dd}");
                    dataGridView1.Rows.Add(log.Date.ToString("yyyy-MM-dd"), log.Time,
                        log.ProductName, log.ProductCode, log.Description);
                }
            }

            if (comboBox1.Text == "Time")
            {
                DateTime dateStart = FormFilterTime.timeStart.Value;
                DateTime dateEnd = FormFilterTime.timeEnd.Value;

                var logsByTime = form1.db.GetLogsByTimeRange(new TimeSpan(dateStart.Hour, dateStart.Minute, dateStart.Second), 
                    new TimeSpan(dateEnd.Hour, dateEnd.Minute, dateEnd.Second));

                foreach (var log in logsByTime)
                {
                    Console.WriteLine($"{log.ID} | {log.ProductName} | {log.Date:yyyy-MM-dd}");
                    dataGridView1.Rows.Add(log.Date.ToString("yyyy-MM-dd"), log.Time,
                        log.ProductName, log.ProductCode, log.Description);
                }
            }

            if (comboBox1.Text == "DateTime")
            {
                DateTime dateStart = FormFilterDateTime.dateStart.Value;

                DateTime timeStart = FormFilterDateTime.timeStart.Value;
                DateTime timeEnd = FormFilterDateTime.timeEnd.Value;

                var logsByDateTime = form1.db.GetLogsByDateTimeRange(dateStart,
                new TimeSpan(timeStart.Hour, timeStart.Minute, timeStart.Second), 
                new TimeSpan(timeEnd.Hour, timeEnd.Minute, timeEnd.Second));

                foreach (var log in logsByDateTime)
                {
                    Console.WriteLine($"{log.ID} | {log.ProductName} | {log.Date:yyyy-MM-dd}");
                    dataGridView1.Rows.Add(log.Date.ToString("yyyy-MM-dd"), log.Time,
                        log.ProductName, log.ProductCode, log.Description);
                }
            }

            if (comboBox1.Text == "Product Name")
            {
                string text = FormFilter.textBox1.Text;

                var logsRange = form1.db.GetLogsByProductName(text);

                foreach (var log in logsRange)
                {
                    Console.WriteLine($"{log.ID} | {log.ProductName} | {log.Date:yyyy-MM-dd}");
                    dataGridView1.Rows.Add(log.Date.ToString("yyyy-MM-dd"), log.Time,
                        log.ProductName, log.ProductCode, log.Description);
                }
            }

            if (comboBox1.Text == "Product Code")
            {
                string text = FormFilter.textBox1.Text;

                var logsRange = form1.db.GetLogsByProductCode(text);

                foreach (var log in logsRange)
                {
                    Console.WriteLine($"{log.ID} | {log.ProductName} | {log.Date:yyyy-MM-dd}");
                    dataGridView1.Rows.Add(log.Date.ToString("yyyy-MM-dd"), log.Time,
                        log.ProductName, log.ProductCode, log.Description);
                }
            }

            if (comboBox1.Text == "Description")
            {
                string text = FormFilter.textBox1.Text;

                var logsRange = form1.db.GetLogsByDescription(text);

                foreach (var log in logsRange)
                {
                    Console.WriteLine($"{log.ID} | {log.ProductName} | {log.Date:yyyy-MM-dd}");
                    dataGridView1.Rows.Add(log.Date.ToString("yyyy-MM-dd"), log.Time,
                        log.ProductName, log.ProductCode, log.Description);
                }
            }
        }

        private void bt_getAll_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            var allLogs = form1.db.GetAllLogs();
            foreach (var log in allLogs)
            {
                Console.WriteLine($"{log.ID} | {log.Date:yyyy-MM-dd} {log.Time} | {log.ProductName} | {log.Description}");
                dataGridView1.Rows.Add(log.Date.ToString("yyyy-MM-dd"), log.Time,
                    log.ProductName, log.ProductCode, log.Description);
            }
        }
    }
}
