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
using AbsMedical.Data;
using AbsMedical.Controllers;
using AbsMedical.Forms;
using AbsMedical.Utils;

namespace AbsMedical
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string password = txtPassword.Text;
            string email = txtEmail.Text;

            if(DoctorController.IsValid(email,password))
            {
                HomeWindow form = new HomeWindow();
                form.Show();
                this.Close();
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
