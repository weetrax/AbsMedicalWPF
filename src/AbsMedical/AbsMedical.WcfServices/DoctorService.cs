using AbsMedical.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AbsMedical.Data;
using AbsMedical.Shared;

namespace AbsMedical.WcfServices
{
    public class DoctorService : IDoctorService
    {


        public DoctorS GetDoctor(string guid)
        {
            using (rfidEntities db = new rfidEntities())
            {
                var doctorEntity = db.doctor.FirstOrDefault(w => w.Guid == guid);
                if (doctorEntity != null)
                {
                    return TranslateDoctorEntityToDoctorS(doctorEntity);
                }
                else
                {
                    throw new Exception("Invalid Doctor Id");
                }

            }
        }

        public DoctorS Find(string email, string password)
        {
            using (rfidEntities db = new rfidEntities())
            {
                var doctorEntity = db.doctor.FirstOrDefault(w => w.Email == email && w.Password == password);
                if (doctorEntity != null)
                {
                    return TranslateDoctorEntityToDoctorS(doctorEntity);
                }
                else
                {
                    throw new Exception("Invalid Doctor Email/Password");
                }

            }
        }

        public bool UpdateDoctor(DoctorS doctor)
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

        private DoctorS TranslateDoctorEntityToDoctorS(doctor doctor)
        {
            DoctorS doc = new DoctorS();
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
    }
}
