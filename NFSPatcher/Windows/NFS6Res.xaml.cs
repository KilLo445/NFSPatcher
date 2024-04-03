using System;
using System.Windows;

namespace NFSPatcher.Windows
{
    public partial class NFS6Res : Window
    {
        public static string selectedRes;
        public static string selectedResLink;

        public static bool resConfirmed = false;

        string widescreenLink = "https://github.com/KilLo445/NFSPatcher/raw/main/Remote/PatchFiles/Games/HP2/WidescreenFix/";

        public NFS6Res()
        {
            InitializeComponent();
        }

        private void ConfirmRes_Click(object sender, RoutedEventArgs e)
        {
            string comboBoxSelection = ComboBox.Text;
            if (comboBoxSelection == null || comboBoxSelection == "")
            {
                MessageBox.Show("Please select a resolution.", "NFSPatcher", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (comboBoxSelection != null)
            {
                selectedRes = comboBoxSelection;
                selectedResLink = widescreenLink + comboBoxSelection + ".zip";
                resConfirmed = true;
                this.Close();

                //if (comboBoxSelection == "1280x720")
                //{

                //}
                //if (comboBoxSelection == "1280x768")
                //{

                //}
                //if (comboBoxSelection == "1280x800")
                //{

                //}
                //if (comboBoxSelection == "1366x768")
                //{

                //}
                //if (comboBoxSelection == "1440x900")
                //{

                //}
                //if (comboBoxSelection == "1600x900")
                //{

                //}
                //if (comboBoxSelection == "1600x1024")
                //{

                //}
                //if (comboBoxSelection == "1680x1050")
                //{

                //}
                //if (comboBoxSelection == "1920x1080")
                //{

                //}
                //if (comboBoxSelection == "1920x1200")
                //{

                //}
                //if (comboBoxSelection == "2560x1080")
                //{

                //}
                //if (comboBoxSelection == "2560x1440")
                //{

                //}
                //if (comboBoxSelection == "2560x1600")
                //{

                //}
                //if (comboBoxSelection == "3840x1440")
                //{

                //}
                //if (comboBoxSelection == "3840x2160")
                //{

                //}
            }
            else { return; }
        }
    }
}
