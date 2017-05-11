using AbsMedical.Data;
using AbsMedical.WCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AbsMedical.WCF
{

    public class StudentService : IStudentService
    {

        /// <summary>
        /// Get a student object by its Id
        /// </summary>
        /// <param name="studentGuid">Identifier of the student</param>
        /// <returns>student object</returns>
        public Student GetStudent(string StudentGuid)
        {
            using (rfidEntities db = new rfidEntities())
            {
                var studentEntity = db.student.FirstOrDefault(i => i.Guid == StudentGuid);
                if (studentEntity != null) return EntityParser.EntityToObject(studentEntity);
                else return null;
            }
        }

        /// <summary>
        /// Get a student object by its social security number
        /// </summary>
        /// <param name="value">social security number of the student</param>
        /// <returns>Student object</returns>
        public Student GetStudentBySocialSecurityNumber(string value)
        {
            using (rfidEntities db = new rfidEntities())
            {
                var studentEntity = db.student.FirstOrDefault(s => s.SocialSecurityNumber == value);
                if (studentEntity != null) return EntityParser.EntityToObject(studentEntity);
                else return null;
            }
        }

        /// <summary>
        /// Delete a student from the Database
        /// </summary>
        /// <param name="studentGuid">Id of the student</param>
        /// <returns>True if the suppression was made</returns>
        public bool DeleteStudent(string studentGuid)
        {
            using (rfidEntities db = new rfidEntities())
            {
                var studentEntity = db.student.FirstOrDefault(s => s.Guid == studentGuid);
                db.student.Remove(studentEntity);
                int result = db.SaveChanges();
                return result > 0;
            }
        }

        /// <summary>
        /// Get a student object by its firstname, lastname & birthdate
        /// </summary>
        /// <param name="firstname">Student's firstname</param>
        /// <param name="lastname">Student's lastname</param>
        /// <param name="birthdate">Student's birthdate</param>
        /// <returns>Student object</returns>
        public Student GetStudentByFilters(string firstname, string lastname, DateTime birthdate)
        {
            List<Student> students = new List<Student>();
            using (rfidEntities db = new rfidEntities())
            {
                var studentEntity = (from s in db.student where s.Firstname.Contains(firstname) && s.Lastname.Contains(lastname) && s.Birthdate == birthdate select s).FirstOrDefault();

                if (studentEntity != null)
                {
                    return EntityParser.EntityToObject(studentEntity);
                }
                else return null;
            }
        }

        /// <summary>
        /// Update student's informations
        /// </summary>
        /// <param name="student">Student object to update</param>
        /// <returns>True if the insertion was made</returns>
        public bool UpdateStudent(Student student)
        {
            using (rfidEntities db = new rfidEntities())
            {
                try
                {
                    var query = (from d in db.student where d.Guid == student.Guid select d).First();
                    query.Firstname = student.Firstname;
                    query.Lastname = student.Lastname;
                    query.Email = student.Email;
                    query.PostalCode = student.PostalCode;
                    query.City = student.City;
                    query.CountryId = student.CountryId;
                    query.Address = student.Address;
                    query.Phone = student.Phone;
                    query.SchoolGuid = student.SchoolGuid;
                    query.StudentId = student.StudentId;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Register a student in the Database
        /// </summary>
        /// <param name="student">Student object to add</param>
        /// <returns>True if the insertion was made</returns>
        public bool RegisterStudent(Student student)
        {
            using (rfidEntities db = new rfidEntities())
            {
                db.student.Add(EntityParser.ObjectToEntity(student));
                int result = db.SaveChanges();
                return result > 0;
            }
        }
    }
}
