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
        private string CurrentDoctorGuid
        {
            get;
        }

        public ManageStudentsWindow(string loggedAsContent, string doctorGuid)
        {
            this.CurrentDoctorGuid = doctorGuid;
            InitializeComponent();
            lblLogedAs.Content = loggedAsContent;
        }

        private void btnOurGitHub_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/weetrax/AbsMedicalWPF");
        }

        private void tileBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            HomeWindow window = new HomeWindow(CurrentDoctorGuid);
            window.Show();
        }

        private void tileRegisterStudent_Click(object sender, RoutedEventArgs e)
        {
            RegisterStudentWindow window = new RegisterStudentWindow(lblLogedAs.Content.ToString());
            window.Show();
        }

        private void tileEditStudent_Click(object sender, RoutedEventArgs e)
        {

            EditStudentWindow window = new EditStudentWindow(lblLogedAs.Content.ToString());
            window.Show();
        }
    }
}
