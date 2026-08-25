using BORFinanceCommon.Authentication;
using System.Security.Claims;

namespace BORFinanceApi.JWT
{
    
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public long? UserId
        {
            get
            {
                var userId = _httpContextAccessor.HttpContext?
                    .User?
                    .FindFirstValue(ClaimTypes.NameIdentifier);

                return long.TryParse(userId, out var id)
                    ? id
                    : null;
            }
        }

        public bool IsAuthenticated =>
            _httpContextAccessor.HttpContext?
                .User?
                .Identity?
                .IsAuthenticated ?? false;
    }
}
