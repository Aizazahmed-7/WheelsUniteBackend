
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Security
{
    public class IsHostRequirement : IAuthorizationRequirement
    {
    }

    public class IsHostRequirementHandler : AuthorizationHandler<IsHostRequirement>
    {
        private readonly DataContext _Dbcontext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IsHostRequirementHandler(DataContext Dbcontext, IHttpContextAccessor httpContextAccessor)
        {
            _Dbcontext = Dbcontext;
            _httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsHostRequirement requirement)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null) return Task.CompletedTask;

            var EventId = Guid.Parse(_httpContextAccessor.HttpContext?.Request.RouteValues
                .SingleOrDefault(x => x.Key == "id").Value?.ToString());

            var attendee = _Dbcontext.ActivityAttendees
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.AppUserId == userId && x.EventId == EventId)
            .Result;

            if (attendee == null) return Task.CompletedTask;

            if (attendee.IsHost) context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}