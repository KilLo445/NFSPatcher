using NFSPatcher.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace NFSPatcher
{
    public partial class MainWindow : Window
    {
        // Strings
        public static string version = "1.0.4";
        string latestVer = "https://raw.githubusercontent.com/KilLo445/NFSPatcher/main/Remote/latest-version.txt";

        string githubLink = "https://github.com/KilLo445/NFSPatcher";
        string latestRelease = "https://github.com/KilLo445/NFSPatcher/releases/latest";
        string faqLink = "https://github.com/KilLo445/NFSPatcher/blob/main/FAQ.md";

        // Paths
        string rootPath;
        string tempPath;

        bool updateAvailable;
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

            var random = new Random();
            var list = new List<string> {
                "VROOOOM",
                "First, I'm gonna take your ride, then I'm gonna take your girl!",
                "Sit down! How you been?",
                "I want every single unit after the guy. Everyone? EVERYONE!",
                "Bus stop's that way, champ.",
                "Hey, where's your fancy ride?",
                "Ouch! That's totally weak dude!",
                "How's your car running?",
                "Cars? I got covered.",
                "I'm glad you're here, sugar plum.",
                "EA Games. Make running these games challenging... get it??? ...",
            };
            int index = random.Next(list.Count);
            randText.Text = list[index];

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

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            CheckForUpdates();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            try
            {
                Directory.Delete(tempPath, true);
            }
            catch { }
        }

        private void CheckForUpdates()
        {
            try
            {
                Version localVersion = new Version(version);
                WebClient webClient = new WebClient();
                Version onlineVersion = new Version(webClient.DownloadString(latestVer));
                if (onlineVersion.IsDifferentThan(localVersion))
                {
                    updateAvailable = true;
                    MessageBoxResult updateAvailableMSG = MessageBox.Show("An update for NFS Patcher has been found! Would you like to download it?", "Update Available", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    if (updateAvailableMSG == MessageBoxResult.Yes) { Process.Start(latestRelease); Application.Current.Shutdown(); }
                }
                else { updateAvailable = false; }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking for updates.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            try
            {
                NFS4 hsWindow = new NFS4();
                this.Close();
                hsWindow.Show();
            }
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        // NFS: Hot Pursuit 2
        private void NFS6()
        {
            try
            {
                NFS6 hp2Window = new NFS6();
                this.Close();
                hp2Window.Show();
            }
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
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

        private void OpenTemp_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(tempPath))
            {
                try
                {
                    Process.Start(tempPath);
                }
                catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
            else { MessageBox.Show("Temp path does not exist.", "NFSPatcher", MessageBoxButton.OK, MessageBoxImage.Error); }
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

        private void CheckUpdate_Click(object sender, RoutedEventArgs e)
        {
            CheckForUpdates();
            if (updateAvailable == false) { MessageBox.Show("No update available", "Check for updates", MessageBoxButton.OK, MessageBoxImage.Information); }
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

        private void ViewFAQ_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(faqLink);
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
                Process.Start(latestRelease);
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

        private void ExitApp_Click(object sender, RoutedEventArgs e)
        {
            if(Directory.Exists(tempPath))
            {
                try
                {
                    Directory.Delete(tempPath, true);
                }
                catch { }
            }

            try
            {
                Application.Current.Shutdown();
            }
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }

    struct Version
    {
        internal static Version zero = new Version(0, 0, 0);

        private short major;
        private short minor;
        private short subMinor;

        internal Version(short _major, short _minor, short _subMinor)
        {
            major = _major;
            minor = _minor;
            subMinor = _subMinor;
        }
        internal Version(string _version)
        {
            string[] versionStrings = _version.Split('.');
            if (versionStrings.Length != 3)
            {
                major = 0;
                minor = 0;
                subMinor = 0;
                return;
            }

            major = short.Parse(versionStrings[0]);
            minor = short.Parse(versionStrings[1]);
            subMinor = short.Parse(versionStrings[2]);
        }

        internal bool IsDifferentThan(Version _otherVersion)
        {
            if (major != _otherVersion.major)
            {
                return true;
            }
            else
            {
                if (minor != _otherVersion.minor)
                {
                    return true;
                }
                else
                {
                    if (subMinor != _otherVersion.subMinor)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override string ToString()
        {
            return $"{major}.{minor}.{subMinor}";
        }
    }
}
