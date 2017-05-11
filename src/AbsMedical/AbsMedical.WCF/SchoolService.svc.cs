using AbsMedical.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AbsMedical.WCF
{
    public class SchoolService : ISchoolService
    {
        
        /// <summary>
        /// Get a school object by its Id
        /// </summary>
        /// <param name="schoolGuid">Id of the school</param>
        /// <returns>School object</returns>
        public School GetSchool(string schoolGuid)
        {
            rfidEntities db = new rfidEntities();
            var query = (from s in db.school where s.Guid == schoolGuid select s).FirstOrDefault();
            if(query != null)
            {
                return EntityParser.EntityToObject(query);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Get all schools from the Database
        /// </summary>
        /// <returns>List of schools</returns>
        public List<School> GetSchools()
        {
            List<School> schools = new List<School>();
            rfidEntities db = new rfidEntities();
            var query = db.school.ToList();
            if(query.Count > 0)
            {
                foreach (school s in query)
                {
                    schools.Add(EntityParser.EntityToObject(s));
                }
                return schools;
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// Get all schools that are in a country
        /// </summary>
        /// <param name="countryId">Country Id</param>
        /// <returns>List of schools</returns>
        public List<School> GetSchoolsByCountry(string countryId)
        {
            List<School> schools = new List<School>();
            rfidEntities db = new rfidEntities();
            var query = (from s in db.school where s.CountryId == countryId select s).ToList();
            if(query.Count > 0)
            {
                foreach(school sc in query)
                {
                    schools.Add(EntityParser.EntityToObject(sc));
                }
                return schools;
            }
            else
            {
                return null;
            }
        }
    }
}
