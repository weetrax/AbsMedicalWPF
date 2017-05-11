using AbsMedical.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AbsMedical.WCF
{
    public class AbsMedicalService : IAbsMedicalService
    {

        /// <summary>
        /// Get a list of all student's MedicalAbs prescribed by the doctor 
        /// </summary>
        /// <param name="studentGuid">Id of the student</param>
        /// <param name="doctorGuid">Id of the doctor</param>
        /// <returns></returns>
        public List<MedicalAbs> GetAbsMedicalByStudent(string studentGuid, string doctorGuid)
        {
            List<MedicalAbs> historic = null;
            using (rfidEntities db = new rfidEntities())
            {
                var query = (from a in db.absmedical where a.StudentGuid == studentGuid && a.DoctorGuid == doctorGuid orderby a.VisitDate select a).ToList();
                if(query.Count > 0)
                {
                    historic = new List<MedicalAbs>();
                    foreach(absmedical abs in query)
                    {
                        historic.Add(EntityParser.EntityToObject(abs));
                    }
                }
            }
            return historic;
        }


        /// <summary>
        /// Register a new MedicalAbs to the Database
        /// </summary>
        /// <param name="medicalAbs">MedicalAbs object to add</param>
        /// <returns>True if the insertion was made</returns>
        public bool RegisterAbsMedical(MedicalAbs medicalAbs)
        {
            using (rfidEntities db = new rfidEntities())
            {
                try
                {
                    db.absmedical.Add(EntityParser.ObjectToEntity(medicalAbs));
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        
    }
}
