using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using WinForms = System.Windows.Forms;

namespace NFSPatcher.Windows
{
    public partial class NFS18 : Window
    {
        // Strings
        string gameName = "Need for Speed: The Run";
        string gameNameS = "NFS: The Run";
        string exeName = "Need For Speed The Run.exe";

        // Paths
        string rootPath;
        string tempPath;

        string installPath;
        string gameExe;
        string noCDPath;

        // Bat Files
        string createBat;
        string deleteBat;

        // Bools
        bool isBusy = false;
        bool isLimited = false;
        bool installDetected = false;
        bool selectPathCanceled = false;

        // Links
        string noCDLink = "https://github.com/KilLo445/NFSPatcher/raw/main/Remote/PatchFiles/Games/TR/Crack/Need%20For%20Speed%20The%20Run.exe";
        string noCDLELink = "https://github.com/KilLo445/NFSPatcher/raw/main/Remote/PatchFiles/Games/TR/Crack/LE/Need%20For%20Speed%20The%20Run.exe";

        public NFS18()
        {
            InitializeComponent();

            rootPath = Directory.GetCurrentDirectory();
            tempPath = Path.Combine(Path.GetTempPath(), "NFSPatcher", "TR");

            createBat = Path.Combine(tempPath, "NFS_TR_CREATE_FIREWALL.bat");
            deleteBat = Path.Combine(tempPath, "NFS_TR_DELETE_FIREWALL.bat");

            if (Directory.Exists(tempPath)) { Directory.Delete(tempPath, true); }
            Directory.CreateDirectory(tempPath);
        }

        private void CloseButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isBusy == true) { return; }
            try
            {
                try
                {
                    Directory.Delete(tempPath);
                }
                catch { }

                MainWindow mWindow = new MainWindow();
                this.Close();
                mWindow.Show();
            }
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void GoBTN_Click(object sender, RoutedEventArgs e)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\EA Games\Need for Speed(TM) The Run", true);
            Object obPath = key.GetValue("Install Dir");
            if (obPath != null)
            {
                installPath = (obPath as String);
                if (installPath.EndsWith("\\") || installPath.EndsWith("/")) { installPath = installPath.Remove(installPath.Length - 1, 1); }
                key.Close();
                installDetected = true;
            }
            else { installDetected = false; }

