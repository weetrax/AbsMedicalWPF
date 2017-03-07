﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbsMedical.Data;
using AbsMedical.Utils;
using System.Data.Entity.Core;

namespace AbsMedical.Controllers
{
    public class DoctorController
    {
        /// <summary>
        /// Get a doctor object in the Database by its identifier
        /// </summary>
        /// <param name="doctorGuid">Identifier of the doctor</param>
        /// <returns>A doctor object</returns>
        public static doctor Get(string doctorGuid)
        {
            using (rfidEntities db = new rfidEntities())
            {
                return db.doctor.FirstOrDefault(d => d.Guid == doctorGuid);
            }
        }

        /// <summary>
        /// Get a doctor object in the Database by its email and password
        /// </summary>
        /// <param name="email">Email of the doctor</param>
        /// <param name="password">Password of the doctor</param>
        /// <returns>A doctor object</returns>
        public static doctor Find(string email, string password)
        {
            string md5Password = Encryption.GetMD5Hash(password);
            using (rfidEntities db = new rfidEntities())
            {
                var query = db.doctor.FirstOrDefault(d => d.Email == email && d.Password == md5Password);
                return query;
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
        public static bool Update(doctor doctor)
        {
            using (rfidEntities db = new rfidEntities())
            {
                try
                {
                    var query = (from d in db.doctor where d.Guid == doctor.Guid select d).First();
                    query.Firstname = doctor.Firstname;
                    query.Lastname = doctor.Lastname;
                    query.Email = doctor.Email;
                    query.PostalCode = doctor.PostalCode;
                    query.City = doctor.City;
                    query.CountryId = doctor.CountryId;
                    query.Address = doctor.Address;
                    query.Phone = doctor.Phone;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
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
            string md5password = Encryption.GetMD5Hash(newPassword);

            using (rfidEntities db = new rfidEntities())
            {
                try
                {
                    doctor doctor = db.doctor.First(d => d.Guid == doctorGuid);
                    doctor.Password = md5password;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

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
                        string md5Password = Encryption.GetMD5Hash(mailConfig.Password);
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
                        string md5Password = Encryption.GetMD5Hash(mailConfig.Password);
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
