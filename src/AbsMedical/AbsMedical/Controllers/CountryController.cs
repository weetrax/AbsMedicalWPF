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

        /// <summary>
        /// Get a list of all countries
        /// </summary>
        /// <returns>List of Country</returns>
        public static List<Country> GetCountries()
        {
            using (CountryServiceReference.CountryServiceClient serv = new CountryServiceReference.CountryServiceClient())
            {
                return serv.GetCountries();
            }
        }
    }
}
