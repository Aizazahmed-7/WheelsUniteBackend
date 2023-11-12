using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application
{
    public class Likes
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _context = context;

            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                // var Likes = await _context.Likes.FirstOrDefaultAsync(x => x.Username == "bob");

                // Console.WriteLine("Likes: " + Likes.Id);

                // _context.Likes.RemoveRange(Likes);
                _context.SaveChanges();

                var post = await _context.Posts.Include(p => p.Likes).FirstOrDefaultAsync(p => p.Id == request.Id);

                if (post == null) return null;

                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

                if (user == null) return null;

                var like = post.Likes.FirstOrDefault(x => x.Username == user.UserName);

                if (like != null)
                {
                    post.Likes.Remove(like);
                }
                else
                {
                    var Newlike = new Like
                    {
                        Post = post,
                        Username = user.UserName,
                        Id = Guid.NewGuid().ToString()
                    };

                    post.Likes.Add(Newlike);
                }

                var result = await _context.SaveChangesAsync() > 0;

                return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Failed to like post");
            }
        }
    }
}