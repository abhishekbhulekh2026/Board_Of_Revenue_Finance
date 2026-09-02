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
    public interface ILoanGuarantorService
    {
        Task<IEnumerable<LoanGuarantorDto>> GetAllAsync();

        Task<LoanGuarantorDto?> GetByIdAsync(long id);

        Task<bool> AddAsync(LoanGuarantorDto dto);

        Task<bool> UpdateAsync(LoanGuarantorDto dto);

        Task<bool> DeleteAsync(long id);
    }

    public class LoanGuarantorService : ILoanGuarantorService
    {
        private readonly ILoanGuarantorRepository _loanGuarantorRepository;
        private readonly BORFinanceDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly ILogger<LoanGuarantorService> _logger;

        public LoanGuarantorService(
            ILoanGuarantorRepository loanGuarantorRepository,
            BORFinanceDbContext context,
            ICurrentUserService currentUserService,
            IMapper mapper,
            ILogger<LoanGuarantorService> logger)
        {
            _loanGuarantorRepository = loanGuarantorRepository;
            _context = context;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<LoanGuarantorDto>> GetAllAsync()
        {
            var guarantors =
                await _loanGuarantorRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<LoanGuarantorDto>>(
                guarantors);
        }

        public async Task<LoanGuarantorDto?> GetByIdAsync(long id)
        {
            var guarantor =
                await _loanGuarantorRepository.GetByIdAsync(id);

            if (guarantor == null)
                return null;

            return _mapper.Map<LoanGuarantorDto>(guarantor);
        }

        public async Task<bool> AddAsync(LoanGuarantorDto dto)
        {
            // Validate Loan
            var loanExists = await _context.Loans
                .AnyAsync(x => x.LoanId == dto.LoanId);

            if (!loanExists)
                throw new Exception("Loan not found.");

            // Optional: validate Employee
            if (dto.EmployeeId.HasValue)
            {
                var employeeExists = await _context.Employees
                    .AnyAsync(x =>
                        x.EmployeeId == dto.EmployeeId.Value);

                if (!employeeExists)
                    throw new Exception("Employee not found.");
            }

            // Optional: validate Membership
            if (dto.MembershipId.HasValue)
            {
                var membershipExists = await _context.Memberships
                    .AnyAsync(x =>
                        x.MembershipId == dto.MembershipId.Value);

                if (!membershipExists)
                    throw new Exception("Membership not found.");
            }

            var entity = _mapper.Map<LoanGuarantor>(dto);

            entity.CreatedAt = DateTime.UtcNow;
            entity.CreatedBy = _currentUserService.UserId;

            entity.IsApproved = false;
            entity.ApprovedAt = null;
            entity.ApprovedBy = null;

            await _loanGuarantorRepository.AddAsync(entity);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(LoanGuarantorDto dto)
        {
            var entity =
                await _loanGuarantorRepository
                    .GetByIdAsync(dto.LoanGuarantorId);

            if (entity == null)
                return false;

            // Don't allow normal update to modify approval information
            _mapper.Map(dto, entity);

            //entity.UpdatedAt = DateTime.UtcNow;
            //entity.UpdatedBy = _currentUserService.UserId;

            _loanGuarantorRepository.Update(entity);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity =
                await _loanGuarantorRepository.GetByIdAsync(id);

            if (entity == null)
                return false;

            _loanGuarantorRepository.Delete(entity);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
