using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AbsMedical.Forms
{
    /// <summary>
    /// Logique d'interaction pour ManageStudentsWindow.xaml
    /// </summary>
    public partial class ManageStudentsWindow : MetroWindow
    {
        public ManageStudentsWindow()
        {
            InitializeComponent();
        }

        private void btnOurGitHub_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/weetrax/AbsMedicalWPF");
        }
        #region Dialog
        private async void ShowAlert(string title)
        {
            var dialog = (BaseMetroDialog)this.Resources["CustomCloseDialog"];
            dialog.Title = title;

            await this.ShowMetroDialogAsync(dialog);
            await dialog.WaitUntilUnloadedAsync();
        }

        private async void btnCloseCustomDialog_Click(object sender, RoutedEventArgs e)
        {
            var dialog = (BaseMetroDialog)this.Resources["CustomCloseDialog"];

            await this.HideMetroDialogAsync(dialog);

            RegisterStudentWindow window = new RegisterStudentWindow(lblLogedAs.Content.ToString());
            window.Show();
        }
        #endregion
    }
}
