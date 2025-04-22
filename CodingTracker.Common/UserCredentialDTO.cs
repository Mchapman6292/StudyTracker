using System.ComponentModel.DataAnnotations;

// Data transfer objects (DTOs) like UserCredentialDTO should be simple and not have dependencies injected into them. Their role is often to carry data with little or no behavior (methods). If you need to perform validation or other logic on the data in UserCredentialDTO, it's usually done in a service class that handles business logic, not directly in the DTO.

namespace CodingTracker.Common.UserCredentials.UserCredentialDTOs
{
    public class UserCredentialDTO
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
