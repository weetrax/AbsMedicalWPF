﻿using AbsMedical.Data;
using AbsMedical.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AbsMedical.WCF
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "DoctorService" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez DoctorService.svc ou DoctorService.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class DoctorService : IDoctorService
    {
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
    }
}
