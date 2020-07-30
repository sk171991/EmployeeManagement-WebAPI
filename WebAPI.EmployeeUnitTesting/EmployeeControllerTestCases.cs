using EmployeeManagement.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using WebAPI.BusinessLayer;
using WebAPI.DataAccessLayer;
using WebAPI.DataAccessLayer.Models;

namespace WebAPI.EmployeeUnitTesting
{
    [TestFixture]
    public class EmployeeControllerTestCases
    {
        #region MoqObjects
        private readonly Mock<IEmployeeBAL> mockEmployee;
        private readonly EmployeeController employeeController;

        public EmployeeControllerTestCases()
        {
            mockEmployee = new Mock<IEmployeeBAL>();
            employeeController = new EmployeeController(mockEmployee.Object);
        }
        #endregion

        #region MoqTestCases
        [Test(Description = "Get Employee Details by Employee ID")]
        public void Test_GetEmployee_ByController()
        {
            //Arrange
            List<EmployeeEntity> empData = new List<EmployeeEntity>
            {
                new EmployeeEntity()
                {
                    Name = "Sanjay Kumar",
                    EmployeeID = "M100",

                }
            };
            mockEmployee.Setup(x => x.GetEmployee(It.IsAny<string>())).Returns(empData.AsQueryable());

            //Act
            var data = employeeController.GetEmployee("M100");
            if (data is OkNegotiatedContentResult<IQueryable<EmployeeEntity>>)
            {
                // Here's how you can do it. 
                var result = data as OkNegotiatedContentResult<IQueryable<EmployeeEntity>>;
                var content = result.Content;
                //Assert
                Assert.IsNotNull(content);
                foreach (var items in content)
                {
                    Assert.AreEqual("M100", items.EmployeeID);
                }
            }
           
          
        }

        [Test(Description = "Get All Employee Details")]
        public void Test_GetAllEmployee_ByController()
        {
            //Arrange
            mockEmployee.Setup(x => x.GetAllEmployees()).Returns(GetTestEmployees().AsQueryable());
            //Act
            var data = employeeController.GetAllEmployeeData();
            if (data is OkNegotiatedContentResult<IQueryable<EmployeeEntity>>)
            {
                // Here's how you can do it. 
                var result = data as OkNegotiatedContentResult<IQueryable<EmployeeEntity>>;
                var content = result.Content.Count();
                //Assert
                Assert.IsNotNull(content);
                Assert.AreEqual(2, content);
            }


        }

        [Test(Description = "Add Employee Details")]
        public void Test_AddEmployee_ByController()
        {
            //Arrange
            EmployeeEntity empData = new EmployeeEntity()
            {
                Name = "Sanjay Kumar",
                EmployeeID = "M102",
                UserLocation = "Texas",
                EmailID = "Testing@abce.com"
            };

            mockEmployee.Setup(x => x.AddEmployee(It.IsAny<EmployeeEntity>())).Returns("success");

            //Act
            var data = employeeController.AddEmployee(empData);

            if (data is NegotiatedContentResult<string> )
            {
                var content = data as NegotiatedContentResult<string>;
                //Assert
                Assert.IsNotNull(content);
                Assert.AreEqual("success", content.Content);
            }

        }

        [Test(Description = "Delete Employee Details")]
        public void Test_DelEmployee_ByController()
        {
            //Arrange
            mockEmployee.Setup(x => x.DeleteEmployee(It.IsAny<string>())).Returns("success");

            //Act
            var actualData = employeeController.DeleteEmployee("M100");

            //Assert
            if (actualData is NegotiatedContentResult<string>)
            {
                var content = actualData as NegotiatedContentResult<string>;
                //Assert
                Assert.IsNotNull(content);
                Assert.AreEqual("success", content.Content);
            }
        }
        #endregion


        #region TestEmployeeData
        private List<EmployeeEntity> GetTestEmployees()
        {
            return new List<EmployeeEntity>()
            {
                GetTestEmployeeOne(),
                GetTestEmployeeTwo(),
            };
        }

        private EmployeeEntity GetTestEmployeeOne()
        {
            return new EmployeeEntity
            {
                Name = "Sanjay Kumar",
                EmailID = "Sanjay@abc.com",
                EmployeeID = "m100",
                UserLocation = "bbsr"
            };
        }

        private EmployeeEntity GetTestEmployeeTwo()
        {
            return new EmployeeEntity
            {
                Name = "Priyanka",
                EmailID = "Test@abc.com",
                EmployeeID = "m101",
                UserLocation = "USA"
            };
        }
        #endregion
    }
}
