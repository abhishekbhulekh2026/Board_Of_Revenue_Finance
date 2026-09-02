using AutoMapper;
using BORFinanceCommon.Authentication;
using BORFinanceCommon.Exceptions;
using BORFinanceDomain.Loans;
using BORFinanceDTO;
using BORFinanceRepository.Interfaces;
using Microsoft.Extensions.Logging;
using SchoolDatabase.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BORFinanceBusiness
{
    public interface ILoanInstallmentService
    {
        Task<IEnumerable<LoanInstallmentDto>> GetAllAsync();

        Task<LoanInstallmentDto?> GetByIdAsync(
            long installmentId);

        Task<bool> CreateAsync(
            LoanInstallmentDto dto);

        Task<bool> UpdateAsync(
            LoanInstallmentDto dto);

        Task<bool> DeleteAsync(
            long installmentId);

        Task<IEnumerable<LoanInstallmentDto>>
            GetLoanInstallmentsByLoanIdAsync(long loanId);
    }

    public class LoanInstallmentService : ILoanInstallmentService
    {
        private readonly ILoanInstallmentRepository _installmentRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly BORFinanceDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly ILogger<LoanInstallmentService> _logger;

        public LoanInstallmentService(
            ILoanInstallmentRepository installmentRepository,
            ILoanRepository loanRepository,
            BORFinanceDbContext context,
            ICurrentUserService currentUserService,
            IMapper mapper,
            ILogger<LoanInstallmentService> logger)
        {
            _installmentRepository = installmentRepository;
            _loanRepository = loanRepository;
            _context = context;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<LoanInstallmentDto>>
            GetAllAsync()
        {
            var installments =
                await _installmentRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<LoanInstallmentDto>>(
                installments);
        }

        public async Task<LoanInstallmentDto?>
            GetByIdAsync(long installmentId)
        {
            var installment =
                await _installmentRepository
                    .GetByIdAsync(installmentId);

            if (installment == null)
                return null;

            return _mapper.Map<LoanInstallmentDto>(
                installment);
        }

        public async Task<IEnumerable<LoanInstallmentDto>>
            GetLoanInstallmentsByLoanIdAsync(long loanId)
        {
            var installments =
                await _installmentRepository
                    .GetLoanInstallmentByLoanIdAsync(loanId);
            return _mapper.Map<IEnumerable<LoanInstallmentDto>>(
                installments);
        }


        public async Task<bool> CreateAsync(
            LoanInstallmentDto dto)
        {
            // Validate Loan
            var loan =
                await _loanRepository
                    .GetByIdAsync(dto.LoanId);

            if (loan == null)
            {
                throw new BusinessException(
                    "Loan not found.");
            }

            // Installment number must be unique
            // within the same loan.
            if (await _installmentRepository
                .ExistsByInstallmentNumberAsync(
                    dto.LoanId,
                    dto.InstallmentNumber))
            {
                throw new BusinessException(
                    "Installment number already exists for this loan.");
            }

            if (dto.PrincipalAmount < 0 ||
                dto.InterestAmount < 0 ||
                dto.InstallmentAmount < 0 ||
                dto.PaidAmount < 0)
            {
                throw new BusinessException(
                    "Installment amounts cannot be negative.");
            }

            if (dto.PaidAmount > dto.InstallmentAmount)
            {
                throw new BusinessException(
                    "Paid amount cannot be greater than installment amount.");
            }

            ValidatePaymentInformation(dto);

            var installment =
                _mapper.Map<LoanInstallment>(dto);

            installment.LoanInstallmentId = 0;

            if (installment.PaidAmount <= 0)
            {
                installment.Status = "Pending";
                installment.PaymentDate = null;
            }
            else if (installment.PaidAmount <
                     installment.InstallmentAmount)
            {
                installment.Status = "Partial";
            }
            else
            {
                installment.Status = "Paid";
            }

            installment.CreatedAt =
                DateTime.UtcNow;

            await _installmentRepository
                .AddAsync(installment);

            // SaveChanges is handled according to
            // your repository/service pattern.
            return await _context.SaveChangesAsync()>0;
        }

        public async Task<bool> UpdateAsync(
            LoanInstallmentDto dto)
        {
            var installment =
                await _installmentRepository
                    .GetByIdAsync(dto.LoanInstallmentId);

            if (installment == null)
            {
                throw new BusinessException(
                    "Loan installment not found.");
            }

            var loan =
                await _loanRepository
                    .GetByIdAsync(dto.LoanId);

            if (loan == null)
            {
                throw new BusinessException(
                    "Loan not found.");
            }

            if (await _installmentRepository
                .ExistsByInstallmentNumberAsync(
                    dto.LoanId,
                    dto.InstallmentNumber,
                    dto.LoanInstallmentId))
            {
                throw new BusinessException(
                    "Installment number already exists for this loan.");
            }

            if (dto.PrincipalAmount < 0 ||
                dto.InterestAmount < 0 ||
                dto.InstallmentAmount < 0 ||
                dto.PaidAmount < 0)
            {
                throw new BusinessException(
                    "Installment amounts cannot be negative.");
            }

            if (dto.PaidAmount > dto.InstallmentAmount)
            {
                throw new BusinessException(
                    "Paid amount cannot be greater than installment amount.");
            }

            ValidatePaymentInformation(dto);

            installment.LoanId =
                dto.LoanId;

            installment.InstallmentNumber =
                dto.InstallmentNumber;

            installment.DueDate =
                dto.DueDate;

            installment.PrincipalAmount =
                dto.PrincipalAmount;

            installment.InterestAmount =
                dto.InterestAmount;

            installment.InstallmentAmount =
                dto.InstallmentAmount;

            installment.PaidAmount =
                dto.PaidAmount;

            installment.PaymentDate =
                dto.PaymentDate;

            installment.PaymentMode =
                dto.PaymentMode;

            installment.ChequeNumber =
                dto.ChequeNumber;

            installment.ChequeDate =
                dto.ChequeDate;

            if (installment.PaidAmount <= 0)
            {
                installment.Status = "Pending";
            }
            else if (installment.PaidAmount <
                     installment.InstallmentAmount)
            {
                installment.Status = "Partial";
            }
            else
            {
                installment.Status = "Paid";
            }

            _installmentRepository.Update(
                installment);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(
            long installmentId)
        {
            var installment =
                await _installmentRepository
                    .GetByIdAsync(installmentId);

            if (installment == null)
            {
                throw new BusinessException(
                    "Loan installment not found.");
            }

            if (installment.PaidAmount > 0)
            {
                throw new BusinessException(
                    "Paid installment cannot be deleted.");
            }

            _installmentRepository.Delete(
                installment);

            return await _context.SaveChangesAsync() > 0;
        }

        private static void ValidatePaymentInformation(
            LoanInstallmentDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.PaymentMode))
            {
                // No payment information is fine
                // for a pending installment.
                if (dto.PaidAmount > 0)
                {
                    throw new BusinessException(
                        "Payment mode is required when payment has been made.");
                }

                return;
            }

            if (dto.PaymentMode.Equals(
                    "Cheque",
                    StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrWhiteSpace(
                    dto.ChequeNumber))
                {
                    throw new BusinessException(
                        "Cheque number is required for cheque payment.");
                }

                if (!dto.ChequeDate.HasValue)
                {
                    throw new BusinessException(
                        "Cheque date is required for cheque payment.");
                }
            }
            else if (!dto.PaymentMode.Equals(
                         "SalaryDeduction",
                         StringComparison.OrdinalIgnoreCase))
            {
                throw new BusinessException(
                    "Invalid payment mode.");
            }
        }

    }
}
