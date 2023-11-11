
using Application.Core;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Cars
{
    public class SetMain
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string Id { get; set; }
        }


        public class Handler : IRequestHandler<Command, Result<Unit>>
        {

            private readonly IUserAccessor _userAccessor;
            private readonly DataContext _context;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _context = context;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                Guid ID = Guid.Empty;
                try { ID = Guid.Parse(request.Id); } catch { return Result<Unit>.Failure("Invalid ID"); }
                var user = await _context.Users.Include(c => c.Cars).FirstOrDefaultAsync(u => u.UserName == _userAccessor.GetUsername());

                if (user == null) return null;


                var MainCar = user.Cars.FirstOrDefault(x => x.IsMain);
                var car = user.Cars.FirstOrDefault(c => c.Id == ID);

                if (car == null) return null;

                if (MainCar != null) MainCar.IsMain = false;  // set the current main car to false

                car.IsMain = true; // set the new main car to true

                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Result<Unit>.Success(Unit.Value);

                return Result<Unit>.Failure("Problem setting main car");
            }
        }
    }
}