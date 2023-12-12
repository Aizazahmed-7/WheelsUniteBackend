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

                foreach (var chat in chats)
                {
                    var TimeAgo = DateTime.Parse(chat.CreatedAt);

                    chat.CreatedAt = TimeAgoResolver(TimeAgo) ;
                }

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