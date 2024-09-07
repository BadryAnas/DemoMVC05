using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Data;
using Company.Service.Interfaces.Department.Dto;

namespace Company.Service.Interfaces
{
    public interface IDepartmentService
    {
        DepartmentDto GetById(int? id);
        IEnumerable<DepartmentDto> GetAll();
        void Delete(DepartmentDto department);
        void Update(DepartmentDto department);
        void Add(DepartmentDto department);
    }
}
