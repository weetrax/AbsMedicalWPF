﻿using AbsMedical.Controllers;
using AbsMedical.Data;
using AbsMedical.DoctorServiceReference;
using AbsMedical.Utils;
using AbsMedical.WCF;
using iTextSharp.text;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AbsMedical.Forms
{
    /// <summary>
    /// Logique d'interaction pour AbsMedWindow.xaml
    /// </summary>
    public partial class AbsMedWindow : MetroWindow
    {
        private bool Added = false;
        #region Properties
        private WCF.Student CurrentStudent
        {
            get;
        }

        private Doctor CurrentDoctor
        {
            get
            {
                return DoctorController.Get(CurrentDoctorGuid);
            }
        }

        private WCF.School StudentSchool
        {
            get
            {
                return SchoolController.Get(CurrentStudent.SchoolGuid);
            }
        }

        private string CurrentDoctorGuid
        {
            get;
        }

        private MedicalAbs GetAbsMedicalValue()
        {
            MedicalAbs absMedical = new MedicalAbs()
            {
                VisitDate = visitDate.SelectedDate.Value,
                Note = txtMotive.Text,
                DoctorGuid = CurrentDoctorGuid,
                StudentGuid = CurrentStudent.Guid,
                StartDate = dtStart.SelectedDate,
                EndDate = dtEnd.SelectedDate
            };
            return absMedical;
        }
        #endregion

        public AbsMedWindow(WCF.Student student, string doctorGuid)
        {
            this.CurrentStudent = student;
            this.CurrentDoctorGuid = doctorGuid;
            InitializeComponent();
            BindData();
        }

        private void BindData()
        {
            this.lblLogedAs.Content = "Logged as " + CurrentDoctor.Firstname + " " + CurrentDoctor.Lastname;
            
            //Student binding
            lblStudentId.Content = CurrentStudent.StudentId;
            lblFirstname.Content = CurrentStudent.Firstname;
            lblLastname.Content = CurrentStudent.Lastname;
            lblSecurityNumber.Content = CurrentStudent.SocialSecurityNumber;
            lblBirthDate.Content = CurrentStudent.Birthdate;
            lblBirthPlace.Content = CurrentStudent.Birthplace;
            lblAddress.Content = CurrentStudent.Address;
            lblAddress2.Content = CurrentStudent.PostalCode + " " + CurrentStudent.City + ", " + CurrentStudent.CountryId;
            lblPhone.Content = CurrentStudent.Phone;
            lblMail.Content = CurrentStudent.Email;

            //School binding
            lblSchool.Content = StudentSchool.Name;
            lblSchoolAdrs.Content = StudentSchool.Address;
            lblSchoolAdrs2.Content = StudentSchool.PostalCode + " " + StudentSchool.City + ", " + StudentSchool.CountryId;
            lblSchoolPhone.Content = StudentSchool.Phone;
            lblSchoolMail.Content = StudentSchool.Email;

            //Proof
            visitDate.SelectedDate = DateTime.Now;
            dtStart.SelectedDate = DateTime.Now;
            dtEnd.SelectedDate = DateTime.Now;

            //Mail
            txtSubject.Text = "Justificatif Absence Médicale du " + DateTime.Now.ToString("dd/MM/yyyy");
            txtBody.Text = "";
            chkBxSendStudent.IsChecked = true;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            MailConfiguration mailConfig = DoctorController.GetMailConfiguration(CurrentDoctor.MailConfigurationGuid);
            if (mailConfig == null)
            {
                ShowAlert("Error: Wrong mail configuration. Change your configuration in your profile settings.");
            }
            else
            {
                RegisterAbs(false);
                List<string> sendTo = new List<string> { StudentSchool.Email };
                List<string> sendToCC = new List<string> { };
                if (chkBxSendStudent.IsChecked == true)
                {
                    sendToCC.Add(CurrentStudent.Email);
                }
                string subject = txtSubject.Text;
                StringBuilder body = new StringBuilder();
                body.Append(txtBody.Text);
                if (Utils.Mail.Send(mailConfig, sendTo, sendToCC, subject, body, null, false))
                {
                    ShowAlert("Mail sent.");
                }
                else
                {
                    ShowAlert("Error: mail not sent.");
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void btnExportPDF_Click(object sender, RoutedEventArgs e)
        {
            RegisterAbs(false);
            if (PDF.CreatePDF(CurrentStudent, CurrentDoctor, GetAbsMedicalValue()))
            {
                ShowAlert("PDF has been created");
                
            }
        }

        private void RegisterAbs(bool showAlert)
        {
            if(!Added)
            {
                MedicalAbs absMed = new MedicalAbs()
                {
                    DoctorGuid = CurrentDoctorGuid,
                    EndDate = dtEnd.SelectedDate.Value,
                    StartDate = dtStart.SelectedDate.Value,
                    Note = txtMotive.Text,
                    StudentGuid = CurrentStudent.Guid,
                    VisitDate = visitDate.SelectedDate.Value
                };
                if (AbsMedicalController.RegisterAbsMedical(absMed))
                {
                    if (showAlert)
                    {
                        ShowAlert("Student certificate successfully registered.");
                    }
                    Added = true;
                }
                else
                {
                    if (showAlert)
                    {
                        ShowAlert("An error occured when register student. Please try again.");
                    }
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            RegisterAbs(true);
        }
    }
}
