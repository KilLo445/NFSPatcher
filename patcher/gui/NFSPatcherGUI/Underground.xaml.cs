using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Media;
using System.Net;
using System.Windows;

namespace NFSPatcherGUI
{
    public partial class Underground : Window
    {
        private string rootPath;
        private string tempFolder;
        private string usPatch4zip;
        private string usPatch4exe;
        private string usPatch4gamePath;
        private string wfp;
        private string wfpTemp;
        private string wfpGamePath;
        private string wfpCfgGamePath;
        private string noCDexe;
        private string noCDgamePath;
        private string dinput;
        private string dinputGamePath;
        private string scriptsTemp;
        private string scriptsGamePath;

        public Underground()
        {
            InitializeComponent();

            rootPath = Directory.GetCurrentDirectory();
            tempFolder = Path.Combine(rootPath, "temp");
            usPatch4zip = Path.Combine(rootPath, "US_PATCH_4.zip");
            usPatch4exe = Path.Combine(rootPath, "temp", "US_PATCH_4.exe");
            wfp = Path.Combine(rootPath, "NFSUnderground.WidescreenFix.zip");
            wfpTemp = Path.Combine(rootPath, "temp", "WFP");
            noCDexe = Path.Combine(rootPath, "Speed.exe");
            dinput = Path.Combine(rootPath, "temp", "WFP", "dinput8.dll");
            scriptsTemp = Path.Combine(rootPath, "temp", "WFP", "scripts");
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {

        }

        private void Patch_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            dialog.Description = "Please select your Need for Speed: Underground install folder.";
            dialog.UseDescriptionForTitle = true;
            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                SystemSounds.Exclamation.Play();
                MessageBox.Show("Please note that the patcher may freeze during downloads, this is normal, do not close the window.", "NFSPatcherGUI");

                System.IO.Directory.CreateDirectory(tempFolder);
                System.IO.Directory.CreateDirectory(wfpTemp);

                WebClient webClient = new WebClient();

                webClient.DownloadFile(new Uri("https://github.com/KilLo445/NFSPatcher/raw/main/patcher/underground/US_PATCH_4.zip"), usPatch4zip);
                webClient.DownloadFile(new Uri("https://github.com/ThirteenAG/WidescreenFixesPack/releases/download/nfsu/NFSUnderground.WidescreenFix.zip"), wfp);

                try
                {
                    wfpGamePath = Path.Combine(dialog.SelectedPath);

                    ZipFile.ExtractToDirectory(usPatch4zip, tempFolder);
                    File.Delete(usPatch4zip);
                    ZipFile.ExtractToDirectory(wfp, wfpGamePath);
                    File.Delete(wfp);

                    usPatch4gamePath = Path.Combine((dialog.SelectedPath), "US_PATCH_4.exe");
                    dinputGamePath = Path.Combine((dialog.SelectedPath), "dinput8.dll");
                    scriptsGamePath = Path.Combine((dialog.SelectedPath), "scripts");
                    File.Move(usPatch4exe, usPatch4gamePath);

                    Process.Start(usPatch4gamePath);
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"{ex}");
                }

                SystemSounds.Exclamation.Play();
                MessageBox.Show("Patch complete!", "Need for Speed: Underground");
                File.Delete(usPatch4gamePath);
                Directory.Delete(tempFolder, true);
                MessageBoxResult patchCompleteWFP = System.Windows.MessageBox.Show("Would you like to open the Widescreen Fix configuration?", "Need for Speed: Underground", System.Windows.MessageBoxButton.YesNo);
                if (patchCompleteWFP == MessageBoxResult.Yes)
                {
                    wfpCfgGamePath = Path.Combine(dialog.SelectedPath, "scripts", "NFSUnderground.WidescreenFix.ini");

                    Process.Start(wfpCfgGamePath);
                }
            }
        }

        private void DownloadCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mwindow = new MainWindow();
            mwindow.Show();
            this.Close();
        }

        private void NoCD_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            dialog.Description = "Please select your Need for Speed: Underground install folder.";
            dialog.UseDescriptionForTitle = true;
            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                WebClient webClient = new WebClient();

                webClient.DownloadFile(new Uri("https://github.com/KilLo445/NFSPatcher/raw/main/patcher/underground/NoCD/Speed.exe"), noCDexe);

                noCDgamePath = Path.Combine((dialog.SelectedPath), "Speed.exe");

                try
                {
                    File.Delete(noCDgamePath);
                    File.Move(@"Speed.exe", noCDgamePath);

                    SystemSounds.Exclamation.Play();
                    MessageBox.Show("NoCD Patch installed!", "Need for Speed: Underground");
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"{ex}");
                }
            }
        }
    }
}
