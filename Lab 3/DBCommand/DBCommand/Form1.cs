using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DBCommand
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder results = new StringBuilder();
            using (sqlConnection1)
            {
                try
                {
                    sqlConnection1.Open(); SqlDataReader reader = sqlCommand1.ExecuteReader();
                    while (reader.Read())
                    { for (int i = 0; i < reader.FieldCount; i++)
                        { results.Append(reader[i].ToString() + "\t"); }
                        results.Append(Environment.NewLine); }
                    ResultsTextBox.Text = results.ToString();
                }
                catch (SqlException ex)
                { MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error); }


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StringBuilder results = new StringBuilder(); using (sqlConnection1)
            {
                sqlCommand1.CommandText = "SELECT CustomerID, CompanyName FROM Customers;" + "SELECT ProductName, UnitPrice, QuantityPerUnit FROM Products;";
                try
                {
                    sqlConnection1.Open(); SqlDataReader reader = sqlCommand1.ExecuteReader(); 
               
                
                  bool MoreResults = false;
                     do { while (reader.Read())
                { 
                            for (int i = 0; i < reader.FieldCount; i++)
                            { results.Append(reader[i].ToString() + "\t"); } 
                            results.Append(Environment.NewLine); }
                        MoreResults = reader.NextResult(); }
                        while (MoreResults);
                        ResultsTextBox.Text = results.ToString();
                }

           
                 catch (SqlException ex)
                { MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            StringBuilder results = new StringBuilder();
            using (sqlConnection1)
            {
                try
                {
                    sqlConnection1.Open();
                    SqlDataReader reader = sqlCommand2.ExecuteReader();
                    while (reader.Read())
                    { for (int i = 0; i < reader.FieldCount; i++)
                        { results.Append(reader[i].ToString() + "\t"); } 
                        results.Append(Environment.NewLine); 
                    }
                    ResultsTextBox.Text = results.ToString();
                }
                catch (SqlException ex)
                { MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (sqlConnection1)
            { sqlCommand3.CommandText = "CREATE TABLE SalesPersons (" + "[SalesPersonID] [int] IDENTITY(1,1) NOT NULL, " + "[FirstName] [nvarchar](50)  NULL, " + "[LastName] [nvarchar](50)  NULL)";
                try { sqlConnection1.Open(); sqlCommand3.ExecuteNonQuery(); 
                    MessageBox.Show("Таблица SalesPersons создана"); }
                catch (SqlException ex) { MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error); } 
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            StringBuilder results = new StringBuilder();
            try
            {
                sqlCommand4.Parameters["@City"].Value = CityTextBox.Text;
                sqlConnection1.Open();
                SqlDataReader reader = sqlCommand4.ExecuteReader();
                while (reader.Read())
                { for (int i = 0; i < reader.FieldCount; i++)
                    { results.Append(reader[i].ToString() + "\t"); }
                    results.Append(Environment.NewLine); }
                ResultsTextBox.Text = results.ToString();
            }
            catch (SqlException ex)
            {                
               MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { sqlConnection1.Close(); }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            StringBuilder results = new StringBuilder();
            try
            {
                sqlCommand5.Parameters["@CategoryName"].Value = CategoryNameTextBox.Text; sqlCommand5.Parameters["@OrdYear"].Value = OrdYearTextBox.Text;
                sqlConnection1.Open(); 
                SqlDataReader reader = sqlCommand5.ExecuteReader();
                while (reader.Read()) { for (int i = 0; i < reader.FieldCount; i++) 
                    { results.Append(reader[i].ToString() + "\t");
                    } results.Append(Environment.NewLine);
                }
                ResultsTextBox.Text = results.ToString();
            }
            catch (SqlException ex) { MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { sqlConnection1.Close(); }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}


