using Entity;
using Entity.Identity;


namespace OptionsSetup.Authentication
{
    public interface IJwtProvider
    {
       public string GenerateToken(ApplicationUser user);
    }
}
