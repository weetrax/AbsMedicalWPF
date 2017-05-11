﻿using AbsMedical.Data;
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
using AbsMedical.NFC;
using AbsMedical.Controllers;
using MahApps.Metro.Controls;
using System.Diagnostics;
using AbsMedical.DoctorServiceReference;
using AbsMedical.WCF;
using MahApps.Metro.Controls.Dialogs;

namespace AbsMedical.Forms
{
    /// <summary>
    /// Logique d'interaction pour HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : MetroWindow
    {
        #region Properties
        private Doctor CurrentDoctor
        {
            get
            {
                return DoctorController.Get(CurrentDoctorGuid);
            }
        }

        private string CurrentDoctorGuid
        {
            get;
        }
        #endregion

        public HomeWindow(string doctorGuid)
        {

            this.CurrentDoctorGuid = doctorGuid;
            InitializeComponent();
            SetTitle();
        }

        private void SetTitle()
        {
            this.lblLogedAs.Content = "Logged as " + CurrentDoctor.Firstname + " " + CurrentDoctor.Lastname;
        }

        #region Click Event
        private void tileRegisterStudent_Click(object sender, RoutedEventArgs e)
        {
            ManageStudentsWindow window = new ManageStudentsWindow(lblLogedAs.Content.ToString(), CurrentDoctorGuid);
            window.Show();
            this.Hide();
        }

        private void tileExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void tileProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfileWindow window = new ProfileWindow(CurrentDoctorGuid);
            window.Show();
        }

        private void tileRegisterNewCertificate_Click(object sender, RoutedEventArgs e)
        {
            NFCReader.establishContext();
            NFCReader.SelectDevice();
            if (NFCReader.connectCard())
            {
                string studentGuid = NFCReader.getcardUID();
                WCF.Student student = StudentController.Get(studentGuid);
                if (student != null)
                {
                    AbsMedWindow window = new AbsMedWindow(student, CurrentDoctorGuid);
                    window.Show();
                }
                else
                {
                    ShowAlert("The student does not exist.");
                }
            }
        }

        private void btnOurGitHub_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/weetrax/AbsMedicalWPF");
        }
        #endregion Event
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

            RegisterStudentWindow window = new RegisterStudentWindow(lblLogedAs.Content.ToString());
            window.Show();
        }
        #endregion

        private void tileHistory_Click(object sender, RoutedEventArgs e)
        {
            string logedAs = "Logged as " + CurrentDoctor.Firstname + " " + CurrentDoctor.Lastname;
            HistoricWindow window = new HistoricWindow(CurrentDoctorGuid, logedAs);
            window.Show();
        }
    }
}
