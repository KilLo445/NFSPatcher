using NFSPatcher.Windows;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace NFSPatcher
{
    public partial class MainWindow : Window
    {
        // Strings
        string version = "0.4.1";

        string githubLink = "https://github.com/KilLo445/NFSPatcher";
        string latestReleasePage = "https://github.com/KilLo445/NFSPatcher/releases/latest";

        // Paths
        string rootPath;
        string tempPath;

        bool delTempOnStart = false;

        public MainWindow()
        {
            if (Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1)
            {
                MessageBox.Show("NFSPatcher is already running.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }

            InitializeComponent();

            rootPath = Directory.GetCurrentDirectory();
            tempPath = Path.Combine(Path.GetTempPath(), "NFSPatcher");

            VersionText.Text = "v" + version;

            if (delTempOnStart == true)
            {
                if (Directory.Exists(tempPath))
                {
                    try
                    {
                        Directory.Delete(tempPath, true);
                        MessageBox.Show("Temp path deleted.", "NFSPatcher", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
                }
                else { MessageBox.Show("Temp path does not exist.", "NFSPatcher", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
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
                if (comboBoxSelection == "Need for Speed II SE")
                {
                    NFS2();
                }
                if (comboBoxSelection == "Need for Speed III: Hot Pursuit")
                {
                    NFS3();
                }
                if (comboBoxSelection == "Need for Speed: High Stakes")
                {
                    NFS4();
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
                if (comboBoxSelection == "Need for Speed: The Run")
                {
                    NFS18();
                }
            }
            else { return; }
        }

        ////////////////////
        // Games
        ////////////////////

        // NFS II SE
        private void NFS2()
        {
            try
            {
                NFS2 iiWindow = new NFS2();
                this.Close();
                iiWindow.Show();
            }
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        // NFS III: Hot Pursuit
        private void NFS3()
        {
            try
            {
                NFS3 iiiWindow = new NFS3();
                this.Close();
                iiiWindow.Show();
            }
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        // NFS: High Stakes
        private void NFS4()
        {
            MessageBox.Show("Coming soon.", "NFSPatcher", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // NFS: Hot Pursuit 2
        private void NFS6()
        {
            MessageBox.Show("Coming soon.", "NFSPatcher", MessageBoxButton.OK, MessageBoxImage.Information);
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
            try
            {
                NFS8 ug2Window = new NFS8();
                this.Close();
                ug2Window.Show();
            }
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        // NFS: Most Wanted (2005)
        private void NFS9()
        {
            try
            {
                NFS9 mwWindow = new NFS9();
                this.Close();
                mwWindow.Show();
            }
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        // NFS: Carbon
        private void NFS10()
        {
            try
            {
                NFS10 cWindow = new NFS10();
                this.Close();
                cWindow.Show();
            }
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        // NFS: The Run
        private void NFS18()
        {
            try
            {
                NFS18 trWindow = new NFS18();
                this.Close();
                trWindow.Show();
            }
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
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

        private void Settings_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                this.Settings.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Gear/GearGray.png"));
            }
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void Settings_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                this.Settings.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Gear/GearWhite.png"));
            }
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void RootPath_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(rootPath);
            }
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void DelTemp_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(tempPath))
            {
                try
                {
                    Directory.Delete(tempPath, true);
                    MessageBox.Show("Temp path deleted.", "NFSPatcher", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
            else { MessageBox.Show("Temp path does not exist.", "NFSPatcher", MessageBoxButton.OK, MessageBoxImage.Error); }
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
