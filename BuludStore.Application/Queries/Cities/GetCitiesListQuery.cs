using Bulud.Base;
using Bulud.Base.Queries;
using BuludStore.Domain.Entities;
using MediatR;

namespace BuludStore.Application.Queries.Cities;

public class GetCitiesListQuery : RequestQuery, IRequest<ListResult<Province>>
{
    
}