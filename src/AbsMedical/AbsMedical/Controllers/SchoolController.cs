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

        /// <summary>
        /// Get a school object by its Id
        /// </summary>
        /// <param name="schoolGuid">id of the school</param>
        /// <returns>School object</returns>
        public static School Get(string schoolGuid)
        {
            using (SchoolServiceReference.SchoolServiceClient serv = new SchoolServiceReference.SchoolServiceClient())
            {
                return serv.GetSchool(schoolGuid);
            }
        }

        /// <summary>
        /// Get all schools
        /// </summary>
        /// <returns>List of schools</returns>
        public static List<School> GetSchools()
        {
            using (SchoolServiceReference.SchoolServiceClient serv = new SchoolServiceReference.SchoolServiceClient())
            {
                return serv.GetSchools();
            }
        }

        /// <summary>
        /// Get all school by a country
        /// </summary>
        /// <param name="countryId">Country Id</param>
        /// <returns>A list of schools</returns>
        public static List<School> GetSchoolsByCountry(string countryId)
        {
            using (SchoolServiceReference.SchoolServiceClient serv = new SchoolServiceReference.SchoolServiceClient())
            {
                return serv.GetSchoolsByCountry(countryId);
            }
        }
    }
}
