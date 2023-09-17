using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace NFSPatcher
{
    
    public partial class About : Window
    {
        string Web = "GITHUB.COM/KILLO445/NFSPATCHER";
        string Web2 = "https://github.com/KilLo445/NFSPatcher";

        public About()
        {
            InitializeComponent();

            this.Topmost = true;
            AboutWebsite.Text = $"{Web}";
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                try
                {
                    DragMove();
                }
                catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }

        private void CloseButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.Topmost = false;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void AboutWebsite_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Process.Start(Web2);
            }
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
