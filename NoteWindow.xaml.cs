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
using System.Windows.Shapes;


namespace DailyPlanner
{
    public partial class NoteWindow : Window
    {
        public string NoteTitle => TitleTextBox.Text;
        public string NoteContent => ContentTextBox.Text;
        public DateTime NoteDate { get; private set; } = DateTime.Now;

        public NoteWindow(string title = "", string content = "", DateTime? date = null)
        {
            InitializeComponent();

           
            if (!string.IsNullOrEmpty(title)) TitleTextBox.Text = title;
            if (!string.IsNullOrEmpty(content)) ContentTextBox.Text = content;
            if (date.HasValue) NoteDate = date.Value;

            UpdatePlaceholderText();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void TitleTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TitleTextBox.Text == "Введите заголовок...")
            {
                TitleTextBox.Text = "";
                TitleTextBox.Foreground = Brushes.Black;
            }
        }

        private void TitleTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text))
            {
                TitleTextBox.Text = "Введите заголовок...";
                TitleTextBox.Foreground = Brushes.Gray;
            }
        }

        private void ContentTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (ContentTextBox.Text == "Введите текст...")
            {
                ContentTextBox.Text = "";
                ContentTextBox.Foreground = Brushes.Black;
            }
        }

        private void ContentTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ContentTextBox.Text))
            {
                ContentTextBox.Text = "Введите текст...";
                ContentTextBox.Foreground = Brushes.Gray;
            }
        }

        private void UpdatePlaceholderText()
        {
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text))
            {
                TitleTextBox.Text = "Введите заголовок...";
                TitleTextBox.Foreground = Brushes.Gray;
            }

            if (string.IsNullOrWhiteSpace(ContentTextBox.Text))
            {
                ContentTextBox.Text = "Введите текст...";
                ContentTextBox.Foreground = Brushes.Gray;
            }
        }
    }
}

