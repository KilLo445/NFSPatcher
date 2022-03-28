using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Media;
using System.Net;
using System.Windows;

namespace NFSPatcherGUI
{
    
    public partial class HotPursuit : Window
    {
        private string rootPath;
        private string tempFolder;
        private string modernPatchzip;
        private string gamePath;

        public HotPursuit()
        {
            InitializeComponent();

            rootPath = Directory.GetCurrentDirectory();
            tempFolder = Path.Combine(rootPath, "temp");
            modernPatchzip = Path.Combine(rootPath, "ModernPatch.zip");
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {

        }

        private void Patch_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            dialog.Description = "Please select your Need for Speed III: Hot Pursuit install folder.";
            dialog.UseDescriptionForTitle = true;
            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                SystemSounds.Exclamation.Play();
                MessageBox.Show("Please note that the patcher may freeze during downloads, this is normal, do not close the window.", "NFSPatcherGUI");

                WebClient webClient = new WebClient();

                webClient.DownloadFile(new Uri("https://github.com/KilLo445/NFSPatcher/raw/main/patcher/hp/ModernPatch/ModernPatch.zip"), modernPatchzip);

                try
                {
                    gamePath = Path.Combine((dialog.SelectedPath));

                    using (ZipArchive source = ZipFile.Open(modernPatchzip, ZipArchiveMode.Read, null))
                    {
                        foreach (ZipArchiveEntry entry in source.Entries)
                        {
                            string fullPath = Path.GetFullPath(Path.Combine(gamePath, entry.FullName));

                            if (Path.GetFileName(fullPath).Length != 0)
                            {
                                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                                entry.ExtractToFile(fullPath, true);
                            }
                        }
                    }
                    File.Delete(modernPatchzip);
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"{ex}");
                }

                SystemSounds.Exclamation.Play();
                MessageBox.Show("Patch complete!", "Need for Speed III: Hot Pursuit");
            }
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mwindow = new MainWindow();
            mwindow.Show();
            this.Close();
        }
    }
}
