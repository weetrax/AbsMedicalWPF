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
            using (rfidEntities db = new rfidEntities())
            {
                string md5Password = Encryption.GetMD5Hash(password);
                var doctorEntity = db.doctor.FirstOrDefault(w => w.Email == email && w.Password == md5Password);
                if (doctorEntity != null)
                {
                    return TranslateDoctorEntityToDoctor(doctorEntity);
                }
                else
                {
                    throw new Exception("Invalid Doctor Email/Password");
                }
            }
        }

        /// <summary>
        /// Get a doctor object in the Database by its identifier
        /// </summary>
        /// <param name="doctorGuid">Identifier of the doctor</param>
        /// <returns>A doctor object</returns>
        public Doctor GetDoctor(string doctorGuid)
        {
            using (rfidEntities db = new rfidEntities())
            {
                var doctorEntity = db.doctor.FirstOrDefault(w => w.Guid == doctorGuid);
                if (doctorEntity != null)
                {
                    return TranslateDoctorEntityToDoctor(doctorEntity);
                }
                else
                {
                    throw new Exception("Invalid Doctor Id");
                }
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
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    throw new Exception("Cannot update profile");
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
                    throw new Exception("Unable to update the password");
                }

            }
        }

        /// <summary>
        /// Add a doctor object in the Database
        /// </summary>
        /// <param name="doctor">doctor object to add</param>
        public bool CreateDoctor(doctor doctor)
        {
            using (rfidEntities db = new rfidEntities())
            {
                try
                {
                    db.doctor.Add(doctor);
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
        public MailConfiguration GetMailConfiguration(string doctorGuid)
        {
            using (rfidEntities db = new rfidEntities())
            {
                var mailConfEntity = db.mailconfiguration.FirstOrDefault(m => m.DoctorGuid == doctorGuid);
                if(mailConfEntity != null)
                {
                    return TranslateMailConfigurationEntityToMailConfiguration(mailConfEntity);
                }
                else
                {
                    throw new Exception("Unable to get doctor's mail configuration");
                }
            }
        }

        /// <summary>
        /// Register a mailconfiguration to the Database
        /// </summary>
        /// <param name="mailConfig">mailconfiguration object to add</param>
        /// <returns>Boolean indicating if the insertion was made</returns>
        public bool RegisterMailConfiguration (mailconfiguration mailConfig)
        {
            using (rfidEntities db = new rfidEntities())
            {
                if (!MailConfigurationAlreadyExist(mailConfig.DoctorGuid))
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
                        throw new Exception("Unable to register a mail configuration");
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
                        throw new Exception("Unable to update a mail configuration");
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

        /// <summary>
        /// Translate a AbsMedical.Data.doctor object into a Doctor object to not give a database entity
        /// </summary>
        /// <param name="doctor"></param>
        /// <returns></returns>
        private Doctor TranslateDoctorEntityToDoctor(doctor doctor)
        {
            Doctor doc = new Doctor();
            doc.Guid = doctor.Guid;
            doc.Firstname = doctor.Firstname;
            doc.Lastname = doctor.Lastname;
            doc.Email = doctor.Email;
            doc.Phone = doctor.Phone;
            doc.Password = doctor.Password;
            doc.Address = doctor.Address;
            doc.PostalCode = doctor.PostalCode;
            doc.City = doctor.City;
            doc.CountryId = doctor.CountryId;
            return doc;
        }

        /// <summary>
        /// Translate a AbsMedical.Data.mailconfiguration object into a MailConfiguration object to not give a database entity
        /// </summary>
        /// <param name="conf"></param>
        /// <returns></returns>
        private MailConfiguration TranslateMailConfigurationEntityToMailConfiguration(mailconfiguration conf)
        {
            MailConfiguration mailConf = new MailConfiguration();
            mailConf.Guid = conf.Guid;
            mailConf.Email = conf.Email;
            mailConf.Password = conf.Password;
            mailConf.Smtp = conf.Smtp;
            mailConf.Port = conf.Port;
            mailConf.DoctorGuid = conf.DoctorGuid;
            return mailConf;
        }
    }
}
