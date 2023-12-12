using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Comments
{
    public class List
    {
        public class Query : IRequest<Result<List<CommentDto>>>
        {
            public Guid PostId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<CommentDto>>>
        {
            private readonly DataContext _Context;
            private readonly IMapper _Mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _Context = context;
                _Mapper = mapper;
            }
            public async Task<Result<List<CommentDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var Comments = await _Context.Comments
                    .Where(x => x.Post.Id == request.PostId)
                    .OrderBy(x => x.CreatedAt)
                    .ProjectTo<CommentDto>(_Mapper.ConfigurationProvider)
                    .ToListAsync();

                return Result<List<CommentDto>>.Success(Comments);
            }
        }

    }
}