using Company.Data;
using Company.Service.Interfaces.Employee.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Interfaces
{
    public interface IEmployeeService
    {
        EmployeeDto GetById(int? id);
        IEnumerable<EmployeeDto> GetAll();
        void Delete(EmployeeDto employee);
        void Update(EmployeeDto employee);
        void Add(EmployeeDto employee);
        IEnumerable<EmployeeDto> GetByName(string name);

    }
}
