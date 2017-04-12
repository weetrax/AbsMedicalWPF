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
        /// Get a student object in the Database by its Id
        /// </summary>
        /// <param name="studentGuid">Identifier of the student</param>
        /// <returns>student object</returns>
        public static student Find(string studentGuid)
        {
            rfidEntities db = new rfidEntities();
            return db.student.FirstOrDefault(w => w.Guid == studentGuid);
        }

        public static Student Get(string studentGuid)
        {
            using (StudentServiceReference.StudentServiceClient serv = new StudentServiceReference.StudentServiceClient())
            {
                return serv.GetStudent(studentGuid);
            }
        }
    }
}
