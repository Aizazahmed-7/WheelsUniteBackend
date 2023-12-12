
namespace Application.Chats
{
    public class TimeResolver
    {
            public static string TimeAgoResolver(DateTime dateTime){
            DateTime createdAt = dateTime;
            TimeSpan timeDifference = DateTime.Now - createdAt;

                if (timeDifference.TotalMinutes < 1)
                {
                    return "Just now";
                }
                else if (timeDifference.TotalHours < 1)
                {
                    return $"{(int)timeDifference.TotalMinutes} minute{((int)timeDifference.TotalMinutes != 1 ? "s" : "")}";
                }
                else if (timeDifference.TotalDays < 1)
                {
                    return $"{(int)timeDifference.TotalHours} hour{((int)timeDifference.TotalHours != 1 ? "s" : "")}";
                }
                else if (timeDifference.TotalDays < 30)
                {
                    return $"{(int)timeDifference.TotalDays} day{((int)timeDifference.TotalDays != 1 ? "s" : "")}";
                }
                else if (timeDifference.TotalDays < 365)
                {
                    int months = (int)(timeDifference.TotalDays / 30);
                    return $"{months} month{(months != 1 ? "s" : "")}";
                }
                else
                {
                    int years = (int)(timeDifference.TotalDays / 365);
                    return $"{years} year{(years != 1 ? "s" : "")} ago";
                }
        }
    }
}