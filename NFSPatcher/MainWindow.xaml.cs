using NFSPatcher.Windows;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NFSPatcher
{
    public partial class MainWindow : Window
    {
        // Strings
        string version = "0.1.0";

        string githubLink = "https://github.com/KilLo445/NFSPatcher";
        string latestReleasePage = "https://github.com/KilLo445/NFSPatcher/releases/latest";

        // Paths
        string rootPath;

        public MainWindow()
        {
            if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1)
            {
                MessageBox.Show("NFSPatcher is already running.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }

            InitializeComponent();

            rootPath = Directory.GetCurrentDirectory();

            VersionText.Text = "v" + version;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            string tempPath = Path.Combine(Path.GetTempPath(), "NFSPatcher");
            if (Directory.Exists(tempPath))
            {
                try
                {
                    Directory.Delete(tempPath);
                } catch { }
            }
        }

        private void GoBTN_Click(object sender, RoutedEventArgs e)
        {
            string comboBoxSelection = ComboBox.Text;
            if (comboBoxSelection == null || comboBoxSelection == "")
            {
                MessageBox.Show("Please select a game.", "NFSPatcher", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (comboBoxSelection != null)
            {
                if (comboBoxSelection == "Need for Speed III: Hot Pursuit")
                {
                    NFS3();
                }
                if (comboBoxSelection == "Need for Speed: Hot Pursuit 2")
                {
                    NFS6();
                }
                if (comboBoxSelection == "Need for Speed: Underground")
                {
                    NFS7();
                }
                if (comboBoxSelection == "Need for Speed: Underground 2")
                {
                    NFS8();
                }
                if (comboBoxSelection == "Need for Speed: Most Wanted")
                {
                    NFS9();
                }
                if (comboBoxSelection == "Need for Speed: Carbon")
                {
                    NFS10();
                }
            }
            else { return; }
        }

        ////////////////////
        // Games
        ////////////////////

        // NFS III: Hot Pursuit
        private void NFS3()
        {
            MessageBox.Show("Coming soon.", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // NFS: Hot Pursuit 2
        private void NFS6()
        {
            MessageBox.Show("Coming soon.", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // NFS: Underground
        private void NFS7()
        {
            try
            {
                NFS7 ugWindow = new NFS7();
                this.Close();
                ugWindow.Show();
            }
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        // NFS: Underground 2
        private void NFS8()
        {
            MessageBox.Show("Coming soon.", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // NFS: Most Wanted (2005)
        private void NFS9()
        {
            MessageBox.Show("Coming soon.", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // NFS: Carbon
        private void NFS10()
        {
            MessageBox.Show("Coming soon.", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        ////////////////////
        // Settings
        ////////////////////

        private void Settings_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Image image = sender as Image;
                ContextMenu contextMenu = image.ContextMenu;
                contextMenu.PlacementTarget = image;
                contextMenu.IsOpen = true;
                e.Handled = true;
            }
        }

        private void RootPath_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(rootPath);
            }
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void ViewChangelog_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Changelog changelogWindow = new Changelog();
                changelogWindow.Show();
            }
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void OpenGitHub_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(githubLink);
            }
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void OpenReleasePage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(latestReleasePage);
            }
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void ViewAbout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                About aboutWindow = new About();
                aboutWindow.Show();
            }
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
