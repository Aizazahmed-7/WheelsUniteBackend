
using System.Text.Json;
using Application.Core;
using Application.Profiles;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Hosting.Builder;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Events
{
    public class Details
    {
        public class Query : IRequest<Result<EventDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<EventDto>>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public Handler(DataContext context , IMapper mapper)
            {
                this.context = context; 
                this.mapper = mapper;
            }

            public async Task<Result<EventDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var Event = await this.context.Events
                .ProjectTo<EventDto>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

                return Result<EventDto>.Success(Event);
            }
        }
    }   
}