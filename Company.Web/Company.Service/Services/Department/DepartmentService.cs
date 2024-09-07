using Company.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Data;
using Company.Repository.Interfaces;
using Company.Service.Interfaces.Department.Dto;
using AutoMapper;
using Company.Service.Interfaces.Employee.Dto;

namespace Company.Service.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public void Add(DepartmentDto departmentDto)
        {
            //var mappedDepartment = new DepartmentDto
            //{
            //    Code = department.Code,
            //    Name = department.Name,
            //    CreateAt = DateTime.Now,
            //};

            Department department = _mapper.Map<Department>(departmentDto);

            _unitOfWork.departmentRepository.Add(department);
            _unitOfWork.Complete();

        }

        public void Delete(DepartmentDto departmentDto)
        {
            Department department = _mapper.Map<Department>(departmentDto);

            _unitOfWork.departmentRepository.Delete(department);
            _unitOfWork.Complete();

        }

        public IEnumerable<DepartmentDto> GetAll()
        {
            var departments = _unitOfWork.departmentRepository.GetAll();

            IEnumerable<DepartmentDto> departmentDto = _mapper.Map<IEnumerable<DepartmentDto>>(departments);


            return departmentDto;
        }

        public DepartmentDto GetById(int? id)
        {
            if (id is null)
                return null;

            var department = _unitOfWork.departmentRepository.GetById(id.Value);

            if (department is null)
                return null;
            DepartmentDto departmentDto = _mapper.Map<DepartmentDto>(department);

            return departmentDto; 
        }

        public void Update(DepartmentDto departmentDto)
        {
            Department department = _mapper.Map<Department>(departmentDto);
            _unitOfWork.departmentRepository.Update(department);
            _unitOfWork.Complete();
        }
    }
}
