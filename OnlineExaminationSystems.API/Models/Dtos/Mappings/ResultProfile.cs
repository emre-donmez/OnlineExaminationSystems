using AutoMapper;
using OnlineExaminationSystems.API.Models.Entities;

namespace OnlineExaminationSystems.API.Models.Dtos.Mappings
{
    public class ResultProfile : Profile
    {
        public ResultProfile()
        {
            CreateMap<ResultUpdateRequestModel, Result>().ReverseMap();
        }
    }
}
