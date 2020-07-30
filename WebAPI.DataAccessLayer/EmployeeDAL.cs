using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DataAccessLayer.Models;

namespace WebAPI.DataAccessLayer
{
    public class EmployeeDAL : IEmployeeDAL
    {
        readonly API201Entities entityObj;

        public EmployeeDAL(API201Entities entity)
        {
            entityObj = entity;
        }
        
        public IQueryable<EmployeeDetail> GetEmployee(string empID)
        { 
            var employeeDetails = from emp in entityObj.EmployeeDetails
                                  where emp.EmployeeID == empID
                                  select emp;
           return employeeDetails.AsQueryable();
        }

        public string AddEmployee(EmployeeDetail employeeDetail)
        {
            employeeDetail.CreatedTimeStamp = DateTime.Now;
            employeeDetail.LastModifiedTimeStamp = DateTime.Now;
            employeeDetail.MetaActive = 1;

            entityObj.EmployeeDetails.Add(employeeDetail);
            int retVal = entityObj.SaveChanges();
            if(retVal == 1)
            {
                return "success";
            }

            else
            {
                return "failed";
            }
        }

        public string DeleteEmployee(string empID)
        {
            var employeeDetails = from emp in entityObj.EmployeeDetails
                                  where emp.EmployeeID == empID
                                  select emp;
            if (employeeDetails.Count() != 0)
            {
                foreach (var items in employeeDetails)
                {
                    items.MetaActive = 0;
                    items.LastModifiedTimeStamp = DateTime.Now;
                }
                int retVal = entityObj.SaveChanges();
                if (retVal == 1)
                {
                    return "success";
                }

                else
                {
                    return "failed";
                }
            }
            else
            {
                return "NotFound";
            }
        }

        public IQueryable<EmployeeDetail> GetAllEmployees()
        {
            var employeeDetails = from emp in entityObj.EmployeeDetails
                                  where emp.MetaActive == 1
                                  select emp;
            return employeeDetails.AsQueryable();
        }
    }
}
