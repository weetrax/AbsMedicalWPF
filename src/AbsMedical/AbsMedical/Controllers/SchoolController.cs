using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbsMedical.Data;
using AbsMedical.WCF;

namespace AbsMedical.Controllers
{
    public static class SchoolController
    {
        public static School Get(string schoolGuid)
        {
            using (SchoolServiceReference.SchoolServiceClient serv = new SchoolServiceReference.SchoolServiceClient())
            {
                return serv.GetSchool(schoolGuid);
            }
        }

        public static List<School> GetSchools()
        {
            using (SchoolServiceReference.SchoolServiceClient serv = new SchoolServiceReference.SchoolServiceClient())
            {
                return serv.GetSchools();
            }
        }

        public static List<School> GetSchoolsByCountry(string countryId)
        {
            using (SchoolServiceReference.SchoolServiceClient serv = new SchoolServiceReference.SchoolServiceClient())
            {
                return serv.GetSchoolsByCountry(countryId);
            }
        }
    }
}
