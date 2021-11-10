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

namespace My_Briefcase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void activity_Checked(object sender, RoutedEventArgs e)
        {
            NewFile Encryption = new();
            Encryption.Show();
            this.Close();
        }

        private void activity1_Checked(object sender, RoutedEventArgs e)
        {
            Decrypt dec = new();
            dec.Show();
            this.Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // DragMove();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        { 
            if(txtUsername.Text == "" && txtPassword.Password == "")
            {
                MessageBox.Show("Please input the user name and password fields");
            }
            else
            {
                LoginStack.Visibility = Visibility.Collapsed;
                WhatDo.Visibility = Visibility.Visible;
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            WhatDo.Visibility = Visibility.Collapsed;
        }
    }
}
