using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;

namespace My_Briefcase
{
    /// <summary>
    /// Interaction logic for Decrypt.xaml
    /// </summary>
    public partial class Decrypt : Window
    {
        public Decrypt()
        {
            InitializeComponent();
        }

        public string path;
        public string filename;
        public string status;

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the appropriate file name", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                filename = comboBox.SelectedValue.ToString();
                MessageBoxResult result = MessageBox.Show("Are you sure you want to Decrypt the Data ?", "Confirm Decryption", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    path = filename;
                    DecryptData();
                }
            }
        }

        private void DecryptData()
        {
            try
            {
                TripleDES tDES = new();
                tDES.DecryptFile(path);
                GC.Collect();
                TextReader reader = new StreamReader(path);
                richTextBox.Document.Blocks.Add(new Paragraph(new Run(reader.ReadToEnd())));
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            status = "Decrypted";
            label2.Content = this.status.ToString();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (status == "Decrypted")
            {
                
                if (path == "")
                {
                    MessageBox.Show("Please select the appropriate file name", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
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
                    MainWindow home = new();
                    home.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Please select an appropriarte encrypted file", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string[] filePaths = Directory.GetFiles(@"D:\My Secrets\", "*.file");
            foreach (string file in filePaths)
            {
                comboBox.Items.Add(file);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            MainWindow home = new();
            home.Show();
            this.Close();
        }
    }
}
