using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DAL.Entities.HumanResources
{
    [Serializable]
    public class Employee:Person.Person
    {
        //Following is a way where Id will be set from the person, refer mapping for the class employee (when not using the subclass)
        //public virtual int Id { get; set; }
        public virtual string NationalIDNumber { get; set; }
        public virtual string LoginId { get; set; }
        public virtual string JobTitle { get; set; }
        public virtual DateTime BirthDate { get; set; }
        public virtual string MaritalStatus { get; set; }
        public virtual string Gender { get; set; }
        public virtual DateTime HireDate { get; set; }

        public virtual IList<EmployeeDepartmentHistory> EmploymentDepartmentHistories { get; set; }

        public Employee()
        {
            EmploymentDepartmentHistories = new List<EmployeeDepartmentHistory>();
        }

        public virtual void AddEmployeeDepartmentHistory(EmployeeDepartmentHistory employeeDepartmentHistory)
        {
            employeeDepartmentHistory.Employee = this;
            this.EmploymentDepartmentHistories.Add(employeeDepartmentHistory);
        }
    }

    [Serializable]
    public class Department
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string GroupName { get; set; }
    }

    [Serializable]
    public class Shift
    {
        public virtual int Id { get; set; }
        public virtual string ShiftName { get; set; }
        [XmlIgnore]
        public virtual TimeSpan StartTime { get; set; }
        [XmlIgnore]
        public virtual TimeSpan EndTime { get; set; }
    }

    
    [Serializable]
    public class EmployeeDepartmentHistory
    {
        public virtual Shift Shift { get; set; }
        public virtual Department Department { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
        public virtual Employee Employee { get; set; }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

    }
}
