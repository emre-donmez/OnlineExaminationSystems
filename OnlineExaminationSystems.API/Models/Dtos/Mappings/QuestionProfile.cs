using AutoMapper;
using OnlineExaminationSystems.API.Models.Entities;

namespace OnlineExaminationSystems.API.Models.Dtos.Mappings
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question,QuestionUpdateRequestModel>().ReverseMap();
            CreateMap<Question, QuestionGetResponseModel>()
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => MapOptions(src)));
        }
        private List<string> MapOptions(Question source)
        {
            var options = new List<string> { source.Option1, source.Option2, source.Option3, source.CorrectAnswer };
            return Shuffle(options);
        }

        private List<string> Shuffle(List<string> options)
        {
            var shuffledOptions = options.OrderBy(x => Guid.NewGuid()).ToList();
            return shuffledOptions;
        }
    }
}
