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
    /// Logique d'interaction pour RegisterStudentWindow.xaml
    /// </summary>
    public partial class RegisterStudentWindow : MetroWindow
    {
        private bool Added = false;
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
            BindSchoolsByCountry(countryId);
        }

        private void BindSchoolsByCountry(string countryId)
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
            List<Country> countries = CountryController.GetCountries();

            cbCountries.ItemsSource = countries;
            cbCountries.DisplayMemberPath = "Name";
            cbCountries.SelectedValuePath = "Id";

            cbSchoolCountry.ItemsSource = countries;
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
        private void btnRegisterStudent_Click(object sender, RoutedEventArgs e)
        {
            NFCReader.establishContext();
            NFCReader.SelectDevice();

            if (!string.IsNullOrEmpty(txtFirstname.Text))
            {
                if(!string.IsNullOrEmpty(txtLastname.Text))
                {
                    if(!string.IsNullOrEmpty(txtBirthdate.SelectedDate.ToString()))
                    {
                        if(!string.IsNullOrEmpty(txtBirthplace.Text))
                        {
                            if(!string.IsNullOrEmpty(txtAddress.Text))
                            {
                                if(cbCountries.SelectedItem != null)
                                {
                                    if(!string.IsNullOrEmpty(txtCity.Text))
                                    {
                                        if(!string.IsNullOrEmpty(txtPostalCode.Text))
                                        {
                                            if(!string.IsNullOrEmpty(txtPhone.Text))
                                            {
                                                if(!string.IsNullOrEmpty(txtSocialSecurityNumber.Text))
                                                {
                                                    if(!string.IsNullOrEmpty(cbSchoolCountry.SelectedValue.ToString()))
                                                    {
                                                        if(!string.IsNullOrEmpty(txtStudentId.Text))
                                                        {
                                                            if(NFCReader.connectCard())
                                                            {
                                                                if(StudentController.Get(NFCReader.getcardUID()) == null)
                                                                {
                                                                    Student student = new Student()
                                                                    {
                                                                        Guid = NFCReader.getcardUID(),
                                                                        Lastname = txtLastname.Text,
                                                                        Firstname = txtFirstname.Text,
                                                                        Email = txtEmail.Text,
                                                                        StudentId = txtStudentId.Text,
                                                                        Phone = txtPhone.Text,
                                                                        Birthdate = txtBirthdate.SelectedDate.Value,
                                                                        Birthplace = txtBirthplace.Text,
                                                                        Address = txtAddress.Text,
                                                                        City = txtCity.Text,
                                                                        PostalCode = txtPostalCode.Text,
                                                                        CountryId = cbCountries.SelectedValue.ToString(),
                                                                        SchoolGuid = cbSchool.SelectedValue.ToString(),
                                                                        SocialSecurityNumber = txtSocialSecurityNumber.Text
                                                                    };
                                                                    if (StudentController.RegisterStudent(student) && !Added)
                                                                    {
                                                                        ShowAlert("Student " + student.Firstname + " " + student.Lastname + " has been added.");
                                                                        lblMessage.Foreground = Brushes.Green;
                                                                        lblMessage.Visibility = Visibility.Visible;
                                                                        lblMessage.Text = "Student " + student.Firstname + " " + student.Lastname + " has been added.";
                                                                        Added = true;
                                                                    }
                                                                    else
                                                                    {
                                                                        ShowAlert("An error occured when register student. Please try again.");
                                                                        if(Added)
                                                                        {
                                                                            lblMessage.Foreground = Brushes.Red;
                                                                            lblMessage.Visibility = Visibility.Visible;
                                                                            lblMessage.Text = "Student has already been added.";
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    ShowAlert("This student card is already bind to a student.");
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            ShowAlert("Student Id attribute missing !");
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    ShowAlert("Social security number attribute missing !");
                                                }
                                            }
                                            else
                                            {
                                                ShowAlert("Phone number attribute missing !");
                                            }
                                        }
                                        else
                                        {
                                            ShowAlert("Zip code attribute missing !");
                                        }
                                    }
                                    else
                                    {
                                        ShowAlert("City attribute missing !");
                                    }
                                }
                                else
                                {
                                    ShowAlert("Country attribute missing !");
                                }
                            }
                            else
                            {
                                ShowAlert("Address attribute missing !");
                            }
                        }
                        else
                        {
                            ShowAlert("Birthplace attribute missing !");
                        }
                    }
                    else
                    {
                        ShowAlert("Birthdate attribute missing !");
                    }
                }
                else
                {
                    ShowAlert("Lastname attribute missing !");
                }
            }
            else
            {
                ShowAlert("Firstname attribute missing !");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
