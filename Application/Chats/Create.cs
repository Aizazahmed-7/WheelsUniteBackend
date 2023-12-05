using Application.Core;
using Application.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Chats
{
    public class Create
    {
        public class Command : IRequest<Result<ChatDTO>>
        {
            public string Body { get; set; }
            public string RecipientUsername { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Body).NotEmpty();
                RuleFor(x => x.RecipientUsername).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command,Result<ChatDTO>>
        {

            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;
            private readonly DataContext _context;
            public Handler( DataContext dataContext, IUserAccessor userAccessor, IMapper mapper)
            {
                _mapper = mapper;
                _userAccessor = userAccessor;
                _context = dataContext;
            }

            public async Task<Result<ChatDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var sender = await _context.Users
                    .Include(p => p.Photos)
                    .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

                var recipient = await _context.Users
                    .Include(p => p.Photos)
                    .FirstOrDefaultAsync(x => x.UserName == request.RecipientUsername);

                if (recipient == null) return null;

                var chat = new Domain.Chat
                {
                    Body = request.Body,
                    Sender = sender,
                    Recipient = recipient,
                };

                _context.Chats.Add(chat);

                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    var chatDto = _mapper.Map<ChatDTO>(chat);
                    return Result<ChatDTO>.Success(chatDto);
                }

                return Result<ChatDTO>.Failure("Failed to send message");
            }
        }

    }

}