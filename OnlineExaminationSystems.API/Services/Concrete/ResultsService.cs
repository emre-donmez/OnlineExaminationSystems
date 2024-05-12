using AutoMapper;
using OnlineExaminationSystems.API.Data.Repository;
using OnlineExaminationSystems.API.Models.Entities;
using OnlineExaminationSystems.API.Services.Abstract;

namespace OnlineExaminationSystems.API.Services.Concrete
{
    public class ResultsService : CrudService<Result>, IResultsService
    {
        public ResultsService(IGenericRepository<Result> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
