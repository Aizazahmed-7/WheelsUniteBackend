using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using Domain;
using MediatR;
using Persistence;

namespace Application.Events
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command , Result<Unit>> 
        {
            private readonly DataContext dataContext;

            public Handler(DataContext dataContext)
            {
                this.dataContext = dataContext;
            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var Event = await this.dataContext.Events.FindAsync(request.Id);
                
                if( Event == null) return null;


                this.dataContext.Remove( Event);

               var result =  await this.dataContext.SaveChangesAsync() > 0 ;

               if(!result) return Result<Unit>.Failure("Failed to delete  Event");

                return Result<Unit>.Success(Unit.Value);

            }
        }
    }
}