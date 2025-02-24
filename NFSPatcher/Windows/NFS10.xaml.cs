﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using WinForms = System.Windows.Forms;

namespace NFSPatcher.Windows
{
    public partial class NFS10 : Window
    {
        // Strings
        string gameName = "Need for Speed: Carbon";
        string gameNameS = "NFS: Carbon";
        string exeName = "NFSC.exe";

        // Paths
        string rootPath;
        string tempPath;

        string installPath;
        string gameExe;
        string noCDPath;

        string patchZip;
        string patchExe;
        string wfpZip;
        string wfpPath;
        string dInputDLL;

        string patchName = "patch_1.2_1.3_1.4";
        string patchNameZip = "nfsc_v1.4_en.zip";
        string patchNameExe = "patch_1.2_1.3_1.4.exe";

        string patchNameCE = "patch_1.2_1.3_1.4";
        string patchNameZipCE = "nfsc_ce_v1.4_us.zip";
        string patchNameExeCE = "patch_1.2_1.3_1.4.exe";

        // Bools
        bool isBusy = false;
        bool isCollectors = false;
        bool installDetected = false;
        bool selectPathCanceled = false;

        // Links
        string patchLink = "https://github.com/KilLo445/NFSPatcher/raw/main/Remote/PatchFiles/Games/Carbon/nfsc_v1.4_en.zip";
        string patchLinkCE = "https://github.com/KilLo445/NFSPatcher/raw/main/Remote/PatchFiles/Games/Carbon/nfsc_ce_v1.4_us.zip";
        string dInputMapperLink = "https://github.com/KilLo445/NFSPatcher/raw/main/Remote/PatchFiles/DirectInputMapper/dimap.dll";
        string widescreenLink = "https://github.com/ThirteenAG/WidescreenFixesPack/releases/download/nfsc/NFSCarbon.WidescreenFix.zip";
        string widescreenLinkBak = "https://github.com/KilLo445/NFSPatcher/raw/main/Remote/PatchFiles/Games/Carbon/WidescreenFix/NFSCarbon.WidescreenFix.zip";

        string noCDLink = "https://github.com/KilLo445/NFSPatcher/raw/main/Remote/PatchFiles/Games/Carbon/NoCD/NFSC.exe";

        public NFS10()
        {
            InitializeComponent();

            rootPath = Directory.GetCurrentDirectory();
            tempPath = Path.Combine(Path.GetTempPath(), "NFSPatcher", "Carbon");

            patchZip = Path.Combine(tempPath, patchNameZip);
            patchExe = Path.Combine(tempPath, patchNameExe);

            wfpZip = Path.Combine(tempPath, "NFSCarbon.WidescreenFix.zip");
            dInputDLL = Path.Combine(tempPath, "dimap.dll");
        }

