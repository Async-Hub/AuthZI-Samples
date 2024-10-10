using Authzi.Security;
using Microsoft.AspNetCore.Authentication;

namespace Api.Orleans
{
    public class AspNetCoreAccessTokenProvider : IAccessTokenProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AspNetCoreAccessTokenProvider(IHttpContextAccessor httpContextAccessorResolver)
        {
            // ReSharper disable once JoinNullCheckWithUsage
            if (httpContextAccessorResolver == null)
            {
                throw new ArgumentNullException(nameof(httpContextAccessorResolver), 
                    "The value for IHttpContextAccessor can not be null.");
            }

            _httpContextAccessor = httpContextAccessorResolver;
        }

        public async Task<string> RetrieveTokenAsync()
        {
            //var httpContextAccessor = _httpContextAccessorResolver.Invoke();

            // The first approach
            var token1 = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            
            // The second approach
            var token2 = _httpContextAccessor.HttpContext.Request
                .Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

            return token1 ?? token2;
        }
    }
}
