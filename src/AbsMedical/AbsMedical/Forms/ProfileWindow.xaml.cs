using AbsMedical.Controllers;
using AbsMedical.Data;
using AbsMedical.Utils;
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
    public partial class ProfileWindow : Window
    {
        private doctor CurrentDoctor
        {
            get;
        }

        public ProfileWindow(doctor doctor)
        {
            this.CurrentDoctor = doctor;
            InitializeComponent();
            BindDoctorData();
        }

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
                txtPasswordConf.Text = mailConf.Password;
                txtSmtpConf.Text = mailConf.Smtp;
                txtPortConf.Text = mailConf.Port.ToString();
                lblMessageConf.Foreground = Brushes.Green;
                lblMessageConf.Content = "Mail configuration valid.";
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
            cbCountries.SelectedValue = CurrentDoctor.country.Id;
        }

        private mailconfiguration GetMailConfigurationValues()
        {
            mailconfiguration mailConfig = new mailconfiguration()
            {
                Email = txtEmailConf.Text,
                Password = txtPasswordConf.Text,
                Smtp = txtSmtpConf.Text,
                Port = Convert.ToInt32(txtPortConf.Text),
                Provider = "",
                DoctorGuid = CurrentDoctor.Guid
            };
            return mailConfig;
        }

        private void btnSaveConf_Click(object sender, RoutedEventArgs e)
        {
            
            if(Mail.IsValidClient(GetMailConfigurationValues()))
            {
                if(DoctorController.RegisterMailConfiguration(GetMailConfigurationValues()))
                {
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

        private void txtEmailConf_SelectionChanged(object sender, RoutedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (textbox.Text.Contains("@gmail"))
            {
                txtSmtpConf.Text = "smtp.gmail.com";
                txtPortConf.Text = "587";
            }
            if(textbox.Text.Contains("@hotmail"))
            {
                txtSmtpConf.Text = "smtp.live.com";
                txtPortConf.Text = "587";
            }
            if(textbox.Text.Contains("@yahoo"))
            {
                txtSmtpConf.Text = "smtp.mail.yahoo.com";
                txtPortConf.Text = "587";
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
    }
}
