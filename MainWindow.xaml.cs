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
using System.Data;
using System.Data.SqlClient;


namespace DailyPlanner
{
    public partial class MainWindow : Window
    {
        private readonly string connectionString = @"Server=DESKTOP-N9AD6FJ;Database=DailyPlanner;Trusted_Connection=True;";
        private DataTable notesTable = new DataTable();

        public MainWindow()
        {
            InitializeComponent();
            LoadNotes();
        }

        private void LoadNotes()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Notes", connection);
                    notesTable.Clear();
                    adapter.Fill(notesTable);
                    NotesDataGrid.ItemsSource = notesTable.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
                }
            }
        }

        private void AddNote_Click(object sender, RoutedEventArgs e)
        {
            NoteWindow noteWindow = new NoteWindow();
            if (noteWindow.ShowDialog() == true)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("INSERT INTO Notes (Title, Content, Date) VALUES (@Title, @Content, @Date)", connection);
                        command.Parameters.AddWithValue("@Title", noteWindow.NoteTitle);
                        command.Parameters.AddWithValue("@Content", noteWindow.NoteContent);
                        command.Parameters.AddWithValue("@Date", noteWindow.NoteDate);
                        command.ExecuteNonQuery();
                        LoadNotes();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка добавления записи: {ex.Message}");
                    }
                }
            }
        }

        private void DeleteNote_Click(object sender, RoutedEventArgs e)
        {
            if (NotesDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите запись для удаления.");
                return;
            }

            DataRowView selectedRow = (DataRowView)NotesDataGrid.SelectedItem;
            int id = (int)selectedRow["Id"];

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DELETE FROM Notes WHERE Id = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                    LoadNotes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления записи: {ex.Message}");
                }
            }
        }

        private void EditNote_Click(object sender, RoutedEventArgs e)
        {
            if (NotesDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите запись для редактирования.");
                return;
            }

            DataRowView selectedRow = (DataRowView)NotesDataGrid.SelectedItem;
            int id = (int)selectedRow["Id"];
            string title = (string)selectedRow["Title"];
            string content = (string)selectedRow["Content"];
            DateTime date = (DateTime)selectedRow["Date"];

            NoteWindow noteWindow = new NoteWindow(title, content, date);
            if (noteWindow.ShowDialog() == true)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("UPDATE Notes SET Title = @Title, Content = @Content, Date = @Date WHERE Id = @Id", connection);
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@Title", noteWindow.NoteTitle);
                        command.Parameters.AddWithValue("@Content", noteWindow.NoteContent);
                        command.Parameters.AddWithValue("@Date", noteWindow.NoteDate);
                        command.ExecuteNonQuery();
                        LoadNotes();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка редактирования записи: {ex.Message}");
                    }
                }
            }

        }
    }
}

        
