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
