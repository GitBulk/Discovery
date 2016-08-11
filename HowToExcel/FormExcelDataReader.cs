using Excel;
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

namespace HowToExcel
{
    public partial class FormExcelDataReader : Form
    {
        public FormExcelDataReader()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            string filePath = "Book1.xls";
            if (File.Exists(filePath))
            {
                var excelData = new ExcelData(filePath);
                var rows = excelData.GetData(0, true);
                List<FootballPlayer> playerList = new List<FootballPlayer>();
                foreach (var row in rows)
                {
                    //DateTime date = DateTime.FromOADate(Convert.ToDouble(row["JoinDate"]));
                    var player = new FootballPlayer
                    {
                        Name = row["Name"].ToString(),
                        JoinDate = DateTime.FromOADate(Convert.ToDouble(row["JoinDate"]))
                    };

                    playerList.Add(player);
                }
                dataGridView1.DataSource = playerList;
            }
            else
            {
                MessageBox.Show("no");
            }
        }

        class FootballPlayer
        {
            public string Name { get; set; }
            public DateTime JoinDate { get; set; }
        }

        class ExcelData
        {
            string path;
            public ExcelData(string path)
            {
                this.path = path;
            }
            public IExcelDataReader GetExcelReader()
            {
                FileStream fileStream = File.Open(this.path, FileMode.Open, FileAccess.Read);
                IExcelDataReader reader = null;
                try
                {
                    if (this.path.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(fileStream);
                    }
                    if (this.path.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);
                    }
                    return reader;
                }
                catch (Exception)
                {

                    throw;
                }
            }

            public IEnumerable<string> GetWorksheetNames()
            {
                var reader = GetExcelReader();
                var workbook = reader.AsDataSet();
                var sheets = from DataTable table in workbook.Tables
                             select table.TableName;
                return sheets;
            }

            public IEnumerable<DataRow> GetData(int sheetIndex, bool isFirstRowAsColumnNames)
            {
                var reader = GetExcelReader();
                reader.IsFirstRowAsColumnNames = isFirstRowAsColumnNames;
                var workSheet = reader.AsDataSet().Tables[sheetIndex];
                var rows = from DataRow row in workSheet.Rows
                           select row;
                return rows;
            }
        }
    }
}
