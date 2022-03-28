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

    public partial class MostWanted : Window
    {
        private string rootPath;
        private string tempFolder;
        private string usPatchzip;
        private string usPatchexe;
        private string usPatchgamePath;
        private string wfp;
        private string wfpTemp;
        private string wfpGamePath;
        private string wfpCfgGamePath;
        private string noCDexe;
        private string noCDgamePath;
        private string scriptsGamePath;

        public MostWanted()
        {
            InitializeComponent();

            rootPath = Directory.GetCurrentDirectory();
            tempFolder = Path.Combine(rootPath, "temp");
            usPatchzip = Path.Combine(rootPath, "nfsmwpatch1.3.zip");
            usPatchexe = Path.Combine(rootPath, "temp", "nfsmwpatch1.3.exe");
            wfp = Path.Combine(rootPath, "NFSMostWanted.WidescreenFix.zip");
            wfpTemp = Path.Combine(rootPath, "temp", "WFP");
            noCDexe = Path.Combine(rootPath, "speed.exe");
        }

        private void Patch_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            dialog.Description = "Please select your Need for Speed: Most Wanted install folder.";
            dialog.UseDescriptionForTitle = true;
            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                SystemSounds.Exclamation.Play();
                MessageBox.Show("Please note that the patcher may freeze during downloads, this is normal, do not close the window.", "NFSPatcherGUI");

                System.IO.Directory.CreateDirectory(tempFolder);
                System.IO.Directory.CreateDirectory(wfpTemp);

                WebClient webClient = new WebClient();

                webClient.DownloadFile(new Uri("https://github.com/KilLo445/NFSPatcher/raw/main/patcher/mw/nfsmwpatch1.3.zip"), usPatchzip);
                webClient.DownloadFile(new Uri("https://github.com/ThirteenAG/WidescreenFixesPack/releases/download/nfsmw/NFSMostWanted.WidescreenFix.zip"), wfp);

                try
                {
                    wfpGamePath = Path.Combine(dialog.SelectedPath);

                    ZipFile.ExtractToDirectory(usPatchzip, tempFolder);
                    File.Delete(usPatchzip);
                    ZipFile.ExtractToDirectory(wfp, wfpGamePath);
                    File.Delete(wfp);

                    usPatchgamePath = Path.Combine((dialog.SelectedPath), "nfsmwpatch1.3.exe");
                    scriptsGamePath = Path.Combine((dialog.SelectedPath), "scripts");
                    File.Move(usPatchexe, usPatchgamePath);

                    Process.Start(usPatchgamePath);
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"{ex}");
                }

                SystemSounds.Exclamation.Play();
                MessageBox.Show("Patch complete!", "Need for Speed: Most Wanted");
                File.Delete(usPatchgamePath);
                Directory.Delete(tempFolder, true);
                MessageBoxResult patchCompleteWFP = System.Windows.MessageBox.Show("Would you like to open the Widescreen Fix configuration?", "Need for Speed: Most Wanted", System.Windows.MessageBoxButton.YesNo);
                if (patchCompleteWFP == MessageBoxResult.Yes)
                {
                    wfpCfgGamePath = Path.Combine(dialog.SelectedPath, "scripts", "NFSMostWanted.WidescreenFix.ini");

                    Process.Start(wfpCfgGamePath);
                }
            }
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
            dialog.Description = "Please select your Need for Speed: Most Wanted install folder.";
            dialog.UseDescriptionForTitle = true;
            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                WebClient webClient = new WebClient();

                webClient.DownloadFile(new Uri("https://github.com/KilLo445/NFSPatcher/raw/main/patcher/mw/NoCD/speed.exe"), noCDexe);

                noCDgamePath = Path.Combine((dialog.SelectedPath), "speed.exe");

                try
                {
                    File.Delete(noCDgamePath);
                    File.Move(@"speed.exe", noCDgamePath);

                    SystemSounds.Exclamation.Play();
                    MessageBox.Show("NoCD Patch installed!", "Need for Speed: Most Wanted");
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"{ex}");
                }
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {

        }
    }
}
