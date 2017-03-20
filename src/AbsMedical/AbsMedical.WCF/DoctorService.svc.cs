using AbsMedical.Data;
using AbsMedical.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AbsMedical.WCF
{
    public class DoctorService : IDoctorService
    {
        /// <summary>
        /// Get a doctor object in the Database by its email and password
        /// </summary>
        /// <param name="email">Email of the doctor</param>
        /// <param name="password">Password of the doctor</param>
        /// <returns>A doctor object</returns>
        public Doctor Find(string email, string password)
        {
            rfidEntities db = new rfidEntities();
            string md5Password = Encryption.GetMD5Hash(password);
            var doctorEntity = db.doctor.FirstOrDefault(w => w.Email == email && w.Password == md5Password);
            if (doctorEntity != null)
            {
                return EntityParser.EntityToObject(doctorEntity);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get a doctor object in the Database by its identifier
        /// </summary>
        /// <param name="doctorGuid">Identifier of the doctor</param>
        /// <returns>A doctor object</returns>
        public Doctor GetDoctor(string doctorGuid)
        {
            rfidEntities db = new rfidEntities();
            var doctorEntity = db.doctor.FirstOrDefault(w => w.Guid == doctorGuid);
            if (doctorEntity != null)
            {
                return EntityParser.EntityToObject(doctorEntity);
            }
            else
            {
                return null;
            }
            
        }

        /// <summary>
        /// Update doctor informations
        /// </summary>
        /// <param name="doctor">doctor object to update</param>
        /// <returns>Boolean indicating if the insertion was made</returns>
        public bool UpdateDoctor(Doctor doctor)
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
                    query.MailConfigurationGuid = doctor.MailConfigurationGuid;
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
        public bool UpdatePassword(string doctorGuid, string newPassword)
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
        /// Add a doctor object in the Database
        /// </summary>
        /// <param name="doctor">doctor object to add</param>
        public bool CreateDoctor(Doctor doctor)
        {
            using (rfidEntities db = new rfidEntities())
            {
                try
                {
                    db.doctor.Add(EntityParser.ObjectToEntity(doctor));
                    int result = db.SaveChanges();
                    return result > 0;
                }
                catch (Exception)
                {
                    throw new Exception("Unable to create a doctor");
                }

            }
        }

        /// <summary>
        /// Get the mailconfiguration of the doctor
        /// </summary>
        /// <param name="doctorGuid">Identifier of the doctor</param>
        /// <returns>A mailconfiguration object</returns>
        public MailConfiguration GetMailConfiguration(string confGuid)
        {
            rfidEntities db = new rfidEntities();
            var mailConfEntity = (from d in db.mailconfiguration where d.Guid == confGuid select d).FirstOrDefault();
            if(mailConfEntity != null)
            {
                return EntityParser.EntityToObject(mailConfEntity);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Register a mailconfiguration to the Database
        /// </summary>
        /// <param name="mailConfig">mailconfiguration object to add</param>
        /// <returns>Boolean indicating if the insertion was made</returns>
        public bool RegisterMailConfiguration (MailConfiguration mailConfig, Doctor doctor)
        {
            using (rfidEntities db = new rfidEntities())
            {
                if (string.IsNullOrEmpty(doctor.MailConfigurationGuid))
                {
                    try
                    {
                        string md5Password = Encryption.Encrypt(mailConfig.Password);
                        mailConfig.Password = md5Password;
                        db.mailconfiguration.Add(EntityParser.ObjectToEntity(mailConfig));

                        var currentDoctor = db.doctor.First(w => w.Guid == doctor.Guid);
                        currentDoctor.MailConfigurationGuid = mailConfig.Guid;

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
                        var query = db.mailconfiguration.First(m => m.Guid == doctor.MailConfigurationGuid);
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
        private bool MailConfigurationAlreadyExist(string doctorGuid)
        {
            return GetMailConfiguration(doctorGuid) != null;
        }
    }
}
