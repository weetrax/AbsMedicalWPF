using AbsMedical.Data;
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
    public partial class AbsMedWindow : Window
    {
        private student CurrentStudent
        {
            get;
            set;
        }
        private doctor CurrentDoctor
        { get; set; }

        public AbsMedWindow(student student, doctor doctor)
        {
            this.CurrentStudent = student;
            InitializeComponent();
            BindData();
        }

        private void BindData()
        {
            //Student
            Id.Content = CurrentStudent.StudentId;
            firstname.Content = CurrentStudent.Firstname;
            lastname.Content = CurrentStudent.Lastname;
            securityNo.Content = CurrentStudent.SocialSecurityNumber;
            birth.Content = "TODO";
            birthPlace.Content = "TODO";
            address.Content = CurrentStudent.Address;
            address2.Content = CurrentStudent.PostalCode + " " + CurrentStudent.City + ", " + CurrentStudent.country.Name;
            Phone.Content = "TODO";
            Mail.Content = CurrentStudent.Email;

            //School
            school.Content = CurrentStudent.school.Name;
            schoolAdrs.Content = CurrentStudent.school.Address;
            schoolAdrs2.Content = CurrentStudent.school.PostalCode + " " + CurrentStudent.school.City + ", " + CurrentStudent.school.country.Name;
            schoolPhone.Content = CurrentStudent.school.Tel;
            schoolMail.Content = CurrentStudent.school.Email;

            //Proof
            Date.Content = DateTime.Now.ToString("dd/MM/yyyy");
            dtStart.SelectedDate = DateTime.Now;
        }
    }
}
