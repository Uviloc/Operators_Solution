using Emitter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OperatorsSolution
{
    public partial class DataBaseForm : Form
    {

        private SQLiteDataAdapter? dataAdapter;
        private DataTable? dataTable;
        private readonly string connectionString = "Data Source=TemporaryTestDB.db;Version=3;";
        private SQLiteConnection? connection;



        public DataBaseForm()
        {
            InitializeComponent();


            LoadData();
        }
        private void LoadData()
        {
            try
            {
                if (connection == null || connection.State != ConnectionState.Open) connection = new SQLiteConnection(connectionString);

                connection.Open();

                // Load data into a DataTable
                string query = "SELECT * FROM People";
                dataAdapter = new SQLiteDataAdapter(query, connection);

                // CommandBuilder to automatically generate INSERT/UPDATE/DELETE commands
                var commandBuilder = new SQLiteCommandBuilder(dataAdapter);

                dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                // Bind the DataTable to the DataGridView
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private void saveButton_Click(object? sender, EventArgs e)
        {
            try
            {
                if (dataAdapter == null || dataTable == null) return;

                // Update the database with changes made in the DataGridView
                dataAdapter.Update(dataTable);
                MessageBox.Show("Changes saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving data: " + ex.Message);
            }
        }
    }
}
