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

    public interface IDesignationService
    {
        Task<IEnumerable<DropdownItemDto<int>>> GetItemDtosAsync();
        Task<bool> AddDesignation(DesignationDto designationDto);
    }
    public class DesignationService : IDesignationService
    {
        private readonly IDesignationRepository _designationRepository;
        private readonly BORFinanceDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<Designation> _logger;
        private readonly ICurrentUserService _currentUserService;
        public DesignationService(IDesignationRepository designationRepository, BORFinanceDbContext context, IMapper mapper,ILogger<Designation> logger, ICurrentUserService currentUserService)
        {
            _designationRepository = designationRepository;
            _logger = logger;
            _mapper = mapper;
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<DropdownItemDto<int>>> GetItemDtosAsync()
        {
            var designation = await _designationRepository.GetAllAsync();
            return designation.Select(d => new DropdownItemDto<int>
            {
                Id = d.DesignationId,
                Name = d.DesignationName
            });

        }

        public async Task<bool> AddDesignation(DesignationDto designationDto)
        {

            var designation = _mapper.Map<Designation>(designationDto);
             await _designationRepository.AddAsync(designation);
            return await _context.SaveChangesAsync() > 0;

        }

    }
}
