using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbsMedical.Data;
using AbsMedical.Utils;
using System.Data.Entity.Core;
using AbsMedical.Shared;
using AbsMedical.DoctorServiceReference;
using System.ServiceModel;
using AbsMedical.WCF;

namespace AbsMedical.Controllers
{
    public static class DoctorController
    {
        /// <summary>
        /// Get a doctor object in the Database by its identifier
        /// </summary>
        /// <param name="doctorGuid">Identifier of the doctor</param>
        /// <returns>A doctor object</returns>
        public static Doctor Get(string doctorGuid)
        {
            using (DoctorServiceReference.DoctorServiceClient serv = new DoctorServiceReference.DoctorServiceClient())
            {
                return serv.GetDoctor(doctorGuid);
            }
        }

        /// <summary>
        /// Get a doctor object in the Database by its email and password
        /// </summary>
        /// <param name="email">Email of the doctor</param>
        /// <param name="password">Password of the doctor</param>
        /// <returns>A doctor object</returns>
        public static Doctor Find(string email, string password) 
        {
            try
            {
                using (DoctorServiceReference.DoctorServiceClient serv = new DoctorServiceReference.DoctorServiceClient())
                {
                    return serv.Find(email, password);
                }
            }
            catch (ServiceActivationException)
            {
                return null;
            }
           
        }

        /// <summary>
        /// Add a doctor object in the Database
        /// </summary>
        /// <param name="doctor">doctor object to add</param>
        /// <returns>Boolean indicating if the insertion was made</returns>
        public static bool Create(Doctor doctor)
        {
            using (DoctorServiceReference.DoctorServiceClient serv = new DoctorServiceReference.DoctorServiceClient())
            {
                return serv.CreateDoctor(doctor);
            }
        }

        /// <summary>
        /// Update doctor informations
        /// </summary>
        /// <param name="doctor">doctor object to update</param>
        /// <returns>Boolean indicating if the insertion was made</returns>
        public static bool Update(Doctor doctor)
        {
            using (DoctorServiceReference.DoctorServiceClient serv = new DoctorServiceReference.DoctorServiceClient())
            {
                return serv.UpdateDoctor(doctor);
            }
        }

        /// <summary>
        /// Update the password of the doctor
        /// </summary>
        /// <param name="doctorGuid">Identifier of the doctor</param>
        /// <param name="newPassword">New password</param>
        /// <returns></returns>
        public static bool UpdatePassword(string doctorGuid, string newPassword)
        {
            using (DoctorServiceReference.DoctorServiceClient serv = new DoctorServiceReference.DoctorServiceClient())
            {
                return serv.UpdatePassword(doctorGuid, newPassword);
            }
        }

        /// <summary>
        /// Get the mailconfiguration of the doctor
        /// </summary>
        /// <param name="doctorGuid">Identifier of the doctor</param>
        /// <returns>A mailconfiguration object</returns>
        public static MailConfiguration GetMailConfiguration(string confGuid)
        {
            using (DoctorServiceReference.DoctorServiceClient serv = new DoctorServiceReference.DoctorServiceClient())
            {
                return serv.GetMailConfiguration(confGuid);
            }
        }

        /// <summary>
        /// Register a mailconfiguration to the Database
        /// </summary>
        /// <param name="mailConfig">mailconfiguration object to add</param>
        /// <returns>Boolean indicating if the insertion was made</returns>
        public static bool RegisterMailConfiguration(MailConfiguration mailConfig, Doctor doctor)
        {
            using (DoctorServiceReference.DoctorServiceClient serv = new DoctorServiceReference.DoctorServiceClient())
            {
                return serv.RegisterMailConfiguration(mailConfig, doctor);
            }
        }
    }
}
