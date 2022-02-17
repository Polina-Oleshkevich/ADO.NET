using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.Common;

namespace DBConnection
{
    public partial class Form1 : Form
    {


        //string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Northwind; Integrated Security=true";
        public Form1()
        {
            InitializeComponent();

            this.connection.StateChange += new StateChangeEventHandler(this.connection_StateChange);

        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        SqlConnection connection = new SqlConnection();
        static string GetConnectionStringByName(string name)
        {
            string returnValue = null;
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
            if (settings != null)
                returnValue = settings.ConnectionString;
            return returnValue;

        }
        string connectionString = GetConnectionStringByName("DBConnect.NorthwindConnectionString");



        private void ConnectBDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            { if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    MessageBox.Show("Соединение с базой данных " + connection.Database + " выполнено успешно " + "\nСервер: " + connection.DataSource);
                }
                else MessageBox.Show("Соединение с базой данных уже установлено");
            }
            catch (SqlException XcpSQL)
            { foreach (SqlError se in XcpSQL.Errors)
                { MessageBox.Show(se.Message, "Источник ошибки: " + se.Source, MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void DisconnectBDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close(); MessageBox.Show("Соединение с базой данных закрыто");
            }
            else MessageBox.Show("Соединение с базой данных уже закрыто");
        }

        private async void ConnectAsyncBDToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                if (connection.State != ConnectionState.Open)
                { connection.ConnectionString = connectionString;
                    await connection.OpenAsync();
                    MessageBox.Show("Соединение с базой данных " + connection.Database + " выполнено успешно " + "\nСервер: " + connection.DataSource);
                }


                else MessageBox.Show("Соединение с базой данных уже установлено");
            }
            catch (Exception Xcp)
            {
                MessageBox.Show(Xcp.Message, "Unexpected Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void connection_StateChange(object sender, StateChangeEventArgs e)
        {
            ConnectBDToolStripMenuItem.Enabled = (e.CurrentState == ConnectionState.Closed);
            ConnectAsyncBDToolStripMenuItem.Enabled = (e.CurrentState == ConnectionState.Closed);
            DisconnectBDToolStripMenuItem.Enabled = (e.CurrentState == ConnectionState.Open);
        }

        private void ConnectionListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;
            if (settings != null)
            { foreach (ConnectionStringSettings cs in settings)
                {
                    string str = String.Format("Name = {0}\nProviderName = {1}\nConnectionString = {2}", cs.Name, cs.ProviderName, cs.ConnectionString);
                    MessageBox.Show(str, "Параметры подключений");
                }
            }
        }
//Практичесая 2
        private void button1_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            { 
                MessageBox.Show("Сначала подключитесь к базе");
                return;
            }
            using (connection)
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "USE [Northwind] SELECT COUNT(*) FROM [dbo].[Products]";
                try
                {
                    int number = (int)command.ExecuteScalar();
                    label1.Text = number.ToString();
                }
                catch (SqlException ex)
                { MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try { int number = WorkWithDataBase.ExecuteScalarMetod(connectionString, "USE [Northwind] SELECT COUNT(*) FROM [dbo].[Products]");
                label2.Text = number.ToString(); } 
            catch (SqlException ex)
            { MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand("USE [Northwind] SELECT [ProductName], [UnitPrice], [QuantityPerUnit] FROM [dbo].[Products]", connection); connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ListViewItem newItem = listView1.Items.Add(reader["ProductName"].ToString());
                        newItem.SubItems.Add(reader.GetDecimal(1).ToString());
                        newItem.SubItems.Add(reader["QuantityPerUnit"].ToString());
                    }

                }
                catch (SqlException ex) { MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try { }
                catch (SqlException ex)
                { MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                SqlTransaction sqlTran = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();
                command.Transaction = sqlTran;
                command.CommandText = "USE [Northwind] INSERT INTO [Products] ([ProductName], [UnitPrice], [QuantityPerUnit]) VALUES('Wrong size', 12, '1 boxes')"; command.ExecuteNonQuery();
                command.CommandText = "USE [Northwind] INSERT INTO[Products] ([ProductName], [UnitPrice], [QuantityPerUnit]) VALUES('Wrong color', 25, '100 ml')"; command.ExecuteNonQuery();
                sqlTran.Commit();
                MessageBox.Show("Строки записаны в базу данных");
                MessageBox.Show("Строки записаны в базу данных");
                try { sqlTran.Rollback(); } catch (Exception exRollback) { MessageBox.Show(exRollback.Message); }
            }
        }

       
       
    }
}




