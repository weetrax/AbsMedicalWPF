using AbsMedical.Data;
using AbsMedical.WCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AbsMedical.WCF
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "StudentService" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez StudentService.svc ou StudentService.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class StudentService : IStudentService
    {
        public Student GetStudent(string StudentGuid)
        {
            rfidEntities db = new rfidEntities();
            var studentEntity = db.student.FirstOrDefault(i => i.Guid == StudentGuid);
            if (studentEntity != null) return EntityParser.EntityToObject(studentEntity);
            else return null;
        }

        public bool RegisterStudent(Student student)
        {
            rfidEntities db = new rfidEntities();
            db.student.Add(EntityParser.ObjectToEntity(student));
            int result = db.SaveChanges();
            return result > 0;
        }
    }
}
