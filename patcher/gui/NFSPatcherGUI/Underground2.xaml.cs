using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Media;
using System.Net;
using System.Windows;

namespace NFSPatcherGUI
{
    /// <summary>
    /// Interaction logic for Underground2.xaml
    /// </summary>
    public partial class Underground2 : Window
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

        public Underground2()
        {
            InitializeComponent();

            rootPath = Directory.GetCurrentDirectory();
            tempFolder = Path.Combine(rootPath, "temp");
            usPatchzip = Path.Combine(rootPath, "nfsu2_v1.2_us.zip");
            usPatchexe = Path.Combine(rootPath, "temp", "nfsu2_v1.2_us.exe");
            wfp = Path.Combine(rootPath, "NFSUnderground2.WidescreenFix.zip");
            wfpTemp = Path.Combine(rootPath, "temp", "WFP");
            noCDexe = Path.Combine(rootPath, "SPEED2.EXE");
        }

        private void NoCD_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            dialog.Description = "Please select your Need for Speed: Underground 2 install folder.";
            dialog.UseDescriptionForTitle = true;
            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                WebClient webClient = new WebClient();

                webClient.DownloadFile(new Uri("https://github.com/KilLo445/NFSPatcher/raw/main/patcher/underground2/NoCD/SPEED2.EXE"), noCDexe);

                noCDgamePath = Path.Combine((dialog.SelectedPath), "SPEED2.EXE");

                try
                {
                    File.Delete(noCDgamePath);
                    File.Move(@"SPEED2.EXE", noCDgamePath);

                    SystemSounds.Exclamation.Play();
                    MessageBox.Show("NoCD Patch installed!", "Need for Speed: Underground 2");
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"{ex}");
                }
            }
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mwindow = new MainWindow();
            mwindow.Show();
            this.Close();
        }

        private void Patch_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            dialog.Description = "Please select your Need for Speed: Underground 2 install folder.";
            dialog.UseDescriptionForTitle = true;
            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                SystemSounds.Exclamation.Play();
                MessageBox.Show("Please note that the patcher may freeze during downloads, this is normal, do not close the window.", "NFSPatcherGUI");

                System.IO.Directory.CreateDirectory(tempFolder);
                System.IO.Directory.CreateDirectory(wfpTemp);

                WebClient webClient = new WebClient();

                webClient.DownloadFile(new Uri("https://github.com/KilLo445/NFSPatcher/raw/main/patcher/underground2/nfsu2_v1.2_us.zip"), usPatchzip);
                webClient.DownloadFile(new Uri("https://github.com/ThirteenAG/WidescreenFixesPack/releases/download/nfsu2/NFSUnderground2.WidescreenFix.zip"), wfp);

                try
                {
                    wfpGamePath = Path.Combine(dialog.SelectedPath);

                    ZipFile.ExtractToDirectory(usPatchzip, tempFolder);
                    File.Delete(usPatchzip);
                    ZipFile.ExtractToDirectory(wfp, wfpGamePath);
                    File.Delete(wfp);

                    usPatchgamePath = Path.Combine((dialog.SelectedPath), "nfsu2_v1.2_us.exe");
                    scriptsGamePath = Path.Combine((dialog.SelectedPath), "scripts");
                    File.Move(usPatchexe, usPatchgamePath);

                    Process.Start(usPatchgamePath);
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"{ex}");
                }

                SystemSounds.Exclamation.Play();
                MessageBox.Show("Patch complete!", "Need for Speed: Underground 2");
                File.Delete(usPatchgamePath);
                Directory.Delete(tempFolder, true);
                MessageBoxResult patchCompleteWFP = System.Windows.MessageBox.Show("Would you like to open the Widescreen Fix configuration?", "Need for Speed: Underground 2", System.Windows.MessageBoxButton.YesNo);
                if (patchCompleteWFP == MessageBoxResult.Yes)
                {
                    wfpCfgGamePath = Path.Combine(dialog.SelectedPath, "scripts", "NFSUnderground2.WidescreenFix.ini");

                    Process.Start(wfpCfgGamePath);
                }
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {

        }
    }
}
