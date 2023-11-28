using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Posts
{
    public class ListByUser
    {
        public class Query : IRequest<Result<List<PostDto>>>
        {
            public string Username { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<PostDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
            {
                _mapper = mapper;
                _userAccessor = userAccessor;
                _context = context;

            }
            public async Task<Result<List<PostDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                string currentUser =  _userAccessor.GetUsername();

                var posts = await _context.Posts
                    .Include(p => p.AppUser)
                    .Where(p => p.AppUser.UserName == request.Username)
                    .ProjectTo<PostDto>(_mapper.ConfigurationProvider, new { currentUsername = currentUser })
                    .ToListAsync();

                return Result<List<PostDto>>.Success(posts);
            }
        }

    }  
}
