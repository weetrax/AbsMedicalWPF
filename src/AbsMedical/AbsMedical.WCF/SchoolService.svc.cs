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
