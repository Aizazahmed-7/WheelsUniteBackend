
using Domain;
using AutoMapper;
using Application.Events;
using Application.Comments;
using Application.Posts;
using Application.CarForSale;
using Application.Chats;
using Application.Cars;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            string currentUsername = null;
            CreateMap<Event, Event>();
            CreateMap<Event, EventDto>()
                .ForMember(d => d.HostUsername, o => o.MapFrom(s => s.Attendees.FirstOrDefault(x => x.IsHost).AppUser.UserName))
                .ForMember(d => d.Date, o => o.MapFrom(s => s.Date.ToString("yyyy-MM-dd")))
                .ForMember(d => d.HostProfilePicture, o => o.MapFrom(s => s.Attendees.FirstOrDefault(x => x.IsHost).AppUser.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.Photo.Url));


            CreateMap<Location, LocationDto>()
                .ForMember(d => d.Latitude, o => o.MapFrom(s => s.Latitude))
                .ForMember(d => d.Longitude, o => o.MapFrom(s => s.Longitude));

            CreateMap<EventAttendee, AttendeeDto>()
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.AppUser.DisplayName))
                .ForMember(d => d.Username, o => o.MapFrom(s => s.AppUser.UserName))
                .ForMember(d => d.Bio, o => o.MapFrom(s => s.AppUser.Bio))
                .ForMember(d => d.Image, o => o.MapFrom(s => s.AppUser.Photos.FirstOrDefault(x => x.IsMain).Url));

            CreateMap<AppUser, Profiles.Profile>()
                .ForMember(d => d.Image, o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(d => d.FollowingCount, o => o.MapFrom(s => s.Followings.Count))
                .ForMember(d => d.FollowersCount, o => o.MapFrom(s => s.Followers.Count))
                .ForMember(d => d.Following, o => o.MapFrom(s => s.Followers.Any(x => x.Observer.UserName == currentUsername)))
                .ForMember(d => d.Cars, o => o.MapFrom(s => s.Cars));

            CreateMap<Car,CarDTO>();


            CreateMap<Comment, CommentDto>()
                .ForMember(d => d.Username, o => o.MapFrom(s => s.Author.UserName))
                .ForMember(d => d.Image, o => o.MapFrom(s => s.Author.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(d => d.CreatedAt, o => o.MapFrom(s => s.CreatedAt.ToString("yyyy-MM-dd")));

            CreateMap<Reply, ReplyDto>()
                .ForMember(d => d.Username, o => o.MapFrom(s => s.Author.UserName))
                .ForMember(d => d.Image, o => o.MapFrom(s => s.Author.Photos.FirstOrDefault(x => x.IsMain).Url));


            CreateMap<Post, PostDto>()
                .ForMember(d => d.LikesCount, o => o.MapFrom(s => s.Likes.Count))
                .ForMember(d => d.IsLiked, o => o.MapFrom(s => s.Likes.Any(x => x.Username == currentUsername)))
                .ForMember(d => d.User, o => o.MapFrom(s => s.AppUser));
            CreateMap<AppUser, PostDto.SimpleUser>()
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.UserName))
                .ForMember(d => d.DisplayPhoto, o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain).Url));


            CreateMap<Domain.CarForSale , CarForSaleListDTO>()
                .ForMember(d => d.Make , o => o.MapFrom(s => s.Car.Make))
                .ForMember(d => d.Model , o => o.MapFrom(s => s.Car.Model))
                .ForMember(d => d.Price , o => o.MapFrom(s => s.Car.Price))
                .ForMember(d => d.Location , o => o.MapFrom(s => s.Location));

            CreateMap<Domain.CarForSale , CarForSaleDetailsDTO>()
                .ForMember(d => d.Car , o => o.MapFrom(s => s.Car))
                .ForMember(d => d.Location , o => o.MapFrom(s => s.Location))
                .ForMember(d => d.UserName , o => o.MapFrom(s => s.Car.AppUser.UserName))
                .ForMember(d => d.Description , o => o.MapFrom(s => s.Description))
                .ForMember(d => d.Date , o => o.MapFrom(s => s.Date.ToString("yyyy-MM-dd")));

            CreateMap<Chat, ChatDTO>()
                .ForMember(d => d.SenderUsername, o => o.MapFrom(s => s.Sender.UserName))
                .ForMember(d => d.RecipientUsername, o => o.MapFrom(s => s.Recipient.UserName))
                .ForMember(d => d.SenderImage, o => o.MapFrom(s => s.Sender.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(d => d.RecipientImage, o => o.MapFrom(s => s.Recipient.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(d => d.ConversationId, o => o.MapFrom(s => s.ConversationId));

             CreateMap<AppUser,Profiles.UserDto>()
                .ForMember(d => d.userName , o => o.MapFrom(s => s.UserName))
                .ForMember(d => d.displayPhoto , o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain).Url));   
                
        }

    }
}