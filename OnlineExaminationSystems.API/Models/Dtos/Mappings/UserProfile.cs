using AutoMapper;
using OnlineExaminationSystems.API.Model.Dtos.User;

namespace OnlineExaminationSystems.API.Model.Dtos.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            this.CreateMap<Entities.User, UserUpdateRequestModel>().ReverseMap();
        }
    }
}