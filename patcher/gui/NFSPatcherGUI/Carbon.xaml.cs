using Ookii.Dialogs.Wpf;
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
    /// Interaction logic for Carbon.xaml
    /// </summary>
    public partial class Carbon : Window
    {
        private string rootPath;
        private string tempFolder;
        private string usPatchzip;
        private string usPatchCEzip;
        private string usPatchexe;
        private string usPatchgamePath;
        private string wfp;
        private string wfpTemp;
        private string wfpGamePath;
        private string wfpCfgGamePath;
        private string noCDexe;
        private string noCDgamePath;
        private string scriptsGamePath;

        public Carbon()
        {
            InitializeComponent();

            rootPath = Directory.GetCurrentDirectory();
            tempFolder = Path.Combine(rootPath, "temp");
            usPatchzip = Path.Combine(rootPath, "nfsc_v1.4_en.zip");
            usPatchCEzip = Path.Combine(rootPath, "nfsc_ce_v1.4_us.zip");
            usPatchexe = Path.Combine(rootPath, "temp", "patch_1.2_1.3_1.4.exe");
            wfp = Path.Combine(rootPath, "NFSCarbon.WidescreenFix.zip");
            wfpTemp = Path.Combine(rootPath, "temp", "WFP");
            noCDexe = Path.Combine(rootPath, "NFSC.exe");
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {

        }

        private void Patch_Click(object sender, RoutedEventArgs e)
        {
            using (TaskDialog dialog = new TaskDialog())
            {
                dialog.WindowTitle = "Need for Speed: Carbon";
                dialog.MainInstruction = "What verion of Need for Speed: Carbon is installed?";
                dialog.Content = "The patcher supports both the Normal and Collector's Edition of Need for Speed: Carbon, but it has only been tested on the Collector's Edition, so it may not work properly on the normal version.";
                TaskDialogButton collectorsEdition = new TaskDialogButton("Collector's Edition");
                TaskDialogButton normalEdition = new TaskDialogButton("Normal Edition");
                dialog.Buttons.Add(collectorsEdition);
                dialog.Buttons.Add(normalEdition);
                TaskDialogButton button = dialog.ShowDialog(this);
                if (button == collectorsEdition)
                    Patch_CE();
                else if (button == normalEdition)
                    Patch_Normal();
            }
        }

        private void Patch_CE()
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            dialog.Description = "Please select your Need for Speed: Carbon install folder.";
            dialog.UseDescriptionForTitle = true;
            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                SystemSounds.Exclamation.Play();
                MessageBox.Show("Please note that the patcher may freeze during downloads, this is normal, do not close the window.", "NFSPatcherGUI");

                System.IO.Directory.CreateDirectory(tempFolder);
                System.IO.Directory.CreateDirectory(wfpTemp);

                WebClient webClient = new WebClient();

                webClient.DownloadFile(new Uri("https://github.com/KilLo445/NFSPatcher/raw/main/patcher/carbon/nfsc_ce_v1.4_us.zip"), usPatchCEzip);
                webClient.DownloadFile(new Uri("https://github.com/ThirteenAG/WidescreenFixesPack/releases/download/nfsc/NFSCarbon.WidescreenFix.zip"), wfp);

                try
                {
                    wfpGamePath = Path.Combine(dialog.SelectedPath);

                    ZipFile.ExtractToDirectory(usPatchCEzip, tempFolder);
                    File.Delete(usPatchCEzip);
                    ZipFile.ExtractToDirectory(wfp, wfpGamePath);
                    File.Delete(wfp);

                    usPatchgamePath = Path.Combine((dialog.SelectedPath), "patch_1.2_1.3_1.4.exe");
                    scriptsGamePath = Path.Combine((dialog.SelectedPath), "scripts");
                    File.Move(usPatchexe, usPatchgamePath);

                    Process.Start(usPatchgamePath);
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"{ex}");
                }

                SystemSounds.Exclamation.Play();
                MessageBox.Show("Patch complete!", "Need for Speed: Carbon");
                File.Delete(usPatchgamePath);
                Directory.Delete(tempFolder, true);
                MessageBoxResult patchCompleteWFP = System.Windows.MessageBox.Show("Would you like to open the Widescreen Fix configuration?", "Need for Speed: Carbon", System.Windows.MessageBoxButton.YesNo);
                if (patchCompleteWFP == MessageBoxResult.Yes)
                {
                    wfpCfgGamePath = Path.Combine(dialog.SelectedPath, "scripts", "NFSCarbon.WidescreenFix.ini");

                    Process.Start(wfpCfgGamePath);
                }
            }
        }
        
        private void Patch_Normal()
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            dialog.Description = "Please select your Need for Speed: Carbon install folder.";
            dialog.UseDescriptionForTitle = true;
            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                SystemSounds.Exclamation.Play();
                MessageBox.Show("Please note that the patcher may freeze during downloads, this is normal, do not close the window.", "NFSPatcherGUI");

                System.IO.Directory.CreateDirectory(tempFolder);
                System.IO.Directory.CreateDirectory(wfpTemp);

                WebClient webClient = new WebClient();

                webClient.DownloadFile(new Uri("https://github.com/KilLo445/NFSPatcher/raw/main/patcher/carbon/nfsc_ce_v1.4_us.zip"), usPatchzip);
                webClient.DownloadFile(new Uri("https://github.com/ThirteenAG/WidescreenFixesPack/releases/download/nfsc/NFSCarbon.WidescreenFix.zip"), wfp);

                try
                {
                    wfpGamePath = Path.Combine(dialog.SelectedPath);

                    ZipFile.ExtractToDirectory(usPatchzip, tempFolder);
                    File.Delete(usPatchzip);
                    ZipFile.ExtractToDirectory(wfp, wfpGamePath);
                    File.Delete(wfp);

                    usPatchgamePath = Path.Combine((dialog.SelectedPath), "patch_1.2_1.3_1.4.exe");
                    scriptsGamePath = Path.Combine((dialog.SelectedPath), "scripts");
                    File.Move(usPatchexe, usPatchgamePath);

                    Process.Start(usPatchgamePath);
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"{ex}");
                }

                SystemSounds.Exclamation.Play();
                MessageBox.Show("Patch complete!", "Need for Speed: Carbon");
                File.Delete(usPatchgamePath);
                Directory.Delete(tempFolder, true);
                MessageBoxResult patchCompleteWFP = System.Windows.MessageBox.Show("Would you like to open the Widescreen Fix configuration?", "Need for Speed: Carbon", System.Windows.MessageBoxButton.YesNo);
                if (patchCompleteWFP == MessageBoxResult.Yes)
                {
                    wfpCfgGamePath = Path.Combine(dialog.SelectedPath, "scripts", "NFSCarbon.WidescreenFix.ini");

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
            MessageBoxResult NocDConfirmation = System.Windows.MessageBox.Show("The NoCD patch is for the Collector's Edition of Need for Speed: Carbon, it may not work on the normal edition, do you want to continue?", "Need for Speed: Carbon", System.Windows.MessageBoxButton.YesNo);
            if (NocDConfirmation == MessageBoxResult.Yes)
            {
                var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
                dialog.Description = "Please select your Need for Speed: Carbon install folder.";
                dialog.UseDescriptionForTitle = true;
                if (dialog.ShowDialog(this).GetValueOrDefault())
                {
                    WebClient webClient = new WebClient();

                    webClient.DownloadFile(new Uri("https://github.com/KilLo445/NFSPatcher/raw/main/patcher/carbon/NoCD/NFSC.exe"), noCDexe);

                    noCDgamePath = Path.Combine((dialog.SelectedPath), "NFSC.exe");

                    try
                    {
                        File.Delete(noCDgamePath);
                        File.Move(@"NFSC.exe", noCDgamePath);

                        SystemSounds.Exclamation.Play();
                        MessageBox.Show("NoCD Patch installed!", "Need for Speed: Carbon");
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show($"{ex}");
                    }
                }
            }
        }
    }
}
