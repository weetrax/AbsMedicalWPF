using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AbsMedical.WCF
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "ICountryService" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface ICountryService
    {
        [OperationContract]
        List<Country> GetCountries();
    }

    [Serializable]
    [DataContract]
    public class Country
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}
