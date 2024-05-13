using AutoMapper;
using OnlineExaminationSystems.API.Models.Entities;

namespace OnlineExaminationSystems.API.Models.Dtos.Mappings
{
    public class AnswerProfile : Profile
    {
        public AnswerProfile()
        {
            CreateMap<Answer, AnswerUpdateRequestModel>().ReverseMap();
        }
    }
}
