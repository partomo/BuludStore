using BuludStore.Domain.Entities;
using MediatR;

namespace BuludStore.Application.Commands.Cities;

public class DeleteCityCommand  : IRequest<City>
{
    public int Id { get; set; }
}