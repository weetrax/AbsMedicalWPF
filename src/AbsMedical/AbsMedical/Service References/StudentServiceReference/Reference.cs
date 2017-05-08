﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AbsMedical.StudentServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="StudentServiceReference.IStudentService")]
    public interface IStudentService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStudentService/GetStudent", ReplyAction="http://tempuri.org/IStudentService/GetStudentResponse")]
        AbsMedical.WCF.Student GetStudent(string StudentGuid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStudentService/GetStudent", ReplyAction="http://tempuri.org/IStudentService/GetStudentResponse")]
        System.Threading.Tasks.Task<AbsMedical.WCF.Student> GetStudentAsync(string StudentGuid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStudentService/RegisterStudent", ReplyAction="http://tempuri.org/IStudentService/RegisterStudentResponse")]
        bool RegisterStudent(AbsMedical.WCF.Student student);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStudentService/RegisterStudent", ReplyAction="http://tempuri.org/IStudentService/RegisterStudentResponse")]
        System.Threading.Tasks.Task<bool> RegisterStudentAsync(AbsMedical.WCF.Student student);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStudentService/GetStudentBySocialSecurityNumber", ReplyAction="http://tempuri.org/IStudentService/GetStudentBySocialSecurityNumberResponse")]
        AbsMedical.WCF.Student GetStudentBySocialSecurityNumber(string value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStudentService/GetStudentBySocialSecurityNumber", ReplyAction="http://tempuri.org/IStudentService/GetStudentBySocialSecurityNumberResponse")]
        System.Threading.Tasks.Task<AbsMedical.WCF.Student> GetStudentBySocialSecurityNumberAsync(string value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStudentService/GetStudentByFilters", ReplyAction="http://tempuri.org/IStudentService/GetStudentByFiltersResponse")]
        AbsMedical.WCF.Student GetStudentByFilters(string firstname, string lastname, System.DateTime birthdate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStudentService/GetStudentByFilters", ReplyAction="http://tempuri.org/IStudentService/GetStudentByFiltersResponse")]
        System.Threading.Tasks.Task<AbsMedical.WCF.Student> GetStudentByFiltersAsync(string firstname, string lastname, System.DateTime birthdate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStudentService/UpdateStudent", ReplyAction="http://tempuri.org/IStudentService/UpdateStudentResponse")]
        bool UpdateStudent(AbsMedical.WCF.Student student);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStudentService/UpdateStudent", ReplyAction="http://tempuri.org/IStudentService/UpdateStudentResponse")]
        System.Threading.Tasks.Task<bool> UpdateStudentAsync(AbsMedical.WCF.Student student);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStudentService/DeleteStudent", ReplyAction="http://tempuri.org/IStudentService/DeleteStudentResponse")]
        bool DeleteStudent(string studentGuid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStudentService/DeleteStudent", ReplyAction="http://tempuri.org/IStudentService/DeleteStudentResponse")]
        System.Threading.Tasks.Task<bool> DeleteStudentAsync(string studentGuid);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IStudentServiceChannel : AbsMedical.StudentServiceReference.IStudentService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class StudentServiceClient : System.ServiceModel.ClientBase<AbsMedical.StudentServiceReference.IStudentService>, AbsMedical.StudentServiceReference.IStudentService {
        
        public StudentServiceClient() {
        }
        
        public StudentServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public StudentServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StudentServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StudentServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public AbsMedical.WCF.Student GetStudent(string StudentGuid) {
            return base.Channel.GetStudent(StudentGuid);
        }
        
        public System.Threading.Tasks.Task<AbsMedical.WCF.Student> GetStudentAsync(string StudentGuid) {
            return base.Channel.GetStudentAsync(StudentGuid);
        }
        
        public bool RegisterStudent(AbsMedical.WCF.Student student) {
            return base.Channel.RegisterStudent(student);
        }
        
        public System.Threading.Tasks.Task<bool> RegisterStudentAsync(AbsMedical.WCF.Student student) {
            return base.Channel.RegisterStudentAsync(student);
        }
        
        public AbsMedical.WCF.Student GetStudentBySocialSecurityNumber(string value) {
            return base.Channel.GetStudentBySocialSecurityNumber(value);
        }
        
        public System.Threading.Tasks.Task<AbsMedical.WCF.Student> GetStudentBySocialSecurityNumberAsync(string value) {
            return base.Channel.GetStudentBySocialSecurityNumberAsync(value);
        }
        
        public AbsMedical.WCF.Student GetStudentByFilters(string firstname, string lastname, System.DateTime birthdate) {
            return base.Channel.GetStudentByFilters(firstname, lastname, birthdate);
        }
        
        public System.Threading.Tasks.Task<AbsMedical.WCF.Student> GetStudentByFiltersAsync(string firstname, string lastname, System.DateTime birthdate) {
            return base.Channel.GetStudentByFiltersAsync(firstname, lastname, birthdate);
        }
        
        public bool UpdateStudent(AbsMedical.WCF.Student student) {
            return base.Channel.UpdateStudent(student);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateStudentAsync(AbsMedical.WCF.Student student) {
            return base.Channel.UpdateStudentAsync(student);
        }
        
        public bool DeleteStudent(string studentGuid) {
            return base.Channel.DeleteStudent(studentGuid);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteStudentAsync(string studentGuid) {
            return base.Channel.DeleteStudentAsync(studentGuid);
        }
    }
}
