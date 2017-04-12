using AbsMedical.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AbsMedical.WCF
{

    public class CountryService : ICountryService
    {
        public List<Country> GetCountries()
        {
            List<Country> countries = new List<Country>();
            rfidEntities db = new rfidEntities();
            var query = (from c in db.country select c).ToList();
            if(query.Count > 0)
            {
                foreach(country c in query)
                {
                    countries.Add(EntityParser.EntityToObject(c));
                }
                return countries;
            }
            else
            {
                return null;
            }
        }
    }
}
