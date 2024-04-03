using System;
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
    public partial class NFS6 : Window
    {
        // Strings
        string gameName = "Need for Speed: Hot Pursuit 2";
        string gameNameS = "NFS: Hot Pursuit 2";
        string exeName = "NFSHP2.exe";

        // Paths
        string rootPath;
        string tempPath;

        string installPath;
        string discPath;
        string gameExe;


        // Bools
        bool isBusy = false;
        bool installDetected = false;
        bool selectPathCanceled = false;

        // Links

        public NFS6()
        {
            InitializeComponent();

            rootPath = Directory.GetCurrentDirectory();
            tempPath = Path.Combine(Path.GetTempPath(), "NFSPatcher", "HP2");
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
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\EA Games\Need For Speed Hot Pursuit 2", true);
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

            string comboBoxSelection = ComboBox.Text;
            if (comboBoxSelection == null || comboBoxSelection == "")
            {
                MessageBox.Show("Please select a patch method.", "NFSPatcher", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (comboBoxSelection != null)
            {
                if (comboBoxSelection == "Main Patches")
                {
                    MainPatch();
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
                WinForms.FolderBrowserDialog discDialog = new WinForms.FolderBrowserDialog();
                discDialog.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;
                discDialog.Description = $"Please select your {gameNameS} disc.\n\n(Location with AutoRun.exe)";
                discDialog.RootFolder = Environment.SpecialFolder.MyComputer;
                discDialog.ShowNewFolderButton = false;
                WinForms.DialogResult pathResult = discDialog.ShowDialog();
                if (pathResult == WinForms.DialogResult.OK)
                {
                    discPath = Path.Combine(discDialog.SelectedPath);

                    if (File.Exists(Path.Combine(discPath, "AutoRun.exe")))
                    {
                        InstallPatch();
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
            catch (Exception ex) { MessageBox.Show($"{ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return; }
        }

        private async Task InstallPatch()
        {
            StatusText.Text = $"Removing Read Only..."; await Task.Run(() => RemoveReadOnly(Path.Combine(installPath)));
            await Task.Delay(200);

            StatusText.Text = $"Copying directory... (1 of 7)";
            await Task.Run(() => CopyFilesRecursively(Path.Combine(discPath, "actors"), Path.Combine(installPath, "actors")));
            StatusText.Text = $"Copying directory... (2 of 7)";
            await Task.Run(() => CopyAIDir(Path.Combine(discPath, "AI"), Path.Combine(installPath, "AI")));
            StatusText.Text = $"Copying directory... (3 of 7)";
            await Task.Run(() => CopyFilesRecursively(Path.Combine(discPath, "audio"), Path.Combine(installPath, "audio")));
            StatusText.Text = $"Copying directory... (4 of 7)";
            await Task.Run(() => CopyFilesRecursively(Path.Combine(discPath, "Cars"), Path.Combine(installPath, "Cars")));
            StatusText.Text = $"Copying directory... (5 of 7)";
            await Task.Run(() => CopyMoviesDir(Path.Combine(discPath, "movies"), Path.Combine(installPath, "movies")));
            StatusText.Text = $"Copying directory... (6 of 7)";
            await Task.Run(() => CopyParticleDir(Path.Combine(discPath, "Particle"), Path.Combine(installPath, "Particle")));
            StatusText.Text = $"Copying directory... (7 of 7)";
            await Task.Run(() => CopyFilesRecursively(Path.Combine(discPath, "tracks"), Path.Combine(installPath, "tracks")));

            await Task.Delay(200);
            StatusText.Text = $"Waiting for user...";
            MessageBox.Show("Please select a resolution.", "NFSPatcher", MessageBoxButton.OK, MessageBoxImage.Information);
            NFS6Res resWindow = new NFS6Res();
            resWindow.Show();
            while (NFS6Res.resConfirmed == false) { await Task.Delay(150); }
            DLWidescreen();
        }

        private void DLWidescreen()
        {
            StatusText.Text = $"Downloading Widescreen Patch... ({NFS6Res.selectedRes})";
            Directory.CreateDirectory(tempPath); Directory.CreateDirectory(Path.Combine(tempPath, "Widescreen"));
            pb.IsIndeterminate = false;
            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(PatchDLComplete);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            webClient.DownloadFileAsync(new Uri(NFS6Res.selectedResLink), Path.Combine(tempPath, NFS6Res.selectedRes + ".zip"));
        }

        private void PatchDLComplete(object sender, AsyncCompletedEventArgs e)
        {
            StatusText.Text = $"Extracting ({NFS6Res.selectedRes}.zip)";
            ZipFile.ExtractToDirectory(Path.Combine(tempPath, NFS6Res.selectedRes + ".zip"), Path.Combine(tempPath, "Widescreen"));
            InstallPatch2();
        }

        private async Task InstallPatch2()
        {
            pb.IsIndeterminate = true;
            pb.Value = 0;
            StatusText.Text = $"Removing Read Only..."; await Task.Run(() => RemoveReadOnly(Path.Combine(installPath)));
            await Task.Delay(200);

            StatusText.Text = $"Installing Widescreen Patch..";
            await Task.Run(() => CopyFilesRecursively(Path.Combine(tempPath, "Widescreen"), Path.Combine(installPath)));

            StatusText.Text = "Welcome to NFSPatcher!";
            pb.IsIndeterminate = false;
            MessageBox.Show("All patches installed successfully!", $"{gameName}", MessageBoxButton.OK, MessageBoxImage.Information);
            PatchList.Visibility = Visibility.Visible;
            CloseButton.Visibility = Visibility.Visible;
            return;
        }

        ////////////////////
        // Other
        ////////////////////

        private static void CopyAIDir(string sourcePath, string targetPath)
        {
            Directory.CreateDirectory(targetPath);
            File.Copy(Path.Combine(sourcePath, "AIChaseDefs.ini"), Path.Combine(targetPath, "AIChaseDefs.ini"), true);
            File.Copy(Path.Combine(sourcePath, "AIRacers.ini"), Path.Combine(targetPath, "AIRacers.ini"), true);
            File.Copy(Path.Combine(sourcePath, "racetune.ini"), Path.Combine(targetPath, "racetune.ini"), true);
        }

        private static void CopyMoviesDir(string sourcePath, string targetPath)
        {
            Directory.CreateDirectory(targetPath);
            File.Copy(Path.Combine(sourcePath, "hp2E.mad"), Path.Combine(targetPath, "hp2E.mad"), true);
            File.Copy(Path.Combine(sourcePath, "logoE.mad"), Path.Combine(targetPath, "logoE.mad"), true);
            File.Copy(Path.Combine(sourcePath, "logoS.mad"), Path.Combine(targetPath, "logoS.mad"), true);
        }

        private static void CopyParticleDir(string sourcePath, string targetPath)
        {
            Directory.CreateDirectory(targetPath);
            File.Copy(Path.Combine(sourcePath, "carfx.fsh"), Path.Combine(targetPath, "carfx.fsh"), true);
            File.Copy(Path.Combine(sourcePath, "carfx.ini"), Path.Combine(targetPath, "carfx.ini"), true);
            File.Copy(Path.Combine(sourcePath, "particle.fsh"), Path.Combine(targetPath, "particle.fsh"), true);
            File.Copy(Path.Combine(sourcePath, "particle.mpb"), Path.Combine(targetPath, "particle.mpb"), true);
        }

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
