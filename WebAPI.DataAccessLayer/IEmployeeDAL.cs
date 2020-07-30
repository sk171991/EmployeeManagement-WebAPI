using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DataAccessLayer.Models;

namespace WebAPI.DataAccessLayer
{
    public interface IEmployeeDAL
    {
        IQueryable<EmployeeDetail> GetEmployee(string empID);

        string AddEmployee(EmployeeDetail employeeDetail);

        string DeleteEmployee(string empID);

        IQueryable<EmployeeDetail> GetAllEmployees();
    }
}
