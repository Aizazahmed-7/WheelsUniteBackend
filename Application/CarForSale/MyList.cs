using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CarForSale
{
    public class MyList
    {
        public class Query : IRequest<Result<List<CarForSaleDetailsDTO>>> { }

        public class Handler : IRequestHandler<Query, Result<List<CarForSaleDetailsDTO>>>
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

            public async Task<Result<List<CarForSaleDetailsDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == userAccessor.GetUsername());
                if (user == null) return null;

                Console.WriteLine(user.UserName);

                var MyCarsOnSale = await this.context.CarsForSale
                    .Where(x => x.Car.AppUserId == user.Id)
                    .ProjectTo<CarForSaleDetailsDTO>(this.mapper.ConfigurationProvider)
                    .ToListAsync();

                return Result<List<CarForSaleDetailsDTO>>.Success(MyCarsOnSale);
            }
        }
    }

}