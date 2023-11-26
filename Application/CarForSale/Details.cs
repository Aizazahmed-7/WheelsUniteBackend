
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CarForSale
{
    public class Details
    {
        public class Query : IRequest<Result<CarForSaleDetailsDTO>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<CarForSaleDetailsDTO>>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public Handler(DataContext context , IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<Result<CarForSaleDetailsDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var carForSale = await this.context.CarsForSale
                .ProjectTo<CarForSaleDetailsDTO>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

                return Result<CarForSaleDetailsDTO>.Success(carForSale);
            }
        }
    }
}