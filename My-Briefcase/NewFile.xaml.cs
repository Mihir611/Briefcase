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
using System.IO;
using System.Windows.Threading;
using System.Security.AccessControl;

namespace My_Briefcase
{
    /// <summary>
    /// Interaction logic for NewFile.xaml
    /// </summary>
    public partial class NewFile : Window
    {
        public NewFile()
        {
            InitializeComponent();
        }

        public string path;
        public string ans = "";
        public string filename;
        public int ticks = 0;
        readonly DispatcherTimer timer = new();

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Home = new();
            Home.Show();
            this.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            filename = file_Name.Text;
            if (file_Name.Text != "")
            {
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += Timer_Tick;
                timer.Start();
            }
            else
            {
                MessageBox.Show("Please enter the File name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ticks++;
            current_Time.Content = ticks.ToString();

            if (ticks < 5)
            {
                current_Status.Content = "Initiliazing....".ToString();
            }

            if (ticks == 5)
            {
                CheckIfFileExists();
            }

            if (ticks == 10)
            {
                if(ans == "1")
                {
                    timer.Stop();
                    current_Status.Content = "File already exists plaese enter a new file name".ToString();
                }
                else
                {
                    CreatingFile();
                }
            }

            if (ticks == 20)
            {
                Encryptring();
            }

            if (ticks > 27)
            {
                timer.Stop();
                current_Status.Content = "";
                MessageBoxResult result = MessageBox.Show("File has been Encrypted Successfully", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    MainWindow home = new();
                    home.Show();
                    this.Close();
                }
            }
        }

        private void CheckIfFileExists()
        {
            current_Status.Content = "Checking if File Exists".ToString();
            path = @"D:\My Secrets\'" + filename + "'.file";
            ans = File.Exists(path) ? "1" : "0";
        }

        public void CreatingFile()
        {
            current_Status.Content = "Creating the File".ToString();
            string Content = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text;
            TextWriter writer = new StreamWriter(path);
            writer.Write(Content);
            writer.Close();
        }

        public void Encryptring()
        {
            try
            {
                TripleDES tDES = new();
                tDES.EncryptFile(path);
                GC.Collect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            current_Status.Content = "Encrypting...".ToString();
        }
    }
}
