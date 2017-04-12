using AbsMedical.Data;
using AbsMedical.WCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsMedical.Controllers
{
    public static class CountryController
    {
        public static List<Country> GetCountries()
        {
            using (CountryServiceReference.CountryServiceClient serv = new CountryServiceReference.CountryServiceClient())
            {
                return serv.GetCountries();
            }
        }

        public static Country GetCountry(string id)
        {
            var query = (from c in GetCountries() where c.Id == id select c).FirstOrDefault();
            return query;
        }
    }
}
