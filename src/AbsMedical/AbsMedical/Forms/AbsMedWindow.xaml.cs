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
            BindStudentData();
        }

        private void BindStudentData()
        {
            //faire tout les Bind ici avec l'objet CurrentStudent:
            //exemple: lblName.Content = CurrentStudent.Firstname;
        }
    }
}
