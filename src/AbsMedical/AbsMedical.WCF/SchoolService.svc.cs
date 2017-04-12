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
    }
}
