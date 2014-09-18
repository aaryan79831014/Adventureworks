using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using DAL.Entities.HumanResources;
using DAL.Entities.Person;

namespace DAL.Mappings.Humanresources
{
    public class EmployeeMap:SubclassMap<Employee>
    {
        public EmployeeMap()
        {
            Table("HumanResources.Employee");
            //Person in the Foreign constraint is the property with in this class, when not using subclass
            //Id(e => e.Id,"BusinessEntityId").GeneratedBy.Foreign("Person");
            KeyColumn("BusinessEntityID");
            Map(e => e.NationalIDNumber);
            Map(e => e.LoginId);
            Map(e => e.JobTitle);
            Map(e => e.BirthDate);
            Map(e => e.MaritalStatus);
            Map(e => e.Gender);
            Map(e => e.HireDate);
            
            HasMany(e => e.EmploymentDepartmentHistories).KeyColumn("BusinessEntityID").Inverse().Cascade.All();
        }
    }

    public class DepartmentMap : ClassMap<Department>
    {
        public DepartmentMap()
        {
            Id(d => d.Id, "DepartmentID");
            Map(d => d.Name);
            Map(d => d.GroupName);
            Table("HumanResources.Department");
        }
    }

    public class ShiftMap : ClassMap<Shift>
    {
        public ShiftMap()
        {
            Id(s => s.Id, "ShiftID");
            Map(s => s.ShiftName).Column("Name");
            Map(s => s.StartTime).CustomType("TimeAsTimeSpan");
            Map(s => s.EndTime).CustomType("TimeAsTimeSpan");
            Table("HumanResources.Shift");
        }
    }

    public class EmployeeDepartmentHistoryMap : ClassMap<EmployeeDepartmentHistory>
    {
        public EmployeeDepartmentHistoryMap()
        {
            CompositeId().KeyReference(ed => ed.Employee, "BusinessEntityID")
                        .KeyReference(ed => ed.Shift, "ShiftID")
                        .KeyReference(ed => ed.Department, "DepartmentID")
                        .KeyProperty(ed => ed.StartDate);

            Map(ed => ed.EndDate).Nullable();
            Table("HumanResources.EmployeeDepartmentHistory");
        }
    }

}
