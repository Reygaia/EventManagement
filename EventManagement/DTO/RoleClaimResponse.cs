using Entity.Identity;
using System.Security.Claims;

namespace EventManagement.DTO
{
    public class RoleClaimResponse
    {
        public List<SimpleClaim> Claims { get; set; } = new List<SimpleClaim>();
    }
}
