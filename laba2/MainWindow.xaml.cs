using LibraryDecorator;
using Microsoft.Win32;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

namespace laba2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private int n = 10;
        private string filePath = "C:\\Users\\User\\Desktop\\учеба\\ООПиП\\лаб2\\lab2.txt";

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                TextBoxFilePath.Text = openFileDialog.FileName;
            }
        }

        private void GetTime(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxFilePath.Text) && Regex.IsMatch(TextBoxFilePath.Text, "\\.txt$"))
            {
                filePath = TextBoxFilePath.Text;
            }
            else
            {
                MessageBox.Show("Файл не выбран или неверного формата");
                return;
            }
            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    byte[] bufferFull = new byte[fileStream.Length];
                    var decorator = new FileStreamDecorator(fileStream);
                    int bytesRead = decorator.Read(bufferFull, 0, (int)fileStream.Length);
                    if (bytesRead == 0)
                    {
                        MessageBox.Show("Файл пустой");
                        return;
                    }
                    FirstResultTextBlock.Text = "Время, в течение которого поток не использовался в FileStream: " + decorator.GetTime();
                }
                using (var memoryStream = new MemoryStream())
                {
                    using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                    {
                        fileStream.CopyTo(memoryStream);
                    }
                    byte[] bufferFull = new byte[memoryStream.Length];
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    var decorator = new MemoryStreamDecorator(memoryStream);
                    int bytesRead = decorator.Read(bufferFull, 0, (int)memoryStream.Length);
                    if (bytesRead == 0)
                    {
                        MessageBox.Show("Файл пустой");
                        return;
                    }
                    SecondResultTextBlock.Text = "Время, в течение которого поток не использовался в MemoryStream: " + decorator.GetTime();
                }
                using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    using (var bufferStream = new BufferedStream(fileStream))
                    {
                        byte[] bufferFull = new byte[bufferStream.Length];
                        var decorator = new BufferedStreamDecorator(bufferStream);
                        int bytesRead = decorator.Read(bufferFull, 0, (int)bufferStream.Length);
                        if (bytesRead == 0)
                        {
                            MessageBox.Show("Файл пустой");
                            return;
                        }
                        ThirdResultTextBlock.Text = "Время, в течение которого поток не использовался в BufferedStream: " + decorator.GetTime();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}