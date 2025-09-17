using AutoMapper;
using Bulud.Base.Infrastructure;
using BuludStore.Application.Commands.Cities;
using BuludStore.Domain.Entities;
using MediatR;

namespace BuludStore.Application.Handlers.Cities;

public class EditCityCommandHandler(IRepository<City> repository, IMapper mapper) : IRequestHandler<EditCityCommand, City>
{
    public async Task<City> Handle(EditCityCommand request, CancellationToken cancellationToken)
    {
        var city =await repository.Find(request.Id);
        
        mapper.Map(request, city);
        await repository.UpdateAsync(city);
        return city;
    }
}