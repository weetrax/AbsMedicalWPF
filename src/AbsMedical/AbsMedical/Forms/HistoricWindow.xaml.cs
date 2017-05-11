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
    /// Logique d'interaction pour HistoricWindow.xaml
    /// </summary>
    public partial class HistoricWindow : MetroWindow
    {
        public HistoricWindow()
        {
            InitializeComponent();
        }

        #region Click Event
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSocialSecurityNumber.Text))
            {
                Student student = StudentController.GetStudentBySocialSecurityNumber(txtSocialSecurityNumber.Text);
                if (student != null)
                {
                    BindHistoric(student.Guid);
                }
                else
                {
                    ShowAlert("Student does not exist.");
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
            if (!string.IsNullOrEmpty(firstname))
            {
                if (!string.IsNullOrEmpty(lastname))
                {
                    if (dtBirthdate_filters.SelectedDate != null)
                    {
                        DateTime birthdate = DateTime.Parse(dtBirthdate_filters.SelectedDate.Value.ToString("yyyy-MM-dd"));
                        Student student = StudentController.GetStudentByFilters(firstname, lastname, birthdate);
                        if (student != null)
                        {
                            BindHistoric(student.Guid);
                        }
                        else
                        {
                            ShowAlert("Student does not exist.");
                            
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
                    BindHistoric(student.Guid);
                }
                else
                {
                    ShowAlert("The student does not exist.");
                    
                }
            }
        }
        #endregion

        private void BindHistoric(string studentGuid)
        {
            List<MedicalAbs> historic = AbsMedicalController.GetAbsMedicalByStudent(studentGuid);
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
    }
}
