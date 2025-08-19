using System.ComponentModel.DataAnnotations;
using BuludStore.Domain.Entities;
using MediatR;

namespace BuludStore.Application.Queries.Provinces;

public class GetProvinceQuery : IRequest<Province>
{
    [Required]
    public int Id { get; set; }
}