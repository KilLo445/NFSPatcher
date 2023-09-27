using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Input;

namespace NFSPatcher
{
    public partial class Changelog : Window
    {
        private string rootPath;
        private string tempPath;

        string changelogLink = "https://raw.githubusercontent.com/KilLo445/NFSPatcher/main/Changelog.txt";
        string changelogContent;

        int fontSize = 14;

        public Changelog()
        {
            InitializeComponent();

            ChangelogText.Text = "Downloading changelog...";
            ChangelogText.FontSize = fontSize;

            rootPath = Directory.GetCurrentDirectory();
            tempPath = Path.GetTempPath();

            this.Topmost = true;

            try
            {
                WebClient wc = new WebClient();
                wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
                wc.DownloadStringAsync(new Uri(changelogLink));
            }
            catch { ChangelogText.Text = "Download failed."; }
        }

        private void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {   
            try
            {
                ChangelogText.Text = e.Result;
            }
            catch { ChangelogText.Text = "Download failed."; }
        }

        private void FontSizeIncrease_Click(object sender, RoutedEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftShift))
            {
                fontSize = fontSize + 10;
                if (fontSize >= 1)
                {
                    ChangelogText.FontSize = fontSize;
                }
                return;
            }
            else
            {
                fontSize = fontSize + 1;
                if (fontSize >= 1)
                {
                    ChangelogText.FontSize = fontSize;
                }
                return;
            }
        }

        private void FontSizeDecrease_Click(object sender, RoutedEventArgs e)
        {
            if (fontSize == 1)
            {
                return;
            }
            else
            {
                if (Keyboard.IsKeyDown(Key.LeftShift))
                {
                    fontSize = fontSize - 10;
                    if (fontSize >= 1)
                    {
                        ChangelogText.FontSize = fontSize;
                    }
                    else
                    {
                        fontSize = 1;
                        ChangelogText.FontSize = fontSize;
                    }
                    return;
                }
                else
                {
                    fontSize = fontSize - 1;
                    if (fontSize >= 1)
                    {
                        ChangelogText.FontSize = fontSize;
                    }
                    else
                    {
                        fontSize = 1;
                        ChangelogText.FontSize = fontSize;
                    }
                    return;
                }
            }
        }
    }
}
