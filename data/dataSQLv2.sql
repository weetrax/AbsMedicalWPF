#------------------------------------------------------------
#        Script MySQL.
#------------------------------------------------------------


#------------------------------------------------------------
# Table: Student
#------------------------------------------------------------

CREATE TABLE Student(
        Guid                 Varchar (100) NOT NULL ,
        Lastname             Varchar (50) NOT NULL ,
        Firstname            Varchar (50) NOT NULL ,
        Email                Varchar (255) ,
        StudentId            Varchar (50) NOT NULL ,
        SocialSecurityNumber Varchar (50) NOT NULL ,
        Address              Varchar (100) NOT NULL ,
        PostalCode           Varchar (5) NOT NULL ,
        City                 Varchar (50) NOT NULL ,
        CountryId                   Int NOT NULL ,
        SchoolGuid          Varchar (100) NOT NULL ,
        PRIMARY KEY (Guid )
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Doctor
#------------------------------------------------------------

CREATE TABLE Doctor(
        Guid       Varchar (100) NOT NULL ,
        Lastname   Varchar (50) NOT NULL ,
        Firstname  Varchar (50) NOT NULL ,
        Address    Varchar (100) NOT NULL ,
        PostalCode Varchar (7) NOT NULL ,
        City       Varchar (50) NOT NULL ,
        Email      Varchar (255) NOT NULL ,
        Password   Varchar (255) NOT NULL ,
        CountryId         Int NOT NULL ,
        PRIMARY KEY (Guid )
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Country
#------------------------------------------------------------

CREATE TABLE Country(
        Id   Int NOT NULL ,
        Name Varchar (50) NOT NULL ,
        PRIMARY KEY (Id )
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: School
#------------------------------------------------------------

CREATE TABLE School(
        Guid       Varchar (100) NOT NULL ,
        Name       Varchar (75) NOT NULL ,
        Address    Varchar (100) NOT NULL ,
        PostalCode Varchar (7) NOT NULL ,
        City       Varchar (50) NOT NULL ,
        Email      Varchar (255) ,
        Tel        Varchar (25) ,
        TypeId     Int NOT NULL ,
        CountryId         Int NOT NULL ,
        PRIMARY KEY (Guid )
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Visit
#------------------------------------------------------------

CREATE TABLE Visit(
        VisitDate Datetime NOT NULL ,
        PRIMARY KEY (VisitDate )
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Mail
#------------------------------------------------------------

CREATE TABLE Mail(
        Email    Varchar (255) NOT NULL ,
        Password Varchar (255) NOT NULL ,
        Smtp     Varchar (150) NOT NULL ,
        Port     Int NOT NULL ,
        Provider Varchar (100) NOT NULL ,
        DoctorGuid     Varchar (100) NOT NULL ,
        PRIMARY KEY (Email )
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: AbsMedical
#------------------------------------------------------------

CREATE TABLE AbsMedical(
        StartDate    Datetime ,
        EndDate      Datetime ,
        Note         Varchar (255) NOT NULL ,
        Guid         Varchar (100) NOT NULL ,
        VisitDate    Datetime NOT NULL ,
        StudentGuid Varchar (100) NOT NULL ,
        PRIMARY KEY (Guid ,VisitDate ,StudentGuid )
)ENGINE=InnoDB;

ALTER TABLE Student ADD CONSTRAINT FK_Student_Id FOREIGN KEY (CountryId) REFERENCES Country(Id);
ALTER TABLE Student ADD CONSTRAINT FK_Student_Guid_School FOREIGN KEY (SchoolGuid) REFERENCES School(Guid);
ALTER TABLE Doctor ADD CONSTRAINT FK_Doctor_Id FOREIGN KEY (CountryId) REFERENCES Country(Id);
ALTER TABLE School ADD CONSTRAINT FK_School_Id FOREIGN KEY (CountryId) REFERENCES Country(Id);
ALTER TABLE Mail ADD CONSTRAINT FK_Mail_Guid FOREIGN KEY (DoctorGuid) REFERENCES Doctor(Guid);
ALTER TABLE AbsMedical ADD CONSTRAINT FK_AbsMedical_Guid FOREIGN KEY (Guid) REFERENCES Doctor(Guid);
ALTER TABLE AbsMedical ADD CONSTRAINT FK_AbsMedical_VisitDate FOREIGN KEY (VisitDate) REFERENCES Visit(VisitDate);
ALTER TABLE AbsMedical ADD CONSTRAINT FK_AbsMedical_Guid_Student FOREIGN KEY (StudentGuid) REFERENCES Student(Guid);
