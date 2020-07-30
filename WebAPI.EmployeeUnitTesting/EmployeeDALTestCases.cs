using Moq;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using WebAPI.DataAccessLayer;
using WebAPI.DataAccessLayer.Models;

namespace WebAPI.EmployeeUnitTesting
{
    [TestFixture]
    public class EmployeeDALTestCases
    {
       
        #region MoqTestCases
        [Test(Description = "Get Employee Details by Employee ID")]
        public void Test_GetEmployee_DAL()
        {
            //Arrange
            var empData = new List<EmployeeDetail>
            {
                new EmployeeDetail()
                {
                    Name = "Sanjay Kumar",
                    EmployeeID = "M100",

                }
            }.AsQueryable();
            var mockSet = new Mock<DbSet<EmployeeDetail>>();
            mockSet.As<IQueryable<EmployeeDetail>>().Setup(m => m.Provider).Returns(empData.Provider);
            mockSet.As<IQueryable<EmployeeDetail>>().Setup(m => m.Expression).Returns(empData.Expression);
            mockSet.As<IQueryable<EmployeeDetail>>().Setup(m => m.ElementType).Returns(empData.ElementType);
            mockSet.As<IQueryable<EmployeeDetail>>().Setup(m => m.GetEnumerator()).Returns(empData.GetEnumerator());

            var mockContext = new Mock<API201Entities>();
            mockContext.Setup(c => c.EmployeeDetails).Returns(mockSet.Object);

            var service = new EmployeeDAL(mockContext.Object);
            var emp = service.GetEmployee("M100");

            Assert.AreEqual(empData.Count(), emp.Count());


        }

        [Test(Description = "Add Employee Details")]
        public void Test_AddEmployee_DAL()
        {
            
            var empData = new EmployeeDetail
            {
                Name = "Pranay Kumar",
                EmployeeID = "M103",
                EmailId = "Testing@abc.com",
                UserLocation = "Texas"
                
            };
            var mockSet = new Mock<DbSet<EmployeeDetail>>();
            var mockContext = new Mock<API201Entities>();
            mockContext.Setup(m => m.EmployeeDetails).Returns(mockSet.Object);

            var service = new EmployeeDAL(mockContext.Object);
            var emp = service.AddEmployee(empData);

            mockSet.Verify(m => m.Add(It.IsAny<EmployeeDetail>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test(Description = "Get Employee Details")]
        public void Test_GetAllEmployees_DAL()
        {
            //Arrange
            var empData = new List<EmployeeDetail>
            {
                new EmployeeDetail()
                {
                    Name = "Sanjay Kumar",
                    EmployeeID = "M100",

                },
                new EmployeeDetail
                {
                     Name = "Priyanka",
                    EmployeeID = "M101",

                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<EmployeeDetail>>();
            mockSet.As<IQueryable<EmployeeDetail>>().Setup(m => m.Provider).Returns(empData.Provider);
            mockSet.As<IQueryable<EmployeeDetail>>().Setup(m => m.Expression).Returns(empData.Expression);
            mockSet.As<IQueryable<EmployeeDetail>>().Setup(m => m.ElementType).Returns(empData.ElementType);
            mockSet.As<IQueryable<EmployeeDetail>>().Setup(m => m.GetEnumerator()).Returns(empData.GetEnumerator());

            var mockContext = new Mock<API201Entities>();
            mockContext.Setup(m => m.EmployeeDetails).Returns(mockSet.Object);

            var service = new EmployeeDAL(mockContext.Object);
            var employees = service.GetAllEmployees();

            foreach(var items in employees)
            {
                Assert.AreEqual("M100", items.EmployeeID);
            }
        }

        [Test(Description = "Delete Employee Details by Employee ID")]
        public void Test_DeleteEmployee_DAL()
        {
            //Arrange
            var empData = new List<EmployeeDetail>
            {
                new EmployeeDetail()
                {
                    Name = "Sanjay Kumar",
                    EmployeeID = "M100",

                }
            }.AsQueryable();
            var mockSet = new Mock<DbSet<EmployeeDetail>>();
            mockSet.As<IQueryable<EmployeeDetail>>().Setup(m => m.Provider).Returns(empData.Provider);
            mockSet.As<IQueryable<EmployeeDetail>>().Setup(m => m.Expression).Returns(empData.Expression);
            mockSet.As<IQueryable<EmployeeDetail>>().Setup(m => m.ElementType).Returns(empData.ElementType);
            mockSet.As<IQueryable<EmployeeDetail>>().Setup(m => m.GetEnumerator()).Returns(empData.GetEnumerator());

            var mockContext = new Mock<API201Entities>();
            mockContext.Setup(c => c.EmployeeDetails).Returns(mockSet.Object);

            var service = new EmployeeDAL(mockContext.Object);
            var emp = service.DeleteEmployee("M100");

            Assert.AreEqual("success",emp);


        }

        #endregion
    }
}
