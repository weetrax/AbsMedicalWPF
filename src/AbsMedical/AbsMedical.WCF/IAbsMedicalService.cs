using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AbsMedical.WCF
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IAbsMedicalService" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IAbsMedicalService
    {
        [OperationContract]
        bool RegisterAbsMedical(MedicalAbs medicalAbs);

        [OperationContract]
        List<MedicalAbs> GetAbsMedicalByStudent(string studentGuid);
    }

    [Serializable]
    [DataContract]
    public class MedicalAbs
    {
        [DataMember]
        public DateTime VisitDate { get; set; }

        [DataMember]
        public string StudentGuid { get; set; }

        [DataMember]
        public string DoctorGuid { get; set; }

        [DataMember]
        public DateTime? StartDate { get; set; }

        [DataMember]
        public DateTime? EndDate { get; set; }

        [DataMember]
        public string Note { get; set; }
    }
}
