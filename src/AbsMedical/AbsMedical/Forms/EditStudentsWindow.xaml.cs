using AbsMedical.Controllers;
using AbsMedical.NFC;
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
    /// Logique d'interaction pour EditStudentsWindow.xaml
    /// </summary>
    public partial class EditStudentsWindow : MetroWindow
    {
        public EditStudentsWindow(string logedAs)
        {
            InitializeComponent();
            lblLogedAs.Content = logedAs;
        }

        private void btnScan_Click(object sender, RoutedEventArgs e)
        {
            NFCReader.establishContext();
            NFCReader.SelectDevice();
            if (NFCReader.connectCard())
            {
                string studentGuid = NFCReader.getcardUID();
                WCF.Student student = StudentController.Get(studentGuid);
                if (student != null)
                {
                    BindStudent(student);
                }
                else
                {
                    ShowAlert("The student does not exist.");
                }
            }
        }

        private void BindStudent(Student student)
        {
            txtFirstname.Text = student.Firstname;
            txtLastname.Text = student.Lastname;
            txtBirthdate.SelectedDate = student.Birthdate;
            txtBirthplace.Text = student.Birthplace;
            cbCountries.SelectedValue = student.CountryId;
            txtEmail.Text = student.Email;
            txtAddress.Text = student.Address;
            txtCity.Text = student.City;
            txtPhone.Text = student.Phone;
            txtPostalCode.Text = student.PostalCode;
            cbSchool.SelectedValue = student.SchoolGuid;
            txtStudentId.Text = student.StudentId;
            txtSocialSecurityNumber.Text = student.SocialSecurityNumber;
            txtFirstname_filters.Text = txtFirstname.Text;
            txtLastname_filters.Text = txtLastname.Text;
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void BindCountries()
        {
            List<Country> countries = CountryController.GetCountries();
            cbCountries.ItemsSource = countries;
            cbCountries.SelectedValuePath = "Id";
            cbCountries.DisplayMemberPath = "Name";

            cbSchoolCountry.ItemsSource = countries;
            cbSchoolCountry.SelectedValuePath = "Id";
            cbSchoolCountry.DisplayMemberPath = "Name";
        }



        private void btnEditStudent_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Student student = StudentController.GetStudentBySocialSecurityNumber(txtSocialSecurityNumber.Text);
            if(student != null)
            {
                BindStudent(student);
            }
            else
            {
                ShowAlert("Student does not exist.");
            }
        }
    }
}
