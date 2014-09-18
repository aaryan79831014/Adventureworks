using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using DAL;
using System.Diagnostics;
using System.Xml.Serialization;
using System.IO;
using NHibernate.Linq;
using System.Linq;

namespace DAL.Test
{
    [TestClass]
    public class CRUDTest
    {
        private ISessionFactory sessionFactory;

        [TestMethod]
        public void CanGetEmployees()
        {
            using(var session= sessionFactory.OpenSession())
            {
                var employee = session.CreateCriteria<DAL.Entities.HumanResources.Employee>().List<DAL.Entities.HumanResources.Employee>().First();
                Assert.IsTrue(employee.Id > 0);
            }
        }

        [TestMethod]
        public void CanGetPerson()
        {
            using(var session = sessionFactory.OpenSession())
            {
                var person = session.CreateCriteria<Entities.Person.Person>().List<Entities.Person.Person>().First();
                Assert.IsTrue(person.Id > 0);
            }
        }

        [TestMethod]
        public void CanAddPerson()
        {
            using(var session = sessionFactory.OpenSession())
            {
                var person = new DAL.Entities.Person.Person()
                {
                    Title = "Mr",
                    FirstName = "TestFirstName04",
                    LastName = "TestLastName04",
                    PersonType = "EM"
                };

                using (var transaction = session.BeginTransaction())
                {
                    
                    try
                    {
                        session.SaveOrUpdate(person );
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {

                        transaction.Rollback();
                        throw ex;
                    }
                    
                }
            }
        }

        [TestMethod]
        public void CanUpdatePerson()
        {
            using (var session = sessionFactory.OpenSession())
            {
                var listOfPerson = session.CreateCriteria(typeof(Entities.Person.Person)).List<Entities.Person.Person>();
                var person = listOfPerson.First();
                person.EmailPromotion = person.EmailPromotion == 1 ? 0 : 1;

                using (var transaction = session.BeginTransaction())
                {

                    try
                    {
                        session.SaveOrUpdate(person);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }

                }
            }
        }

        [TestMethod]
        public void CanDeletePerson()
        { 
            //Here we add and then delete the person, so to avoid any referencial integrity errors are being thrown
 
            using (var session = sessionFactory.OpenSession())
            {
                int personId = 0;

                var person = new DAL.Entities.Person.Person()
                {
                    Title = "Mr",
                    FirstName = "TestFirstName05",
                    LastName = "TestLastName05",
                    PersonType = "EM"
                };

                using (var transaction = session.BeginTransaction())
                {

                    try
                    {
                        session.SaveOrUpdate(person);
                        transaction.Commit();
                        personId = person.Id;
                        Assert.IsTrue(personId > 0);
                    }
                    catch (Exception ex)
                    {

                        transaction.Rollback();
                        throw ex;
                    }

                }

                var personToDelete = session.CreateCriteria<Entities.Person.Person>().List<Entities.Person.Person>().Where(p => p.Id == personId).First();
                
                using (var transaction = session.BeginTransaction())
                {

                    try
                    {
                        session.Delete(personToDelete);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {

                        transaction.Rollback();
                        throw ex;
                    }

                }

            }
        
        }


        [TestMethod]
        public void CanAddEmployee()
        {
            using (var session = sessionFactory.OpenSession())
            {
                var rnd = new Random();

                var employee = new DAL.Entities.HumanResources.Employee()
                {
                    JobTitle = "Sales Representative",
                    MaritalStatus = "M",
                    Gender = "F",
                    NationalIDNumber= rnd.Next().ToString(),
                    HireDate = DateTime.Now.AddYears(-2),
                    BirthDate = DateTime.Now.AddYears(-30),
                    LoginId = "adventureworks\\" + rnd.Next().ToString(),
                    //Person properties
                    Title = "Mr",
                    FirstName = "TestFirstName04",
                    LastName = "TestLastName04",
                    PersonType = "EM"
                };
                
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(employee);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {

                        transaction.Rollback();
                        throw ex;
                    }

                }
            }
        }

        [TestMethod]
        public void CanUpdateEmployee()
        {
            using (var session = sessionFactory.OpenSession())
            {
                var rnd = new Random();

                var employee = session.CreateCriteria<Entities.HumanResources.Employee>().List<Entities.HumanResources.Employee>().First();

                employee.MaritalStatus = employee.MaritalStatus.ToUpperInvariant() == "M" ? "S" : "M";

                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.SaveOrUpdate(employee);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {

                        transaction.Rollback();
                        throw ex;
                    }

                }
            }
        }

