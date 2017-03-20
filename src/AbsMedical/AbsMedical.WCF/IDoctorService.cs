using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AbsMedical.WCF
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IDoctorService" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IDoctorService
    {
        [OperationContract]
        Doctor GetDoctor(string doctorGuid);

        [OperationContract]
        Doctor Find(string email, string password);

        [OperationContract]
        bool UpdateDoctor(Doctor doctor);

        [OperationContract]
        bool UpdatePassword(string doctorGuid, string newPassword);

        [OperationContract]
        bool CreateDoctor(Doctor doctor);

        [OperationContract]
        bool RegisterMailConfiguration(MailConfiguration conf, Doctor doctor);

        [OperationContract]
        MailConfiguration GetMailConfiguration(string confGuid);
    }

    [Serializable]
    [DataContract]
    public class Doctor
    {
        [DataMember]
        public string Guid { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Firstname { get; set; }
        [DataMember]
        public string Lastname { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string PostalCode { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public int CountryId { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string MailConfigurationGuid { get; set; }
    }

    [Serializable]
    [DataContract]
    public class MailConfiguration
    {
        [DataMember]
        public string Guid { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Smtp { get; set; }
        [DataMember]
        public int Port { get; set; }
    }
}
