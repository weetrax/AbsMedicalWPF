using AbsMedical.Data;
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
using System.Windows.Shapes;

namespace AbsMedical.Forms
{
    /// <summary>
    /// Logique d'interaction pour HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        private doctor CurrentDoctor
        {
            get;
            set;
        }

        public HomeWindow(doctor doctor)
        {
            this.CurrentDoctor = doctor;
            InitializeComponent();
            SetTitle();
        }

        protected void MenuItemClose_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemUpdateProfile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SetTitle()
        {
            this.Title += " - Logged as " + CurrentDoctor.Firstname;
        }
    }
}
