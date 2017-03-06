using AbsMedical.Controllers;
using AbsMedical.Data;
using AbsMedical.Utils;
using MahApps.Metro.Controls;
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
    /// Logique d'interaction pour AbsMedWindow.xaml
    /// </summary>
    public partial class AbsMedWindow : MetroWindow
    {
        #region Properties
        private student CurrentStudent
        {
            get;
        }

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
        #endregion

        public AbsMedWindow(student student, string doctorGuid)
        {
            this.CurrentStudent = student;
            this.CurrentDoctorGuid = doctorGuid;
            InitializeComponent();
            BindData();
        }

        private void BindData()
        {
            //Student binding
            lblStudentId.Content = CurrentStudent.StudentId;
            lblFirstname.Content = CurrentStudent.Firstname;
            lblLastname.Content = CurrentStudent.Lastname;
            lblSecurityNumber.Content = CurrentStudent.SocialSecurityNumber;
            lblBirthDate.Content = CurrentStudent.Birthdate;
            lblBirthPlace.Content = CurrentStudent.Birthplace;
            lblAddress.Content = CurrentStudent.Address;
            lblAddress2.Content = CurrentStudent.PostalCode + " " + CurrentStudent.City + ", " + CurrentStudent.country.Name;
            lblPhone.Content = CurrentStudent.Phone;
            lblMail.Content = CurrentStudent.Email;

            //School binding
            lblSchool.Content = CurrentStudent.school.Name;
            lblSchoolAdrs.Content = CurrentStudent.school.Address;
            lblSchoolAdrs2.Content = CurrentStudent.school.PostalCode + " " + CurrentStudent.school.City + ", " + CurrentStudent.school.country.Name;
            lblSchoolPhone.Content = CurrentStudent.school.Phone;
            lblSchoolMail.Content = CurrentStudent.school.Email;

            //Proof
            lblDate.Content = DateTime.Now.ToString("dd/MM/yyyy");
            dtStart.SelectedDate = DateTime.Now;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            mailconfiguration mailConfig = DoctorController.GetMailConfiguration(CurrentDoctorGuid);  //TODO
            List<string> sendTo = new List<string> { CurrentStudent.school.Email, CurrentStudent.Email };
            StringBuilder body = new StringBuilder();
            body.Append(rtbMotive);
            Utils.Mail.Send(mailConfig, sendTo, "Justificatif Absence Médicale du " + lblDate, body, null);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
