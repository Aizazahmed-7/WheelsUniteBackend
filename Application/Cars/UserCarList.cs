using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Cars
{
    public class UserCarList
    {
        public class Query : IRequest<Result<List<CarDTO>>> { }

        public class Handler : IRequestHandler<Query, Result<List<CarDTO>>>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;
            private readonly IUserAccessor userAccessor;

            public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
            {
                this.context = context;
                this.mapper = mapper;
                this.userAccessor = userAccessor;
            }

            public async Task<Result<List<CarDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {

                var user = await context.Users.Include(u => u.Cars).FirstOrDefaultAsync(x => x.UserName == userAccessor.GetUsername());

                if (user == null) return null;


                var userCars = await context.Cars
                    .Where(x => x.AppUserId == user.Id)
                    .ProjectTo<CarDTO>(mapper.ConfigurationProvider)
                    .ToListAsync();

                return Result<List<CarDTO>>.Success(userCars);
            }
        }
    }
}