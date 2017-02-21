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
        public static doctor Find(string email, string password)
        {
            string md5Password = Encryption.GetMD5Hash(password);
            using (rfidEntities db = new rfidEntities())
            {
                var query = db.doctor.FirstOrDefault(d => d.Email == email && d.Password == md5Password);
                //var query2 = (from d in db.doctor where d.Email == email && d.Password == md5Password select d).FirstOrDefault();
                return query;
            }
        }
    }
}