            if (installDetected == true)
            {
                MessageBoxResult installConfirm = MessageBox.Show($"{gameName} appears to be installed at \"{installPath}\"\n\nIs this correct?", $"{gameName}", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (installConfirm == MessageBoxResult.No)
                {
                    SelectInstallPath();
                    if (selectPathCanceled == true) { return; }
                }
                else { gameExe = Path.Combine(installPath, $"{exeName}"); }
            }
            if (installDetected == false)
            {
                SelectInstallPath();
                if (selectPathCanceled == true) { return; }
            }

            string comboBoxSelection = ComboBox.Text;
            if (comboBoxSelection == null || comboBoxSelection == "")
            {
                MessageBox.Show("Please select a patch method.", "NFSPatcher", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (comboBoxSelection != null)
            {
                if (comboBoxSelection == "Create Firewall Rule")
                {
                    CreateFirewallRule();
                }
                if (comboBoxSelection == "Delete Firewall Rule")
                {
                    DeleteFirewallRule();
                }
                if (comboBoxSelection == "NoCD Patch")
                {
                    NoCDPatch();
                }
            }
            else { return; }
        }

        private void SelectInstallPath()
        {
            WinForms.FolderBrowserDialog pathDialog = new WinForms.FolderBrowserDialog();
            pathDialog.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;
            pathDialog.Description = $"Please select your {gameNameS} install folder.\n\n(Location with {exeName})";
            pathDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            pathDialog.ShowNewFolderButton = false;
            WinForms.DialogResult pathResult = pathDialog.ShowDialog();
            if (pathResult == WinForms.DialogResult.OK)
            {
                installPath = Path.Combine(pathDialog.SelectedPath);
                gameExe = Path.Combine(installPath, $"{exeName}");
                if (!File.Exists(gameExe))
                {
                    selectPathCanceled = true;
                    MessageBox.Show($"Please select the location with {exeName}", "NFSPatcher", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                return;
            }
            else
            {
                pb.IsIndeterminate = false;
                selectPathCanceled = true;
                StatusText.Text = $"Welcome to NFSPatcher!";
                return;
            }
        }

        ////////////////////
        // Firewall
        ////////////////////
        
        private void CreateFirewallRule()
        {
            MessageBoxResult patchConfirm1 = MessageBox.Show($"Are you sure you want to create the firewall rule?", $"{gameName}", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (patchConfirm1 == MessageBoxResult.No) { return; }

            string[] createCMD ={
                            "@echo off",
                            "title NFSPatcher",
                            $"netsh advfirewall firewall add rule name=\"{gameName}\" dir=out program=\"{installPath}\\{exeName}\" profile=any action=block",
                            "exit"
                          };
            try
            {
                if (File.Exists(createBat)) { File.Delete(createBat); }
                File.WriteAllLines(createBat, createCMD);
                Process.Start(createBat);
                MessageBox.Show("Firewall rule created!", "NFSPatcher", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void DeleteFirewallRule()
        {
            MessageBoxResult patchConfirm2 = MessageBox.Show($"Are you sure you want to delete the firewall rule?", $"{gameName}", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (patchConfirm2 == MessageBoxResult.No) { return; }

            string[] deleteCMD ={
                            "@echo off",
                            "title NFSPatcher",
                            $"netsh advfirewall firewall delete rule name=\"{gameName}\"",
                            "exit"
                          };
            try
            {
                if (File.Exists(deleteBat)) { File.Delete(deleteBat); }
                File.WriteAllLines(deleteBat, deleteCMD);
                Process.Start(deleteBat);
                MessageBox.Show("Firewall rule deleted!", "NFSPatcher", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        ////////////////////
        // NoCD
        ////////////////////

        private void NoCDPatch()
        {
            MessageBoxResult isLEConfirm = MessageBox.Show($"Do you have {gameName} Limited Edition?", $"{gameName}", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (isLEConfirm == MessageBoxResult.Yes) { isLimited = true; }
            if (isLEConfirm == MessageBoxResult.No) { isLimited = false; }

            PatchList.Visibility = Visibility.Hidden;

            noCDPath = Path.Combine(tempPath, exeName);

            pb.IsIndeterminate = true;

            MessageBoxResult noCDConfirm = MessageBox.Show("Are you sure you want to install the NoCD Patch?", $"{gameName}", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (noCDConfirm == MessageBoxResult.No)
            {
                pb.IsIndeterminate = false;
                PatchList.Visibility = Visibility.Visible;
                return;
            }

            StatusText.Text = "Downloading NoCD Patch...";

            if (isLimited == true)
            {
                pb.IsIndeterminate = false;
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(NoCDDLComplete);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                webClient.DownloadFileAsync(new Uri(noCDLELink), noCDPath);
            }
            else if (isLimited == false)
            {
                pb.IsIndeterminate = false;
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(NoCDDLComplete);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                webClient.DownloadFileAsync(new Uri(noCDLink), noCDPath);
            }
        }

        private void NoCDDLComplete(object sender, AsyncCompletedEventArgs e)
        {
            pb.Value = 0;
            InstallNoCD();
        }

        private async Task InstallNoCD()
        {
            pb.IsIndeterminate = true;
            try
            {
                if (File.Exists(gameExe))
                {
                    StatusText.Text = $"Deleting {exeName}..."; await Task.Run(() => File.Delete(gameExe));
                }
                await Task.Delay(500);
                StatusText.Text = $"Installing NoCD Patch... (1 of 2)"; await Task.Run(() => File.Copy(noCDPath, gameExe));
                await Task.Delay(500);
                StatusText.Text = $"Installing NoCD Patch... (2 of 2)"; await Task.Run(() => File.Delete(noCDPath));
                await Task.Delay(300);

                StatusText.Text = "Welcome to NFSPatcher!";
                pb.IsIndeterminate = false;
                MessageBox.Show("NoCD Patch installed!", $"{gameName}", MessageBoxButton.OK, MessageBoxImage.Information);
                PatchList.Visibility = Visibility.Visible;
                return;
            }
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        ////////////////////
        // Other
        ////////////////////

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            pb.Value = e.ProgressPercentage;
        }

        private void DragWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                try
                {
                    DragMove();
                }
                catch { }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (isBusy == true)
            {
                e.Cancel = true;
            }
        }
    }
}
