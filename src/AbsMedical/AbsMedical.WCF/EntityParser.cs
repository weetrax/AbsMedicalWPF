using AbsMedical.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbsMedical.WCF
{
    public static class EntityParser
    {
        public static doctor ObjectToEntity(Doctor doctor)
        {
            AbsMedical.Data.doctor doc = new AbsMedical.Data.doctor()
            {
                Guid = doctor.Guid,
                Lastname = doctor.Lastname,
                Firstname = doctor.Firstname,
                Email = doctor.Email,
                Password = doctor.Password,
                Address = doctor.Address,
                PostalCode = doctor.PostalCode,
                City = doctor.City,
                CountryId = doctor.CountryId,
                Phone = doctor.Phone,
                MailConfigurationGuid = doctor.MailConfigurationGuid
            };
            return doc;
        }

        public static Doctor EntityToObject(doctor doctor)
        {
            Doctor doc = new Doctor()
            {
                Guid = doctor.Guid,
                Lastname = doctor.Lastname,
                Firstname = doctor.Firstname,
                Email = doctor.Email,
                Password = doctor.Password,
                Address = doctor.Address,
                PostalCode = doctor.PostalCode,
                City = doctor.City,
                CountryId = doctor.CountryId,
                Phone = doctor.Phone,
                MailConfigurationGuid = doctor.MailConfigurationGuid
            };
            return doc;
        }

        public static MailConfiguration EntityToObject(mailconfiguration conf)
        {
            if (conf != null)
            {
                MailConfiguration mail = new MailConfiguration()
                {
                    Guid = conf.Guid,
                    Email = conf.Email,
                    Password = conf.Password,
                    Port = conf.Port,
                    Smtp = conf.Smtp
                };
                return mail;
            }
            else
            {
                return null;
            }
        }

        public static mailconfiguration ObjectToEntity(MailConfiguration conf)
        {
            if (conf != null)
            {
                mailconfiguration mail = new mailconfiguration()
                {
                    Guid = conf.Guid,
                    Email = conf.Email,
                    Password = conf.Password,
                    Port = conf.Port,
                    Smtp = conf.Smtp
                };
                return mail;
            }
            else
            {
                return null;
            }
        }
    }
}