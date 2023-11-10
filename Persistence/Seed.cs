using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context,
            UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any() && !context.Events.Any())
            {

                var cars = new List<Car> 
                {

                    
                     new Car 
                     {  
                        Model = "X5",
                        Make = "BMW",
                        Color = "Black",
                        Price = 100000,
                        ConditionDetails = "New",
                        AppUserId = "a",
                        Mileage = 1000,
                        IsMain = true 
                    }, 
                    new Car {
                        Model = "X6",
                        Make = "BMW",
                        Color = "Black",
                        Price = 100000,
                        ConditionDetails = "New",
                        AppUserId = "a",
                        Mileage = 1000,
                        IsMain = true 
                    },
                    new Car {
                        Model = "X7",
                        Make = "BMW",
                        Color = "Black",
                        Price = 100000,
                        ConditionDetails = "New",
                        AppUserId = "a",
                        Mileage = 1000,
                        IsMain = true 
                    },
                    new Car {
                        Model = "X8",
                        Make = "BMW",
                        Color = "Black",
                        Price = 100000,
                        ConditionDetails = "New",
                        AppUserId = "a",
                        Mileage = 1000,
                        IsMain = true 
                    },
                };
            
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        DisplayName = "Bob",
                        UserName = "bob",
                        Email = "bob@test.com",
                        Cars = new List<Car> {cars[0]}
                    },
                    new AppUser
                    {
                        DisplayName = "Jane",
                        UserName = "jane",
                        Email = "jane@test.com",
                        Cars =  new List<Car>{cars[1] , cars[2] }
                    },
                    new AppUser
                    {
                        DisplayName = "Tom",
                        UserName = "tom",
                        Email = "tom@test.com",
                        Cars = new List<Car> {cars[3]}
                    },
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }

                var activities = new List<Event>
                {
                    new Event
                    {
                        Title = "Past Event 1",
                        Date = DateTime.UtcNow.AddMonths(-2),
                        Description = "Event 2 months ago",
                        Location = new Location
                        {
                            Latitude = 40.712776,
                            Longitude = -74.005974
                        },
                        Attendees = new List<EventAttendee>
                        {
                            new EventAttendee
                            {
                                AppUser = users[0],
                                IsHost = true
                            }
                        }
                    },
                    new Event
                    {
                        Title = "Past Event 2",
                        Date = DateTime.UtcNow.AddMonths(-1),
                        Description = "Event 1 month ago",
                        Location = new Location
                        {
                            Latitude = 40.712776,
                            Longitude = -74.005974
                        },
                        Attendees = new List<EventAttendee>
                        {
                            new EventAttendee
                            {
                                AppUser = users[0],
                                IsHost = true
                            },
                            new EventAttendee
                            {
                                AppUser = users[1],
                                IsHost = false
                            },
                        }
                    },
                    new Event
                    {
                        Title = "Future Event 1",
                        Date = DateTime.UtcNow.AddMonths(1),
                        Description = "Event 1 month in future",
                        Location = new Location
                        {
                            Latitude = 40.712776,
                            Longitude = -74.005974
                        },
                        Attendees = new List<EventAttendee>
                        {
                            new EventAttendee
                            {
                                AppUser = users[2],
                                IsHost = true
                            },
                            new EventAttendee
                            {
                                AppUser = users[1],
                                IsHost = false
                            },
                        }
                    },
                    new Event
                    {
                        Title = "Future Event 2",
                        Date = DateTime.UtcNow.AddMonths(2),
                        Description = "Event 2 months in future",
                        Location = new Location
                        {
                            Latitude = 40.712776,
                            Longitude = -74.005974
                        },
                        Attendees = new List<EventAttendee>
                        {
                            new EventAttendee
                            {
                                AppUser = users[0],
                                IsHost = true
                            },
                            new EventAttendee
                            {
                                AppUser = users[2],
                                IsHost = false
                            },
                        }
                    },
                    new Event
                    {
                        Title = "Future Event 3",
                        Date = DateTime.UtcNow.AddMonths(3),
                        Description = "Event 3 months in future",
                        Location = new Location
                        {
                            Latitude = 40.712776,
                            Longitude = -74.005974
                        },
                        Attendees = new List<EventAttendee>
                        {
                            new EventAttendee
                            {
                                AppUser = users[1],
                                IsHost = true
                            },
                            new EventAttendee
                            {
                                AppUser = users[0],
                                IsHost = false
                            },
                        }
                    },
                    new Event
                    {
                        Title = "Future Event 4",
                        Date = DateTime.UtcNow.AddMonths(4),
                        Description = "Event 4 months in future",
                        Location = new Location
                        {
                            Latitude = 40.712776,
                            Longitude = -74.005974
                        },
                        Attendees = new List<EventAttendee>
                        {
                            new EventAttendee
                            {
                                AppUser = users[1],
                                IsHost = true
                            }
                        }
                    },
                    new Event
                    {
                        Title = "Future Event 5",
                        Date = DateTime.UtcNow.AddMonths(5),
                        Description = "Event 5 months in future",
                        Location = new Location
                        {
                            Latitude = 40.712776,
                            Longitude = -74.005974
                        },
                        Attendees = new List<EventAttendee>
                        {
                            new EventAttendee
                            {
                                AppUser = users[0],
                                IsHost = true
                            },
                            new EventAttendee
                            {
                                AppUser = users[1],
                                IsHost = false
                            },
                        }
                    },
                    new Event
                    {
                        Title = "Future Event 6",
                        Date = DateTime.UtcNow.AddMonths(6),
                        Description = "Event 6 months in future",
                        Location = new Location
                        {
                            Latitude = 40.712776,
                            Longitude = -74.005974
                        },
                        Attendees = new List<EventAttendee>
                        {
                            new EventAttendee
                            {
                                AppUser = users[2],
                                IsHost = true
                            },
                            new EventAttendee
                            {
                                AppUser = users[1],
                                IsHost = false
                            },
                        }
                    },
                    new Event
                    {
                        Title = "Future Event 7",
                        Date = DateTime.UtcNow.AddMonths(7),
                        Description = "Event 7 months in future",
                        Location = new Location
                        {
                            Latitude = 40.712776,
                            Longitude = -74.005974
                        },
                        Attendees = new List<EventAttendee>
                        {
                            new EventAttendee
                            {
                                AppUser = users[0],
                                IsHost = true
                            },
                            new EventAttendee
                            {
                                AppUser = users[2],
                                IsHost = false
                            },
                        }
                    },
                    new Event
                    {
                        Title = "Future Event 8",
                        Date = DateTime.UtcNow.AddMonths(8),
                        Description = "Event 8 months in future",
                        Location = new Location
                        {
                            Latitude = 40.712776,
                            Longitude = -74.005974
                        },
                        Attendees = new List<EventAttendee>
                        {
                            new EventAttendee
                            {
                                AppUser = users[2],
                                IsHost = true
                            },
                            new EventAttendee
                            {
                                AppUser = users[1],
                                IsHost = false
                            },
                        }
                    }
                };

                await context.Events.AddRangeAsync(activities);
                await context.SaveChangesAsync();
                }
            }
        }
    }

