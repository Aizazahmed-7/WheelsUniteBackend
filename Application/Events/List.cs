
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Events
{
    public class List
    {
        public class Query : IRequest<Result<List<EventDto>>> { }

        public class Handler : IRequestHandler<Query, Result<List<EventDto>>>
        {
            private readonly DataContext context;

            public readonly IMapper mapper ;

            public Handler(DataContext context , IMapper mapper )
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Result<List<EventDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var events = await this.context.Events
                .ProjectTo<EventDto>(this.mapper.ConfigurationProvider)
                .ToListAsync();


                return Result<List<EventDto>>.Success(events);
                
            }
        }
    }
}