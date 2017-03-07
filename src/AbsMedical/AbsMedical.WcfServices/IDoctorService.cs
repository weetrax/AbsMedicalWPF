using AbsMedical.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AbsMedical.WcfServices
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IDoctorService
    {
        //[OperationContract]
        //string GetData(int value);

        [OperationContract]
        DoctorS GetDoctor(string doctorGuid);

        [OperationContract]
        DoctorS Find(string email, string password);

        [OperationContract]
        bool UpdateDoctor(doctor doctor);

        [OperationContract]
        bool UpdatePassword(string doctorGuid, string newPassword);

    }

    [DataContract]
    public class DoctorS
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
        public string Password{get;set;}
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
    }

    // Utilisez un contrat de données comme indiqué dans l'exemple ci-après pour ajouter les types composites aux opérations de service.
    // Vous pouvez ajouter des fichiers XSD au projet. Une fois le projet généré, vous pouvez utiliser directement les types de données qui y sont définis, avec l'espace de noms "AbsMedical.WcfServices.ContractType".
}
