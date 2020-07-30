using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DataAccessLayer;
using WebAPI.DataAccessLayer.Models;

namespace WebAPI.BusinessLayer
{
    public class EmployeeBAL : IEmployeeBAL
    {
        private readonly IEmployeeDAL DALObject;

        public EmployeeBAL(IEmployeeDAL employeeDAL)
        {
            DALObject = employeeDAL;
        }

        public IQueryable<EmployeeEntity> GetEmployee(string empID)
        {
            try
            {
                List<EmployeeEntity> employeeData = new List<EmployeeEntity>();
                IQueryable<EmployeeDetail> employeeDetail;
                employeeDetail = DALObject.GetEmployee(empID);
                foreach (var item in employeeDetail)
                {
                    EmployeeEntity empData = new EmployeeEntity
                    {
                        Name = item.Name,
                        EmployeeID = item.EmployeeID,
                        EmailID = item.EmailId,
                        UserLocation = item.UserLocation
                    };
                    employeeData.Add(empData);
                }
                return employeeData.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string AddEmployee(EmployeeEntity employeeDetail)
        {
            try
            {
                EmployeeDetail employee = new EmployeeDetail
                {
                    Name = employeeDetail.Name,
                    EmployeeID = employeeDetail.EmployeeID,
                    UserLocation = employeeDetail.UserLocation,
                    EmailId = employeeDetail.EmailID
                };
                string retVal = DALObject.AddEmployee(employee);
                
                return retVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DeleteEmployee(string empID)
        {
            try
            {
                string retVal = DALObject.DeleteEmployee(empID);

                return retVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<EmployeeEntity> GetAllEmployees()
        {
            try
            {
                List<EmployeeEntity> employeeList = new List<EmployeeEntity>();
                IQueryable<EmployeeDetail> employeeDetail;
                employeeDetail = DALObject.GetAllEmployees();

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<EmployeeDetail, EmployeeEntity>();
                });

                IMapper mapper = config.CreateMapper();
                if (employeeDetail.Count() > 0)
                {
                    foreach (var emp in employeeDetail)
                    {
                        var empData = mapper.Map<EmployeeDetail, EmployeeEntity>(emp);
                        employeeList.Add(empData);
                    }
                }
                return employeeList.AsQueryable();
                  
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
