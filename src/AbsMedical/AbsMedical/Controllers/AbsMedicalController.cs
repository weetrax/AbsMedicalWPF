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
        public static bool RegisterAbsMedical(MedicalAbs medicalAbs)
        {
            using (AbsMedicalServiceReference.AbsMedicalServiceClient serv = new AbsMedicalServiceReference.AbsMedicalServiceClient())
            {
                return serv.RegisterAbsMedical(medicalAbs);
            }
        }

        public static List<MedicalAbs> GetAbsMedicalByStudent(string studentGuid)
        {
            using (AbsMedicalServiceReference.AbsMedicalServiceClient serv = new AbsMedicalServiceReference.AbsMedicalServiceClient())
            {
                return serv.GetAbsMedicalByStudent(studentGuid);
            }
        }
    }
}
