using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CarForSale
{
    public class List
    {
        public class Query : IRequest<Result<List<CarForSaleDetailsDTO>>> { }

        public class Handler : IRequestHandler<Query, Result<List<CarForSaleDetailsDTO>>>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Result<List<CarForSaleDetailsDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var carsForSale = await this.context.CarsForSale
                .ProjectTo<CarForSaleDetailsDTO>(this.mapper.ConfigurationProvider)
                .ToListAsync();

                return Result<List<CarForSaleDetailsDTO>>.Success(carsForSale);
            }
        }
    }

}