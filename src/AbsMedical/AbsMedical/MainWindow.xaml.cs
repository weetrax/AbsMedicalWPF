using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AbsMedical.Controllers;
using AbsMedical.Forms;
using AbsMedical.Utils;
using MahApps.Metro.Controls;
using AbsMedical.DoctorServiceReference;

namespace AbsMedical
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login();
            }
        }

        private void Login()
        {
            string password = txtPassword.Password.ToString();
            string email = txtEmail.Text;

            WCF.Doctor currentDoctor = DoctorController.Find(email, password);

            if (currentDoctor != null)
            {
                HomeWindow window = new HomeWindow(currentDoctor.Guid);
                window.Show();
                this.Close();
            }
            else
            {
                lblError.Visibility = Visibility.Visible;
                lblError.Content = "Mail address or password invalid.";
            }
        }
    }
}
