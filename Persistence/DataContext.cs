
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<EventAttendee> ActivityAttendees { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserFollowing> Followings { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<CarForSale> CarsForSale { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<EventAttendee>(x => x.HasKey(aa => new { aa.AppUserId, aa.EventId }));
            builder.Entity<EventAttendee>()
                .HasOne(u => u.AppUser)
                .WithMany(a => a.Events)
                .HasForeignKey(aa => aa.AppUserId);

            builder.Entity<EventAttendee>()
                .HasOne(u => u.Event)
                .WithMany(a => a.Attendees)
                .HasForeignKey(aa => aa.EventId);


            builder.Entity<UserFollowing>(b =>
            {
                b.HasKey(k => new { k.ObserverId, k.TargetId });

                b.HasOne(o => o.Observer)
                    .WithMany(f => f.Followings)
                    .HasForeignKey(o => o.ObserverId)
                    .OnDelete(DeleteBehavior.Cascade);


                b.HasOne(o => o.Target)
                    .WithMany(f => f.Followers)
                    .HasForeignKey(o => o.TargetId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Car>()
                .HasOne(u => u.AppUser)
                .WithMany(a => a.Cars)
                .HasForeignKey(aa => aa.AppUserId);

            builder.Entity<Like>()
                .HasOne(u => u.Post)
                .WithMany(a => a.Likes)
                .HasForeignKey(aa => aa.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}