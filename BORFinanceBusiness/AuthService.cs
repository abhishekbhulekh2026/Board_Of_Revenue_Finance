using AutoMapper;
using BORFinanceCommon.Authentication;
using BORFinanceCommon.Exceptions;
using BORFinanceCommon.Models;
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

    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
    public class AuthService : IAuthService
    {
        private readonly BORFinanceDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUserRepository userRepository,IJwtTokenService jwtTokenService, BORFinanceDbContext context, ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
            _context = context;
            _logger = logger;
        }


        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                throw new BusinessException(
                    "Username and password are required.");
            }

            var user = await _userRepository
                .GetUserForLoginAsync(request.Username);

            var userRoles = await _userRepository.GetUserRolesByUserId(request.Username);


            // User not found
            if (user == null)
            {
                _logger.LogWarning(
                    "Login failed for username: {Username}. User not found.",
                    request.Username);

                throw new BusinessException(
                    "Invalid username or password.");
            }

            // Account deleted
            if (user.IsDeleted)
            {
                _logger.LogWarning(
                    "Login attempted for deleted user: {Username}.",
                    request.Username);

                throw new BusinessException(
                   "Login attempted for deleted user: " + user.Username + ".");
            }

            // Account inactive
            if (!user.IsActive)
            {
                _logger.LogWarning(
                    "Login attempted for inactive user: {Username}.",
                    request.Username);

                throw new BusinessException(
                    "Login attempted for inactive user: " + user.Username + ".");
            }

            // Account locked
            if (user.AccountLocked)
            {
                _logger.LogWarning(
                    "Login attempted for locked user: {Username}.",
                    request.Username);

                throw new BusinessException(
                    "Login attempted for locked user: " + user.Username + ".");
            }

            // Password verification
            bool passwordValid = BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.PasswordHash);

            if (!passwordValid)
            {
                user.FailedLoginAttempts++;

                if (user.FailedLoginAttempts >= 5)
                {
                    user.AccountLocked = true;

                    _logger.LogWarning(
                        "User account locked after 5 failed attempts: " + user.Username + "",
                        request.Username);
                }
                else
                {
                    _logger.LogWarning(
                        "Invalid password for username: " + user.Username + ". Failed attempts: " + user.FailedLoginAttempts + "",
                        request.Username,
                        user.FailedLoginAttempts);
                }

                _userRepository.Update(user);

                await _context.SaveChangesAsync();

                throw new BusinessException(
                    "Invalid password for username: " + user.Username + ". Failed attempts: " + user.FailedLoginAttempts + "");
            }

            // Successful login
            user.FailedLoginAttempts = 0;
            user.LastLoginDate = DateTime.UtcNow;

            _userRepository.Update(user);

            await _context.SaveChangesAsync();

            // Generate JWT
            var tokens = _jwtTokenService.GenerateToken(
                user.Id,
                user.Username,
                user.FullName,
                userRoles.RoleId,
                userRoles.Role.RoleCode,
                userRoles.Role.RoleName);

            _logger.LogInformation(
                "User logged in successfully: {Username}",
                user.Username);

            return new LoginResponse
            {
                Success = true,
                Message = "Login successful.",
                UserId = user.Id,
                Username = user.Username,
                FullName = user.FullName,
                RoleId = userRoles.RoleId,
                RoleCode = userRoles.Role.RoleCode,
                RoleName = userRoles.Role.RoleName,
                Tokens = tokens
            };
        }
    }
}
