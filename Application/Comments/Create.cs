using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Comments
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string Text { get; set; }
            public Guid PostId { get; set; }
            public Guid CommentId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Text).NotEmpty();
                RuleFor(x => x)
                .Must(x => x.PostId != default(Guid) ^ x.CommentId != default(Guid))
                .WithMessage("Either PostId or CommentId should be present, but not both.");
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;

            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {


                var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());
                if (user == null) return null;

                if (request.PostId == default(Guid))
                {
                    var parentComment = await _context.Comments.FindAsync(request.CommentId);
                    if (parentComment == null) return null;

                    var reply = new Reply
                    {
                        Author = user,
                        Text = request.Text
                    };

                    parentComment.Replies.Add(reply);
                }
                else
                {

                    var post = await _context.Posts.FindAsync(request.PostId);
                    if (post == null) return null;

                    var comment = new Comment
                    {
                        Author = user,
                        Post = post,
                        Text = request.Text
                    };

                    post.Comments.Add(comment);
                }

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Result<Unit>.Success(Unit.Value);

                return Result<Unit>.Failure("Failed to add comment");
            }
        }
    }
}