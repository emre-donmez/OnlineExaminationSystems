using OnlineExaminationSystems.API.Models.Entities;

namespace OnlineExaminationSystems.API.Services.Abstract
{
    public interface IExamsService : ICrudService<Exam>
    {
        IEnumerable<Exam> GetExamsByLessonId(int lessonId);
    }
}