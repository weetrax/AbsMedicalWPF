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
        public List<MedicalAbs> GetAbsMedicalByStudent(string studentGuid)
        {
            List<MedicalAbs> historic = null;
            using (rfidEntities db = new rfidEntities())
            {
                var query = (from a in db.absmedical where a.StudentGuid == studentGuid select a).ToList();
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
