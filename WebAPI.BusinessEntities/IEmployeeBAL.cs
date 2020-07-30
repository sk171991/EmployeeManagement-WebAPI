using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.BusinessLayer
{
   public interface IEmployeeBAL
    {
        IQueryable<EmployeeEntity> GetEmployee(string empID);

        string AddEmployee(EmployeeEntity employeeDetail);

        string DeleteEmployee(string empID);

        IQueryable<EmployeeEntity> GetAllEmployees();
    }
}
