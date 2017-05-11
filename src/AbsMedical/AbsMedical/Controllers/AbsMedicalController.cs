using AbsMedical.WCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsMedical.Controllers
{
    public static class AbsMedicalController
    {
        /// <summary>
        /// Get the historic of a student by its doctor
        /// </summary>
        /// <param name="studentGuid">Id of the student</param>
        /// <param name="doctorGuid">Id of the doctor</param>
        /// <returns>List of MedicalAbs</returns>
        public static List<MedicalAbs> GetAbsMedicalByStudent(string studentGuid, string doctorGuid)
        {
            using (AbsMedicalServiceReference.AbsMedicalServiceClient serv = new AbsMedicalServiceReference.AbsMedicalServiceClient())
            {
                return serv.GetAbsMedicalByStudent(studentGuid, doctorGuid);
            }
        }

        /// <summary>
        /// Register a medical certificate to the Database
        /// </summary>
        /// <param name="medicalAbs">MedicalAbs object to add</param>
        /// <returns>True if the insertion was made</returns>
        public static bool RegisterAbsMedical(MedicalAbs medicalAbs)
        {
            using (AbsMedicalServiceReference.AbsMedicalServiceClient serv = new AbsMedicalServiceReference.AbsMedicalServiceClient())
            {
                return serv.RegisterAbsMedical(medicalAbs);
            }
        }
    }
}
