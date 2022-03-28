using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace NFSPatcherGUI
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

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            
        }

        private void Carbon_Click(object sender, RoutedEventArgs e)
        {
            Carbon carbonWindow = new Carbon();
            carbonWindow.Show();
            this.Close();
        }

        private void ProStreet_Click(object sender, RoutedEventArgs e)
        {
            ProStreet psWindow = new ProStreet();
            psWindow.Show();
            this.Close();
        }

        private void HotPursuit_Click(object sender, RoutedEventArgs e)
        {
            HotPursuit hpWindow = new HotPursuit();
            hpWindow.Show();
            this.Close();
        }

        private void Underground_Click(object sender, RoutedEventArgs e)
        {
            Underground ugWindow = new Underground();
            ugWindow.Show();
            this.Close();
        }

        private void Underground2_Click(object sender, RoutedEventArgs e)
        {
            Underground2 ug2Window = new Underground2();
            ug2Window.Show();
            this.Close();
        }

        private void MostWanted_Click(object sender, RoutedEventArgs e)
        {
            MostWanted mwWindow = new MostWanted();
            mwWindow.Show();
            this.Close();
        }

        private void GitHub_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/KilLo445/NFSPatcher");
        }
    }
}
