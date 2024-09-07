using Company.Data;
using Company.Data.Contexts;
using Company.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Repositories
{
    internal class EmployeeRepository :GenericRepository<Employee> , IEmployeeRepository
    {
        private readonly CompanyDbContext _context;

        public EmployeeRepository(CompanyDbContext context) : base(context) 
        {
            _context = context;

        }

        public IEnumerable<Employee> GetByName(string name)
            => _context.Employees.Where(x=> x.Name.Trim().ToLower() == name.Trim().ToLower()).ToList();
    }
}
