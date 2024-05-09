using AutoMapper;
using OnlineExaminationSystems.API.Models.Dtos.Lesson;

namespace OnlineExaminationSystems.API.Models.Dtos.Mappings
{
    public class LessonProfile : Profile
    {
        public LessonProfile()
        {
            this.CreateMap<Entities.Lesson, LessonUpdateRequestModel>().ReverseMap();
        }
    }
}
