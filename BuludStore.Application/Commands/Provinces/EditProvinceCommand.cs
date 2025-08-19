using System.ComponentModel.DataAnnotations;
using BuludStore.Domain.Entities;
using MediatR;

namespace BuludStore.Application.Commands.Provinces;

public class EditProvinceCommand : IRequest<Province>
{
    [Required]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }
}