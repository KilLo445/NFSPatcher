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
            }
            else { return; }
        }
    }
}
