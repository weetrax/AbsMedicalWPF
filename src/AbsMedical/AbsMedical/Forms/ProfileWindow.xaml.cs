﻿using AbsMedical.Controllers;
using AbsMedical.DoctorServiceReference;
using AbsMedical.Shared;
using AbsMedical.Utils;
using AbsMedical.WCF;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
    /// Logique d'interaction pour ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : MetroWindow
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

        public ProfileWindow(string doctorGuid)
        {
            this.CurrentDoctorGuid = doctorGuid;
            InitializeComponent();
            BindDoctorData();
        }

        private MailConfiguration GetMailConfigurationValues()
        {
            MailConfiguration mailConfig = new MailConfiguration()
            {
                Guid = Guid.NewGuid().ToString(),
                Email = txtEmailConf.Text,
                Password = txtPasswordConf.Password.ToString(),
                Smtp = txtSmtpConf.Text,
                Port = Convert.ToInt32(txtPortConf.Text),
            };
            return mailConfig;
        }
        #endregion

        #region Data Bindings

        private void BindCountries()
        {
            cbCountries.ItemsSource = CountryController.GetCountries();
            cbCountries.DisplayMemberPath = "Name";
            cbCountries.SelectedValuePath = "Id";
        }

        private void BindEmailConfiguration()
        {
            MailConfiguration mailConf = DoctorController.GetMailConfiguration(CurrentDoctor.MailConfigurationGuid);
            if(mailConf != null)
            {
                txtEmailConf.Text = mailConf.Email;
                txtSmtpConf.Text = mailConf.Smtp;
                txtPasswordConf.Password = Encryption.Decrypt(mailConf.Password);
                txtPortConf.Text = mailConf.Port.ToString();
                lblMessageConf.Foreground = Brushes.Green;
                lblMessageConf.Content = "Mail configuration actually valid.";
            }
            else
            {
                lblMessageConf.Visibility = Visibility.Visible;
                lblMessageConf.Foreground = Brushes.Red;
                lblMessageConf.Content = "No mail configuration register yet.";
            }
        }

        private void BindDoctorData()
        {
            this.lblLogedAs.Content = "Logged as " + CurrentDoctor.Firstname + " " + CurrentDoctor.Lastname;
            BindCountries();
            BindEmailConfiguration();
            txtFirstname.Text = CurrentDoctor.Firstname;
            txtLastname.Text = CurrentDoctor.Lastname;
            txtEmail.Text = CurrentDoctor.Email;
            txtAddress.Text = CurrentDoctor.Address;
            txtCity.Text = CurrentDoctor.City;
            txtPostalCode.Text = CurrentDoctor.PostalCode;
            cbCountries.SelectedValue = CurrentDoctor.CountryId;
            txtPhone.Text = CurrentDoctor.Phone;
        }
        #endregion

        #region Button Click Event

        private void btnSaveProfil_Click(object sender, RoutedEventArgs e)
        {
            Doctor editedDoctor = new Doctor()
            {
                Guid = CurrentDoctor.Guid,
                Password = CurrentDoctor.Password,
                Firstname = txtFirstname.Text,
                Lastname = txtLastname.Text,
                Email = txtEmail.Text,
                Address = txtAddress.Text,
                CountryId = cbCountries.SelectedValue.ToString(),
                City = txtCity.Text,
                PostalCode = txtPostalCode.Text,
                Phone = txtPhone.Text,
                MailConfigurationGuid = CurrentDoctor.MailConfigurationGuid
            };

            if (DoctorController.Update(editedDoctor))
            {
                ShowAlert("Profile successfully updated.");
                lblMessageProfile.Foreground = Brushes.Green;
                lblMessageProfile.Content = "Profile successfully updated.";
                BindDoctorData();

            }
            else
            {
                lblMessageProfile.Foreground = Brushes.Red;
                lblMessageProfile.Content = "Error on update.";
            }
        }

        private void btnUpdateProfile_Click(object sender, RoutedEventArgs e)
        {
            btnSaveProfil.Visibility = Visibility.Visible;
            txtFirstname.IsEnabled = true;
            txtLastname.IsEnabled = true;
            txtEmail.IsEnabled = true;
            txtCity.IsEnabled = true;
            txtPostalCode.IsEnabled = true;
            txtAddress.IsEnabled = true;
            cbCountries.IsEnabled = true;
            txtPhone.IsEnabled = true;
        }

        private void btnSaveConf_Click(object sender, RoutedEventArgs e)
        {
            if (Mail.IsValidClient(GetMailConfigurationValues()))
            {
                if(DoctorController.RegisterMailConfiguration(GetMailConfigurationValues(), CurrentDoctor))
                {
                    ShowAlert("Mail configuration successfully registered.");
                    lblMessageConf.Foreground = Brushes.Green;
                    lblMessageConf.Content = "Mail configuration successfully registered.";
                }
                else
                {
                    lblMessageConf.Foreground = Brushes.Red;
                    lblMessageConf.Content = "Error during the Mail configuration registeration.";
                }
            }
            else
            {
                lblMessageConf.Foreground = Brushes.Red;
                lblMessageConf.Content = "Email or password invalid.";
            }
        }

        private void btnCheckConf_Click(object sender, RoutedEventArgs e)
        {
            bool result = Mail.IsValidClient(GetMailConfigurationValues());
            if(result)
            {
                ShowAlert("Mail configuration valid !");
                lblMessageConf.Foreground = Brushes.Green;
                lblMessageConf.Content = "Mail configuration valid.";
            }
            else
            {
                ShowAlert("Mail configuration invalid");
                lblMessageConf.Foreground = Brushes.Red;
                lblMessageConf.Content = "Mail configuration invalid.";
            }
        }

        private void btnSavePassword_Click(object sender, RoutedEventArgs e)
        {
            string currentMD5Password = CurrentDoctor.Password;
            string currentPassword = txtCurrentPassword.Password.ToString();
            string newPassword = txtNewPassword.Password.ToString();
            string confirmNewPassword = txtConfirmPassword.Password.ToString();

            
            if (currentMD5Password == Encryption.GetMD5Hash(currentPassword))
            {
                if (newPassword == confirmNewPassword)
                {
                    if (DoctorController.UpdatePassword(CurrentDoctor.Guid, newPassword))
                    {
                        ShowAlert("Your password has been updated.");
                        lblMessagePassword.Foreground = Brushes.Green;
                        lblMessagePassword.Content = "Password has been updated.";
                    }
                }
                else
                {
                    lblMessagePassword.Foreground = Brushes.Red;
                    lblMessagePassword.Content = "Passwords are not the same.";
                }
            }
            else
            {
                lblMessagePassword.Foreground = Brushes.Red;
                lblMessagePassword.Content = "Current password is invalid";
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

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

        private void txtEmailConf_SelectionChanged(object sender, RoutedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (textbox.Text.Contains("@gmail"))
            {
                txtSmtpConf.Text = "smtp.gmail.com";
                txtPortConf.Text = "587";
            }
            if (textbox.Text.Contains("@hotmail"))
            {
                txtSmtpConf.Text = "smtp.live.com";
                txtPortConf.Text = "587";
            }
            if (textbox.Text.Contains("@yahoo"))
            {
                txtSmtpConf.Text = "smtp.mail.yahoo.com";
                txtPortConf.Text = "587";
            }
        }
    }
}
