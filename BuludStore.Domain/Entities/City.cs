using System.ComponentModel.DataAnnotations;
using Bulud.Base.Entities;

namespace BuludStore.Domain.Entities;

public class City : BaseEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }
    public int ProvinceId { get; set; }

    public Province? Province { get; set; }
}