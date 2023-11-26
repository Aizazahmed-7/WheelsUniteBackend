

using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Posts
{
    public class List
    {
        public class Query : IRequest<Result<List<PostDto>>>
        {
            public string Username { get; set; }
            public string Predicate { get; set; }
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

                var  profiles = await _context.Followings
                            .Where(x => x.Observer.UserName == currentUser)
                            .Select(u => u.Target)
                            .ProjectTo<Profiles.Profile>(_mapper.ConfigurationProvider, new { currentUsername = currentUser })
                            .ToListAsync();

                var profileUsernames = profiles.Select(p => p.Username).ToList();

                var posts = await _context.Posts
                    .Where(p => profileUsernames.Contains(p.AppUser.UserName))
                    .ProjectTo<PostDto>(_mapper.ConfigurationProvider, new { currentUsername = currentUser })
                    .ToListAsync();

                posts.AddRange(await _context.Posts
                    .Where(p => !profileUsernames.Contains(p.AppUser.UserName))
                    .ProjectTo<PostDto>(_mapper.ConfigurationProvider, new { currentUsername = currentUser })
                    .ToListAsync());

                return Result<List<PostDto>>.Success(posts);
            }
        }

    }
}