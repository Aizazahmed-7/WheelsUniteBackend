

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

                foreach (var chat in chats)
                {
                    var TimeAgo = DateTime.Parse(chat.CreatedAt);

                    // chat.CreatedAt = TimeAgoResolver(TimeAgo) ;
                    chat.CreatedAt = "Hello";
                }

                Console.WriteLine("Hello");
                
                return Result<List<ChatDTO>>.Success(chats);
            }
        }


        public static string TimeAgoResolver(DateTime dateTime){
            DateTime createdAt = dateTime;
            TimeSpan timeDifference = DateTime.Now - createdAt;

                if (timeDifference.TotalMinutes < 1)
                {
                    return "Just now";
                }
                else if (timeDifference.TotalHours < 1)
                {
                    return $"{(int)timeDifference.TotalMinutes} minute{((int)timeDifference.TotalMinutes != 1 ? "s" : "")} ago";
                }
                else if (timeDifference.TotalDays < 1)
                {
                    return $"{(int)timeDifference.TotalHours} hour{((int)timeDifference.TotalHours != 1 ? "s" : "")} ago";
                }
                else if (timeDifference.TotalDays < 30)
                {
                    return $"{(int)timeDifference.TotalDays} day{((int)timeDifference.TotalDays != 1 ? "s" : "")} ago";
                }
                else if (timeDifference.TotalDays < 365)
                {
                    int months = (int)(timeDifference.TotalDays / 30);
                    return $"{months} month{(months != 1 ? "s" : "")} ago";
                }
                else
                {
                    int years = (int)(timeDifference.TotalDays / 365);
                    return $"{years} year{(years != 1 ? "s" : "")} ago";
                }
        }
    }
}