        private void CloseButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isBusy == true) { return; }
            try
            {
                try
                {
                    Directory.Delete(tempPath, true);
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
            Directory.CreateDirectory(tempPath);

            selectPathCanceled = false;

            string comboBoxSelection = ComboBox.Text;
            if (comboBoxSelection == null || comboBoxSelection == "")
            {
                MessageBox.Show("Please select a patch method.", "NFSPatcher", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            MessageBoxResult isCEConfirm = MessageBox.Show($"Do you have {gameName} Collectors Edition?\n\nIf you don't know, you probably do.", $"{gameName}", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (isCEConfirm == MessageBoxResult.Yes) { isCollectors = true; }
            if (isCEConfirm == MessageBoxResult.No) { isCollectors = false; }

            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Electronic Arts\Need for Speed Carbon", true);
            if (key != null)
            {
                Object obPath = key.GetValue("Install Dir");
                if (obPath != null)
                {
                    installPath = (obPath as String);
                    key.Close();
                    installDetected = true;
                }
                else { installDetected = false; }
            }

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
            
            if (comboBoxSelection != null)
            {
                if (comboBoxSelection == "Main Patches")
                {
                    MainPatch();
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
        // Main Patches
        ////////////////////

        private void MainPatch()
        {
            PatchList.Visibility = Visibility.Hidden;
            CloseButton.Visibility = Visibility.Hidden;

            pb.IsIndeterminate = true;

            MessageBoxResult patchConfirm = MessageBox.Show($"Are you sure you want to patch {gameName}?", $"{gameName}", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (patchConfirm == MessageBoxResult.No)
            {
                pb.IsIndeterminate = false;
                PatchList.Visibility = Visibility.Visible;
                CloseButton.Visibility = Visibility.Visible;
                return;
            }

            try
            {
                if (isCollectors == true)
                {
                    StatusText.Text = "Downloading v1.4 Patch... (1 of 3)";
                    patchName = patchNameCE;
                    patchNameZip = patchNameZipCE;
                    patchNameExe = patchNameExeCE;
                    patchZip = Path.Combine(tempPath, patchNameZipCE);
                    patchExe = Path.Combine(tempPath, patchNameExeCE);
                    pb.IsIndeterminate = false;
                    WebClient webClient = new WebClient();
                    webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(PatchDLComplete);
                    webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                    webClient.DownloadFileAsync(new Uri(patchLinkCE), patchZip);
                }
                else if (isCollectors == false)
                {
                    StatusText.Text = "Downloading v1.4 Patch... (1 of 3)";
                    pb.IsIndeterminate = false;
                    WebClient webClient = new WebClient();
                    webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(PatchDLComplete);
                    webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                    webClient.DownloadFileAsync(new Uri(patchLink), patchZip);
                }
            }
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return; }
        }

        private void PatchDLComplete(object sender, AsyncCompletedEventArgs e)
        {
            pb.Value = 0;
            pb.IsIndeterminate = false;
            try
            {
                StatusText.Text = "Downloading Widescreen Fix... (2 of 3)";
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(WFPDLComplete);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                webClient.DownloadFileAsync(new Uri(widescreenLink), wfpZip);
            }
            catch
            {
                StatusText.Text = "Downloading Widescreen Fix (Backup)... (2 of 3)";
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(WFPDLComplete);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                webClient.DownloadFileAsync(new Uri(widescreenLinkBak), wfpZip);
            }
        }

        private void WFPDLComplete(object sender, AsyncCompletedEventArgs e)
        {
            pb.Value = 0;
            pb.IsIndeterminate = false;
            try
            {
                StatusText.Text = "Downloading DirectInputMapper... (3 of 3)";
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DInputDLComplete);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                webClient.DownloadFileAsync(new Uri(dInputMapperLink), dInputDLL);
            }
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return; }
        }

        private void DInputDLComplete(object sender, AsyncCompletedEventArgs e)
        {
            pb.Value = 0;
            InstallPatch();
        }

        private async Task InstallPatch()
        {
            await Task.Delay(500);
            pb.IsIndeterminate = true;
            try
            {
                StatusText.Text = $"Preparing patch...";
                if (File.Exists(Path.Combine(installPath, "dimap.dll")))
                {
                    File.Delete(Path.Combine(installPath, "dimap.dll"));
                }
                if (File.Exists(Path.Combine(installPath, "dinput8.dll")))
                {
                    File.Delete(Path.Combine(installPath, "dinput8.dll"));
                }
                if (Directory.Exists(Path.Combine(installPath, "scripts")))
                {
                    Directory.Delete(Path.Combine(installPath, "scripts"), true);
                }

                StatusText.Text = $"Extracting {patchNameZip}..."; await Task.Run(() => ZipFile.ExtractToDirectory(patchZip, tempPath));
                StatusText.Text = $"Deleting {patchNameZip}..."; await Task.Run(() => File.Delete(patchZip));
                await Task.Delay(500);
                wfpPath = Path.Combine(tempPath, "scripts");
                Directory.CreateDirectory(wfpPath);
                StatusText.Text = $"Extracting NFSCarbon.WidescreenFix.zip..."; await Task.Run(() => ZipFile.ExtractToDirectory(wfpZip, wfpPath));
                StatusText.Text = $"Deleting NFSCarbon.WidescreenFix.zip..."; await Task.Run(() => File.Delete(wfpZip));
                await Task.Delay(500);
                StatusText.Text = $"Starting {patchNameExe}..."; Process.Start(patchExe);
                await Task.Delay(300);
                bool patchRunning;
                StatusText.Text = $"Waiting for {patchNameExe}...";
                if (Process.GetProcessesByName($"{patchName}").Length > 0)
                {
                    patchRunning = true;
                }
                else
                {
                    patchRunning = false;
                }
                while (patchRunning == true)
                {
                    if (Process.GetProcessesByName($"{patchName}").Length > 0)
                    {
                        patchRunning = true;
                    }
                    else
                    {
                        patchRunning = false;
                    }

                    await Task.Delay(500);
                }
                await Task.Delay(800);
                StatusText.Text = $"Deleting ${patchNameExe}..."; await Task.Run(() => File.Delete(patchExe));
                await Task.Delay(300);
                StatusText.Text = $"Installing WidescreenFix... (1 of 4)"; await Task.Run(() => File.Copy(Path.Combine(wfpPath, "dinput8.dll"), Path.Combine(installPath, "dinput8.dll")));
                await Task.Delay(300);
                StatusText.Text = $"Installing WidescreenFix... (2 of 4)"; await Task.Run(() => File.Delete(Path.Combine(wfpPath, "dinput8.dll")));
                await Task.Delay(300);

                StatusText.Text = $"Installing WidescreenFix... (3 of 4)";
                string sourceDir = Path.Combine(wfpPath, "scripts");
                string destDir = Path.Combine(installPath, "scripts");
                var dir = new DirectoryInfo(sourceDir);
                DirectoryInfo[] dirs = dir.GetDirectories();
                Directory.CreateDirectory(destDir);
                foreach (FileInfo file in dir.GetFiles())
                {
                    string targetFilePath = Path.Combine(destDir, file.Name);
                    file.CopyTo(targetFilePath);
                }
                await Task.Delay(300);

                StatusText.Text = $"Installing WidescreenFix... (4 of 4)"; await Task.Run(() => Directory.Delete(wfpPath, true));
                await Task.Delay(300);

                StatusText.Text = $"Installing DirectInputMapper... (1 of 2)"; await Task.Run(() => File.Copy(dInputDLL, Path.Combine(installPath, "dimap.dll")));
                await Task.Delay(300);
                StatusText.Text = $"Installing DirectInputMapper... (2 of 2)"; await Task.Run(() => File.Delete(dInputDLL));
                await Task.Delay(300);
                StatusText.Text = $"Waiting for user...";
                MessageBoxResult wfpCFG = System.Windows.MessageBox.Show("Would you like to open the Widescreen Fix configuration?", $"{gameName}", System.Windows.MessageBoxButton.YesNo);
                if (wfpCFG == MessageBoxResult.Yes)
                {
                    StatusText.Text = $"Opening Widescreen Fix configuration...";
                    string wfpCFGPath = Path.Combine(installPath, "scripts", "NFSCarbon.WidescreenFix.ini");
                    Process.Start(wfpCFGPath);
                }

                StatusText.Text = "Welcome to NFSPatcher!";
                pb.IsIndeterminate = false;
                MessageBox.Show("All patches installed successfully!", $"{gameName}", MessageBoxButton.OK, MessageBoxImage.Information);
                PatchList.Visibility = Visibility.Visible;
                CloseButton.Visibility = Visibility.Visible;
                return;
            }
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        ////////////////////
        // NoCD
        ////////////////////

        private void NoCDPatch()
        {
            PatchList.Visibility = Visibility.Hidden;

            noCDPath = Path.Combine(tempPath, exeName);

            pb.IsIndeterminate = true;

            if (isCollectors == false) { MessageBox.Show($"This NoCD patch was made for {gameName} Collectors Edition.\n\nIt may not work on the normal edition.", $"{gameName}", MessageBoxButton.OK, MessageBoxImage.Information); }
            MessageBoxResult noCDConfirm = MessageBox.Show("Are you sure you want to install the NoCD Patch?", $"{gameName}", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (noCDConfirm == MessageBoxResult.No)
            {
                pb.IsIndeterminate = false;
                PatchList.Visibility = Visibility.Visible;
                return;
            }

            StatusText.Text = "Downloading NoCD Patch...";
            pb.IsIndeterminate = false;
            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(NoCDDLComplete);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            webClient.DownloadFileAsync(new Uri(noCDLink), noCDPath);
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
                CloseButton.Visibility = Visibility.Visible;
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
