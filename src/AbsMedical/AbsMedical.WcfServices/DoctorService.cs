using AbsMedical.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AbsMedical.Data;

namespace AbsMedical.WcfServices
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class DoctorService : IDoctorService
    {
        public doctor GetDoctor(string guid)
        {
            using (rfidEntities db = new rfidEntities())
            {
                var doctorEntity = db.doctor.FirstOrDefault(w => w.Guid == guid);
                if(doctorEntity != null)
                {
                    return doctorEntity;
                }
                else
                {
                    throw new Exception("Invalid Doctor Id");
                }

            }
        }

        public doctor GetDoctor(string email, string password)
        {
            using (rfidEntities db = new rfidEntities())
            {
                var doctorEntity = db.doctor.FirstOrDefault(w => w.Email == email && w.Password == password);
                if (doctorEntity != null)
                {
                    return doctorEntity;
                }
                else
                {
                    throw new Exception("Invalid Doctor Email/Password");
                }

            }
        }

        public bool UpdateDoctor(doctor doctor)
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

        //private DoctorS TranslateDoctorEntityToDoctorS(doctor doctor)
        //{
        //    DoctorS doc = new DoctorS();
        //    doc.DoctorGuid = doctor.Guid;
        //    doc.DoctorFirstname = doctor.Firstname;
        //    doc.DoctorLastname = doctor.Lastname;
        //    doc.DoctorEmail = doctor.Email;
        //    doc.DoctorPhone = doctor.Phone;
        //    doc.DoctorPassword = doctor.Password;
        //    doc.DoctorAddress = doctor.Address;
        //    doc.DoctorPoscalCode = doctor.PostalCode;
        //    doc.DoctorCity = doctor.City;
        //    doc.DoctorCountryId = doctor.CountryId;
        //    return doc;
        //}
    }
}
