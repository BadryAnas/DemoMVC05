using AutoMapper;
using Company.Data;
using Company.Repository.Interfaces;
using Company.Service.Helper;
using Company.Service.Interfaces;
using Company.Service.Interfaces.Employee.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Add(EmployeeDto employeeDto)
        {
            //var mappedEmployee = new EmployeeDto
            //{
            //    Name = employeeDto.Name,
            //    Age = employeeDto.Age, 
            //    Address = employeeDto.Address, 
            //    Salary = employeeDto.Salary,
            //    Email = employeeDto.Email,
            //    PhoneNumber= employeeDto.PhoneNumber,
            //    HiringDate = employeeDto.HiringDate,
            //    DepartmentId = employeeDto.DepartmentId
            //};


            employeeDto.ImageUrl = DocumentSettings.UploadFile(employeeDto.Image, "Images");

            Employee employee = _mapper.Map<Employee>(employeeDto);

            _unitOfWork.employeeRepository.Add(employee);
            _unitOfWork.Complete();

        }

        public void Delete(EmployeeDto employeeDto)
        {

            Employee employee = _mapper.Map<Employee>(employeeDto);

            _unitOfWork.employeeRepository.Delete(employee);
            _unitOfWork.Complete();

        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            var employees = _unitOfWork.employeeRepository.GetAll();

            IEnumerable<EmployeeDto> employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return employeesDto;
        }

        public EmployeeDto GetById(int? id)
        {
            if (id == null)
                return null;

            var employee = _unitOfWork.employeeRepository.GetById(id.Value);
            if (employee == null)
                return null;

            EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(employee);


            return employeeDto;
        }

        public IEnumerable<EmployeeDto> GetByName(string name)
        {
            var employees = _unitOfWork.employeeRepository.GetByName(name);
            IEnumerable<EmployeeDto> employeeDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return employeeDto;
        }

        public void Update(EmployeeDto employeeDto)
        {
            Employee employee = _mapper.Map<Employee>(employeeDto);
            _unitOfWork.employeeRepository.Update(employee);
            _unitOfWork.Complete();
        }
    }
}
