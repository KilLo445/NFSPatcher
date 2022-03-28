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
    /// <summary>
    /// Interaction logic for ProStreet.xaml
    /// </summary>
    public partial class ProStreet : Window
    {
        private string rootPath;
        private string tempFolder;
        private string wfp;
        private string wfpTemp;
        private string wfpGamePath;
        private string wfpCfgGamePath;
        private string noCDexe;
        private string noCDgamePath;
        private string scriptsGamePath;

        public ProStreet()
        {
            InitializeComponent();

            rootPath = Directory.GetCurrentDirectory();
            tempFolder = Path.Combine(rootPath, "temp");
            wfp = Path.Combine(rootPath, "NFSProStreet.GenericFix.zip");
            wfpTemp = Path.Combine(rootPath, "temp", "WFP");
            noCDexe = Path.Combine(rootPath, "nfs.exe");
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {

        }

        private void Patch_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            dialog.Description = "Please select your Need for Speed: ProStreet install folder.";
            dialog.UseDescriptionForTitle = true;
            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                SystemSounds.Exclamation.Play();
                MessageBox.Show("Please note that the patcher may freeze during downloads, this is normal, do not close the window.", "NFSPatcherGUI");

                System.IO.Directory.CreateDirectory(tempFolder);
                System.IO.Directory.CreateDirectory(wfpTemp);

                WebClient webClient = new WebClient();

                webClient.DownloadFile(new Uri("https://github.com/ThirteenAG/WidescreenFixesPack/releases/download/nfsps/NFSProStreet.GenericFix.zip"), wfp);

                try
                {
                    wfpGamePath = Path.Combine(dialog.SelectedPath);

                    ZipFile.ExtractToDirectory(wfp, wfpGamePath);
                    File.Delete(wfp);

                    scriptsGamePath = Path.Combine((dialog.SelectedPath), "scripts");
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"{ex}");
                }

                SystemSounds.Exclamation.Play();
                MessageBox.Show("Patch complete!", "Need for Speed: ProStreet");
                Directory.Delete(tempFolder, true);
                MessageBoxResult patchCompleteWFP = System.Windows.MessageBox.Show("Would you like to open the Generic Fix configuration?", "Need for Speed: ProStreet", System.Windows.MessageBoxButton.YesNo);
                if (patchCompleteWFP == MessageBoxResult.Yes)
                {
                    wfpCfgGamePath = Path.Combine(dialog.SelectedPath, "scripts", "NFSProStreet.GenericFix.ini");

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
            dialog.Description = "Please select your Need for Speed: ProStreet install folder.";
            dialog.UseDescriptionForTitle = true;
            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                WebClient webClient = new WebClient();

                webClient.DownloadFile(new Uri("https://github.com/KilLo445/NFSPatcher/raw/main/patcher/prostreet/NoCD/nfs.exe"), noCDexe);

                noCDgamePath = Path.Combine((dialog.SelectedPath), "nfs.exe");

                try
                {
                    File.Delete(noCDgamePath);
                    File.Move(@"nfs.exe", noCDgamePath);

                    SystemSounds.Exclamation.Play();
                    MessageBox.Show("NoCD Patch installed!", "Need for Speed: ProStreet");
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"{ex}");
                }
            }
        }
    }
}
