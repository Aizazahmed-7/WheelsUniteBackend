using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles
{
    public class Search
    {
        public class Query : IRequest<Result<List<UserDto>>>
        {
            public string SearchTerm { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<UserDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<Result<List<UserDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                if(request.SearchTerm == null) {
                    var Allusers = await _context.Users
                    .Include(p => p.Photos)
                    .ToListAsync();
                    var AlluserDto = _mapper.Map<List<UserDto>>(Allusers);
                    return Result<List<UserDto>>.Success(AlluserDto);
                }

                var users = await _context.Users
                    .Include(p => p.Photos)
                    .Where(u => u.UserName.Contains(request.SearchTerm))
                    .ToListAsync();

                var userDto = _mapper.Map<List<UserDto>>(users);

                return Result<List<UserDto>>.Success(userDto);
            }
        }

    }
}