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
        private Student SelectedStudent
        {
            get; set;
        }

        public EditStudentsWindow(string logedAs)
        {
            InitializeComponent();
            BindCountries();
            lblLogedAs.Content = logedAs;
        }

        private void ClearControls()
        {
            txtFirstname.Clear();
            txtFirstname_filters.Clear();
            txtLastname.Clear();
            txtLastname_filters.Clear();
            txtSocialSecurityNumber.Clear();
            cbCountries.SelectedItem = null;
            txtBirthplace.Clear();
            txtBirthdate.SelectedDate = null;
            txtCity.Clear();
            txtAddress.Clear();
            txtPostalCode.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            cbSchoolCountry.SelectedItem = null;
            cbSchool.SelectedItem = null;
            txtStudentId.Clear();
            dtBirthdate_filters.SelectedDate = null;
            SelectedStudent = null;
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
                    List<Student> students = new List<Student> { student };
                    
                }
                else
                {
                    ShowAlert("The student does not exist.");
                    ClearControls();
                }
            }
        }

        private void BindStudent(Student student)
        {
            School studentSchool = SchoolController.Get(student.SchoolGuid);
            if (studentSchool != null)
            {
                cbSchoolCountry.SelectedValue = studentSchool.CountryId;
                BindSchoolsByCountry(studentSchool.CountryId);
            }
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

            dtBirthdate_filters.SelectedDate = student.Birthdate;
            
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

        private void BindSchoolsByCountry(string countryId)
        {
            List<School> schools = SchoolController.GetSchoolsByCountry(countryId);
            cbSchool.ItemsSource = schools;
            cbSchool.SelectedValuePath = "Guid";
            cbSchool.DisplayMemberPath = "DisplayedName";

        }

        private void BindSchools()
        {
            List<School> schools = SchoolController.GetSchools();
            cbSchool.ItemsSource = schools;
            cbSchool.SelectedValuePath = "Guid";
            cbSchool.DisplayMemberPath = "DisplayedName";
        }


        private void btnEditStudent_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedStudent != null)
            {
                if (!string.IsNullOrEmpty(txtFirstname.Text))
                {
                    if (!string.IsNullOrEmpty(txtLastname.Text))
                    {
                        if (cbCountries.SelectedItem != null)
                        {
                            if (!string.IsNullOrEmpty(txtAddress.Text))
                            {
                                if (!string.IsNullOrEmpty(txtCity.Text))
                                {
                                    if (!string.IsNullOrEmpty(txtPostalCode.Text))
                                    {
                                        if (!string.IsNullOrEmpty(txtPhone.Text))
                                        {
                                            if (cbSchool.SelectedItem != null)
                                            {
                                                if (!string.IsNullOrEmpty(txtStudentId.Text))
                                                {
                                                    Student student = new Student()
                                                    {
                                                        Guid = SelectedStudent.Guid,
                                                        Lastname = txtLastname.Text,
                                                        Firstname = txtFirstname.Text,
                                                        Email = txtEmail.Text,
                                                        StudentId = txtStudentId.Text,
                                                        Phone = txtPhone.Text,
                                                        Birthdate = SelectedStudent.Birthdate,
                                                        Birthplace = SelectedStudent.Birthplace,
                                                        Address = txtAddress.Text,
                                                        City = txtCity.Text,
                                                        PostalCode = txtPostalCode.Text,
                                                        CountryId = cbCountries.SelectedValue.ToString(),
                                                        SchoolGuid = cbSchool.SelectedValue.ToString(),
                                                        SocialSecurityNumber = SelectedStudent.SocialSecurityNumber
                                                    };
                                                    if (StudentController.UpdateStudent(student))
                                                    {
                                                        ShowAlert("Student " + student.Firstname + " " + student.Lastname + " has been updated");
                                                    }
                                                    else
                                                    {
                                                        ShowAlert("An error occured while updating student. Please try again.");
                                                    }
                                                }
                                                else
                                                {
                                                    ShowAlert("StudentId attribute missing !");
                                                }
                                            }
                                            else
                                            {
                                                ShowAlert("School attribute missing !");
                                            }
                                        }
                                        else
                                        {
                                            ShowAlert("Phone attribute missing !");
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
                                ShowAlert("Address attribute missing !");
                            }
                        }
                        else
                        {
                            ShowAlert("Country attribute missing !");
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
            else
            {
                ShowAlert("You must choose a student !");
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSocialSecurityNumber.Text))
            {
                Student student = StudentController.GetStudentBySocialSecurityNumber(txtSocialSecurityNumber.Text);
                if (student != null)
                {
                    SelectedStudent = student;
                    BindStudent(SelectedStudent);
                    
                }
                else
                {
                    ShowAlert("Student does not exist.");
                    ClearControls();
                }
            }
            else
            {
                ShowAlert("Social Security Number attribute missing !");
            }
        }

        private void btnSearchbyName_Click(object sender, RoutedEventArgs e)
        {
            string firstname = txtFirstname_filters.Text;
            string lastname = txtLastname_filters.Text;
            if(!string.IsNullOrEmpty(firstname))
            {
                if(!string.IsNullOrEmpty(lastname))
                {
                    if(dtBirthdate_filters.SelectedDate != null)
                    {
                        DateTime birthdate = DateTime.Parse(dtBirthdate_filters.SelectedDate.Value.ToString("yyyy-MM-dd"));
                        Student student = StudentController.GetStudentByFilters(firstname, lastname, birthdate);
                        if(student != null)
                        {
                            BindStudent(student);
                        }
                        else
                        {
                            ShowAlert("Student does not exist.");
                            ClearControls();
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

        private void cbSchoolCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb.SelectedValue != null)
            {
                string countryId = cb.SelectedValue.ToString();
                BindSchoolsByCountry(countryId);
            }
        }

        private void btnDeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedStudent != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure to delete this student ?", "Confirmation", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        if (StudentController.DeleteStudent(SelectedStudent.Guid))
                        {
                            ShowAlert("Student has been deleted");
                            this.Close();
                        }
                        break;
                }
            }
            else
            {
                ShowAlert("You must choose a student !");
            }
        }
    }
}
