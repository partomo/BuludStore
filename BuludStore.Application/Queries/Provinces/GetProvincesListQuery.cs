using Bulud.Base;
using Bulud.Base.Queries;
using BuludStore.Domain.Entities;
using MediatR;

namespace BuludStore.Application.Queries.Provinces;

public class GetProvincesListQuery : RequestQuery, IRequest<ListResult<Province>>
{
    
}