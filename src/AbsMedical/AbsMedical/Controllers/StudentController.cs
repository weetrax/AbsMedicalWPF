using AbsMedical.Data;
using AbsMedical.WCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsMedical.Controllers
{
    public static class StudentController
    {
        /// <summary>
        /// Get a student object by its Id
        /// </summary>
        /// <param name="studentGuid">Identifier of the student</param>
        /// <returns>student object</returns>
        public static Student Get(string studentGuid)
        {
            using (StudentServiceReference.StudentServiceClient serv = new StudentServiceReference.StudentServiceClient())
            {
                return serv.GetStudent(studentGuid);
            }
        }

        /// <summary>
        /// Register a student in the Database
        /// </summary>
        /// <param name="student">Student object to add</param>
        /// <returns>True if the insertion was made</returns>
        public static bool RegisterStudent(Student student)
        {
            using (StudentServiceReference.StudentServiceClient serv = new StudentServiceReference.StudentServiceClient())
            {
                return serv.RegisterStudent(student);
            }
        }

        /// <summary>
        /// Get a student object by its social security number
        /// </summary>
        /// <param name="value">social security number of the student</param>
        /// <returns>Student object</returns>
        public static Student GetStudentBySocialSecurityNumber(string value)
        {
            using (StudentServiceReference.StudentServiceClient serv = new StudentServiceReference.StudentServiceClient())
            {
                return serv.GetStudentBySocialSecurityNumber(value);
            }
        }

        /// <summary>
        /// Get a list of students by filters
        /// </summary>
        /// <param name="firstname">Student's firstname</param>
        /// <param name="lastname">Student's lastname</param>
        /// <param name="birthdate">Student's birthdate</param>
        /// <returns></returns>
        public static List<Student> GetStudentsByFilters(string firstname, string lastname, DateTime birthdate)
        {
            using (StudentServiceReference.StudentServiceClient serv = new StudentServiceReference.StudentServiceClient())
            {
                return serv.GetStudentsByFilters(firstname, lastname, birthdate);
            }
        }

        /// <summary>
        /// Update student's informations
        /// </summary>
        /// <param name="student">Student object to update</param>
        /// <returns>True if the insertion was made</returns>
        public static bool UpdateStudent(Student student)
        {
            using (StudentServiceReference.StudentServiceClient serv = new StudentServiceReference.StudentServiceClient())
            {
                return serv.UpdateStudent(student);
            }
        }
    }
}
