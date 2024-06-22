using Entity.Identity;
using System.Security.Claims;


namespace OptionsSetup.Authentication
{
    public interface IJwtProvider
    {
        public string GenerateToken(ApplicationUser user);
        public string UpdateToken(string existingToken, IEnumerable<Claim> newClaims);
    }
}
