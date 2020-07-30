using EmployeeManagement.Controllers;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WebAPI.BusinessLayer;
using WebAPI.DataAccessLayer;
using WebAPI.DataAccessLayer.Models;

namespace WebAPI.EmployeeUnitTesting
{
    [TestFixture]
    public class EmployeeBALTestCases
    {
        #region MoqObjects
        private readonly Mock<IEmployeeDAL> mockEmployee;
        private readonly IEmployeeBAL employeeBAL;

        public EmployeeBALTestCases()
        {
            mockEmployee = new Mock<IEmployeeDAL>();
            employeeBAL = new EmployeeBAL(mockEmployee.Object);
        }
        #endregion

        #region MoqTestCases
        [Test(Description = "Get Employee Details by Employee ID")]
        public void Test_Get_Employee()
        {
            //Arrange
            List<EmployeeDetail> empData = new List<EmployeeDetail>
            {
                new EmployeeDetail()
                {
                    Name = "Sanjay Kumar",
                    EmployeeID = "M100",

                }
            };
            mockEmployee.Setup(x => x.GetEmployee(It.IsAny<string>())).Returns(empData.AsQueryable());

            //Act
            var data = employeeBAL.GetEmployee("M100");

            //Assert
            foreach (var items in data)
            {
                Assert.AreEqual("M100", items.EmployeeID);
            }
        }

        [Test(Description = "Delete Employee Details by Employee ID")]
        public void Test_Delete_Employee()
        {
            //Arrange
            mockEmployee.Setup(x => x.DeleteEmployee(It.IsAny<string>())).Returns("success");

            //Act
            var actualData = employeeBAL.DeleteEmployee("M100");

            //Assert
            Assert.AreEqual("success",actualData);
        }

        [Test(Description = "Add Employee Details")]
        public void Test_Add_Employee()
        {
            //Arrange
            EmployeeEntity empData = new EmployeeEntity()
            {
                Name = "Sanjay Kumar",
                EmployeeID = "M100",
                UserLocation = "Texas",
                EmailID = "Testing@abce.com"
            };

            mockEmployee.Setup(x => x.AddEmployee(It.IsAny<EmployeeDetail>())).Returns("success");

            //Act
            var data = employeeBAL.AddEmployee(empData);

            //Assert
            Assert.AreEqual("success",data);
            
        }

        [Test(Description = "Get All Employee Details")]
        public void Test_Get_AllEmployee()
        {
            //Arrange
            mockEmployee.Setup(x => x.GetAllEmployees()).Returns(GetTestEmployees().AsQueryable());

            //Act
            var data = employeeBAL.GetAllEmployees();

            //Assert
            Assert.AreEqual(2, data.Count());
        }

        #endregion

        #region TestEmployeeData
        private List<EmployeeDetail> GetTestEmployees()
        {
            return new List<EmployeeDetail>()
            {
                GetTestEmployeeOne(),
                GetTestEmployeeTwo(),
            };
        }

        private EmployeeDetail GetTestEmployeeOne()
        {
            return new EmployeeDetail
            {
                Name = "Sanjay Kumar",
                EmailId = "Sanjay@abc.com",
                EmployeeID = "m100",
                UserLocation = "bbsr"
            };
        }

        private EmployeeDetail GetTestEmployeeTwo()
        {
            return new EmployeeDetail
            {
                Name = "Priyanka",
                EmailId = "Test@abc.com",
                EmployeeID = "m101",
                UserLocation = "USA"
            };
        }
        #endregion
    }
}