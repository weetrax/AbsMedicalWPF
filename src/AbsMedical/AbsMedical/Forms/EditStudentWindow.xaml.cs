using AbsMedical.Controllers;
using AbsMedical.NFC;
using AbsMedical.WCF;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Logique d'interaction pour EditStudentWindow.xaml
    /// </summary>
    public partial class EditStudentWindow : MetroWindow
    {
        public EditStudentWindow(string logedAs)
        {
            InitializeComponent();
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

        private void btnScanCard_Click(object sender, RoutedEventArgs e)
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
    }
}
