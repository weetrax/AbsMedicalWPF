﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbsMedical.Data;
using AbsMedical.Utils;
using System.Data.Entity.Core;
using AbsMedical.Shared;

namespace AbsMedical.Controllers
{
    public static class DoctorController
    {
        /// <summary>
        /// Get a doctor object in the Database by its identifier
        /// </summary>
        /// <param name="doctorGuid">Identifier of the doctor</param>
        /// <returns>A doctor object</returns>
        public static DoctorServiceReference.Doctor Get(string doctorGuid)
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
        public static DoctorServiceReference.Doctor Find(string email, string password)
        {
            using (DoctorServiceReference.DoctorServiceClient serv = new DoctorServiceReference.DoctorServiceClient())
            {
                return serv.Find(email, password);
            }
        }

        /// <summary>
        /// Add a doctor object in the Database
        /// </summary>
        /// <param name="doctor">doctor object to add</param>
        /// <returns>Boolean indicating if the insertion was made</returns>
        public static bool Create(doctor doctor)
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
        public static bool Update(DoctorServiceReference.Doctor doctor)
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
                if(!MailConfigurationAlreadyExist(mailConfig.DoctorGuid))
                {
                    try
                    {
                        string md5Password = Encryption.Encrypt(mailConfig.Password);
                        mailConfig.Password = md5Password;
                        db.mailconfiguration.Add(mailConfig);
                        db.SaveChanges();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                else
                {
                    try
                    {
                        string md5Password = Encryption.Encrypt(mailConfig.Password);
                        var query = db.mailconfiguration.First(m => m.DoctorGuid == mailConfig.DoctorGuid);
                        query.Email = mailConfig.Email;
                        query.Password = md5Password;
                        query.Port = mailConfig.Port;
                        query.Smtp = mailConfig.Smtp;
                        db.SaveChanges();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Verify if a doctor has already configure a mail configuration
        /// </summary>
        /// <param name="doctorGuid">Identifier of the doctor</param>
        /// <returns>Boolean</returns>
        private static bool MailConfigurationAlreadyExist(string doctorGuid)
        {
            return GetMailConfiguration(doctorGuid) != null;
        }
    }
}
