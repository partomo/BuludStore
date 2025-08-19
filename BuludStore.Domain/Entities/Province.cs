using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Bulud.Base.Entities;

namespace BuludStore.Domain.Entities;

public class Province : BaseEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    [JsonIgnore]
    public List<City>? Counties { get; set; }
}