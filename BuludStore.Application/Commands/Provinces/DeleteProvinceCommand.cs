using System.ComponentModel.DataAnnotations;
using BuludStore.Domain.Entities;
using MediatR;

namespace BuludStore.Application.Commands.Provinces;

public class DeleteProvinceCommand : IRequest<Province>
{
    [Required]
    public int Id { get; set; }
}