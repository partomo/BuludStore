using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BuludStore.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Bazta.Identity.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        [MaxLength(100)]
        public string? FirstName { get; set; }
        [MaxLength(100)]
        public string? LastName { get; set; }
        [MaxLength(10)]
        public string? NationalCode { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        [JsonIgnore]
        public DateTime? DeletedAt { get; set; }
        public List<AppUserRole>? UserRoles { get; set; }
    }
}
