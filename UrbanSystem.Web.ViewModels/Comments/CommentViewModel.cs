using AutoMapper;
using UrbanSystem.Data.Models;
using UrbanSystem.Services.Mapping;

namespace UrbanSystem.Web.ViewModels
{
    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime AddedOn { get; set; }
        public string UserName { get; set; } = null!;
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
        }
    }
}