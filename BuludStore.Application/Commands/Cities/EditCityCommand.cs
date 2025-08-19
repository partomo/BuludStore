using System.ComponentModel.DataAnnotations;
using BuludStore.Domain.Entities;
using MediatR;

namespace BuludStore.Application.Commands.Cities;

public class EditCityCommand : IRequest<City>
{
    [Required]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }
    public int ProvinceId { get; set; }
}