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
        /// <summary>
        /// Get a doctor object in the Database by its email and password
        /// </summary>
        /// <param name="email">Email of the doctor</param>
        /// <param name="password">Password of the doctor</param>
        /// <returns>A doctor object</returns>
        public static doctor Find(string email, string password)
        {
            string md5Password = Encryption.GetMD5Hash(password);
            rfidEntities db = new rfidEntities();
            
                var query = db.doctor.FirstOrDefault(d => d.Email == email && d.Password == md5Password);
                //var query2 = (from d in db.doctor where d.Email == email && d.Password == md5Password select d).FirstOrDefault();
                return query;
            
        }

        /// <summary>
        /// Add a doctor object in the Database
        /// </summary>
        /// <param name="doctor">doctor object to add</param>
        /// <returns>Boolean indicating if the insertion was made</returns>
        public static bool Create (doctor doctor)
        {
            using (rfidEntities db = new rfidEntities())
            {
                db.doctor.Add(doctor);
                int result = db.SaveChanges();
                return result > 0;
            }
        }

        /// <summary>
        /// Update doctor informations
        /// </summary>
        /// <param name="doctor">doctor object to update</param>
        /// <returns>Boolean indicating if the insertion was made</returns>
        public static bool Update (doctor doctor)
        {
            using (rfidEntities db = new rfidEntities())
            {
                db.doctor.Attach(doctor);
                int result = db.SaveChanges();
                return result > 0;
            }
        }

        /// <summary>
        /// Get the mailconfiguration of the doctor
        /// </summary>
        /// <param name="doctorGuid">Identifier of the doctor</param>
        /// <returns>A mailconfiguration object</returns>
        public static mailconfiguration GetMailConfiguration(string doctorGuid)
        {
            using (rfidEntities db = new rfidEntities())
            {
                return db.mailconfiguration.FirstOrDefault(m => m.DoctorGuid == doctorGuid);
            }
        }

        /// <summary>
        /// Register a mailconfiguration to the Database
        /// </summary>
        /// <param name="mailConfig">mailconfiguration object to add</param>
        /// <returns>Boolean indicating if the insertion was made</returns>
        public static bool RegisterMailConfiguration(mailconfiguration mailConfig)
        {
            using (rfidEntities db = new rfidEntities())
            {
                db.mailconfiguration.Add(mailConfig);
                int result = db.SaveChanges();
                return result > 0;
            }
        }
    }
}
