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

        public IEnumerable<Exam> GetExamsByLessonId(int lessonId)
        {
            var query = "SELECT id AS Id,Name,lesson_id AS LessonId,question_count AS QuestionCount,Duration,started_date AS StartedDate FROM Exams WHERE lesson_id=@LessonId";

            var parameters = new { LessonId = lessonId };
            var exams = _repository.ExecuteQuery(query, parameters);

            return exams;
        }
    }
}