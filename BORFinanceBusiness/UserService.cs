using AutoMapper;
using BORFinanceDomain.Entities.Security;
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
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();

        Task<UserDto?> GetByIdAsync(long id);

        Task<UserDto?> GetByUsernameAsync(string username);

        Task<bool> CreateAsync(UserDto userDTO);

        Task<bool> UpdateAsync(UserDto userDTO);

        Task<bool> DeleteAsync(long id);

        Task<bool> ActivateAsync(long id);

        Task<bool> DeactivateAsync(long id);

        Task<bool> LockAccountAsync(long id);

        Task<bool> UnlockAccountAsync(long id);

        Task<bool> ChangePasswordAsync(long id, string passwordHash);

        Task<bool> UsernameExistsAsync(string username);

        Task<bool> EmailExistsAsync(string email);
    }

    public class UserService : IUserService
    {
        private readonly BORFinanceDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        public UserService(BORFinanceDbContext context, IMapper mapper, IUserRepository userRepository, ILogger<UserService> logger)
        {
            _context = context;
            _mapper = mapper;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var user = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(user);
        }

        public async Task<UserDto?> GetByIdAsync(long id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDto?>(user);
        }

        public async Task<UserDto?> GetByUsernameAsync(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            return _mapper.Map<UserDto?>(user);
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _userRepository.UsernameExistsAsync(username);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _userRepository.EmailExistsAsync(email);
        }

        public async Task<bool> CreateAsync(UserDto userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);


            if (await _userRepository.UsernameExistsAsync(user.Username))
                throw new InvalidOperationException("Username already exists.");

            if (!string.IsNullOrWhiteSpace(user.Email) &&
                await _userRepository.EmailExistsAsync(user.Email))
                throw new InvalidOperationException("Email already exists.");

            user.CreatedDate = DateTime.UtcNow;
            user.IsActive = true;
            user.IsDeleted = false;
            user.AccountLocked = false;
            user.FailedLoginAttempts = 0;
            user.PasswordHash = user.PasswordHash;

            await _userRepository.AddAsync(user);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(UserDto userDTO)
        {
            var user = _mapper.Map<User>(userDTO);

            var existing = await _userRepository.GetByIdAsync(user.Id);

            if (existing == null)
                return false;

            existing.FullName = user.FullName;
            existing.MobileNumber = user.MobileNumber;
            existing.Email = user.Email;
            //existing.RoleId = user.RoleId;
            existing.ProfilePic = user.ProfilePic;
            existing.UpdatedBy = user.UpdatedBy;
            existing.UpdatedDate = DateTime.UtcNow;

            _userRepository.Update(existing);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return false;

            user.IsDeleted = true;
            user.IsActive = false;
            user.UpdatedDate = DateTime.UtcNow;

            _userRepository.Update(user);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ActivateAsync(long id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return false;

            user.IsActive = true;

            _userRepository.Update(user);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeactivateAsync(long id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return false;

            user.IsActive = false;

            _userRepository.Update(user);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> LockAccountAsync(long id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return false;

            user.AccountLocked = true;

            _userRepository.Update(user);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UnlockAccountAsync(long id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return false;

            user.AccountLocked = false;
            user.FailedLoginAttempts = 0;

            _userRepository.Update(user);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ChangePasswordAsync(long id, string passwordHash)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return false;

            user.PasswordHash = passwordHash;
            user.UpdatedDate = DateTime.UtcNow;

            _userRepository.Update(user);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
