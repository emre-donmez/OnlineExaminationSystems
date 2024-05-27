using OnlineExaminationSystems.API.Models.Entities;

namespace OnlineExaminationSystems.API.Services.Abstract
{
    public interface IResultsService : ICrudService<Result>
    {
        void CalculateResults(int examId);
        IEnumerable<Result> GetResultsByExamId(int examId);
    }
}
