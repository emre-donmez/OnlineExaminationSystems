using AutoMapper;
using OnlineExaminationSystems.API.Models.Dtos.User;

namespace OnlineExaminationSystems.API.Models.Dtos.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Entities.User, UserUpdateRequestModel>().ReverseMap();
        }
    }
}