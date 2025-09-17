using System.ComponentModel.DataAnnotations;
using BuludStore.Domain.Entities;
using MediatR;

namespace BuludStore.Application.Queries.Cities;

public class GetCityQuery : IRequest<City>
{
    [Required]
    public int Id { get; set; }
}