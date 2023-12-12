using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Posts
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext context , IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;

            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                Guid guid;
                try {
                     guid = Guid.Parse(request.Id);
                } catch (Exception) {
                    return Result<Unit>.Failure("Invalid Id");
                }

                var post = await _context.Posts.FindAsync(guid);

                if (post == null) return null;

                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());
                if (user == null) return Result<Unit>.Unauthorized();
                
                if (post.AppUserId != user.Id) return Result<Unit>.Unauthorized();

                _context.Remove(post);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to delete the post");

                return Result<Unit>.Success(Unit.Value);
            }
        }

    }
}