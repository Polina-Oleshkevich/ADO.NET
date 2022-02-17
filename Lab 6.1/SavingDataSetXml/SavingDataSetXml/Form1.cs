using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SavingDataSetXml
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void FillDataSetButton_Click(object sender, EventArgs e)
        {
            CustomersAdapter.Fill(northwindDataSet1.Customers); OrdersAdapter.Fill(northwindDataSet1.Orders);
            CustomersGrid.DataSource = northwindDataSet1.Customers;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SaveXmlDataButton_Click(object sender, EventArgs e)
        {
            try 
            { 
                northwindDataSet1.WriteXml("Northwind.xml"); 
                MessageBox.Show("Data save as XML"); 
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }
        }

        private void SaveXmlSchemaButton_Click(object sender, EventArgs e)
        {
            try
            { 
                northwindDataSet1.WriteXmlSchema("Northwind.xsd");
                MessageBox.Show("Schema save as XML");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
