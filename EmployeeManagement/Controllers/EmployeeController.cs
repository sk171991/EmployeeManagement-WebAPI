using Microsoft.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.BusinessLayer;

namespace EmployeeManagement.Controllers
{ 
    
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeBAL bal_object;
        public EmployeeController(IEmployeeBAL _bal_Obj)
        {
            bal_object = _bal_Obj;
        }

        [HttpGet]
        [Route("api/GetEmployee")]
        [ResponseType(typeof(EmployeeEntity))]
        public IHttpActionResult GetEmployee(string empID)
        {
            IQueryable<EmployeeEntity> empData;
            empData = bal_object.GetEmployee(empID);
            if (empData != null)
            {
                return Ok(empData);
            }
            else if(empData.Count() == 0)
            {
                return NotFound();
            }
            return InternalServerError();
        }

        [HttpPost]
        [Route("api/AddEmployee")]
        [ResponseType(typeof(string))]
        public IHttpActionResult AddEmployee(EmployeeEntity employee)
        {
            
            string retVal = bal_object.AddEmployee(employee);
            if (retVal != null)
            {
                return Content(HttpStatusCode.Created, retVal);
            }
            //else
            //{
            //    return NotFound();
            //}
            return InternalServerError();
        }

        [HttpDelete]
        [Route("api/DeleteEmployee")]
        [ResponseType(typeof(string))]
        public IHttpActionResult DeleteEmployee(string empID)
        {
            string retVal = bal_object.DeleteEmployee(empID);
            if (retVal == "success" || retVal == "failed")
            {
                return Content(HttpStatusCode.NoContent, retVal);
            }
            else if(retVal == "NotFound")
            {
                return Content(HttpStatusCode.NotFound, "ID Not Found");
            }
            return InternalServerError();
        }

        [HttpGet]
        [Route("api/GetAllEmployeeData")]
        [ResponseType(typeof(EmployeeEntity))]
        public IHttpActionResult GetAllEmployeeData()
        {
            IQueryable<EmployeeEntity> employeeEntities;
            employeeEntities = bal_object.GetAllEmployees();
            if (employeeEntities != null)
            {
                return Content(HttpStatusCode.OK, employeeEntities);
            }
            
            return InternalServerError();
        }
    }
}
