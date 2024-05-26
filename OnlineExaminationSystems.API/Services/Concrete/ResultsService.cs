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

        public IEnumerable<Result> GetResultsByExamId(int examId)
        {
            var query = $"SELECT id AS Id,user_id AS UserId,exam_id AS ExamId,Score FROM Results where exam_id =@Id";
            var parameters = new { Id = examId };

            return _repository.ExecuteQuery(query, parameters);
        }
    }
}
