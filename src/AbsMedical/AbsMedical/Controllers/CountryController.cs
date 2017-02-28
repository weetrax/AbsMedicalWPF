using AbsMedical.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsMedical.Controllers
{
    public class CountryController
    {
        public static List<country> GetCountries()
        {
            using (rfidEntities db = new rfidEntities())
            {
                return db.country.ToList();
            }
        }

        public static country GetCountry(string name)
        {
            using (rfidEntities db = new rfidEntities())
            {
                return db.country.FirstOrDefault(c => c.Name == name);
            }
        }
    }
}
