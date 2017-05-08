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
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "StudentService" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez StudentService.svc ou StudentService.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class StudentService : IStudentService
    {
        public Student GetStudent(string StudentGuid)
        {
            rfidEntities db = new rfidEntities();
            var studentEntity = db.student.FirstOrDefault(i => i.Guid == StudentGuid);
            if (studentEntity != null) return EntityParser.EntityToObject(studentEntity);
            else return null;
        }

        public Student GetStudentBySocialSecurityNumber(string value)
        {
            rfidEntities db = new rfidEntities();
            var studentEntity = db.student.FirstOrDefault(s => s.SocialSecurityNumber == value);
            if (studentEntity != null) return EntityParser.EntityToObject(studentEntity);
            else return null;
        }

        public List<Student> GetStudentsByFilters(string firstname, string lastname, DateTime birthdate)
        {
            List<Student> students = new List<Student>();
            rfidEntities db = new rfidEntities();
            var studentEntities = (from s in db.student where s.Firstname.Contains(firstname) && s.Lastname.Contains(lastname) && s.Birthdate == birthdate select s).ToList();

            if(studentEntities.Count > 0)
            {
                foreach(student stu in studentEntities)
                {
                    students.Add(EntityParser.EntityToObject(stu));
                }
            }
            return students;
        }

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

        public bool RegisterStudent(Student student)
        {
            rfidEntities db = new rfidEntities();
            db.student.Add(EntityParser.ObjectToEntity(student));
            int result = db.SaveChanges();
            return result > 0;
        }
    }
}
