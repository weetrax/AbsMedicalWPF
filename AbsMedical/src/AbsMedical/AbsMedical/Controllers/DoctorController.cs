using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbsMedical.Data;
using AbsMedical.Utils;

namespace AbsMedical.Controllers
{
    public class DoctorController
    {
        public static bool IsValid(string email, string password)
        {
            string md5Password = Encryption.GetMD5Hash(password);
            using (rfidEntities db = new rfidEntities())
            {
                var query =  db.doctor.Where(w => w.Email == email && w.Password == md5Password).Any();
                return query;
            }
        }
    }
}
