using BuludStore.Domain.Entities;

namespace BuludStore.Identity.Application.DTOs
{
    public class UserDto
    {
        public required string Id { get; set; }
        public required string UserName { get; set; }
        public required string PhoneNumber { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string NationalCode { get; set; } = string.Empty;
        public int? CountyId { get; set; }
        public City? City { get; set; }
        public IList<string>? Roles { get; set; }
    }
}
