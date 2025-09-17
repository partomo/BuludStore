using AutoMapper;
using Bulud.Base.Infrastructure;
using BuludStore.Application.Commands.Cities;
using BuludStore.Domain.Entities;
using MediatR;

namespace BuludStore.Application.Handlers.Cities;

public class DeleteCityCommandHandler(IRepository<City> repository, IMapper mapper) : IRequestHandler<DeleteCityCommand, City>
{
    public async Task<City> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
    {
        var city = await repository.Find(request.Id);
        await repository.DeleteAsync(city);
        return city;
    }
}