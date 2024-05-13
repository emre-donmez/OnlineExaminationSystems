using AutoMapper;
using OnlineExaminationSystems.API.Data.Repository;
using OnlineExaminationSystems.API.Models.Entities;
using OnlineExaminationSystems.API.Services.Abstract;

namespace OnlineExaminationSystems.API.Services.Concrete
{
    public class AnswersService : CrudService<Answer>, IAnswersService
    {
        public AnswersService(IGenericRepository<Answer> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
