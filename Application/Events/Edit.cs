
using Application.Events;
using Application.Core;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Events
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Event Event { get; set; }
            
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Event).SetValidator(new EventValidator());
                
            }
        }
        
        public class Handler : IRequestHandler<Command , Result<Unit>>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public Handler(DataContext context , IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }
            
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await this.context.Events
                .Include(x => x.Location)
                .FirstOrDefaultAsync( x => x.Id == request.Event.Id);

                if(activity == null) return null;

                this.mapper.Map(request.Event, activity);


                var result = await this.context.SaveChangesAsync() > 0;

                if(!result) return Result<Unit>.Failure("Failed to update activity");

                return Result<Unit>.Success(Unit.Value);
        
            }
        }
    }
}