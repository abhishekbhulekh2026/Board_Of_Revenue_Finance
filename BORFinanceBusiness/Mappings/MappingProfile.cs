using AutoMapper;
using BORFinanceDomain.Entities.Employees;
using BORFinanceDomain.Entities.Security;
using BORFinanceDomain.Loans;
using BORFinanceDomain.Members;
using BORFinanceDTO;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BORFinanceBusiness.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<User, UserDto>()
                .ForMember(
                    dest => dest.Password,
                    opt => opt.Ignore());

            CreateMap<UserDto, User>()
                .ForMember(
                    dest => dest.PasswordHash,
                    opt => opt.Ignore());

            // Role
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<UserRole, UserRoleDto>().ReverseMap();
            CreateMap<Permission, PermissionDto>().ReverseMap();
            CreateMap<RolePermission, RolePermissionDto>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Designation, DesignationDto>().ReverseMap();
            CreateMap<Membership, MembershipDto>().ReverseMap();
            CreateMap<Loan, LoanDto>().ReverseMap();
            CreateMap<LoanInstallment, LoanInstallmentDto>().ReverseMap();
            CreateMap<LoanType, LoanTypeDto>().ReverseMap();
            CreateMap<LoanGuarantor, LoanGuarantorDto>().ReverseMap();
        }
    }
}
