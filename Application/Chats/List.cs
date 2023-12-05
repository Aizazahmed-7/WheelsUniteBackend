

using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Chats
{
    public class List
    {
        public class Query : IRequest<Result<List<ChatDTO>>>
        {
            public string RecipientUsername { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<List<ChatDTO>>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;
            public Handler(DataContext context , IUserAccessor userAccessor , IMapper mapper)
            {
                _userAccessor = userAccessor;
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<List<ChatDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var chats = await _context.Chats
                    .Where(x => x.Recipient.UserName == _userAccessor.GetUsername() 
                        && x.Sender.UserName == request.RecipientUsername
                        || x.Recipient.UserName == request.RecipientUsername
                        && x.Sender.UserName == _userAccessor.GetUsername())
                    .OrderBy(x => x.CreatedAt)
                    .ProjectTo<ChatDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                
                return Result<List<ChatDTO>>.Success(chats);
            }
        }
    }


}