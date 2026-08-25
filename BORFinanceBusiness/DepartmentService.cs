using AutoMapper;
using Microsoft.Extensions.Logging;
using BORFinanceCommon.Authentication;
using BORFinanceDomain.Entities.Employees;
using BORFinanceDTO;
using BORFinanceRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolDatabase.Context;

namespace BORFinanceBusiness
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DropdownItemDto<int>>> GetItemDtosAsync();
        Task<bool> AddDepartmentAsync(DepartmentDto departmentDto);
    }
    public class DepartmentService : IDepartmentService
    {
        private readonly BORFinanceDbContext _context;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ILogger<DepartmentService> _logger;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        public DepartmentService(BORFinanceDbContext context, IDepartmentRepository departmentRepository,
        ILogger<DepartmentService> logger, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _departmentRepository = departmentRepository;
            _logger = logger;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<DropdownItemDto<int>>> GetItemDtosAsync()
        {
            var departments = await _departmentRepository.GetAllAsync();
            return departments.Select(d => new DropdownItemDto<int>
            {
                Id = d.DepartmentId,
                Name = d.DepartmentName
            });
        }

        public async Task<bool> AddDepartmentAsync(DepartmentDto departmentDto)
        {
            var department = _mapper.Map<Department>(departmentDto); 
            
            await _departmentRepository.AddAsync(department);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
