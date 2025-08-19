using AutoMapper;
using Bulud.Base.Infrastructure;
using BuludStore.Application.Commands.Cities;
using BuludStore.Domain.Entities;
using MediatR;

namespace BuludStore.Application.Handlers.Cities;

public class AddCityCommandHandler(IRepository<City> repository, IMapper mapper) : IRequestHandler<AddCityCommand, City>
{
    public async Task<City> Handle(AddCityCommand request, CancellationToken cancellationToken)
    {
        var city = mapper.Map<City>(request);
        await repository.AddAsync(city);
        return city;
    }
}