using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAdapterWizard
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = northwindDataSet1.Customers;
            sqlDataAdapter1.Fill(northwindDataSet1.Customers);

        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            sqlDataAdapter1.Update(northwindDataSet1);
        }

        private void DataAdapter1_RowUpdating(object sender, EventArgs e)
        {
            
        }

        private void sqlDataAdapter1_RowUpdated(object sender, System.Data.SqlClient.SqlRowUpdatedEventArgs e)
        {
            NorthwindDataSet.CustomersRow CustRow = (NorthwindDataSet.CustomersRow)e.Row; MessageBox.Show(CustRow.CustomerID.ToString() + " has been updated");
            northwindDataSet1.Customers.Clear();
            sqlDataAdapter1.Fill(northwindDataSet1.Customers);
        }

        private void sqlDataAdapter1_RowUpdating(object sender, System.Data.SqlClient.SqlRowUpdatingEventArgs e)
        {
            NorthwindDataSet.CustomersRow CustRow = (NorthwindDataSet.CustomersRow)e.Row;
            DialogResult response = MessageBox.Show("Continue updating " + CustRow.CustomerID.ToString() + "?", "Continue Update?", MessageBoxButtons.YesNo);
            if (response == DialogResult.No)
            { e.Status = UpdateStatus.SkipCurrentRow; }
        }

        private void sqlDataAdapter1_FillError(object sender, FillErrorEventArgs e)
        {
            DialogResult response = MessageBox.Show("The following error occurred while Filling the DataSet: " + e.Errors.Message.ToString() +
            " Continue attempting to fill?", "FillError Encountered", MessageBoxButtons.YesNo);
            if (response == DialogResult.Yes) { e.Continue = true; } 
            else
            { e.Continue = false; }
        }
    }
}

