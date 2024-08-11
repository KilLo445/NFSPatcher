using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WinForms = System.Windows.Forms;

namespace NFSPatcher.Windows
{
    public partial class NFS4 : Window
    {
        // Strings
        string gameName = "Need for Speed: High Stakes";
        string gameNameS = "NFS High Stakes";
        string exeName = "NFSHS.EXE";

        // Paths
        string rootPath;
        string tempPath;

        string installPath;
        string discPath;
        string gameExe;

        string patchZip;

        string patchNameZip = "nfs4_modern_patch.zip";
        string patchName2 = "setupMP.exe";

        // Bools
        bool isBusy = false;
        bool selectPathCanceled = false;

        // Links
        string patchLink = "https://github.com/KilLo445/NFSPatcher/raw/main/Remote/PatchFiles/Games/HS/ModernPatch/nfs4_modern_patch.zip";
        string patchLink2 = "https://github.com/KilLo445/NFSPatcher/raw/main/Remote/PatchFiles/Games/HS/ModernPatch/setupMP.exe";

        public NFS4()
        {
            InitializeComponent();

            rootPath = Directory.GetCurrentDirectory();
            tempPath = Path.Combine(Path.GetTempPath(), "NFSPatcher", "HS");

            patchZip = Path.Combine(tempPath, patchNameZip);
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

            if (comboBoxSelection != null)
            {
                if (comboBoxSelection == "Main Patches")
                {
                    SelectInstallPath();
                    if (selectPathCanceled == true) { return; }
                    MainPatch();
                }
            }
            else { return; }
        }

        private void SelectInstallPath()
        {
            WinForms.FolderBrowserDialog pathDialog = new WinForms.FolderBrowserDialog();
            pathDialog.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;
            pathDialog.Description = $"Please select where you would like to install {gameNameS}";
            pathDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            pathDialog.ShowNewFolderButton = true;
            WinForms.DialogResult pathResult = pathDialog.ShowDialog();
            if (pathResult == WinForms.DialogResult.OK)
            {
                installPath = Path.Combine(pathDialog.SelectedPath);
                gameExe = Path.Combine(installPath, $"{exeName}");
                if (Directory.GetFileSystemEntries(installPath).Length == 0)
                {
                    selectPathCanceled = false;
                    return;
                }
                else
                {
                    MessageBox.Show("Please select an empty folder.", "NFSPatcher", MessageBoxButton.OK, MessageBoxImage.Error);
                    selectPathCanceled = true;
                    return;
                }

            }
            else
            {
                pb.IsIndeterminate = false;
                selectPathCanceled = true;
                PatchList.Visibility = Visibility.Visible;
                CloseButton.Visibility = Visibility.Visible;
                StatusText.Text = $"Welcome to NFSPatcher!";
                return;
            }
        }

        ////////////////////
        // Main Patches
        ////////////////////

        private void MainPatch()
        {
            if (Directory.Exists(tempPath)) { Directory.Delete(tempPath, true); Directory.CreateDirectory(tempPath); }

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
                pb.IsIndeterminate = false;
                try
                {
                    StatusText.Text = "Downloading Modern Patch...";
                    Directory.CreateDirectory(tempPath);
                    WebClient webClient = new WebClient();
                    webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(PatchDLComplete);
                    webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                    webClient.DownloadFileAsync(new Uri(patchLink), patchZip);
                }
                catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            }
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return; }
        }

        private void PatchDLComplete(object sender, AsyncCompletedEventArgs e)
        {
            pb.Value = 0;
            MPSetupDL();
        }

        private void MPSetupDL()
        {
            try
            {
                StatusText.Text = "Downloading MP Setup...";
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(PatchDLComplete2);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                webClient.DownloadFileAsync(new Uri(patchLink2), Path.Combine(tempPath, patchName2));
            }
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return; }
        }

        private void PatchDLComplete2(object sender, AsyncCompletedEventArgs e)
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
                StatusText.Text = $"Extracting {patchNameZip}..."; await Task.Run(() => ZipFile.ExtractToDirectory(patchZip, Path.Combine(tempPath, "ModernPatch")));
                StatusText.Text = $"Deleting {patchNameZip}..."; await Task.Run(() => File.Delete(patchZip));
                await Task.Delay(300);
                StatusText.Text = $"Waiting for user...";
                WinForms.FolderBrowserDialog discDialog = new WinForms.FolderBrowserDialog();
                discDialog.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;
                discDialog.Description = $"Please select your {gameNameS} disc.\n\n(Location with AutoRun.exe)";
                discDialog.RootFolder = Environment.SpecialFolder.MyComputer;
                discDialog.ShowNewFolderButton = false;
                WinForms.DialogResult pathResult = discDialog.ShowDialog();
                if (pathResult == WinForms.DialogResult.OK)
                {
                    discPath = Path.Combine(discDialog.SelectedPath);

                    if (File.Exists(Path.Combine(discPath, "AUTORUN.EXE")))
                    {
                        StatusText.Text = $"Copying directory... (1 of 2)";
                        await Task.Run(() => CopyFilesRecursively(Path.Combine(discPath, "DATA"), Path.Combine(installPath, "DATA")));
                        StatusText.Text = $"Copying directory... (2 of 2)";
                        await Task.Run(() => CopyFilesRecursively(Path.Combine(discPath, "SAVEDATA"), Path.Combine(installPath, "SAVEDATA")));
                        await Task.Delay(250);
                        StatusText.Text = $"Removing Read Only..."; await Task.Run(() => RemoveReadOnly(Path.Combine(installPath)));
                        await Task.Delay(200);
                        StatusText.Text = $"Installing Modern Patch...";
                        await Task.Run(() => CopyFilesRecursively(Path.Combine(tempPath, "ModernPatch"), Path.Combine(installPath)));
                        await Task.Delay(250);
                        StatusText.Text = $"Installing MP Setup...";
                        await Task.Run(() => File.Copy(Path.Combine(tempPath, patchName2), Path.Combine(installPath, patchName2), true));
                        await Task.Delay(250);
                        MessageBox.Show("Run setupMP.exe within the game directory to configure the game.", "Modern Patch", MessageBoxButton.OK, MessageBoxImage.Information);
                        await Task.Delay(250);
                        StatusText.Text = "Welcome to NFSPatcher!";
                        pb.IsIndeterminate = false;
                        MessageBox.Show("All patches installed successfully!", $"{gameName}", MessageBoxButton.OK, MessageBoxImage.Information);
                        PatchList.Visibility = Visibility.Visible;
                        CloseButton.Visibility = Visibility.Visible;
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Please select correct folder.");
                        pb.IsIndeterminate = false;
                        selectPathCanceled = true;
                        PatchList.Visibility = Visibility.Visible;
                        CloseButton.Visibility = Visibility.Visible;
                        StatusText.Text = $"Welcome to NFSPatcher!";
                        return;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        ////////////////////
        // Other
        ////////////////////

        private static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }
        }

        public static void RemoveReadOnly(string sourcePath)
        {
            foreach (string file in Directory.GetFiles(sourcePath, "*", SearchOption.AllDirectories))
            {
                File.SetAttributes(file, File.GetAttributes(file) & ~FileAttributes.ReadOnly);
            }
        }

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

