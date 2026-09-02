using AutoMapper;
using BORFinanceCommon.Authentication;
using BORFinanceCommon.Exceptions;
using BORFinanceDomain.Loans;
using BORFinanceDTO;
using BORFinanceRepository.Interfaces;
using Microsoft.Extensions.Logging;
using SchoolDatabase.Context;

namespace BORFinanceBusiness
{

    public interface ILoanService
    {
        Task<IEnumerable<LoanDto>> GetAllAsync();

        Task<LoanDto?> GetByIdAsync(
            long loanId);

        Task<bool> CreateAsync(
            LoanDto dto);

        Task<bool> UpdateAsync(
            LoanDto dto);

        Task<bool> DeleteAsync(
            long loanId);
    }
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly BORFinanceDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly ILogger<LoanService> _logger;

        public LoanService(
            ILoanRepository loanRepository,
            BORFinanceDbContext context,
            ICurrentUserService currentUserService,
            IMapper mapper,
            ILogger<LoanService> logger)
        {
            _loanRepository = loanRepository;
            _context = context;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<LoanDto>> GetAllAsync()
        {
            var loans =
                await _loanRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<LoanDto>>(
                loans);
        }

        public async Task<LoanDto?> GetByIdAsync(
            long loanId)
        {
            var loan =
                await _loanRepository.GetByIdAsync(
                    loanId);

            if (loan == null)
                return null;

            return _mapper.Map<LoanDto>(loan);
        }

        public async Task<bool> CreateAsync(
            LoanDto dto)
        {
            // Membership validation
            if (!await _loanRepository.IsMembershipActiveAsync(
         dto.MembershipId))
            {
                throw new BusinessException(
                    "Membership not found or inactive.");
            }

            // Loan number uniqueness
            if (await _loanRepository
                .ExistsByLoanNumberAsync(
                    dto.LoanNumber))
            {
                _logger.LogWarning(
                    "Duplicate loan number: {LoanNumber}",
                    dto.LoanNumber);

                throw new BusinessException(
                    "Loan number already exists.");
            }

            // Validate requested amount
            if (dto.RequestedAmount <= 0)
            {
                throw new BusinessException(
                    "Requested amount must be greater than zero.");
            }

            // New loan should start as Pending
            var loan =
                _mapper.Map<Loan>(dto);

            loan.LoanId = 0;

            loan.Status = "Pending";

            loan.ApprovedAmount = 0;

            loan.ApplicationDate =
                dto.ApplicationDate == default
                    ? DateTime.UtcNow
                    : dto.ApplicationDate;

            loan.ApprovalDate = null;
            loan.DisbursementDate = null;
            loan.ApprovedBy = null;

            loan.CreatedAt = DateTime.UtcNow;
            loan.CreatedBy =
                _currentUserService.UserId;

            loan.UpdatedAt = null;
            loan.UpdatedBy = null;

            await _loanRepository.AddAsync(loan);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(
            LoanDto dto)
        {
            var loan =
                await _loanRepository.GetByIdAsync(
                    dto.LoanId);

            if (loan == null)
            {
                throw new BusinessException(
                    "Loan not found.");
            }

            // Membership validation
            if (!await _loanRepository.IsMembershipActiveAsync(
         dto.MembershipId))
            {
                throw new BusinessException(
                    "Membership not found or inactive.");
            }

            // Loan number uniqueness
            if (await _loanRepository
                .ExistsByLoanNumberAsync(
                    dto.LoanNumber,
                    dto.LoanId))
            {
                throw new BusinessException(
                    "Loan number already exists.");
            }

            // Don't allow changing critical data
            // after approval
            if (loan.Status != "Pending")
            {
                throw new BusinessException(
                    "Only pending loans can be modified.");
            }

            if (dto.RequestedAmount <= 0)
            {
                throw new BusinessException(
                    "Requested amount must be greater than zero.");
            }

            loan.MembershipId =
                dto.MembershipId;

            loan.LoanNumber =
                dto.LoanNumber;

            loan.LoanTypeId =
                dto.LoanTypeId;

            loan.RequestedAmount =
                dto.RequestedAmount;

            loan.InterestRate =
                dto.InterestRate;

            loan.TenureMonths =
                dto.TenureMonths;

            loan.ApplicationDate =
                dto.ApplicationDate;

            loan.Purpose =
                dto.Purpose;

            loan.Remarks =
                dto.Remarks;

            loan.UpdatedAt =
                DateTime.UtcNow;

            loan.UpdatedBy =
                _currentUserService.UserId;

            _loanRepository.Update(loan);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(
            long loanId)
        {
            var loan =
                await _loanRepository.GetByIdAsync(
                    loanId);

            if (loan == null)
            {
                throw new BusinessException(
                    "Loan not found.");
            }

            // Financial records should not be
            // physically deleted once created.
            if (await _loanRepository
                .HasInstallmentsAsync(loanId))
            {
                throw new BusinessException(
                    "Loan cannot be deleted because installments are linked to it.");
            }

            if (loan.Status != "Pending")
            {
                throw new BusinessException(
                    "Only pending loans can be deleted.");
            }

            _loanRepository.Delete(loan);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
