using System.ComponentModel.DataAnnotations;
using BuludStore.Domain.Entities;
using MediatR;

namespace BuludStore.Application.Commands.Provinces;

public class AddProvinceCommand : IRequest<Province>
{
    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }
}