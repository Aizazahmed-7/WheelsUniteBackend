using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Events
{
    public class ListByUser
    {
        public class Query : IRequest<Result<List<EventDto>>>
        {
            public string Username { get; set; }
        }

        
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
                .Where(x => x.Attendees.Any(a => a.AppUser.UserName == request.Username && a.IsHost ))
                .ProjectTo<EventDto>(this.mapper.ConfigurationProvider)
                .ToListAsync();


                return Result<List<EventDto>>.Success(events);
                
            }
        }

    }
}