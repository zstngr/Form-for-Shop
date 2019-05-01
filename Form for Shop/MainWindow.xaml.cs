using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.IO;
using System.Data;

namespace Form_for_Shop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private String dbFileName;
        private SQLiteConnection dbConnection;
        private SQLiteCommand dbCommand;

        public MainWindow()
        {
            InitializeComponent();
            dbFileName = "Movies.sqlite";
            dbConnection = new SQLiteConnection();
            dbCommand = new SQLiteCommand();

            statusConnection.Text = "Disconnected";
            
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(dbFileName))
            {
                SQLiteConnection.CreateFile(dbFileName);
            }
            else
            {
                MessageBox.Show("Database already exists");
            }
            try
            {
                dbConnection = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                dbConnection.Open();
                dbCommand.Connection = dbConnection;

                dbCommand.CommandText = "CREATE TABLE IF NOT EXISTS Catalog (id INTEGER PRIMARY KEY AUTOINCREMENT, Title TEXT, Director TEXT, MovieStartDate TEXT, Price INT)";
                dbCommand.ExecuteNonQuery();
                statusConnection.Text = "Connected";
            }
            catch (SQLiteException ex)
            {
                statusConnection.Text = "Disconnected";
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void ButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            DataTable dTable = new DataTable();
            String sqlQuery;
            try
            {
                dbConnection = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                dbConnection.Open();
                dbCommand.Connection = dbConnection;
                statusConnection.Text = "Connected";
            }
            catch (SQLiteException ex)
            {
                statusConnection.Text = "Disconnected";
                MessageBox.Show("Error: " + ex.Message);
            }

            try
            {
                sqlQuery = "SELECT * FROM Catalog";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, dbConnection);
                adapter.Fill(dTable);

                dataGridMovies.ItemsSource = dTable.DefaultView;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            DataTable dTable = new DataTable();
            String sqlQuery;
            try
            {
                dbConnection = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                dbConnection.Open();
                dbCommand.Connection = dbConnection;
                statusConnection.Text = "Connected";
            }
            catch (SQLiteException ex)
            {
                statusConnection.Text = "Disconnected";
                MessageBox.Show("Error: " + ex.Message);
            }

            try
            {
                sqlQuery = "SELECT * FROM Catalog";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, dbConnection);
                adapter.Fill(dTable);

                dataGridMovies.ItemsSource = dTable.DefaultView;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ButtonClearTable_Click(object sender, RoutedEventArgs e)
        {
            //DataTable dTable = new DataTable();
            //dataGridMovies.ItemsSource = dTable.DefaultView;
            //dataGridMovies.Items.Refresh();
            try
            {
                dbConnection = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                dbConnection.Open();
                dbCommand.Connection = dbConnection;

                dbCommand.CommandText = "DELETE FROM Catalog WHERE id > 0";
                dbCommand.ExecuteNonQuery();
                dataGridMovies.Items.Refresh();
            }
            catch (SQLiteException ex)
            {
                statusConnection.Text = "Disconnected";
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (dbConnection.State != ConnectionState.Open)
            {
                MessageBox.Show("Open connection with database");
                return;
            }
            AddData addData = new AddData();
            if (addData.ShowDialog() == true)
            {
                try
                {
                    dbCommand.CommandText = $"INSERT INTO Catalog('Title', 'Director', 'MovieStartDate', 'Price') " +
                        $"values ('{addData.MovieTitle}' , '{addData.Director}' , '{addData.MovieStartDate}', '{addData.Price}')";
                    dbCommand.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }

}
