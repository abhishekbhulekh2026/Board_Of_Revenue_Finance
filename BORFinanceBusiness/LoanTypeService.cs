using AutoMapper;
using BORFinanceCommon.Authentication;
using BORFinanceDomain.Loans;
using BORFinanceDTO;
using BORFinanceRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolDatabase.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceBusiness
{
    public interface ILoanTypeService
    {
        Task<IEnumerable<LoanTypeDto>> GetAllAsync();

        Task<LoanTypeDto?> GetByIdAsync(int id);

        Task<bool> AddAsync(LoanTypeDto dto);

        Task<bool> UpdateAsync(LoanTypeDto dto);

        Task<bool> DeleteAsync(int id);
    }

    public class LoanTypeService : ILoanTypeService
    {
        private readonly ILoanTypeRepository _loanTypeRepository;
        private readonly BORFinanceDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly ILogger<LoanTypeService> _logger;

        public LoanTypeService(
            ILoanTypeRepository loanTypeRepository,
            BORFinanceDbContext context,
            ICurrentUserService currentUserService,
            IMapper mapper,
            ILogger<LoanTypeService> logger)
        {
            _loanTypeRepository = loanTypeRepository;
            _context = context;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<LoanTypeDto>> GetAllAsync()
        {
            var loanTypes = await _loanTypeRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<LoanTypeDto>>(loanTypes);
        }

        public async Task<LoanTypeDto?> GetByIdAsync(int id)
        {
            var loanType = await _loanTypeRepository.GetByIdAsync(id);

            if (loanType == null)
                return null;

            return _mapper.Map<LoanTypeDto>(loanType);
        }

        public async Task<bool> AddAsync(LoanTypeDto dto)
        {
            var existing = await _context.LoanTypes
                .AnyAsync(x =>
                    x.LoanTypeCode == dto.LoanTypeCode ||
                    x.LoanTypeName == dto.LoanTypeName);

            if (existing)
                throw new Exception("Loan type already exists.");

            var loanType = _mapper.Map<LoanType>(dto);

            loanType.CreatedAt = DateTime.UtcNow;
            loanType.CreatedBy = _currentUserService.UserId;
            loanType.IsActive = true;

            await _loanTypeRepository.AddAsync(loanType);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(LoanTypeDto dto)
        {
            var loanType =
                await _loanTypeRepository.GetByIdAsync(dto.LoanTypeId);

            if (loanType == null)
                return false;

            var duplicate = await _context.LoanTypes
                .AnyAsync(x =>
                    x.LoanTypeId != dto.LoanTypeId &&
                    (x.LoanTypeCode == dto.LoanTypeCode ||
                     x.LoanTypeName == dto.LoanTypeName));

            if (duplicate)
                throw new Exception("Another loan type with the same code or name already exists.");

            _mapper.Map(dto, loanType);

            //loanType.UpdatedAt = DateTime.UtcNow;
            //loanType.UpdatedBy = _currentUserService.UserId;

            _loanTypeRepository.Update(loanType);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var loanType =
                await _loanTypeRepository.GetByIdAsync(id);

            if (loanType == null)
                return false;

            // Recommended for master data:
            loanType.IsActive = false;

            //loanType.UpdatedAt = DateTime.UtcNow;
            //loanType.UpdatedBy = _currentUserService.UserId;

            _loanTypeRepository.Update(loanType);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
