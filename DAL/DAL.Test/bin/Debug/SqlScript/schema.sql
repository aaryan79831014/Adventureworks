
    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKEE625E94B98DC5CE]') AND parent_object_id = OBJECT_ID('HumanResources.EmployeeDepartmentHistory'))
alter table HumanResources.EmployeeDepartmentHistory  drop constraint FKEE625E94B98DC5CE


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKEE625E9472FD7408]') AND parent_object_id = OBJECT_ID('HumanResources.EmployeeDepartmentHistory'))
alter table HumanResources.EmployeeDepartmentHistory  drop constraint FKEE625E9472FD7408


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKEE625E949F052FD1]') AND parent_object_id = OBJECT_ID('HumanResources.EmployeeDepartmentHistory'))
alter table HumanResources.EmployeeDepartmentHistory  drop constraint FKEE625E949F052FD1


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKD050243FFDE58E5D]') AND parent_object_id = OBJECT_ID('Person.Person'))
alter table Person.Person  drop constraint FKD050243FFDE58E5D


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK738F6A51B0A6CCE2]') AND parent_object_id = OBJECT_ID('HumanResources.Employee'))
alter table HumanResources.Employee  drop constraint FK738F6A51B0A6CCE2


    if exists (select * from dbo.sysobjects where id = object_id(N'HumanResources.Department') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table HumanResources.Department

    if exists (select * from dbo.sysobjects where id = object_id(N'HumanResources.EmployeeDepartmentHistory') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table HumanResources.EmployeeDepartmentHistory

    if exists (select * from dbo.sysobjects where id = object_id(N'HumanResources.Shift') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table HumanResources.Shift

    if exists (select * from dbo.sysobjects where id = object_id(N'Person.BusinessEntity') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Person.BusinessEntity

    if exists (select * from dbo.sysobjects where id = object_id(N'Person.Person') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Person.Person

    if exists (select * from dbo.sysobjects where id = object_id(N'HumanResources.Employee') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table HumanResources.Employee

    create table HumanResources.Department (
        DepartmentID INT IDENTITY NOT NULL,
       Name NVARCHAR(255) null,
       GroupName NVARCHAR(255) null,
       primary key (DepartmentID)
    )

    create table HumanResources.EmployeeDepartmentHistory (
        BusinessEntityID INT not null,
       ShiftID INT not null,
       DepartmentID INT not null,
       StartDate DATETIME not null,
       EndDate DATETIME null,
       primary key (BusinessEntityID, ShiftID, DepartmentID, StartDate)
    )

    create table HumanResources.Shift (
        ShiftID INT IDENTITY NOT NULL,
       Name NVARCHAR(255) null,
       StartTime TIME null,
       EndTime TIME null,
       primary key (ShiftID)
    )

    create table Person.BusinessEntity (
        BusinessEntityID INT IDENTITY NOT NULL,
       primary key (BusinessEntityID)
    )

    create table Person.Person (
        BusinessEntityID INT not null,
       Title NVARCHAR(255) null,
       LastName NVARCHAR(255) null,
       FirstName NVARCHAR(255) null,
       PersonType NVARCHAR(255) null,
       EmailPromotion INT null,
       primary key (BusinessEntityID)
    )

    create table HumanResources.Employee (
        BusinessEntityID INT not null,
       NationalIDNumber NVARCHAR(255) null,
       LoginId NVARCHAR(255) null,
       JobTitle NVARCHAR(255) null,
       BirthDate DATETIME null,
       MaritalStatus NVARCHAR(255) null,
       Gender NVARCHAR(255) null,
       HireDate DATETIME null,
       primary key (BusinessEntityID)
    )

    alter table HumanResources.EmployeeDepartmentHistory 
        add constraint FKEE625E94B98DC5CE 
        foreign key (BusinessEntityID) 
        references HumanResources.Employee

    alter table HumanResources.EmployeeDepartmentHistory 
        add constraint FKEE625E9472FD7408 
        foreign key (ShiftID) 
        references HumanResources.Shift

    alter table HumanResources.EmployeeDepartmentHistory 
        add constraint FKEE625E949F052FD1 
        foreign key (DepartmentID) 
        references HumanResources.Department

    alter table Person.Person 
        add constraint FKD050243FFDE58E5D 
        foreign key (BusinessEntityID) 
        references Person.BusinessEntity

    alter table HumanResources.Employee 
        add constraint FK738F6A51B0A6CCE2 
        foreign key (BusinessEntityID) 
        references Person.Person
