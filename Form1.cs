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

namespace WinniefashionSbc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox_color.Items.Add("SINGLE");
            comboBox_color.Items.Add("MIX");
        }
                
        private int remanining = 0;
        private int pieces = 0;
        private int pulanam = 0;
        string box;
        private int quantity = 0;
        string color;
        string mix;

        private void Quantitycalc()
        {
            if (remanining == 0)
            {
                int quantity = int.Parse(textBox_quantity.Text);
                remanining = quantity;
            }
            pulanam = int.Parse(textBox_PulanamID.Text);
           pieces = int.Parse(richTextBox_pieces.Text);
            box = textBox_BOXNO.Text;
            color = comboBox_color.Text;
            mix = textBox_MIX.Text;

            if (remanining >= pieces)
            {
                remanining -= pieces;
                textBox_quantity.Text = remanining.ToString();
                listBox_summary.Items.Add($" HP ID : {pulanam} ,  PCS : {pieces} ");
            }
            else
            {
                //richTextBox_pieces.Text = "Not enough quantity. Remaining: " + remanining.ToString();
            }

            if (remanining == 0)
            {
                textBox_quantity.ForeColor = Color.Red;
            }
            else
            {
                textBox_quantity.ForeColor = Color.Red;
            }
            quantity = int.Parse(textBox_quantity.Text);
            if (quantity == 0)
            {
                MessageBox.Show("SOLD OUT");
                return;
            }
        }

        private void BTN_CLICK_Click_1(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string folderpath = @"C:\Users\T0613\Desktop\MRSWINNIE\CSVFILEORDERING";
            string filepath = Path.Combine(folderpath, date + ".csv");

            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }

            // Create a DataTable and add columns
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("BOX NO", typeof(string));
            dataTable.Columns.Add("COLOR", typeof(string)); 
            dataTable.Columns.Add("MIX COLOR CODE", typeof(string));
              dataTable.Columns.Add("HP ID", typeof(string)); 
            dataTable.Columns.Add("PRICE", typeof(string));
            dataTable.Columns.Add("PIECES", typeof(string));
           
            // Add data to the DataTable
            dataTable.Rows.Add(textBox_BOXNO.Text, comboBox_color.Text, textBox_MIX.Text,textBox_PulanamID.Text, textBox_Price.Text, richTextBox_pieces.Text);

            // Check if the file exists
            if (!File.Exists(filepath))
            {
                // Write the DataTable to a CSV file
                using (StreamWriter writer = new StreamWriter(filepath))
                {
                    // Write the column headers
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        if (i > 0)
                        {
                            writer.Write(",");
                        }
                        writer.Write(dataTable.Columns[i].ColumnName);
                    }
                    writer.WriteLine();

                    // Write the data rows
                    foreach (DataRow row in dataTable.Rows)
                    {
                        for (int i = 0; i < dataTable.Columns.Count; i++)
                        {
                            if (i > 0)
                            {
                                writer.Write(",");
                            }
                            writer.Write(row[i].ToString());
                        }
                        writer.WriteLine();
                    }
                }
            }
            else
            {
                // Append data to the existing CSV file
                using (StreamWriter writer = File.AppendText(filepath))
                {
                    // Write the data rows
                    foreach (DataRow row in dataTable.Rows)
                    {
                        for (int i = 0; i < dataTable.Columns.Count; i++)
                        {
                            if (i > 0)
                            {
                                writer.Write(",");
                            }
                            writer.Write(row[i].ToString());
                        }
                        writer.WriteLine();
                    }
                }
            }

            // Make the text boxes read-only
            textBox_BOXNO.ReadOnly = true;
            //  textBox_PulanamID.ReadOnly = true;
            textBox_quantity.ReadOnly = true;
            //textBox_Price.ReadOnly = true;
            //  richTextBox_pieces.ReadOnly = true;
            Quantitycalc();
            richTextBox_pieces.Clear();
            textBox_PulanamID.Clear();
            textBox_MIX.Clear();
        }

        private void button_new_Click_1(object sender, EventArgs e)
        {

            textBox_BOXNO.ReadOnly = false;
            textBox_quantity.ReadOnly = false;
            textBox_BOXNO.Clear();
            textBox_quantity.Clear();
            textBox_Price.Clear();
            listBox_summary.Items.Clear();
        }

        private void textBox_BOXNO_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
