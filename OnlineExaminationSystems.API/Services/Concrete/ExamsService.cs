using AutoMapper;
using OnlineExaminationSystems.API.Data.Repository;
using OnlineExaminationSystems.API.Models.Entities;
using OnlineExaminationSystems.API.Services.Abstract;

namespace OnlineExaminationSystems.API.Services.Concrete
{
    public class ExamsService : CrudService<Exam>, IExamsService
    {
        public ExamsService(IGenericRepository<Exam> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}