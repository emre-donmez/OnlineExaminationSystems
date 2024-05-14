using AutoMapper;
using OnlineExaminationSystems.API.Models.Entities;

namespace OnlineExaminationSystems.API.Models.Dtos.Mappings
{
    public class ExamProfile : Profile
    {
        public ExamProfile()
        {
            CreateMap<ExamUpdateRequestModel,Exam>().ReverseMap();
        }
    }
}
