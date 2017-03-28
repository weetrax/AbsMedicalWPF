using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AbsMedical.WCF
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IStudentService" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IStudentService
    {
        [OperationContract]
        Student GetStudent(string StudentGuid);
    }

    [Serializable]
    [DataContract]
    public class Student
    {
        [DataMember]
        public string Guid { get; set; }
        [DataMember]
        public string Firstname { get; set; }
        [DataMember]
        public string Lastname { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public DateTime Birthdate { get; set; }
        [DataMember]
        public string Birthplace { get; set; }
        [DataMember]
        public string StudentId { get; set; }
        [DataMember]
        public string SocialSecurityNumber { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string PostalCode { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string CountryId { get; set; }
        [DataMember]
        public string SchoolGuid { get; set; }
    }
}