        [TestMethod]
        //[ExpectedException(typeof(NHibernate.TransactionException))] //Cannot delete employee, you can only set not surrent
        public void CanDeleteEmployee()
        {
            //here we add a new employee and delete the employee to stop the referencial integrity kicking in
            using (var session = sessionFactory.OpenSession())
            {
                var rnd = new Random();
                int employeeId= 0;

                var employee = new DAL.Entities.HumanResources.Employee()
                {
                    JobTitle = "Sales Representative",
                    MaritalStatus = "M",
                    Gender = "F",
                    NationalIDNumber = rnd.Next().ToString(),
                    HireDate = DateTime.Now.AddYears(-2),
                    BirthDate = DateTime.Now.AddYears(-30),
                    LoginId = "adventureworks\\" + rnd.Next().ToString(),
                    //Person properties
                    Title = "Mr",
                    FirstName = "EmployeeFirstName01",
                    LastName = "employeeLastName01",
                    PersonType = "EM"
                };

                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(employee);
                        transaction.Commit();
                        employeeId = employee.Id;
                        Assert.IsTrue(employeeId > 0);
                    }
                    catch (Exception ex)
                    {

                        transaction.Rollback();
                        throw ex;
                    }

                }

                //Now delete the created employee
                var employeeToDelete = session.CreateCriteria<Entities.HumanResources.Employee>().List<Entities.HumanResources.Employee>().Where(e => e.Id== employeeId).First();

                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(employeeToDelete);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {

                        Assert.IsTrue(ex.InnerException.Message.Contains("The transaction ended in the trigger. The batch has been aborted"));
                    }

                }

            }
        }

        [TestMethod]
        public void CanGetEmployeeDepartmentHistory()
        {
            using(var session = sessionFactory.OpenSession())
            {
                var employeeHistory = session.CreateCriteria<Entities.HumanResources.EmployeeDepartmentHistory>().List<Entities.HumanResources.EmployeeDepartmentHistory>().First();
                Assert.IsNotNull(employeeHistory.Employee);

            }

        }

        [TestMethod]
        public void CanAddEmployeeDepartmentHistory()
        {
            //for testing we add a new employee and then assign the top most department and top most shift to that new employee
            var rnd = new Random();

            var employee = new DAL.Entities.HumanResources.Employee
            {
                JobTitle = "Sales Representative",
                MaritalStatus = "M",
                Gender = "F",
                NationalIDNumber = rnd.Next().ToString(),
                HireDate = DateTime.Now.AddYears(-2),
                BirthDate = DateTime.Now.AddYears(-30),
                LoginId = "adventureworks\\" + rnd.Next().ToString(),
                //Person properties
                Title = "Mr",
                FirstName = "TestFirstName05",
                LastName = "TestLastName05",
                PersonType = "EM"
            };

            var session = sessionFactory.OpenSession();

            employee.AddEmployeeDepartmentHistory(new Entities.HumanResources.EmployeeDepartmentHistory
            {
                Department = session.CreateCriteria<Entities.HumanResources.Department>().List<Entities.HumanResources.Department>()[0],
                Shift = session.CreateCriteria<Entities.HumanResources.Shift>().List<Entities.HumanResources.Shift>()[0],
                StartDate = DateTime.Now
            });

            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    session.SaveOrUpdate(employee);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }

        }

        [TestInitialize]
        public void TestInitialize()
        {
            sessionFactory = CreateSessionFactory();
        }


        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
               .Database(MsSqlConfiguration.MsSql2008
                   .ConnectionString(c => c
                    .Server("(local)")
                    .Database("AdventureWorks2008R2")
                    .Username("sa")
                    .Password("*****")) //Update password
                    .ShowSql()
                )
               .Mappings(m =>
                 m.FluentMappings.AddFromAssemblyOf<DAL.Entities.HumanResources.Employee>()
                 //Create the hbm files to a folder called HBM
                 .ExportTo(Path.Combine(System.Environment.CurrentDirectory, "HBM")))
                 //Create the sql schema file (sql to create tables) to a file called schema.sql
                 .ExposeConfiguration(config => new SchemaExport(config).SetOutputFile(Path.Combine(System.Environment.CurrentDirectory, "SqlScript", "schema.sql")).Execute(true, false, false))
               .BuildSessionFactory();
        }

        private string Serialize<T>(T dataToSerialize)
        {
            var serializer = new XmlSerializer(typeof(T));
            var writer = new StringWriter();
            serializer.Serialize(writer, dataToSerialize);
            writer.Close();
            return writer.ToString();           
        }
    }
}
