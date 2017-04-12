using AbsMedical.Controllers;
using AbsMedical.WCF;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
    /// Logique d'interaction pour RegisterStudentWindow.xaml
    /// </summary>
    public partial class RegisterStudentWindow : MetroWindow
    {
        public RegisterStudentWindow(string loggedAsContent)
        {
            InitializeComponent();
            lblLogedAs.Content = loggedAsContent;
            bindCountries();
        }

        private void cbSchoolCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            string countryId = cb.SelectedValue.ToString();
            bindSchoolsByCountry(countryId);
        }

        private void bindSchoolsByCountry(string countryId)
        {
            List<School> schools = SchoolController.GetSchoolsByCountry(countryId);
            bindSchools(schools);
        }

        private void bindSchools(List<School> schools)
        {
            cbSchool.ItemsSource = schools;
            cbSchool.DisplayMemberPath = "DisplayedName";
            cbSchool.SelectedValuePath = "Guid";
        }

        private void bindCountries()
        {
            cbCountries.ItemsSource = CountryController.GetCountries();
            cbCountries.DisplayMemberPath = "Name";
            cbCountries.SelectedValuePath = "Id";

            cbSchoolCountry.ItemsSource = CountryController.GetCountries();
            cbSchoolCountry.DisplayMemberPath = "Name";
            cbSchoolCountry.SelectedValuePath = "Id";
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
        }
        #endregion

        private void txtSchoolName_SelectionChanged(object sender, RoutedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            string texte = txt.Text;
            var schools = cbSchool.Items.Cast<School>().ToList();
            if(schools.Count > 0) MessageBox.Show(schools[0].Guid);
            //todo faire une recherche par nom ou adresse ....
        }
    }
}
