using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Chats
{
    public class ListCurrent
    {
        public class Query : IRequest<Result<List<ChatDTO>>>{}

        public class Handler : IRequestHandler<Query, Result<List<ChatDTO>>>
        {
            private readonly IUserAccessor userAccessor;
            private readonly DataContext context;
            private readonly IMapper mapper;

            public Handler(IUserAccessor userAccessor, DataContext context, IMapper mapper)
            {
                this.userAccessor = userAccessor;
                this.context = context;
                this.mapper = mapper;

            }

            public async Task<Result<List<ChatDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var currentUsername = userAccessor.GetUsername();
    
                var chats =  await context.Chats
                    .Where(x => x.Sender.UserName == currentUsername || x.Recipient.UserName == currentUsername)
                    .OrderByDescending(x => x.CreatedAt)
                    .ProjectTo<ChatDTO>(mapper.ConfigurationProvider)
                    .GroupBy(x => x.ConversationId)
                    .Select(x => x.OrderByDescending(y => y.CreatedAt).First())
                    .ToListAsync();
                
                 chats = chats.OrderByDescending(x => x.CreatedAt).ToList(); 

                return Result<List<ChatDTO>>.Success(chats);
            }
        }
        
    }
}