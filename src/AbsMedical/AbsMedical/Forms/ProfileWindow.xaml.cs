using AbsMedical.Controllers;
using AbsMedical.Data;
using AbsMedical.Utils;
using MahApps.Metro.Controls;
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
        private doctor CurrentDoctor
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

        #region Data Bindings
        private void BindCountries()
        {
            cbCountries.ItemsSource = CountryController.GetCountries();
            cbCountries.DisplayMemberPath = "Name";
            cbCountries.SelectedValuePath = "Id";
        }

        private void BindEmailConfiguration()
        {
            mailconfiguration mailConf = DoctorController.GetMailConfiguration(CurrentDoctor.Guid);
            if(mailConf != null)
            {
                txtEmailConf.Text = mailConf.Email;
                txtSmtpConf.Text = mailConf.Smtp;
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
            doctor editedDoctor = new doctor()
            {
                Guid = CurrentDoctor.Guid,
                Password = CurrentDoctor.Password,
                Firstname = txtFirstname.Text,
                Lastname = txtLastname.Text,
                Email = txtEmail.Text,
                Address = txtAddress.Text,
                CountryId = Convert.ToInt32(cbCountries.SelectedValue),
                City = txtCity.Text,
                PostalCode = txtPostalCode.Text,
                Phone = txtPhone.Text
            };

            if (DoctorController.Update(editedDoctor))
            {
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
            
            if(Mail.IsValidClient(GetMailConfigurationValues()))
            {
                if(DoctorController.RegisterMailConfiguration(GetMailConfigurationValues()))
                {
                    MessageBox.Show("Mail configuration successfully registered.");
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
                MessageBox.Show("Mail configuration valid !");
                lblMessageConf.Foreground = Brushes.Green;
                lblMessageConf.Content = "Mail configuration valid.";
            }
            else
            {
                MessageBox.Show("Mail configuration invalid");
                lblMessageConf.Foreground = Brushes.Red;
                lblMessageConf.Content = "Mail configuration invalid.";
            }
        }

        private void btnSavePassword_Click(object sender, RoutedEventArgs e)
        {
            string currentMD5Password = CurrentDoctor.Password;
            string currentPassword = txtCurrentPassword.Text;
            string newPassword = txtNewPassword.Text;
            string confirmNewPassword = txtConfirmPassword.Text;

            if (currentMD5Password == Encryption.GetMD5Hash(currentPassword))
            {
                if (newPassword == confirmNewPassword)
                {
                    if (DoctorController.UpdatePassword(CurrentDoctor.Guid, newPassword))
                    {
                        MessageBox.Show("Your password has been updated.");
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

        private mailconfiguration GetMailConfigurationValues()
        {
            mailconfiguration mailConfig = new mailconfiguration()
            {
                Guid = Guid.NewGuid().ToString(),
                Email = txtEmailConf.Text,
                Password = txtPasswordConf.Password.ToString(),
                Smtp = txtSmtpConf.Text,
                Port = Convert.ToInt32(txtPortConf.Text),
                DoctorGuid = CurrentDoctorGuid
            };
            return mailConfig;
        }

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
