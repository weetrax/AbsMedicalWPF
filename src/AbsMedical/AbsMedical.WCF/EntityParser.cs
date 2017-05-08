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

        public static student ObjectToEntity(Student student)
        {
            AbsMedical.Data.student stu = new AbsMedical.Data.student()
            {
                Guid = student.Guid,
                Lastname = student.Lastname,
                Firstname = student.Firstname,
                Email = student.Email,
                Phone = student.Phone,
                Birthdate = student.Birthdate,
                Birthplace = student.Birthplace,
                StudentId = student.StudentId,
                SocialSecurityNumber = student.SocialSecurityNumber,
                Address = student.Address,
                PostalCode = student.PostalCode,
                City = student.City,
                CountryId = student.CountryId,
                SchoolGuid = student.SchoolGuid
            };
            return stu;
        }

        public static Student EntityToObject(student student)
        {
            Student stu = new Student()
            {
                Guid = student.Guid,
                Lastname = student.Lastname,
                Firstname = student.Firstname,
                Email = student.Email,
                Phone = student.Phone,
                Birthdate = student.Birthdate,
                Birthplace = student.Birthplace,
                StudentId = student.StudentId,
                SocialSecurityNumber = student.SocialSecurityNumber,
                Address = student.Address,
                PostalCode = student.PostalCode,
                City = student.City,
                CountryId = student.CountryId,
                SchoolGuid = student.SchoolGuid,
                DisplayedName = student.Lastname + " " + student.Firstname
            };
            return stu;
        }

        public static school ObjectToEntity(School school)
        {
            school s = new school
            {
                Guid = school.Guid,
                Name = school.Name,
                Address = school.Address,
                PostalCode = school.PostalCode,
                CountryId = school.CountryId,
                Email = school.Email,
                Phone = school.Phone,
                City = school.City
            };
            return s;
        }

        public static School EntityToObject(school school)
        {
            School s = new School()
            {
                Guid = school.Guid,
                Name = school.Name,
                Address = school.Address,
                PostalCode = school.PostalCode,
                CountryId = school.CountryId,
                Email = school.Email,
                Phone = school.Phone,
                City = school.City,
                DisplayedName = school.Name + " - " + school.Address + " - " + school.PostalCode,
            };
            return s;
        }

        public static absmedical ObjectToEntity(MedicalAbs abs)
        {
            absmedical absMedical = new absmedical()
            {
                VisitDate = abs.VisitDate,
                StudentGuid = abs.StudentGuid,
                EndDate = abs.EndDate,
                StartDate = abs.StartDate,
                Note = abs.Note,
                DoctorGuid = abs.DoctorGuid
            };
            return absMedical;
        }

        public static MedicalAbs EntityToObject(absmedical abs)
        {
            MedicalAbs absMedical = new MedicalAbs()
            {
                VisitDate = abs.VisitDate,
                StudentGuid = abs.StudentGuid,
                EndDate = abs.EndDate,
                StartDate = abs.StartDate,
                Note = abs.Note,
                DoctorGuid = abs.DoctorGuid
            };
            return absMedical;
        }

        public static Country EntityToObject(country country)
        {
            Country c = new Country()
            {
                Id = country.Id,
                Name = country.Name
            };
            return c;

        }
    }
}