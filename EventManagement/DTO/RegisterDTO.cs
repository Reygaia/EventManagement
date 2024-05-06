using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace EventManagement.DTO
{
    public class RegisterDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required,DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Required,DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "Password Does not match")]
        public string ConfirmPassword {  get; set; } = string.Empty;
    }
}
