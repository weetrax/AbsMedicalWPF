using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AbsMedical.WCF
{
    [ServiceContract]
    public interface ISchoolService
    {
        [OperationContract]
        School GetSchool(string schoolGuid);

        [OperationContract]
        List<School> GetSchoolsByCountry(string countryId);

        [OperationContract]
        List<School> GetSchools();
    }

    [Serializable]
    [DataContract]
    public class School
    {
        [DataMember]
        public string Guid { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string PostalCode { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string CountryId { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string DisplayedName { get;  set;}
    }
}
